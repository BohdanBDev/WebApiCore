using AutoMapper;
using Interngram.Domain.Services.Interfaces;
using Interngram.Repository.Core.Interfaces.Repositories;
using Interngram.Repository.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interngram.Domain.DTOs;
using Interngram.Domain.Exceptions;

namespace Interngram.Domain.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivityService(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> FollowUserAsync(string followerId, string userId)
        {
            if (followerId == userId)
            {
                throw new ActivityException("Users can't follow themselves");
            }

            var follower = await _userRepository.GetAsync(followerId);
            var user = await _userRepository.GetAsync(userId);

            if (follower == null || user == null)
            {
                throw new UserException("User doesn't exist");
            }

            var findUserInFollower = follower.Subscriptions.SingleOrDefault(u => u == userId);
            var findFollowerInUser = user.Subscribers.SingleOrDefault(u => u == followerId);

            if (findUserInFollower != null && findFollowerInUser != null)
            {
                throw new ActivityException("User has already been followed");
            }

            follower.Subscriptions.Add(userId);
            user.Subscribers.Add(followerId);

            _userRepository.Update(user);
            _userRepository.Update(follower);
            await _unitOfWork.CompleteAsync();

            var mappedFollower = _mapper.Map<UserDTO>(follower);
            return mappedFollower;
        }

        public async Task<UserDTO> UnfollowUserAsync(string unfollowerId, string userId)
        {
            if (unfollowerId == userId)
            {
                throw new ActivityException("Users can't unfollow themselves");
            }

            var unfollower = await _userRepository.SingleOrDefaultAsync(u => u.Id == unfollowerId);
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == userId);

            if (unfollower == null || user == null)
            {
                throw new UserException("User doesn't exist");
            }

            var findUserInFollower = unfollower.Subscriptions.SingleOrDefault(u => u == userId);
            var findFollowerInUser = user.Subscribers.SingleOrDefault(u => u == unfollowerId);

            if (findUserInFollower == null && findFollowerInUser == null)
            {
                throw new UserException("User hasn't been followed");
            }

            unfollower.Subscriptions.Remove(userId);
            user.Subscribers.Remove(unfollowerId);

            _userRepository.Update(user);
            _userRepository.Update(unfollower);
            await _unitOfWork.CompleteAsync();

            var mappedFollower = _mapper.Map<UserDTO>(unfollower);
            return mappedFollower;
        }
    }
}
