
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Parameters
{
    /// <summary>
    /// ComboBox parameter
    /// </summary>
    public class ComboBox : Parameter
    {
        public int Value;           /*value of the parameter*/
        public CheckBox Parent;     /*a checkbox if the current parameter depends from an option*/

        /// <summary>
        /// Constructor
        /// </summary>
        public ComboBox(string name, int value) : base(name)
        {
            Type = Constants.COMBOBOX;
            Value = value;
        }

        /// <summary>
        /// Constructor with Checkbox
        /// </summary>
        public ComboBox(string name, int value, CheckBox parent) : base(name)
        {
            Parent = parent;
            Type = Constants.COMBOBOX;
            Value = value;
        }

        /// <summary>
        /// Generate XLSTAT parameter configuration
        /// </summary>
        public override string GetForm()
        {
            return (Parent?.GetForm() ?? string.Empty) + Name + Constants.COMBOBOX2 + Value + Constants.EOP;
        }
    }
}
