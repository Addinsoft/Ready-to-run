using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FSTController : Controller
    {
        /// <summary>
        /// Creates a Free sorting ready to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "T": {
        ///             "labels": ["S1", "S2", "S3"],
        ///             "table": [[1, 1, 1], [2, 2, 2], [3, 3, 2], [4, 4, 3], [5, 5, 4], [6, 4, 5], [4, 4, 3], [7, 1, 2], [6, 4, 5], [8, 6, 6], [8, 7, 6], [6, 1, 7], [8, 6, 6], [1, 7, 1]]
        ///         },
        ///         "ObsLabels": {
        ///             "labels": ["Obervations labels"],
        ///             "table": [["Obs 1"], ["Obs 2"], ["Obs 3"], ["Obs 4"], ["Obs 5"], ["Obs 6"], ["Obs 7"], ["Obs 8"], ["Obs 9"], ["Obs 10"], ["Obs 11"], ["Obs 12"], ["Obs 13"], ["Obs 14"]]
        ///         },
        ///         "Method": 2
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT statistic descriptive analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.FST data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
