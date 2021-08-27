using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class PLS : Analyze
    {
        public Data<double> Y { get; set; }

        public Data<double> X { get; set; }
        
        public Data<double> Q { get; set; }

        public Data<string> ObsLabels { get; set; }

        public Data<double> W { get; set; }

        public PLS()
        {
            Id = 1;
            Name = Ressources.strings.PLS;
            Trigram = "PLS";
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (Y is null || (X is null && Q is null))
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
                new RefEdit<double>("RefEdit_Y", Y),
            };

            if (X != null && X.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_X", X, new CheckBox("CheckBox_X", true)));
            if (Q != null && Q.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_Q", Q, new CheckBox("CheckBox_Q", true)));
            if (ObsLabels != null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
            if (W != null && W.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_W", W, new CheckBox("CheckBox_W", true)));
        }
    }
}
