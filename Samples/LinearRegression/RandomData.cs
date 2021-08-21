using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliSample
{
    /// <summary>
    /// Utilities function to generate random data
    /// </summary>
    class RandomData
    {
        public const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static XLSTAT.Models.Data<double> GenerateRandomData(int n, int p, string label)
        {
            XLSTAT.Models.Data<double> data = new();
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
            XLSTAT.Models.Data<string> data = new();
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
