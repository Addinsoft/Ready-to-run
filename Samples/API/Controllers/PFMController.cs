using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PFMController : Controller
    {
        /// <summary>
        /// Creates a Preference mapping reday to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "Y": {
        ///             "labels": ["Y1", "Y2"],
        ///             "table": [[-1.583, -0.267], [-0.361, -1.297], [0.413, 0.617], [0.093, -1.847], [-0.152, -2.038], [0.038, 0.166], [0.202, 1.254], [-1.149, 0.787], [-1.539, 1.276]]
        ///         },
        ///         "X": {
        ///             "labels": ["X1", "X2"],
        ///             "table": [[0.689, -0.837], [0.181, 0.880], [0.938, 3.239], [-0.039, 1.193], [0.449, 1.975], [0.087, -0.701], [1.290, -0.809], [1.025, 0.772], [-0.732, 0.979]]
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT Preference mapping analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.PFM data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
