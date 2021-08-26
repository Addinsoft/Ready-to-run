using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UNIController : Controller
    {
        /// <summary>
        /// Creates a descriptive statistic reday to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "X": {
        ///             "labels": ["X1", "X2"],
        ///             "table": [[0.689, -0.837], [0.181, 0.880], [0.938, 3.239], [-0.039, 1.193], [0.449, 1.975], [0.087, -0.701], [1.290, -0.809], [1.025, 0.772], [-0.732, 0.979]]
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT statistic descriptive analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.UNI data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
