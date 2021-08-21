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
                IExcel xl = new();
                data.UpdateParameters();
                Byte[] bytes = File.ReadAllBytes(xl.Build(data));
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
    }
}
