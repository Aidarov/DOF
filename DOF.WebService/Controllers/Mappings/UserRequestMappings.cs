using DOF.WebService.Controllers.Request.User;
using DOF.WebService.Services.User;
using System;

namespace DOF.WebService.Controllers.Mappings
{
    public static class UserRequestMappings
    {
        public static UserAddData MapToUserAddData(this UserAddRequest userAddRequest, int createdBy, DateTime createdAt, bool isDeleted)
        {
            return new UserAddData()
            {
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                IsBlocked = isDeleted,
                Email = userAddRequest.Email,
                FirstName = userAddRequest.FirstName,
                LastName = userAddRequest.LastName,
                MiddleName = userAddRequest.MiddleName,
                Login = userAddRequest.Login,
                Role = userAddRequest.Role
            };
        }

        public static UserUpdateData MapToUserUpdateData(this UserUpdateRequest userUpdateRequest)
        {
            return new UserUpdateData()
            {
                Id = userUpdateRequest.Id,
                Email = userUpdateRequest.Email,
                FirstName = userUpdateRequest.FirstName,
                LastName = userUpdateRequest.LastName,
                MiddleName = userUpdateRequest.MiddleName,
                Login = userUpdateRequest.Login,
                Role = userUpdateRequest.Role                
            };
        }

        public static UserSearchData MapToUserSearchData(this UserSearchRequest userSearchRequest)
        {
            return new UserSearchData()
            {
                Login = userSearchRequest.Login,
                Email = userSearchRequest.Login,
                FirstName = userSearchRequest.Login,
                LastName = userSearchRequest.Login,
                MiddleName = userSearchRequest.Login,
                Role = userSearchRequest.Role
            };
        }

    }
}
