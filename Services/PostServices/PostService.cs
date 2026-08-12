using practice_dotnet.Data;
using practice_dotnet.Entities;
using Microsoft.EntityFrameworkCore;
using practice_dotnet.Helpers;
using practice_dotnet.DTOs;

namespace practice_dotnet.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;
        public PostService(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public Task CreateComment()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> CreatePost(ReqPostDto post, int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);

            if (!userExists)
            {
                return Response<bool>.Fail("User was not found");
            }

            string? imageUrl = null;
            if (post.Image != null && post.Image.Length > 0)
            {
                imageUrl = await UploadImageAsync(post.Image);
            }

            var newPost = new UserPost
            {
                Title = post.Title,
                Content = post.Content,
                ImageUrl = imageUrl,
                UserId = userId
            };

            _context.UserPosts.Add(newPost);
            await _context.SaveChangesAsync();

            return Response<bool>.Ok(true);
        }

        public Task DeleteComment()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeletePost(int postId, int userId)
        {
            var post = await _context.UserPosts
                .FirstOrDefaultAsync(p => p.Id == postId && p.UserId == userId);

            if (post == null)
            {
                return Response<bool>.Fail("Post not found or access denied.");
            }

            if (!string.IsNullOrEmpty(post.ImageUrl))
            {
                DeleteImageFile(post.ImageUrl);
            }

            _context.UserPosts.Remove(post);
            await _context.SaveChangesAsync();

            return Response<bool>.Ok(true);
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

        private async Task<string> UploadImageAsync(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");

            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{uniqueFileName}";
        }

        private void DeleteImageFile(string imageUrl)
        {
            try
            {
                string relativePath = imageUrl.TrimStart('/', '\\');

                string filePath = Path.Combine(_environment.ContentRootPath, imageUrl);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete image file: {ex.Message}");
            }
        }
    }
}
