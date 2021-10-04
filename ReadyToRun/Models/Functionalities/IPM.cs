using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class IPM : Analyze
    {
        public Data<double> T { get; set; }
        public Data<string> ObsLabels { get; set; }
        public int Type { get; set; }

        public IPM()
        {
            Id = 154;
            Name = Ressources.strings.IPM;
            Trigram = "IPM";
        }


        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (T is null)
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
                new RefEdit<double>("RefEditT", T),
            };

            if (ObsLabels != null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
            if (Type > 1)
                Parameters.Add(new ComboBox("ComboBoxType", Type));
        }
    }
}
