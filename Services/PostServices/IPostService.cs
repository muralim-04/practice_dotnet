using practice_dotnet.DTOs;
using practice_dotnet.Helpers;

namespace practice_dotnet.Services.PostServices
{
    public interface IPostService
    {
        Task<Response<PostResDto>> CreatePost(PostReqDto post, int userId);
        Task GetPost();
        Task<Response<PagedResult<PostResDto>>> GetAllPosts(int pageNumber, int pageSize);
        Task GetUserPosts();
        Task EditPost();
        Task<Response<bool>> DeletePost(int postId, int userId);
        Task<Response<bool>> DeletePostAdmin(int postId);

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
