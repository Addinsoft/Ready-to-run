using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PRMController : Controller
    {
        /// <summary>
        /// Creates a Preference mapping ready to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "T": {
        ///             "labels": ["X1", "Y1", "X2", "Y2"],
        ///             "table": [[7.5,17,3,32.5], [22.5,18,5.5,24], [34,33,34,20], [30,37,56,26], [6.5,35,18,24], [11,36,17,31.5], [37.5,37,56,22],[8,33,6,16]]
        ///         },
        ///         "ObjLabels": {
        ///             "labels": ["Objects"],
        ///             "table": [["Immedia_MP"], ["Carrefour_MP"], ["Immedia_SRB"], ["Casino_SRB"], ["Innocent_PBC"], ["Casino_PBC"], ["Innocent_SB"], ["Carrefour_SB"]]
        ///         },
        ///         "Type": 0
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT Preference mapping analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.PRM data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
