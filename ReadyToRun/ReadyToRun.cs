using System;
using System.IO;
using XLSTAT.Models;
using XLSTAT.Utilitties;

namespace XLSTAT
{
    public class ReadyToRun
    {
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
    }
}
