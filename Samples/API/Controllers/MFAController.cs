using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MFAController : Controller
	{
        /// <summary>
        /// Creates a MFA ready to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///          "Data": {
        ///                 "labels": ["Tab_1", "Tab_2", "Tab_3", "Tab_4", "Tab_5"],
        ///                 "table": [[
        ///     				{
        ///     						"labels": ["A1", "A2"],
        ///     						"table": [["Yes", "Yes"], ["Yes", "Yes"], ["Yes", "DK"], ["Yes", "Yes"], ["Yes", "Yes"], ["Yes", "DK"], ["No", "No"], ["No", "Yes"], ["No", "DK"], ["No", "Yes"], ["No", "Yes"], ["No", "No"]]
        ///     				}
        ///     				],
        ///                     [
        ///     				{
        ///                         "labels": ["B1", "B2", "B3"],
        ///                         "table": [["5", "No", "DK"], [ "4", "No", "DK"], ["3", "No", "Yes"],  ["1", "No", "No"],  ["1", "No", "No"], ["3", "No", "No"],  ["1", "No", "DK"],  ["4", "No", "DK"], ["5", "Yes", "DK"],  ["3", "No", "DK"],  ["3", "No", "DK"], ["3", "No", "DK"]]
        ///                     }
        ///     				],
        ///                     [
        ///     				{
        ///                         "labels": ["Insect7", "Insect8", "Insect9", "Insect10"],
        ///                         "table": [["32", "47", "19", "17"], ["36", "44", "42", "18"], ["29", "31", "37", "27"], ["7", "27", "46", "49"], ["22", "19", "48", "35"], ["2", "11", "10", "0"], ["23", "3", "21", "17"], ["46", "9", "8", "35"], ["6", "4", "35", "38"], ["23", "10", "1", "27"], ["5", "15", "5", "18"], ["6", "22", "19", "19"]]
        ///                     }
        ///     				],
        ///                     [
        ///     				{
        ///                         "labels": ["Insect4", "Insect5", "Insect76"],
        ///                         "table": [["9", "5", "7"], ["15", "5", "7"], ["12", "1", "26"], ["14", "3", "25"],  ["20", "0", "10"], ["24", "39", "2"], ["33", "7", "7"], ["42", "11", "17"], ["46", "25", "18"], ["47", "24", "7"],  ["30", "3", "8"], ["31", "28", "12"]]
        ///                     }
        ///     				],
        ///                     [
        ///     				{
        ///                         "labels": ["C1", "C2", "C3", "C4"],
        ///                         "table": [["a", "c", "e", "h"], ["a", "d", "e", "h"], ["a", "c", "e", "h"], ["a", "c", "e", "h"], ["a", "c", "f", "i"], ["b", "c", "f", "i"], ["b", "d", "f", "h"], ["b", "d", "f", "h"], ["b", "d", "g", "j"], ["b", "c", "g", "j"], ["b", "d", "g", "h"], ["b", "d", "g", "h"]]
        ///                     }
        ///     				]
        ///                 ]
        ///          }
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run a Multiple Factor analysis</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.MFA data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
