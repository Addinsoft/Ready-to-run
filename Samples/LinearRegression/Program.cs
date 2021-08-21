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
            XLSTAT.Models.Functionalities.REG analyze = new(RandomData.GenerateRandomData(10, 2, "Y"), RandomData.GenerateRandomData(10, 2, "Y"), RandomData.GenerateStringRandomData(10, 2, "Obs"));
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(analyze);
            
            if (result.Success)
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\result.txt", result.Data);
            else
                Console.WriteLine(((XLSTAT.Utilitties.ErrorResult<string>)result).Message);
        }
    }
}
