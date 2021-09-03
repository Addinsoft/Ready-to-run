using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class CATA : Analyze
    {
        public Data<double> T { get; set; }
        public Data<string> P { get; set; }
        public Data<string> J { get; set; }
        public Data<string> S { get; set; }
        public string Ideal { get; set; }
        public int format { get; internal set; }


        public CATA()
        {
            Id = 195;
            Name = Ressources.strings.CATA;
            Trigram = "CAT";
            format = 1;
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            if (format > 1 && (T is null || P is null || J is null))
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
            };

            if (T != null && T.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEditT", T));
            if (P != null && P.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_P", P));
            if (J != null && J.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_J", J));
            if (S != null && S.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEditS", S, new CheckBox("CheckBoxLiking2", true)));
            if (format > 1)
                Parameters.Add(new ComboBox("ComboBoxFormat", format));
            if (Ideal != null)
                Parameters.Add(new TextBox<string>("TextBoxIdealName", Ideal, new CheckBox("CheckBoxIdealP", true))); 
        }
    }
}
