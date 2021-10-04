
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Parameters
{
    /// <summary>
    /// Checkbox parameter
    /// </summary>
    public class OptionButton : Parameter
    {
        public int Index;                       /*value of the option button */
        public string[] Options { get; set; }   /*options list */
        public string[] XLSTATOptions;          /*XLSTTA options list name*/

        /// <summary>
        /// Constructor
        /// </summary>
        public OptionButton(int index, string[] options, string[] xlstatoptions) : base("")
        {
            Type = Constants.OPTIONBUTTON;
            Options = options;
            XLSTATOptions = xlstatoptions;
            Index = index;
        }

        /// <summary>
        /// Generate XLSTAT parameter configuration
        /// </summary>
        public override string GetForm()
        {
            return XLSTATOptions[Index] + Constants.COMA2  + 1 + Constants.EOP;
        }
    }
}
