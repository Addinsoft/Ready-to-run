using Xunit;
using XLSTAT.Models.Parameters;
using XLSTAT.Models;

namespace ReadyToRun.test
{
    public class Parameters_tests
    {
        [Fact]
        public void TestCheckBox()
        {
            CheckBox cb = new("Checkbox_test", true);

            Assert.False(cb.GetForm() != "Checkbox_test,,True,True,,True,,False,\n", "Checkbox error");
        }

        [Fact]
        public void TestRefedit()
        {
            CheckBox cb = new("Checkbox_test", true);
            Data<double> x = new();
            RefEdit<double> re = new("Refedit_test", x, cb);

            Assert.False(re.GetForm() != "Checkbox_test,,True,True,,True,,False,\nRefedit_test,RefEdit0,,True,,True,,False,\n", "Checkbox error");
        }

        [Fact]
        public void TestTextbox()
        {
            CheckBox cb = new("Checkbox_test", true);
            TextBox<double> tx = new("Textbox_test", 2, cb);

            Assert.False(tx.GetForm() != "Checkbox_test,,True,True,,True,,False,\nTextbox_test,,2,True,,True,,False,\n", "Checkbox error");
        }
    }
}
