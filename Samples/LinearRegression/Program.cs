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
        public const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static XLSTAT.Models.Data<double> GenerateRandomData(int n, int p, string label)
        {
            XLSTAT.Models.Data<double> data = new XLSTAT.Models.Data<double>();
            data.Table = new double[n][];
            data.Labels = new string[p];
            var rand = new Random();

            for (int i = 0; i < n; ++i)
                data.Table[i] = new double[p];

            for (int j = 0; j < p; ++j)
                data.Labels[j] = label + (j + 1).ToString();

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < p; ++j)
                    data.Table[i][j] = rand.NextDouble();
            }
            return data;
        }
        public static XLSTAT.Models.Data<string> GenerateStringRandomData(int n, int p, string label)
        {
            XLSTAT.Models.Data<string> data = new XLSTAT.Models.Data<string>();
            data.Table = new string[n][];
            data.Labels = new string[p];
            var rand = new Random();

            for (int i = 0; i < n; ++i)
                data.Table[i] = new string[p];

            for (int j = 0; j < p; ++j)
                data.Labels[j] = label + (j + 1).ToString();

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < p; ++j)
                    data.Table[i][j] = ALPHABET[Convert.ToInt32(rand.NextDouble()) % 26].ToString();
            }
            return data;
        }
    }
}
