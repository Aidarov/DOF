using DOF.WebService.Controllers.Request.OilWell;
using DOF.WebService.Services.OilWell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Mappings
{
    public static class OilWellRequestMappings
    {
        public static OilWellAddData MapToOilWellAddData(this OilWellAddRequest oilWellAddRequest, int createdBy, DateTime createdAt, bool isDeleted)
        {
            return new OilWellAddData()
            {
                Code = oilWellAddRequest.Code,
                Name = oilWellAddRequest.Name,
                ShortName = oilWellAddRequest.ShortName,
                OilField = oilWellAddRequest.OilField,
                Latitude = oilWellAddRequest.Latitude,
                Longitude = oilWellAddRequest.Longitude,
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                IsDeleted = isDeleted
            };
        }

        public static OilWellUpdateData MapToOilWellUpdateData(this OilWellUpdateRequest oilWellUpdateRequest)
        {
            return new OilWellUpdateData()
            {
                Id = oilWellUpdateRequest.Id,
                Code = oilWellUpdateRequest.Code,
                Name = oilWellUpdateRequest.Name,
                ShortName = oilWellUpdateRequest.ShortName,
                OilField = oilWellUpdateRequest.OilField,
                Latitude = oilWellUpdateRequest.Latitude,
                Longitude = oilWellUpdateRequest.Longitude
            };
        }

        public static OilWellSearchData MapToOilFieldSearchData(this OilWellSearchRequest oilWellSearchRequest)
        {
            return new OilWellSearchData()
            {
                OilFieldId = oilWellSearchRequest.OilFieldId,
                Code = oilWellSearchRequest.Code,
                Name = oilWellSearchRequest.Name,
                ShortName = oilWellSearchRequest.ShortName
            };
        }
    }
}
