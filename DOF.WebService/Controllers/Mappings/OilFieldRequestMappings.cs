using DOF.WebService.Controllers.Request.OilField;
using DOF.WebService.Services.OilField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Mappings
{
    public static class OilFieldRequestMappings
    {
        public static OilFieldAddData MapToOilFieldAddData(this OilFieldAddRequest oilFieldAddRequest, int createdBy, DateTime createdAt, bool isDeleted)
        {
            return new OilFieldAddData()
            {
                Code = oilFieldAddRequest.Code,
                Name = oilFieldAddRequest.Name,
                ShortName = oilFieldAddRequest.ShortName,
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                IsDeleted = isDeleted
            };
        }

        public static OilFieldUpdateData MapToOilFieldUpdateData(this OilFieldUpdateRequest oilFieldUpdateRequest)
        {
            return new OilFieldUpdateData()
            {
                Id = oilFieldUpdateRequest.Id,
                Code = oilFieldUpdateRequest.Code,
                Name = oilFieldUpdateRequest.Name,
                ShortName = oilFieldUpdateRequest.ShortName,
            };
        }

        public static OilFieldSearchData MapToOilFieldSearchData(this OilFieldSearchRequest oilFieldSearchRequest)
        {
            return new OilFieldSearchData()
            {
                Code = oilFieldSearchRequest.Code,
                Name = oilFieldSearchRequest.Name,
                ShortName = oilFieldSearchRequest.ShortName
            };
        }
    }
}
