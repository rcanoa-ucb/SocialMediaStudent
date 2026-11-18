using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _reportesService;
        private readonly IMapper _mapper;
        
        public ReportesController(IReportesService reportesservice, IMapper mapper)
        {
            _reportesService = reportesservice;
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
        [HttpGet("dapper/RankingUsuario")]
        public async Task<IActionResult> GetRankingUsuario([FromQuery] int user = 1)
        {
            var TopPostsUser = await _reportesService.GetRankingUsuarioAsync(user);

            var TopPostsUserResult = _mapper.Map<IEnumerable<RankigUsuarioDto>>(TopPostsUser);

            var response = new ApiResponse<IEnumerable<RankigUsuarioDto>>(TopPostsUserResult);

            return Ok(response);
        }
    }
}
