
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.CustomEntities.Reporteria;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _reportesService;

        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("ranking-posts-por-usuario")]
        public async Task<IActionResult> GetRankingPostsPorUsuario()
        {
            var ranking = await _reportesService
                .GetRankingPostsPorUsuarioAsync();

            var response =
                new ApiResponse<IEnumerable<RankingPostUsuario>>(ranking);

            return Ok(response);
        }
    }
}
