using Interngram.Domain.DTOs;
using Interngram.Domain.Services.Interfaces;

namespace Interngram.Domain.Services;

public class UserPreviewService : IUserPreviewService
{
    public List<UserPreviewDTO> SetUserFollowing(List<UserDTO> userSubscriptions, List<UserPreviewDTO> userPreviews)
    {
        if (!userSubscriptions.Any()) return userPreviews;
        
        foreach (var userPreview in userPreviews)
        {
            userPreview.Following = userSubscriptions.SingleOrDefault(u => u.Id == userPreview.Id) != null;
        }

        return userPreviews;
    }
}