using practice_dotnet.DTOs;
using practice_dotnet.Helpers;

namespace practice_dotnet.Services.PostServices
{
    public interface IPostService
    {
        Task<Response<bool>> CreatePost(ReqPostDto post, int userId);
        Task GetPost();
        Task GetAllPosts();
        Task GetUserPosts();
        Task EditPost();
        Task DeletePost();

        // COMMENTS SECTION
        Task CreateComment();
        Task GetPostComments();
        Task GetUserComments();
        Task EditComment();
        Task DeleteComment();

        // LIKES SECTION
        Task LikePost();
        Task UnlikePost();
        Task GetUserLikedposts();
    }
}
