﻿using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CATAController : Controller
    {
        /// <summary>
        /// Creates a CATA ready to run file.
        /// </summary>  
        /// <remarks>
        /// Sample request:
        ///     
        ///     {
        ///         "T": {
        ///             "labels": ["Hard", "Juicy", "Sweet", "Amer", "Intense odor", "Sour", "Crunchy", "Tasty", "Granular", "Sweet", "Odorless", "Tasteless", "Farinux", "Taste of apple", "Astringent"],
        ///             "table": [[1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1], [1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0], [1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0], [1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1], [1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0], [1, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0], [0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0], [0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0], [0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0], [1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0], [0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0], [0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], [1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0], [1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0], [1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0], [1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0], [1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0], [1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0], [1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0], [1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1], [0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0], [1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0], [1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1], [1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0], [1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1], [0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1], [0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0], [1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0], [1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0], [1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0]]
        ///         },
        ///         "P": {
        ///             "labels": ["Product"],
        ///             "table": [["106"], ["992"], ["366"], ["257"], ["548"], ["Ideal"], ["106"], ["992"], ["366"], ["257"], ["548"], ["Ideal"], ["106"], ["992"], ["366"], ["257"], ["548"], ["Ideal"], ["106"], ["992"], ["366"], ["257"], ["548"], ["Ideal"], ["106"], ["992"], ["366"], ["257"], ["548"], ["Ideal"], ["106"], ["992"], ["366"], ["257"], ["548"], ["Ideal"]]
        ///         },
        ///         "J": {
        ///             "labels": ["Judge"],
        ///             "table": [["43"], ["43"], ["43"], ["43"], ["43"], ["43"], ["41"], ["41"], ["41"], ["41"], ["41"], ["41"], ["44"], ["44"], ["44"], ["44"], ["44"], ["44"], ["31"], ["31"], ["31"], ["31"], ["31"], ["31"], ["26"], ["26"], ["26"], ["26"], ["26"], ["26"], ["37"], ["37"], ["37"], ["37"], ["37"], ["37"]]
        ///         },
        ///         "S": {
        ///             "labels": ["Preference"],
        ///             "table": [["4"], ["9"], ["5"], ["8"], ["5"], [""], ["5"], ["9"], ["4"], ["7"], ["7"], [""], ["4"], ["9"], ["6"], ["7"], ["8"], [""], ["9"], ["9"], ["5"], ["7"], ["8"], [""], ["5"], ["4"], ["6"], ["7"], ["9"], [""], ["6"], ["6"], ["4"], ["8"], ["9"], [""]]
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a Microsoft Excel (.xlsm) file with data and a button to run XLSTAT CATA analyse</returns> 
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.CATA data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
