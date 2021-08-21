using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using System.Text.Json;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PLSController : Controller
    {
        [HttpPost]
        public string GetBase64File([FromBody] XLSTAT.Models.Functionalities.PLS data)
        {
            return JsonSerializer.Serialize(XLSTAT.ReadyToRun.GenerateXLSTATFile(data));
        }
    }
}
