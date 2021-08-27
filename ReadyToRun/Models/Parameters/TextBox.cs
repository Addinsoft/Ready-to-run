
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Parameters
{
    /// <summary>
    /// Textbox parameter
    /// </summary>
    public class TextBox<T> : Parameter
    {
        public T Value;             /*value of the parameter*/
        public CheckBox Parent;     /*a checkbox if the current parameter depends from an option*/

        /// <summary>
        /// Constructor
        /// </summary>
        public TextBox(string name, T value) : base(name)
        {
            Type = Constants.TEXTBOX;
            Value = value;
        }

        /// <summary>
        /// Constructor with Checkbox
        /// </summary>
        public TextBox(string name, T value, CheckBox parent) : base(name)
        {
            Parent = parent;
            Type = Constants.TEXTBOX;
            Value = value;
        }

        /// <summary>
        /// Generate XLSTAT parameter configuration
        /// </summary>
        public override string GetForm()
        {
            return (Parent?.GetForm() ?? string.Empty) + Name + Constants.COMA2 + Value + Constants.EOP;
        }
    }
}
