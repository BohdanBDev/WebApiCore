using AutoMapper;
using Interngram.Domain.DTOs;
using Interngram.Domain.Exceptions;
using Interngram.Domain.Filters;
using Interngram.Domain.Services.Interfaces;
using Interngram.Repository.Core.Interfaces;
using Interngram.Repository.Core.Interfaces.Repositories;
using Interngram.Repository.Models;

namespace Interngram.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserPreviewService _userPreviewService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUserPreviewService userPreviewService, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _userPreviewService = userPreviewService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == userDto.Id);

            if (user != null)
            {
                throw new UserException("User already exists");
            }

            var mappedUser = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(mappedUser);
            await _unitOfWork.CompleteAsync();

            var getCreatedUser = await _userRepository.SingleOrDefaultAsync(u => u.Id == userDto.Id);

            var mappedToDto = _mapper.Map<UserDTO>(getCreatedUser);
            return mappedToDto;
        }

        public async Task<List<UserPreviewDTO>> GetUserPreviewSubscribersAsync(string viewerId, string userId, CancellationToken token, PaginationFilter? paginationFilter = null!)
        {
            var viewer = await _userRepository.GetAsync(viewerId);
            if (viewer == null) throw new UserException("Viewer doesn't exist");
            
            var viewerSubscriptionsIds = viewer.Subscriptions;

            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new UserException("User doesn't exist");
            
            var userSubscribersIds = user.Subscribers;
            
            if (paginationFilter != null)
            {
                if (paginationFilter.PageNumber == null || paginationFilter.PageSize == null)
                    throw new NullReferenceException("Page number and page size shouldn't be null");
                
                var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
                userSubscribersIds = userSubscribersIds.Skip(skip.Value).Take(paginationFilter.PageSize.Value).ToList();
            }
            
            if (!userSubscribersIds.Any()) return new List<UserPreviewDTO>();
            
            var viewerSubscriptions = await _userRepository.GetUsersByIdsAsync(viewerSubscriptionsIds);
            if (!viewerSubscriptions.Any() && viewerSubscriptionsIds.Any()) 
                throw new UserException($"Users by viewer subscriptions ids not found. ViewerId: {viewerId} UserId: {userId}");
                
            var viewerSubscriptionsDto = _mapper.Map<List<UserDTO>>(viewerSubscriptions);

            var userSubscribers = await _userRepository.GetUsersByIdsAsync(userSubscribersIds);
            if (!userSubscribers.Any() && userSubscribersIds.Any()) 
                throw new UserException($"Users by user subscribers ids not found. ViewerId: {viewerId} UserId: {userId}");
            
            var userSubscribersDto = _mapper.Map<List<UserDTO>>(userSubscribers);

            var subscribersPreviews = _mapper.Map<List<UserPreviewDTO>>(userSubscribersDto);

            subscribersPreviews = _userPreviewService.SetUserFollowing(viewerSubscriptionsDto, subscribersPreviews);
            
            return subscribersPreviews;
        }

        public async Task<List<UserPreviewDTO>> GetUserPreviewSubscriptionsAsync(string viewerId, string userId, CancellationToken token, PaginationFilter? paginationFilter = null!)
        {
            var viewer = await _userRepository.GetAsync(viewerId);
            if (viewer == null) throw new UserException("Viewer doesn't exist");
            
            var viewerSubscriptionsIds = viewer.Subscriptions;
            
            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new UserException("User doesn't exist");
            
            var userSubscriptionsIds = user.Subscriptions;
            
            if (paginationFilter != null)
            {
                if (paginationFilter.PageNumber == null || paginationFilter.PageSize == null)
                    throw new NullReferenceException("Page number and page size shouldn't be null");
                
                var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
                userSubscriptionsIds =  userSubscriptionsIds.Skip(skip.Value).Take(paginationFilter.PageSize.Value).ToList();
            }

            if (!userSubscriptionsIds.Any()) return new List<UserPreviewDTO>();
            
            var viewerSubscriptions = await _userRepository.GetUsersByIdsAsync(viewerSubscriptionsIds);
            if (!viewerSubscriptions.Any() && viewerSubscriptionsIds.Any()) 
                throw new UserException($"Users by viewer subscriptions ids not found. ViewerId: {viewerId} UserId: {userId}");
                
            var viewerSubscriptionsDto = _mapper.Map<List<UserDTO>>(viewerSubscriptions);

            var userSubscriptions = await _userRepository.GetUsersByIdsAsync(userSubscriptionsIds);
            if (!userSubscriptions.Any() && userSubscriptionsIds.Any()) 
                throw new UserException($"Users by user subscriptions ids not found. ViewerId: {viewerId} UserId: {userId}");
            
            var userSubscriptionsDto = _mapper.Map<List<UserDTO>>(userSubscriptions);

            var subscriptionsPreviews = _mapper.Map<List<UserPreviewDTO>>(userSubscriptionsDto);

            subscriptionsPreviews = _userPreviewService.SetUserFollowing(viewerSubscriptionsDto, subscriptionsPreviews);

            return subscriptionsPreviews;
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDto) 
        {
            if (userDto.Email == null && userDto.Phone == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == userDto.Id);

            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var updatedUser = _mapper.Map(userDto, user);

            _userRepository.Update(updatedUser);
            await _unitOfWork.CompleteAsync();

            var updatedUserDto = _mapper.Map<UserDTO>(updatedUser);
            
            return updatedUserDto;
        }
        public async Task<UserDTO> GetUserAsync(GetUserFilter filter, CancellationToken token)
        {
            var user = _mapper.Map<User>(filter); 

            user = await _userRepository.GetUserFilteredAsync(user, token);
            
            var userToDto = _mapper.Map<UserDTO>(user);

            return userToDto;
        }
    }
}
