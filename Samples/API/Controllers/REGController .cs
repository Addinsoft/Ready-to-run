using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using System.Text.Json;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class REGController : Controller
    {
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.REG data)
        {
            XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(data);
            if (result.Success)
                return result.Data;
            return "";
        }
    }
}
