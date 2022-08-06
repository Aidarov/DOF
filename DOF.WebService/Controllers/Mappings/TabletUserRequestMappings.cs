using DOF.WebService.Controllers.Request.TabletUser;
using DOF.WebService.Services.TabletUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Mappings
{
    public static class TabletUserRequestMappings
    {
        public static TabletUserAddData MapToTabletUserAddData(this TabletUserAddRequest oilWellAddRequest, int createdBy, DateTime createdAt, bool isDeleted)
        {
            return new TabletUserAddData()
            {
                Login = oilWellAddRequest.Login,
                FirstName = oilWellAddRequest.FirstName,
                LastName = oilWellAddRequest.LastName,
                MiddleName = oilWellAddRequest.MiddleName,
                PasswordData = new TabletUserSetNewPasswordData()
                {
                    NewPassword = oilWellAddRequest.NewPassword,
                    ConfirmPassword = oilWellAddRequest.ConfirmPassword
                }
            };
        }

        public static TabletUserUpdateData MapToTabletUserUpdateData(this TabletUserUpdateRequest oilWellUpdateRequest)
        {
            return new TabletUserUpdateData()
            {
                Id = oilWellUpdateRequest.Id,
                FirstName = oilWellUpdateRequest.FirstName,
                LastName = oilWellUpdateRequest.LastName,
                Login = oilWellUpdateRequest.Login,
                MiddleName = oilWellUpdateRequest.MiddleName
            };
        }

        public static TabletUserSearchData MapToTabletUserSearchData(this TabletUserSearchRequest tabletUserSearchRequest)
        {
            return new TabletUserSearchData()
            {
                Login = tabletUserSearchRequest.Login,
                FirstName = tabletUserSearchRequest.FirstName,
                LastName = tabletUserSearchRequest.LastName,
                MiddleName = tabletUserSearchRequest.MiddleName
            };
        }
    }
}
