using System.IO;
using System.Reflection;
using System.Text.Json;
using XLSTAT.Models.Functionalities;
using Xunit;

namespace ReadyToRun.test
{
    public class Analysis_test
    {
        [Fact]
        public void TestANC()
        {
            ANC anc = new();

            Assert.False(anc.Id != 55, "ANC Id error");
            Assert.False(anc.Name != "ANCOVA", "ANC name error");
            Assert.False(anc.Trigram != "ANC", "ANC trigram error");

            anc.X = new();
            anc.Y = new();
            anc.Q = new();

            anc.UpdateParameters();

            string param = anc.BuildParameters();
            Assert.False(param == "", "ANC BuildParameters error");

            string result = "RunProcANC\nForm55.txt\nRefEdit_Y,RefEdit0,,True,,True,,False,\nRefEdit_X,RefEdit0,,True,,True,,False,\nRefEdit_Q,RefEdit0,,True,,True,,False,\n";
            Assert.False(anc.GetParametersModel() != result, "ANC GetParametersModel error");
        }

        [Fact]
        public void TestANO()
        {
            ANO ano = new();

            Assert.False(ano.Id != 54, "ANO Id error");
            Assert.False(ano.Name != "ANOVA", "ANO name error");
            Assert.False(ano.Trigram != "ANO", "ANO trigram error");

            ano.Y = new();
            ano.Q = new();

            ano.UpdateParameters();

            string param = ano.BuildParameters();
            Assert.False(param == "", "ANO BuildParameters error");

            string result = "RunProcANO\nForm54.txt\nRefEdit_Y,RefEdit0,,True,,True,,False,\nRefEdit_Q,RefEdit0,,True,,True,,False,\n";
            Assert.False(ano.GetParametersModel() != result, "ANO GetParametersModel error");
        }

        [Fact]
        public void TestPCA()
        {
            PCA pca = new();

            Assert.False(pca.Id != 22, "PCA Id error");
            Assert.False(pca.Name != "PCA", "PCA name error");
            Assert.False(pca.Trigram != "PCA", "PCA trigram error");

            pca.Y = new();

            pca.UpdateParameters();

            string param = pca.BuildParameters();
            Assert.False(param == "", "PCA BuildParameters error");

            string result = "RunProcPCA\nForm22.txt\nRefEditT,RefEdit0,,True,,True,,False,\n";
            Assert.False(pca.GetParametersModel() != result, "PCA GetParametersModel error");
        }

        [Fact]
        public void TestPFM()
        {
            PFM pfm = new();

            Assert.False(pfm.Id != 30, "PFM Id error");
            Assert.False(pfm.Name != "Preference mapping", "PFM name error");
            Assert.False(pfm.Trigram != "PFM", "PFM trigram error");

            pfm.Y = new();
            pfm.X = new();

            pfm.UpdateParameters();

            string param = pfm.BuildParameters();
            Assert.False(param == "", "PFM BuildParameters error");

            string result = "RunProcPFM\nForm30.txt\nRefEdit_Y,RefEdit0,,True,,True,,False,\nRefEdit_X,RefEdit0,,True,,True,,False,\n";
            Assert.False(pfm.GetParametersModel() != result, "PFM GetParametersModel error");
        }

        [Fact]
        public void TestPLS()
        {
            PLS pls = new();

            Assert.False(pls.Id != 1, "PLS Id error");
            Assert.False(pls.Name != "PLS regression", "PLS name error");
            Assert.False(pls.Trigram != "PLS", "PLS trigram error");

            pls.Y = new();
            pls.X = new();
            pls.X.Table = new double[10][];

            pls.UpdateParameters();

            string param = pls.BuildParameters();
            Assert.False(param == "", "PLS BuildParameters error");

            string result = "RunProcPLS\nForm1.txt\nRefEdit_Y,RefEdit0,,True,,True,,False,\nCheckBox_X,,True,True,,True,,False,\nRefEdit_X,RefEdit0,,True,,True,,False,\n";
            Assert.False(pls.GetParametersModel() != result, "PLS GetParametersModel error");
        }

        [Fact]
        public void TestREG()
        {
            REG reg = new();

            Assert.False(reg.Id != 53, "REG Id error");
            Assert.False(reg.Name != "Linear regression", "REG name error");
            Assert.False(reg.Trigram != "REG", "REG trigram error");

            reg.Y = new();
            reg.X = new();

            reg.UpdateParameters();

            string param = reg.BuildParameters();
            Assert.False(param == "", "REG BuildParameters error");

            string result = "RunProcREG\nForm53.txt\nRefEdit_Y,RefEdit0,,True,,True,,False,\nRefEdit_X,RefEdit0,,True,,True,,False,\n";
            Assert.False(reg.GetParametersModel() != result, "REG GetParametersModel error");
        }

        [Fact]
        public void TestUNI()
        {
            UNI uni = new();

            Assert.False(uni.Id != 9, "UNI Id error");
            Assert.False(uni.Name != "Descriptive statistics", "UNI name error");
            Assert.False(uni.Trigram != "UNI", "UNI trigram error");

            uni.X = new();
            uni.X.Table = new double[10][];

            uni.UpdateParameters();

            string param = uni.BuildParameters();
            Assert.False(param == "", "UNI BuildParameters error");

            string result = "RunProcUNI\nForm9.txt\nCheckBox_X,,True,True,,True,,False,\nRefEdit_X,RefEdit0,,True,,True,,False,\n";
            Assert.False(uni.GetParametersModel() != result, "UNI GetParametersModel error");
        }

        [Fact]
        public void TestFST()
        {
            FST fst = new();

            Assert.False(fst.Id != 242, "FST Id error");
            Assert.False(fst.Name != "Free sorting", "FST name error");
            Assert.False(fst.Trigram != "FST", "FST trigram error");

            fst.T = new();
            fst.T.Table = new double[10][];

            fst.UpdateParameters();

            string param = fst.BuildParameters();
            Assert.False(param == "", "FST BuildParameters error");

            string result = "RunProcFST\nForm242.txt\nRefEditT,RefEdit0,,True,,True,,False,\n";
            Assert.False(fst.GetParametersModel() != result, "FST GetParametersModel error");
        }

        [Fact]
        public void TestIPM()
        {
            IPM ipm = new();

            Assert.False(ipm.Id != 154, "IPM Id error");
            Assert.False(ipm.Name != "Internal Preference Mapping", "IPM name error");
            Assert.False(ipm.Trigram != "IPM", "IPM trigram error");

            ipm.T = new();
            ipm.T.Table = new double[10][];

            ipm.UpdateParameters();

            string param = ipm.BuildParameters();
            Assert.False(param == "", "IPM BuildParameters error");

            string result = "RunProcIPM\nForm154.txt\nRefEditT,RefEdit0,,True,,True,,False,\n";
            Assert.False(ipm.GetParametersModel() != result, "IPM GetParametersModel error");
        }

        [Fact]
        public void TestMFA()
        {
            for (int i = 1; i < 4; ++i)
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Data\\MFA\\MFA" + i + ".json";
                string json = File.ReadAllText(path);

                MFA mfa = JsonSerializer.Deserialize<MFA>(json);

                mfa.UpdateParameters();

                string pathResult = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Data\\MFA\\MFA" + i + "Result.json";
                string jsonResult = File.ReadAllText(pathResult);

                Assert.False(jsonResult == JsonSerializer.Serialize(mfa), "MFA BuildParameters error for test number " + i);
            }
        }
    }
}
