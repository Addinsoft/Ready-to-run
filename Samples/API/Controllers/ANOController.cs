using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using System.Text.Json;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ANOController : Controller
    {
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.ANO data)
        {
            return JsonSerializer.Serialize(XLSTAT.ReadyToRun.GenerateXLSTATFile(data));
        }
    }
}
