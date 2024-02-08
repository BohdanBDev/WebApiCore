using AutoMapper;
using Interngram.Repository.Core.Interfaces.Repositories;
using Interngram.Repository.Core.Interfaces;
using Interngram.Domain.Services.Interfaces;
using Interngram.Domain.DTOs;
using Interngram.Domain.Exceptions;
using Interngram.Repository.Models;

namespace Interngram.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IUserPreviewService _userPreviewService;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUserPreviewService userPreviewService,
            IPostRepository postRepository, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _userPreviewService = userPreviewService;
        }

        public async Task<PostDTO> CreatePostAsync(PostCreateDTO postDto)
        {
            var post = await _postRepository.SingleOrDefaultAsync(p => p.Id == postDto.Id);

            if (post != null)
                throw new PostException("Post already exists");

            var mappedPost = _mapper.Map<Post>(postDto);
            await _postRepository.AddAsync(mappedPost);
            await _unitOfWork.CompleteAsync();

            var getCreatedPost = await _postRepository.SingleOrDefaultAsync(p => p.Id == postDto.Id);

            var mappedToDto = _mapper.Map<PostDTO>(getCreatedPost);
            return mappedToDto;
        }

        public async Task<PostDTO> UpdatePostAsync(PostDTO postDto)
        {
            var post = await _postRepository.SingleOrDefaultAsync(p => p.Id == postDto.Id);

            if (post == null)
                throw new PostException("Post doesn't exist");

            post.Description = postDto.Description;
            post.Image = postDto.Image;

            _postRepository.Update(post);
            await _unitOfWork.CompleteAsync();

            var mappedPost = _mapper.Map<PostDTO>(post);
            return mappedPost;
        }

        public async Task RemovePostAsync(string postId)
        {
            var post = await _postRepository.SingleOrDefaultAsync(p => p.Id == postId);

            if (post == null)
                throw new PostException("Post doesn't exist");

            _postRepository.Remove(post);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<PostDTO>> GetAllPostsByAuthorIdAsync(string authorId)
        {
            var posts = await _postRepository.FindAsync(p => p.AuthorId == authorId);

            if (posts == null)
                throw new PostException("The user with this id does not have any posts");

            var mappedPosts = _mapper.Map<List<PostDTO>>(posts);
            return mappedPosts;
        }

        public async Task<PostDTO> GetPostByPostIdAsync(string postId)
        {
            var post = await _postRepository.GetAsync(postId);

            if (post == null)
                throw new PostException("Post doesn't exist");

            var mappedPost = _mapper.Map<PostDTO>(post);
            return mappedPost;
        }

        public async Task<List<UserPreviewDTO>> GetLikersAsync(string viewerId, string postId)
        {
            var post = await _postRepository.GetAsync(postId);
            var viewer = await _userRepository.GetAsync(viewerId);

            if (post == null) throw new PostException("Post doesn't exist");
            if (viewer == null) throw new UserException("User doesn`t exist");
            
            if (!post.Likes.Any()) return new List<UserPreviewDTO>();
            var likers = await _userRepository.GetUsersByIdsAsync(post.Likes);

            var viewersSubscriptions = await _userRepository.GetUsersByIdsAsync(viewer.Subscriptions);

            var likersPrview = _mapper.Map<List<UserDTO>>(likers);

            var postDto = _mapper.Map<List<UserPreviewDTO>>(likersPrview);
            var userDto = _mapper.Map<List<UserDTO>>(viewersSubscriptions);

            var likersPreviews = _userPreviewService.SetUserFollowing(userDto, postDto);

            return likersPreviews;
        }

        public async Task LikePostAsync(string likerId, string postId)
        {
            var post = await _postRepository.GetAsync(postId);
            var liker = await _userRepository.GetAsync(likerId);

            if (post == null) throw new PostException("Post doesn't exist");
            if (liker == null) throw new UserException("User doesn`t exist");

            if (post.Likes.Contains(likerId))
                throw new PostException("You have already liked this post");

            post.Likes.Add(likerId);
            _postRepository.Update(post);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UnlikePostAsync(string unlikerId, string postId)
        {
            var post = await _postRepository.GetAsync(postId);
            var liker = await _userRepository.GetAsync(unlikerId);

            if (post == null) throw new PostException("Post doesn't exist");

            if (liker == null) throw new UserException("User doesn`t exist");

            if (!post.Likes.Contains(unlikerId)) throw new PostException("You haven't liked this post yet");
            
            post.Likes.Remove(unlikerId);
            _postRepository.Update(post);
            await _unitOfWork.CompleteAsync();
        }
    }
}
