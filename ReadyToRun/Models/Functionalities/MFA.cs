using System;
using System.Collections.Generic;
using System.Linq;
using XLSTAT.Models.Parameters;
using XLSTAT.Utilitties;

namespace XLSTAT.Models.Functionalities
{
    public class MFA : Analyze
    {
        public Data<Data<string>> Data { get; set; }
        public Data<string> ObsLabels { get; set; }
        public Data<double> W { get; set; }
        public bool IsFrequency { get; set; }

        internal Data<string> T { get; set; }
        internal int Tables { get; set; }
        internal int VarPerTable { get; set; }
        internal Data<int> UDFVarPerTables { get; set; }
        internal Data<string> TablesLabels { get; set; }
        internal int DataTypes { get; set; }
        internal Data<int> TablesType { get; set; }

        private const int CONSTANT_TABLE_SIZE = 0;
        private const int USER_DEFINED_TABLE_SIZE = 1;

        private const int QUANTITATIVE_DATA = 0;
        private const int QUALITATIVE_DATA = 1;
        private const int FREQUENCY_DATA = 2;
        private const int MIXED_DATA = 3;

        private bool hasLabel;
        public MFA()
        {
            Id = 78;
            Name = Ressources.strings.MFA;
            Trigram = "MFA";
        }

        public MFA(Data<string>[] data, Data<string> obsLabels = null, Data<double> w = null)
        {
            Id = 78;
            Name = Ressources.strings.MFA;
            Trigram = "MFA";
            // Data = data;
            ObsLabels = obsLabels;
            W = w;
        }

        /// <summary>
        /// Convert input data to XLSTAT format
        /// </summary>
        public void PrepareModel()
        {
            if (Data is null)
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            //number of different table
            Tables = Data.Table.Length;

            //initialisation of XLSTAT data
            T = new Data<string>();
            T.Labels = new string[0];
            T.Table = new string[0][];

            //initialisation of data table type
            TablesType = new Data<int>();
            TablesType.Table = new int[Tables][];

            //initialisation of data table size
            UDFVarPerTables = new Data<int>();
            UDFVarPerTables.Table = new int[Tables][];

            //initialisation of tables labels
            TablesLabels = new Data<string>();
            TablesLabels.Table = new string[Tables][];

            bool hasDbl = false;
            bool hasStr = false;
            bool sizeIsConstant = true;

            for (int i = 0; i < Tables; ++i)
            {
                //if we have label, we concatene labels into one vector
                if (Data.Table[i][0].Labels != null && Data.Table[i][0].Labels.Length > 0)
                {
                    if (Data.Table[i][0].Table[0].Length != Data.Table[i][0].Labels.Length)
                        throw new InternalException(Errors.ERR_LABEL_DATA_DIFFERENT + i);
                    T.Labels = T.Labels.Concat(Data.Table[i][0].Labels).ToArray();
                }
                else if (i > 0 && T.Labels.Length > 0)
                    throw new InternalException(Errors.LABEL_REQUIRED + i);

                //we concatene input data from different matrix into one matrix
                if (i == 0)
                    T.Table = T.Table.Concat(Data.Table[i][0].Table).ToArray();
                else
                {
                    for (int j = 0; j < Data.Table[i][0].Table.Length; ++j)
                        T.Table[j] = T.Table[j].Concat(Data.Table[i][0].Table[j]).ToArray();
                }

                //we store the size (number of variable) for each input matrix
                UDFVarPerTables.Table[i] = new int[1];
                UDFVarPerTables.Table[i][0] = Data.Table[i][0].Table[0].Length;
                if (i > 0)
                    sizeIsConstant = sizeIsConstant && UDFVarPerTables.Table[i][0] == UDFVarPerTables.Table[i - 1][0];

                //we calculate the data type and store it into the TablesType vector
                TablesType.Table[i] = new int[1];
                if (Utilities.IsDoubleArray(Data.Table[i][0]))
                {
                    hasDbl = true;
                    TablesType.Table[i][0] = 0;
                }
                else
                {
                    hasStr = true;
                    TablesType.Table[i][0] = 1;
                }

                //we extract labels
                TablesLabels.Table[i] = new string[1];
                if (Data.Labels != null && Data.Labels[i] != "")
                {
                    hasLabel = true;
                    TablesLabels.Table[i][0] = Data.Labels[i];
                }
            }

            //we set up the XLSTAT parameter to know if inputs matrix have a constant size or not
            if (sizeIsConstant)
                VarPerTable = CONSTANT_TABLE_SIZE;
            else
                VarPerTable = USER_DEFINED_TABLE_SIZE;

            //if we are not using the frequency format, we set up the right format
            if (!IsFrequency)
            {
                if (hasStr && hasDbl)
                    DataTypes = MIXED_DATA;
                else if (hasStr)
                    DataTypes = QUALITATIVE_DATA;
                else if (hasDbl)
                    DataTypes = QUANTITATIVE_DATA;
            }
            else
                DataTypes = FREQUENCY_DATA;

            //if we have label, we add labe to each vector
            if (hasLabel)
            {
                UDFVarPerTables.Labels = new string[1];
                UDFVarPerTables.Labels[0] = "table size";

                TablesType.Labels = new string[1];
                TablesType.Labels[0] = "table data type";

                TablesLabels.Labels = new string[1];
                TablesLabels.Labels[0] = "table labels";
            }
        }

        /// <summary>
        /// Load parameters from input data
        /// Generate an execption if a mandatory parameter is missing
        /// </summary>
        public override void UpdateParameters()
        {
            //Convert input data to XLSTAT format
            PrepareModel();

            if (T is null || Tables < 1 || VarPerTable < 0 || (VarPerTable == USER_DEFINED_TABLE_SIZE && UDFVarPerTables is null) || (DataTypes == FREQUENCY_DATA && TablesType is null))
                throw new InternalException(Errors.ERR_COMPULSORY_DATA + Name);

            Parameters = new List<Parameter>
            {
                new RefEdit<string>("RefEditT", T),
                new TextBox<double>("TextBoxNbTables", Tables),

                new OptionButton(VarPerTable,
                    new string[] { "Equal", "User defined" },
                    new string[] { "OptionButtonEq", "OptionButtonUDF" }
                )
            };

            Parameters.Add(new CheckBox("CheckBoxVarLabels", T.Labels != null && T.Labels.Length > 0));            

            if (VarPerTable == USER_DEFINED_TABLE_SIZE && UDFVarPerTables != null && UDFVarPerTables.Table.Length > 0)
                Parameters.Add(new RefEdit<int>("RefEditNb", UDFVarPerTables));
            if (ObsLabels != null && ObsLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEdit_ObsLabels", ObsLabels, new CheckBox("CheckBox_ObsLabels", true)));
            if (hasLabel && TablesLabels != null && TablesLabels.Table.Length > 0)
                Parameters.Add(new RefEdit<string>("RefEditTabLabels", TablesLabels, new CheckBox("CheckBoxTabLabels", true)));
            if (W != null && W.Table.Length > 0)
                Parameters.Add(new RefEdit<double>("RefEdit_W", W, new CheckBox("CheckBox_W", true)));
            if (DataTypes > -1)
            {
                Parameters.Add(new ComboBox("ComboBoxDType", DataTypes));
                if (DataTypes == MIXED_DATA && TablesType != null && TablesType.Table.Length > 0)
                    Parameters.Add(new RefEdit<int>("RefEditTableType", TablesType));
            }
        }
    }
}
