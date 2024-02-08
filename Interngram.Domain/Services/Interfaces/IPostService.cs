using Interngram.Domain.DTOs;

namespace Interngram.Domain.Services.Interfaces
{
    public interface IPostService
    {
        public Task<PostDTO> CreatePostAsync(PostCreateDTO postDto);
        public Task<PostDTO> UpdatePostAsync(PostDTO postDto);
        public Task RemovePostAsync(string postId);

        public Task<PostDTO> GetPostByPostIdAsync(string postId);
        public Task<List<PostDTO>> GetAllPostsByAuthorIdAsync(string authorId);

        public Task LikePostAsync(string likerId, string postId);
        public Task UnlikePostAsync(string unlikerId, string postId);
        public Task<List<UserPreviewDTO>> GetLikersAsync(string viewerId, string postId);
    }
}
