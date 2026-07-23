namespace practice_dotnet.Services.PostServices
{
    public interface IPostService
    {
        Task CreatePost();
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
