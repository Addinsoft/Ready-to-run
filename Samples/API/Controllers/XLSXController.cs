using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XLSTAT.Models;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class XLSXController : Controller
    {
        /// <summary>
        /// Creates a XLSX file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "labels": ["X1", "X2"],
        ///         "table": [["0.689", "-0.837"], ["0.181", "0.880"], ["0.938", "3.239"], ["-0.039", "1.193"], ["0.449", "1.975"], ["0.087", "-0.701"], ["1.290", "-0.809"], ["1.025", "0.772"], ["-0.732", "0.979"]]
        ///     ]
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsx) file with data</returns> 
        [HttpPost]
        public string  GetBase64File([FromBody] Data<string> data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateDataFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
