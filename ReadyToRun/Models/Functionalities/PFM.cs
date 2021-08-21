using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class PFM : Analyze
    {
        public Data<double> Y { get; set; }
        public Data<double> X { get; set; }
        public Data<string> ObsLabels { get; set; }

        public PFM()
        {
            Id = 30;
            Name = Ressources.strings.PFM;
            Trigram = "PFM";
        }

        //                    "ComboBoxPrem,ComboBox,Preliminary transformation,0," & _

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (X is null || Y is null)
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
                new RefEdit<double>("RefEdit_Y", Y),
                new RefEdit<double>("RefEdit_X", X),
            };

            if (ObsLabels is not null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
        }
    }
}
