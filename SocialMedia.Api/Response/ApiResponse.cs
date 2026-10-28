namespace SocialMedia.Api.Response
{
    public class ApiResponse <T> // T acepota cualqueir clase post coment user
    {
        public T Data {  get; set; }
        public ApiResponse (T data)
        {
            Data = data;   
        }
    }
}
