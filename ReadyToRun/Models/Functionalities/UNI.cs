using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class UNI : Analyze
    {
        public Data<double> X { get; set; }
        public Data<double> Q { get; set; }
        public Data<double> W { get; set; }
        public Data<string> G { get; set; }

        public UNI()
        {
            Id = 9;
            Name = Ressources.strings.UNI;
            Trigram = "UNI";
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (X is null && W is null && Q is null && G is null)
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
            };

            if (X is not null && X.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_X", X, new CheckBox("CheckBox_X", true)));
            if (Q is not null && Q.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_Q", Q, new CheckBox("CheckBox_Q", true)));
            if (W is not null && W.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_W", W, new CheckBox("CheckBox_W", true)));
            if (G is not null && G.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_G", G, new CheckBox("CheckBox_G", true)));
        }
    }
}
