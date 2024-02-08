using Interngram.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.Services.Interfaces
{
    public interface IActivityService
    {
        public Task<UserDTO> FollowUserAsync(string followerId, string userId);

        public Task<UserDTO> UnfollowUserAsync(string unfollowerId, string userId);
    }
}
