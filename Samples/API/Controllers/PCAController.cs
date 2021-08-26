using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using System.Text.Json;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PCAController : Controller
    {
        /// <summary>
        /// Creates a PCA reday to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "Y": {
        ///             "labels": ["Y1", "Y2"],
        ///             "table": [[-1.583, -0.267], [-0.361, -1.297], [0.413, 0.617], [0.093, -1.847], [-0.152, -2.038], [0.038, 0.166], [0.202, 1.254], [-1.149, 0.787], [-1.539, 1.276]]
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT PCA analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.PCA data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
