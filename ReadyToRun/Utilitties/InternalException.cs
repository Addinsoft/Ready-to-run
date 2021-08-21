using System;

namespace XLSTAT.Utilitties
{
    /// <summary>
    /// Internal exception 
    /// </summary>
    public class InternalException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InternalException() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public InternalException(string message)
            : base(message)
        {

        }
    }
}
