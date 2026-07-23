using practice_dotnet.Data;
using practice_dotnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace practice_dotnet.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        public PostService(DataContext context)
        {
            _context = context;
        }

        public Task CreateComment()
        {
            throw new NotImplementedException();
        }

        public Task CreatePost()
        {
            throw new NotImplementedException();
        }

        public Task DeleteComment()
        {
            throw new NotImplementedException();
        }

        public Task DeletePost()
        {
            throw new NotImplementedException();
        }

        public Task EditComment()
        {
            throw new NotImplementedException();
        }

        public Task EditPost()
        {
            throw new NotImplementedException();
        }

        public Task GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task GetPost()
        {
            throw new NotImplementedException();
        }

        public Task GetPostComments()
        {
            throw new NotImplementedException();
        }

        public Task GetUserComments()
        {
            throw new NotImplementedException();
        }

        public Task GetUserLikedposts()
        {
            throw new NotImplementedException();
        }

        public Task GetUserPosts()
        {
            throw new NotImplementedException();
        }

        public Task LikePost()
        {
            throw new NotImplementedException();
        }

        public Task UnlikePost()
        {
            throw new NotImplementedException();
        }
    }
}
