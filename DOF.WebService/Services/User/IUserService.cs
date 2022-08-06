using DOF.WebService.Common;
using DOF.WebService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOF.WebService.Services.User
{
    public interface IUserService
    {
        Task<UserModel> GetById(int id);
        Task<IEnumerable<UserModel>> Get(UserSearchData userSearchData, Pagination pagination, IEnumerable<Sort> sorts);
        Task<int> Add(UserAddData userAddData);
        Task Update(UserUpdateData userUpdateData);
        Task SetNewPassword(UserPasswordSetData userPwdSetData);
        Task UpdatePassword(UserPasswordUpdateData userPwdUpdData);
        Task<UserAuthorizeResponse> Authorize(UserAuthorizeData userAuthorizeData);
        Task Block(long userId);        
    }
}
