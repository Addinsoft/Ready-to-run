using System.Collections.Generic;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class FST : Analyze
    {
        public Data<double> T { get; set; }
        public Data<string> ObsLabels { get; set; }
        public int Method { get; set; }
        public OptionButton MethodList;

        public FST()
        {
            Id = 242;
            Name = Ressources.strings.FST;
            Trigram = "FST";
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
                Parameters.Add(new RefEdit<string>("RefEditObjLabels", ObsLabels, new CheckBox("CheckBoxObjLabels", true)));
            if (Method > 0)
                Parameters.Add(new OptionButton(Method,
                new string[] { "Statis method", "Correspondence analysis", "Correspondence analysis" },
                new string[] { "OptionButtonStatis", "OptionButtonCA", "OptionButtonMCA" }
                ));
        }
    }
}
