using ClosedXML.Excel;
using XLSTAT.Interface;
using XLSTAT.Models;
using XLSTAT.Utilitties;
using System;
using System.Collections.Generic;
using System.IO;
using XLSTAT.Models.Parameters;
using Newtonsoft.Json;

namespace XLSTAT
{
    /// <summary>
    /// Interface with Excel creation file
    /// </summary>
    class IExcel
    {
        private string fullPath;                        /*xlsm file path*/
        private bool isUnzip;                           /*true if the xlsm was already unzipped*/
        private Zip zip;                                /*zip utilitie used for its treatment*/
        private string zipPath;                         /*zip folder path*/
        private readonly string drawing1Res;            /*drawing1Res.xml path*/
        private readonly List<string> sheetsname;       /*list of new sheets*/
        private int lastColumn;                         /*last column filled*/

        /// <summary>
        /// Constructor
        /// </summary>
        public IExcel()
        {
            //Load ressources into the current workspace
            fullPath = Utilities.GetRessource(Constants.TEMPLATE);
            drawing1Res = Utilities.GetRessource(Constants.DRAWING1_FILE);
            sheetsname = new List<string>();
        }

        /// <summary>
        /// Save text displayed into the 'Start' sheet 
        /// </summary>
        public void UpdateMessage(string analysisName)
        {
            UnZipp();

            if (sheetsname.Count > 0)
            {
                //build list of sheets name
                string sheetname = sheetsname[0];
                if (sheetsname.Count > 1)
                {
                    for (int i = 1; i < sheetsname.Count; ++i)
                        sheetname += ", " + sheetsname[i];
                }

                //replace constants in the sheet with dynamics values
                Utilities.ReplaceOnceInFile(
                GetXMLLocation(Constants.SHAREDSTRINGS),
                new List<Tuple<string, string>>()
            {
                Tuple.Create(Constants.STR1, Ressources.strings.ClickButton),
                Tuple.Create(Constants.COLORED1, analysisName),
                Tuple.Create(Constants.STR2, Ressources.strings.DataLocatedOn),
                Tuple.Create(Constants.COLORED2, sheetname),
                Tuple.Create(Constants.STR3, Ressources.strings.Sheet)
            });
            }
        }

        /// <summary>
        /// Save XLSTAT analyse configuration
        /// </summary>
        public void UpdateParameters(Analyze model)
        {
            UnZipp();

            //replace a constant in the xlsm file to store XLSTAT parameters
            Utilities.ReplaceOnceInFile(
                GetXMLLocation(Constants.DRAWINGS1),
                new List<Tuple<string, string>>()
            {
                Tuple.Create(Constants.XXXXLSTATXXX, model.GetParametersModel())
            });
        }

        /// <summary>
        /// Replace a file broken by the Excel library to avoid a bug on shape action
        /// </summary>
        private void EnableButtonMacro()
        {
            UnZipp();

            File.Copy(drawing1Res, GetXMLLocation(Constants.DRAWINGS1), true);
        }

        /// <summary>
        /// Retrieve xml location from the unzipped folder
        /// </summary>
        private string GetXMLLocation(int fileId)
        {
            return fileId switch
            {
                Constants.SHAREDSTRINGS => zipPath + Constants.SHAREDSTRINGS_PATH,
                Constants.DRAWINGS1 => zipPath + Constants.DRAWING1_PATH,
                _ => throw new InternalException(Errors.ERR_XML_LOC + fileId),
            };
        }

        /// <summary>
        /// Unzipp the current xlsm file if necessay
        /// </summary>
        private void UnZipp()
        {
            if (!isUnzip)
            {
                if (zip is null)
                    zip = new Zip(fullPath);

                if (zip is null)
                    throw new InternalException(Errors.ERR_INIT_ZIP);

                zipPath = zip.UnZipp();

                if (string.IsNullOrEmpty(zipPath))
                    throw new InternalException(Errors.ERR_UNZIPP_TEMPLATE);

                isUnzip = true;
            }
        }

        /// <summary>
        /// Save data in a Excel sheet
        /// </summary>
        private string AddData<T>(IXLWorksheet worksheet, Data<T> dataRange)
        {
            string range = string.Empty;
            if (dataRange is not null)
            {
                int start = 1;  /*index to know from which row we start to write data*/

                //write the label first
                if (dataRange.Labels.Length > 0)
                {
                    ++start;
                    for (int i = 0; i < dataRange.Labels.Length; ++i)
                        worksheet.Cell(Utilities.Base26Encode(i + lastColumn) + (1).ToString()).Value = dataRange.Labels[i];
                }

                int maxp = 0;   /*maximal number of column written*/
                int n = dataRange.Table.Length;
                for (int i = 0; i < n; ++i)
                {
                    int p = dataRange.Table[i].Length;
                    maxp = Math.Max(p, maxp);
                    for (int j = 0; j < p; ++j)
                    {
                        worksheet.Cell(Utilities.Base26Encode(j + lastColumn) + (i + start).ToString()).Value = dataRange.Table[i][j];
                    }
                }

                //build the range written for configuring XLSTAT parameter
                range = worksheet.Name + "!$" + Utilities.Base26Encode(lastColumn) + ":$" + Utilities.Base26Encode(lastColumn + maxp - 1);
                lastColumn += maxp;
            }
            return range;
        }

        /// <summary>
        /// Write all dataset to Excel
        /// </summary>
        public void AppendData(Analyze data)
        {
            string sheetname = Ressources.strings.Data + (sheetsname.Count + 1);
            try
            {
                using var workbook = new XLWorkbook(fullPath);
                sheetsname.Add(sheetname);
                IXLWorksheet worksheet = workbook.AddWorksheet(sheetname);

                foreach (Parameter param in data.Parameters)
                {
                    if (param.HasData)
                    {
                        if (param is RefEdit<double> refdbl)
                            refdbl.Range = AddData(worksheet, refdbl.GetData());
                        else if (param is RefEdit<string> refstr)
                            refstr.Range = AddData(worksheet, refstr.GetData());
                    }
                }
                fullPath = fullPath.Replace(Constants.TEMPLATE, Constants.RESULT);
                workbook.SaveAs(fullPath);
            }
            catch
            {
                throw new InternalException(Errors.ERR_APPEND + sheetname);
            }
        }

        /// <summary>
        /// Create a new xlsm file from a generic analyze
        /// </summary>
        public string Build(Analyze data)
        {
            //Write all dataset
            AppendData(data);
            
            //fix library bug
            EnableButtonMacro();
            
            //Write XLSTAT parameters
            UpdateParameters(data);

            //Write informative message
            UpdateMessage(data.Name);

            //clean workspace
            if (isUnzip)
                zip.ZipAndClean();

            return fullPath;
        }
    }
}
