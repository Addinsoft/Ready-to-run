using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class PRM : Analyze
    {
        public Data<double> T { get; set; }
        public Data<string> ObjLabels { get; set; }
        public Data<string> AssesLabels { get; set; }
        public int Method { get; set; }
        public OptionButton OptionButtonStatis;

        public PRM()
        {
            Id = 260;
            Name = Ressources.strings.PRM;
            Trigram = "PRM";
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

            if (ObjLabels != null && ObjLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEditObjLabels", ObjLabels, new CheckBox("CheckBoxObjLabels", true)));
            if (AssesLabels != null && AssesLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEditAssesLabels", AssesLabels, new CheckBox("CheckBoxAssesLabels", true)));
            if (Method > 0)
                Parameters.Add(new OptionButton(Method,
                new string[] { "STATIS", "MFA" },
                new string[] { "OptionButtonStatis", "OptionButtonMFA" }
                ));
        }
    }
}
