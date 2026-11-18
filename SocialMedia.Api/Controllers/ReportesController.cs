using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs.Reporteria;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _reportesService;
        private readonly IMapper _mapper;
        public ReportesController(IReportesService reportesService,
            IMapper mapper)
        {
            _reportesService = reportesService;
            _mapper = mapper;
        }

        [HttpGet("dapper/auto-comentaristas")]
        public async Task<IActionResult> GetAutoComentaristas([FromQuery] int limit = 10)
        {
            var autoComentaristas = await _reportesService.GetAutoComentaristasAsync(limit);

            var autoComentaristasResult = _mapper.Map<IEnumerable<AutoComentaristaDto>>(autoComentaristas);

            var response = new ApiResponse<IEnumerable<AutoComentaristaDto>>(autoComentaristasResult);

            return Ok(response);
        }
        [HttpGet("dapper/ranking-posts-por-usuario")]
        public async Task<IActionResult> GetRankingPostsPorUsuario()
        {
            var ranking = await _reportesService.GetRankingPostsPorUsuarioAsync();

            var resultado = _mapper.Map<IEnumerable<RankingPostUsuarioDto>>(ranking);

            return Ok(new ApiResponse<IEnumerable<RankingPostUsuarioDto>>(resultado));
        }

    }
}
