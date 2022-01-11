using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using XLSTAT.Models;

namespace XLSTAT.Utilitties
{
    /// <summary>
    /// Utilities class 
    /// </summary>
    public class Utilities
    {
        private static readonly char[] base26Chars = Constants.ALPHABET.ToCharArray();  /*Alphabet stored as a char array*/

        /// <summary>
        /// Get letter from alphabet depending its position
        /// </summary>
        public static string Base26Encode(Int64 position)
        {
            string returnValue = null;
            do
            {
                returnValue = base26Chars[position % 26] + returnValue;
                position /= 26;
            } while (position-- != 0);
            return returnValue;
        }

        /// <summary>
        /// Replace all first occurence of the second element from the tuple by the first element
        /// This function supports regex
        /// </summary>
        public static void ReplaceOnceInFile(string path, List<Tuple<string, string>> list)
        {
            string content = File.ReadAllText(path);

            foreach (Tuple<string, string> pair in list)
            {
                Regex regex = new Regex(Regex.Escape(pair.Item1));
                content = regex.Replace(content, pair.Item2, 1);
            }

            File.WriteAllText(path, content);
        }

        /// <summary>
        /// Create a stream from a file
        /// It is used for ressources files
        /// </summary>
        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        /// <summary>
        /// Extract ressource
        /// </summary>
        public static string GetRessource(string name)
        {
            string outputFile = Path.Combine(Workspace.GetWorkspacePath(), name);
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream fileStream = asm.GetManifestResourceStream(string.Format(Constants.RES + name, asm.GetName().Name)))
            {

                if (fileStream == null)
                    throw new InternalException(Errors.ERR_RES_LOAD.Replace(Constants.XXXX, asm.GetName().Name));

                SaveStreamToFile(outputFile, fileStream);
            }

            if (string.IsNullOrEmpty(outputFile))
                throw new InternalException(Errors.ERR_RES_EXTRACT.Replace(Constants.XXXX, asm.GetName().Name));

            return outputFile;
        }

        /// <summary>
        /// Build a unique string ID without special caracters to create the workspace
        /// </summary>
        public static string GetUniqueStringID()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace(Constants.EQUAL, string.Empty);
            GuidString = GuidString.Replace(Constants.SLASH, string.Empty);
            return GuidString.Replace(Constants.PLUS, string.Empty);
        }

        /// <summary>
        /// Loop over each value of the array to test if it is a double or not
        /// </summary>
        public static bool IsDoubleArray(Data<string> array)
        {
            for (int i = 0; i < array.Table.Length; ++i)
            {
                for (int j = 0; j < array.Table[i].Length; ++j)
                {
                    if (!double.TryParse(array.Table[i][j], out _))
                        return false;
                }
            }

            return true;
        }
    }
}
