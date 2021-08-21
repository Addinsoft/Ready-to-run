using System.IO;
using System.IO.Compression;

namespace XLSTAT.Interface
{
    /// <summary>
    /// Zip class utilities
    /// </summary>
    class Zip
    {
        private readonly string originalFile;       /*file from where the zip is extracted*/
        private string zipFolder;                   /*unzipped folder*/

        /// <summary>
        /// Zip constructor
        /// </summary>
        public Zip(string iFile) => originalFile = iFile;

        /// <summary>
        /// Unzipp the original file and give it the same name of the original file
        /// </summary>
        public string UnZipp()
        {
            zipFolder = originalFile;
            //remove .xlsm extension
            zipFolder = zipFolder[0..^5]; 

            ZipFile.ExtractToDirectory(originalFile, zipFolder, true);
            return zipFolder;
        }

        /// <summary>
        /// Build the xlsm file from the unzipped folder and remove the unzipped folder
        /// </summary>
        public void ZipAndClean()
        {
            File.Delete(originalFile);
            ZipFile.CreateFromDirectory(zipFolder, originalFile);
            Directory.Delete(zipFolder, true);
        }
    }
}
