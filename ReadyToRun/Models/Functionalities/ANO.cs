using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class ANO : Analyze
    {
        public Data<double> Y { get; set; }
        public Data<string> Q { get; set; }
        public Data<string> ObsLabels { get; set; }
        public Data<double> W { get; set; }
        public Data<double> Wr { get; set; }
        public double Interaction { get; set; }

        public ANO()
        {
            Id = 54;
            Name = Ressources.strings.ANOVA;
            Trigram = "ANO";
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (Y is null || Q is null)
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
                new RefEdit<double>("RefEdit_Y", Y),
                new RefEdit<string>("RefEdit_Q", Q)
            };

            if (ObsLabels is not null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
            if (W is not null && W.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_W", W, new CheckBox("CheckBox_W", true)));
            if (Wr is not null && Wr.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_Wr", Wr, new CheckBox("CheckBox_Wr", true)));

            if (Interaction > 0)
                Parameters.Add(new TextBox<double>("TextBoxLevel", Interaction, new CheckBox("CheckBox_Interactions", true)));

        }
    }
}
