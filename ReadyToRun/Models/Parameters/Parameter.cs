
namespace XLSTAT.Models.Parameters
{
    /// <summary>
    /// Base class for parameter
    /// </summary>
    public class Parameter
    {
        public string Name;        /*parameter name*/ 
        public string Type;        /*parameter type*/
        public bool HasData;       /*if paramater is linkd to a dataset*/

        /// <summary>
        /// Constructor
        /// </summary>
        public Parameter(string name)
        {
            Name = name;
            HasData = false;
        }

        /// <summary>
        /// Generate XLSTAT parameter configuration
        /// </summary>
        public virtual string GetForm()
        {
            return string.Empty;
        }
    }
}
