using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Enums;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Consumer))]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
            
        }

        [HttpGet()]
        public async Task<IActionResult> GetTest()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Saludo", "hola");

            return Ok(dic);
        }
    }
}
