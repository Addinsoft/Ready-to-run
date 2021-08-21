using System;
using System.IO;
using XLSTAT.Utilitties;

namespace XLSTAT.Models
{
    /// <summary>
    /// Workspace which is unique for each template generation
    /// </summary>
    class Workspace
    {
        private static string folder;       /*workspace folder*/

        /// <summary>
        /// Create the unique folder name and return it
        /// </summary>
        public static string GetWorkspacePath()
        {
            if (String.IsNullOrEmpty(folder))
            {
                try
                {
                    folder = Path.Combine(Directory.GetCurrentDirectory(), Utilities.GetUniqueStringID());
                    Directory.CreateDirectory(folder);
                }
                catch
                {
                    throw new InternalException(Errors.ERR_WS_INIT + folder);
                }
            }

            return folder;
        }

        /// <summary>
        /// Delete the workspace
        /// </summary>
        public static void ClearWorkspace()
        {
            try
            {
                if (Directory.Exists(GetWorkspacePath()))
                {
                    Directory.Delete(GetWorkspacePath(), true);
                    folder = string.Empty;
                }
            }
            catch
            {
                throw new InternalException(Errors.ERR_CLEAR_WS + folder);
            }
        }
    }
}
