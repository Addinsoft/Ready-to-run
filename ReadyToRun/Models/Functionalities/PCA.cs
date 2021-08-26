using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class PCA : Analyze
    {
        [Required]
        public Data<double> Y { get; set; }

        public Data<string> ObsLabels { get; set; }

        public Data<double> W { get; set; }

        public PCA()
        {
            Id = 22;
            Name = Ressources.strings.PCA;
            Trigram = "PCA";
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (Y is null)
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
                new RefEdit<double>("RefEditT", Y),
            };
            if (ObsLabels is not null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
            if (W is not null && W.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_W", W, new CheckBox("CheckBox_W", true)));
        }
    }
}
