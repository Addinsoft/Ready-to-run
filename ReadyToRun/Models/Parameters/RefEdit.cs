
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Parameters
{
    /// <summary>
    /// Refedit parameter
    /// </summary>
    public class RefEdit<T> : Parameter
    {
        public CheckBox Parent;             /*a checkbox if the current parameter depends from an option*/
        public Data<T> Data;                /*data set associated to this field*/
        public string Range;                /*excel sheet range where the data is saved*/

        /// <summary>
        /// Constructor
        /// </summary>
        public RefEdit(string name, Data<T> data) : base(name)
        {
            Type = Constants.REFEDIT;
            Data = data;
            HasData = true;
        }

        /// <summary>
        /// Constructor with Checkbox
        /// </summary>
        public RefEdit(string name, Data<T> data, CheckBox parent) : base(name)
        {
            Type = Constants.REFEDIT;
            Parent = parent;
            Data = data;
            HasData = true;
        }

        /// <summary>
        /// Generate XLSTAT parameter configuration
        /// </summary>
        public override string GetForm()
        {
            return (Parent?.GetForm() ?? string.Empty) + Name + Constants.REFEDIT0 + Range + Constants.EOP;
        }

        /// <summary>
        /// Retrieves dataset
        /// </summary>
        public Data<T> GetData()
        {
            return Data;
        }
    }
}
