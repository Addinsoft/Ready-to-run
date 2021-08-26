using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class ANC : Analyze
    {
        [Required]
        public Data<double> Y { get; set; }
        
        [Required]
        public Data<double> X { get; set; }
        
        [Required]
        public Data<string> Q { get; set; }
        

        public Data<string> ObsLabels { get; set; }
        
        public Data<double> W { get; set; }
        
        public Data<double> Wr { get; set; }
        
        public double Interactions { get; set; }

        public ANC()
        {
            Id = 55;
            Name = Ressources.strings.ANCOVA;
            Trigram = "ANC";
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (X is null || Y is null || Q is null)
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
                new RefEdit<double>("RefEdit_Y", Y),
                new RefEdit<double>("RefEdit_X", X),
                new RefEdit<string>("RefEdit_Q", Q)
            };

            if (ObsLabels is not null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
            if (W is not null && W.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_W", W, new CheckBox("CheckBox_W", true)));
            if (Wr is not null && Wr.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_Wr", Wr, new CheckBox("CheckBox_Wr", true)));

            if (Interactions > 0)
                Parameters.Add(new TextBox<double>("TextBoxLevel", Interactions, new CheckBox("CheckBox_Interactions", true)));

        }
    }
}
