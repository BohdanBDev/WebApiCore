using Interngram.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.Services.Interfaces
{
    public interface IUserPreviewService
    {
        public List<UserPreviewDTO> SetUserFollowing(List<UserDTO> userSubscriptions, List<UserPreviewDTO> userPreviews);
    }
}
