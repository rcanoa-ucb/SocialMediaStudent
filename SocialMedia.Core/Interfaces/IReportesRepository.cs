using SocialMedia.Core.CustomEntities.Reporteria;

namespace SocialMedia.Core.Interfaces
{
    public interface IReportesRepository
    {
        Task<IEnumerable<TopPostUsuario>> GetTopPostsPorUsuarioAsync();

        Task<IEnumerable<AutoComentarista>> GetAutoComentaristasAsync(int limit = 10);
    }
}
