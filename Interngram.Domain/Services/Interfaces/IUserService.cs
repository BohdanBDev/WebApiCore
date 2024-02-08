using AutoMapper;
using Interngram.Domain.DTOs;
using Interngram.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> CreateUserAsync(UserDTO user);
        public Task<List<UserPreviewDTO>> GetUserPreviewSubscriptionsAsync(string viewerId, string userId, CancellationToken token, PaginationFilter? paginationFilter = null!);
        public Task<List<UserPreviewDTO>> GetUserPreviewSubscribersAsync(string viewerId, string userId, CancellationToken token, PaginationFilter? paginationFilter = null!);
        public Task<UserDTO> UpdateUserAsync(UserDTO mappedUser);
        public Task<UserDTO> GetUserAsync(GetUserFilter filter, CancellationToken token);
    }
}
