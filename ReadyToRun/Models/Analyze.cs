using System.Collections.Generic;
using XLSTAT.Models.Parameters;

namespace XLSTAT.Models
{
    /// <summary>
    /// Base class for analysis
    /// </summary>
    public class Analyze
    {
        public List<Parameter> Parameters;      /*list of analyse parameters*/

        public string Name { get; internal set; }        /*name of the analyse*/

        public string Trigram { get; internal set; }     /*unique litteral analyse id*/

        public int Id { get; internal set; }             /*unique digit analyse id*/


        /// <summary>
        /// Generate parameters XLSTAT format
        /// </summary>
        public virtual string BuildParameters()
        {
            string text = string.Empty;
            foreach (Parameter param in Parameters)
            {
                text += param.GetForm();
            }
            return text;
        }

        /// <summary>
        /// Generate analyse XLSTAT configuration
        /// </summary>
        public string GetParametersModel()
        {
            return "RunProc" + Trigram + "\n" + "Form" + Id.ToString() + ".txt" + "\n" + BuildParameters();
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public virtual void UpdateParameters()
        {

        }
    }
}
