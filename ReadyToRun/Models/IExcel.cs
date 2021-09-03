using ClosedXML.Excel;
using XLSTAT.Interface;
using XLSTAT.Models;
using XLSTAT.Utilitties;
using System;
using System.Collections.Generic;
using System.IO;
using XLSTAT.Models.Parameters;

namespace XLSTAT
{
    class IExcel
    {
        private string fullPath;                        /*xlsm file path*/
        private int lastColumn;                         /*last column filled*/

        /// <summary>
        /// Constructor
        /// </summary>
        public IExcel()
        {
            //Load ressources into the current workspace
            fullPath = Utilities.GetRessource(Constants.TEMPLATE);
        }

        /// <summary>
        /// Save data in a Excel sheet
        /// </summary>
        private string AddData<T>(IXLWorksheet worksheet, Data<T> dataRange)
        {
            string range = string.Empty;
            if (dataRange != null)
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
        public string AppendData(Analyze data)
        {
            try
            {
                using (XLWorkbook workbook = new XLWorkbook(fullPath))
                {
                    IXLWorksheet worksheet = workbook.AddWorksheet(Ressources.strings.Data);

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
            }
            catch
            {
                throw new InternalException(Errors.ERR_APPEND);
            }

            return fullPath;
        }

        /// <summary>
        /// Create a new dataset
        /// </summary>
        public string CreateFile<T>(List<Data<T>> datas)
        {
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.AddWorksheet(Ressources.strings.Data);

                    foreach (Data<T> data in datas)
                    {
                          AddData(worksheet, data);
                    }
                    fullPath = fullPath.Replace(Constants.TEMPLATE, Constants.RESULT2);
                    workbook.SaveAs(fullPath);
                }
            }
            catch
            {
                throw new InternalException(Errors.ERR_APPEND);
            }

            return fullPath;
        }
    }
}
