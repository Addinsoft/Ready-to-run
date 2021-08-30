using CliSample;
using System;
using System.IO;

namespace Example
{
    /// <summary>
    /// Command line programm to generate a linear regression template
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            XLSTAT.Models.Functionalities.REG analyze = new XLSTAT.Models.Functionalities.REG(RandomData.GenerateRandomData(10, 2, "Y"), RandomData.GenerateRandomData(10, 2, "Y"), RandomData.GenerateStringRandomData(10, 2, "Obs"));
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(analyze);
            
            if (result.Success)
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\Template_REG.xlsm", Convert.FromBase64String(result.Data));
            else
                Console.WriteLine(((XLSTAT.Utilitties.ErrorResult<string>)result).Message);
        }
    }
}
