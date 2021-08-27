using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using System.Text.Json;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ANOController : Controller
    {
        /// <summary>
        /// Creates an ANOVA reday to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "ObsLabels": {
        ///             "labels": ["Observations"],
        ///             "table": [["Obs1"], ["Obs2"], ["Obs3"], ["Obs4"], ["Obs5"], ["Obs6"], ["Obs7"], ["Obs8"], ["Obs9"]]
        ///             },
        ///         "Y": {
        ///             "labels": ["Y1", "Y2"],
        ///             "table": [[0.689, -0.837], [0.181, 0.880], [0.938, 3.239], [-0.039, 1.193], [0.449, 1.975], [0.087, -0.701], [1.290, -0.809], [1.025, 0.772], [-0.732, 0.979]]
        ///         },
        ///         "Q": {
        ///             "labels": ["Q1", "Q2"],
        ///             "table": [["A", "F"], ["E", "B"], ["E", "A"], ["E", "D"], ["D", "G"], ["C", "B"], ["C", "C"], ["A", "E"], ["F", "A"]]
        ///         },
        ///         "Interactions":3
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT ANOVA analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.ANO data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
