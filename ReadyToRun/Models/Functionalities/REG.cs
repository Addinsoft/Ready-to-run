using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class REG : Analyze
    {
        public Data<double> Y { get; set; }
        public Data<double> X { get; set; }
        public Data<string> ObsLabels { get; set; }
        public Data<double> W { get; set; }
        public Data<double> Wr { get; set; }

        public REG()
        {
            Id = 53;
            Name = Ressources.strings.REG;
            Trigram = "REG";
        }

        public REG(Data<double> y, Data<double> x, Data<string> obsLabels = null, Data<double> w = null, Data<double> wr = null)
        {
            Id = 53;
            Name = Ressources.strings.REG;
            Trigram = "REG";
            Y = y;
            X = x;
            ObsLabels = obsLabels;
            W = w;
            Wr = wr;
        }

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
                new RefEdit<double>("RefEdit_X", X)
            };

            if (ObsLabels is not null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
            if (W is not null && W.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_W", W, new CheckBox("CheckBox_W", true)));
            if (Wr is not null && Wr.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_Wr", Wr, new CheckBox("CheckBox_Wr", true)));
        }
    }
}
