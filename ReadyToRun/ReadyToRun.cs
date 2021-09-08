using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XLSTAT.Models;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT
{
    public class ReadyToRun
    {
        /// <summary>
        /// Public function to create a xlsm file with ready to run button depending the analyse
        /// </summary>
        public static Result<string> GenerateXLSTATFile(Models.Analyze data)
        {
            try
            {
                Byte[] bytes = File.ReadAllBytes(Build(data));

                string result = Convert.ToBase64String(bytes);

                Workspace.ClearWorkspace();

                return new SuccessResult<string>(result);
            }
            catch (InternalException ex)
            {
                Workspace.ClearWorkspace();
                return new ErrorResult<string>(ex.Message);
            }
        }

        /// <summary>
        /// Public function to create a new xlsx file from datasets
        /// </summary>
        public static Result<string> GenerateDataFile<T>(Data<T> data)
        {
            try
            {
                Byte[] bytes = File.ReadAllBytes(CreateXLSXFile(data));

                string result = Convert.ToBase64String(bytes);

                Workspace.ClearWorkspace();

                return new SuccessResult<string>(result);
            }
            catch (InternalException ex)
            {
                Workspace.ClearWorkspace();
                return new ErrorResult<string>(ex.Message);
            }
        }


        /// <summary>
        /// Create a new xlsm file from a generic analyze
        /// </summary>
        public static string Build(Analyze data)
        {
            data.UpdateParameters();

            IExcel excel = new IExcel();

            //Write all dataset into a new excel file (xlsm)
            string filePath = excel.AppendData(data);

            IExcelXml xml = new IExcelXml(filePath);

            //fix library bug
            xml.EnableButtonMacro();

            //Write XLSTAT parameters
            xml.UpdateParameters(data);

            //Write informative message
            xml.UpdateMessage(data.Name);

            return filePath;
        }

        /// <summary>
        /// Create a new xlsx file from datasets
        /// </summary>
        public static string CreateXLSXFile<T>(Data<T> data)
        {
            IExcel excel = new IExcel();

            //Write all dataset into a new excel file (xlsx)
            string filePath = excel.CreateFile(data);

            return filePath;
        }

        /// <summary>
        /// Create a new csv file from a dataset
        /// </summary>
        public static Result<string> GenerateCSVFile<T>(Data<T> data)
        {
            try
            {
                var csv = new StringBuilder();
                string content = "";
                //write the label first
                if (data.Labels != null && data.Labels.Length > 0)
                {
                    for (int i = 0; i < data.Labels.Length; ++i)
                        content += data.Labels[i] + ";";
                    content += System.Environment.NewLine;
                }

                int n = data.Table.Length;
                for (int i = 0; i < n; ++i)
                {
                    int p = data.Table[i].Length;
                    for (int j = 0; j < p; ++j)
                        content += data.Table[i][j] + ";";
                    content += System.Environment.NewLine;
                }

                return new SuccessResult<string>(content);
            }
            catch (InternalException ex)
            {
                Workspace.ClearWorkspace();
                return new ErrorResult<string>(ex.Message);
            }
        }
    }
}
