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

        public async Task<Response<PostResDto>> CreatePost(PostReqDto post, int userId)
        {
            var userName = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

            if (userName == null)
            {
                return Response<PostResDto>.Fail("User was not found");
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

            var userPostDto = new PostResDto
            {
                Id = newPost.Id,
                Title = newPost.Title,
                Content = newPost.Content,
                ImageUrl = newPost.ImageUrl,
                CreatedAt = newPost.CreatedAt,
                UserName = userName
            };

            return Response<PostResDto>.Ok(userPostDto);
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
        public async Task<Response<bool>> DeletePostAdmin(int postId)
        {
            var post = await _context.UserPosts
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return Response<bool>.Fail("Post not found.");
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

        public async Task<Response<PagedResult<PostResDto>>> GetAllPosts(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var totalCount = await _context.UserPosts.CountAsync();

            var posts = await _context.UserPosts
                .OrderByDescending(up => up.CreatedAt)
                .ThenByDescending(up => up.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(up => new PostResDto
                {
                    Id = up.Id,
                    Title = up.Title,
                    Content = up.Content,
                    ImageUrl = up.ImageUrl,
                    CreatedAt = up.CreatedAt,
                    UserName = up.User.UserName
                })
                .ToListAsync();

            var data = new PagedResult<PostResDto>
            {
                Items = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Response<PagedResult<PostResDto>>.Ok(data);
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
