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
        // inyeccion de mapeer y servicios
        private readonly IReportesService _reportesService;
        private readonly IMapper _mapper;

        public ReportesController(IReportesService reportesService, IMapper mapper)
        {
            _mapper = mapper;
            _reportesService = reportesService;
        }

        [HttpGet("dapper/auto-comentaristas")] // ruta 
        public async Task<IActionResult> GetAutoComentaristas([FromQuery] int limit = 10)
        {
            var autoComentaristas = await _reportesService.GetAutoComentaristasAsync(limit);// mandar parameto limit o manda 10

            var autoComentaristasResult = _mapper.Map<IEnumerable<AutoComentaristaDto>>(autoComentaristas); // map de dto   //autoComentaristas origen // destino AutoComentaristaDto 

            var response = new ApiResponse<IEnumerable<AutoComentaristaDto>>(autoComentaristasResult); //

            return Ok(response); // mejor hacer con un try cacht 
        }

        [HttpGet("dapper/ranking-posts-usuario")]
        public async Task<IActionResult> GetRankingPostsUsuario(
        [FromQuery] int limit = 3)
        {
            var ranking = await _reportesService
                .GetRankingPostsUsuarioAsync(limit);

            var rankingDto = _mapper
                .Map<IEnumerable<RankingPostUsuarioDto>>(ranking);

            var response =
                new ApiResponse<IEnumerable<RankingPostUsuarioDto>>(rankingDto);

            return Ok(response);
        }
    }
}
