using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLSTAT.Interface;
using XLSTAT.Utilitties;

namespace XLSTAT.Models
{
    public class IExcelXml
    {
        private Zip zip;                                /*zip utilitie used for its treatment*/
        private string zipPath;                         /*zip folder path*/

        /// <summary>
        /// Unzipp the current xlsm file if necessay
        /// </summary>
        public IExcelXml(string path)
        {
            zip = new Zip(path);

            if (zip is null)
                throw new InternalException(Errors.ERR_INIT_ZIP);

            zipPath = zip.UnZipp();

            if (string.IsNullOrEmpty(zipPath))
                throw new InternalException(Errors.ERR_UNZIPP_TEMPLATE);
        }

        /// <summary>
        /// Retrieve xml location from the unzipped folder
        /// </summary>
        private string GetXMLLocation(int fileId)
        {
            switch (fileId)
            {
                case Constants.SHAREDSTRINGS:
                    return zipPath + Constants.SHAREDSTRINGS_PATH;
                case Constants.DRAWINGS1:
                    return zipPath + Constants.DRAWING1_PATH;
                default:
                    throw new InternalException(Errors.ERR_XML_LOC + fileId);
            }
        }

        /// <summary>
        /// Save text displayed into the 'Start' sheet 
        /// </summary>
        public void UpdateMessage(string analysisName)
        {
                //replace constants in the sheet with dynamics values
                Utilities.ReplaceOnceInFile(
                GetXMLLocation(Constants.SHAREDSTRINGS),
                new List<Tuple<string, string>>()
            {
                Tuple.Create(Constants.STR1, Ressources.strings.ClickButton),
                Tuple.Create(Constants.COLORED1, analysisName),
                Tuple.Create(Constants.STR2, Ressources.strings.DataLocatedOn),
                Tuple.Create(Constants.COLORED2, Ressources.strings.Data),
                Tuple.Create(Constants.STR3, Ressources.strings.Sheet)
            });

            zip.ZipAndClean();
        }

        /// <summary>
        /// Replace a file broken by the Excel library to avoid a bug on shape action
        /// </summary>
        public void EnableButtonMacro()
        {
            File.Copy(Utilities.GetRessource(Constants.DRAWING1_FILE), GetXMLLocation(Constants.DRAWINGS1), true);
        }

        /// <summary>
        /// Save XLSTAT analyse configuration
        /// </summary>
        public void UpdateParameters(Analyze model)
        {
            //replace a constant in the xlsm file to store XLSTAT parameters
            Utilities.ReplaceOnceInFile(
                GetXMLLocation(Constants.DRAWINGS1),
                new List<Tuple<string, string>>()
            {
                Tuple.Create(Constants.XXXXLSTATXXX, model.GetParametersModel())
            });
        }
    }
}
