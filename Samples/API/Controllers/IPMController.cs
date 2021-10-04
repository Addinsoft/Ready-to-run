using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class IPMController : Controller
    {
        /// <summary>
        /// Creates a Preference mapping ready to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "T": {
        ///             "labels": ["A", "B"],
        ///             "table": [[191, 35], [189, 37], [193, 38], [162, 35], [189, 35], [182, 36], [211, 38], [167, 34], [176, 31]]
        ///         },
        ///         "Type": 2
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT Preference mapping analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.IPM data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
