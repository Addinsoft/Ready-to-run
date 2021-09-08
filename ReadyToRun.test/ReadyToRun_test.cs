using System.Collections.Generic;
using XLSTAT.Models;
using XLSTAT.Models.Functionalities;
using XLSTAT.Utilitties;
using Xunit;


namespace ReadyToRun.test
{
    public class ReadyToRun_test
    {
        [Fact]
        public void TestGenerateCSVFile1()
        {
            int n = 3;
            int p = 2;
            Data<string> X = new();
            X.Table = new string[n][];

            for (int i = 0; i < n; ++i)
            {
                X.Table[i] = new string[p];
                for (int j = 0; j < p; ++j)
                    X.Table[i][j] = (10 * i + j).ToString();
            }

            Result<string> result = XLSTAT.ReadyToRun.GenerateCSVFile(X);

            Assert.False(result.Success != true, "TestGenerateCSVFile without label error");
        }

        [Fact]
        public void TestGenerateCSVFile2()
        {
            int n = 3;
            int p = 2;
            Data<string> X = new();
            X.Table = new string[n][];

            for (int i = 0; i < n; ++i)
            {
                X.Table[i] = new string[p];
                for (int j = 0; j < p; ++j)
                    X.Table[i][j] = (10 * i + j).ToString();
            }

            X.Labels = new string[p];
            for (int j = 0; j < p; ++j)
                X.Labels[j] = "Label " + j.ToString();

            Result<string> result = XLSTAT.ReadyToRun.GenerateCSVFile(X);

            Assert.False(result.Success != true, "TestGenerateCSVFile with label error");
        }
        

        [Fact]
        public void TestCreateXLSXFile1()
        {
            int n = 3;
            int p = 2;
            Data<string> X = new();
            X.Table = new string[n][];

            for (int i = 0; i < n; ++i)
            {
                X.Table[i] = new string[p];
                for (int j = 0; j < p; ++j)
                    X.Table[i][j] = (10 * i + j).ToString();
            }

            X.Labels = new string[p];
            for (int j = 0; j < p; ++j)
                X.Labels[j] = "Label " + j.ToString();

            string result = XLSTAT.ReadyToRun.CreateXLSXFile(X);

            Assert.False(result == "", "CreateXLSXFile error");
        }

        [Fact]
        public void TestGenerateXLSXFile1()
        {
            int n = 3;
            int p = 2;
            Data<string> X = new();
            X.Table = new string[n][];

            for (int i = 0; i < n; ++i)
            {
                X.Table[i] = new string[p];
                for (int j = 0; j < p; ++j)
                    X.Table[i][j] = (10 * i + j).ToString();
            }

            X.Labels = new string[p];
            for (int j = 0; j < p; ++j)
                X.Labels[j] = "Label " + j.ToString();

            Result<string> result = XLSTAT.ReadyToRun.GenerateDataFile(X);

            Assert.False(result.Data.Length < 100, "TestGenerateXLSXFile1 error");
        }

        [Fact]
        public void TestGenerateXLSTATFile()
        {
            UNI uni = new();

            int n = 3;
            int p = 2;
            uni.X = new();
            uni.X.Table = new double[n][];

            for (int i = 0; i < n; ++i)
            {
                uni.X.Table[i] = new double[p];
                for (int j = 0; j < p; ++j)
                    uni.X.Table[i][j] = 10 * i + j;
            }

            uni.X.Labels = new string[p];
            for (int j = 0; j < p; ++j)
                uni.X.Labels[j] = "Label " + j.ToString();

            Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(uni);

            Assert.False(result.Data.Length < 100, "TestGenerateXLSTATFile error");
        }

        [Fact]
        public void TestBuild()
        {
            UNI uni = new();

            int n = 3;
            int p = 2;
            uni.X = new();
            uni.X.Table = new double[n][];

            for (int i = 0; i < n; ++i)
            {
                uni.X.Table[i] = new double[p];
                for (int j = 0; j < p; ++j)
                    uni.X.Table[i][j] = 10 * i + j;
            }

            uni.X.Labels = new string[p];
            for (int j = 0; j < p; ++j)
                uni.X.Labels[j] = "Label " + j.ToString();

            string result = XLSTAT.ReadyToRun.Build(uni);

            Assert.False(result == "", "Build XLSTAT file error");
        }
    }
}
