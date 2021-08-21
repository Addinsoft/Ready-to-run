
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Parameters
{
    /// <summary>
    /// Checkbox parameter
    /// </summary>
    class CheckBox : Parameter
    {
        public bool Value;      /*value of the checkbox (true or false)*/

        /// <summary>
        /// Constructor
        /// </summary>
        public CheckBox(string name, bool value) : base(name)
        {
            Type = Constants.CHECKBOX;
            Value = value;
        }

        /// <summary>
        /// Generate XLSTAT parameter configuration
        /// </summary>
        public override string GetForm()
        {
            return Name + Constants.COMA2  + Value + Constants.EOP;
        }
    }
}
