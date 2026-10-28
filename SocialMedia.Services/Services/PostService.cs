using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }

        public async Task InsertPost(Post post)
        {
            //1. Solo se permite registrar publicaciones a usuarios previamente registrados
            var user = await _userRepository.GetUserByIdAsync(post.UserId);
            if (user == null)
                throw new Exception("El usuario no existe");

            //2. No es permitido publicar información o texto que contenga palabras inapropiadas o de concepto ofensivo.
            // no son parte de fluent validation
            if (ContainsForbiddenContent(post.Description))
                throw new Exception("El contenido contiene no es permitido");


            await _postRepository.InsertPost(post);

         
        }

        public async Task UpdatePost(Post post)
        {
            await _postRepository.UpdatePost(post);
        }

        public async Task DeletePost(Post post)
        {
            await _postRepository.DeletePost(post);
        }
        //lisat de palabras o exoresiones no eprmitidas
        private readonly string[] ForbiddenWords =
        {
            "violencia",
            "odio",
            "grosería",
            "discriminación"
        };
        public bool ContainsForbiddenContent(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false; //anula todos los espacios vacios y nulos y no valida nada

            foreach (var word in ForbiddenWords) // si tiene las palabras 
            {
                if (text.Contains(word, StringComparison.OrdinalIgnoreCase)) // el etxto contiene palabras prohibidas y OrdinalIgnoreCase no concidera mayusculas 
                    return true;
            }

            return false;
        }
    }
}






