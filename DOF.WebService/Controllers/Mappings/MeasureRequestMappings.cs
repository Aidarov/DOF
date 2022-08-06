using DOF.WebService.Common;
using DOF.WebService.Controllers.Request.Measure;
using DOF.WebService.Services.Measure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Mappings
{
    public static class MeasureRequestMappings
    {
        public static MeasureAddData MapToMeasureAddData(this MeasureAddRequest measureAddRequest, DateTime acceptedAt, int deviceId, int createdBy)
        {
            return new MeasureAddData()
            {
                AcceptedAt = acceptedAt,
                CreatedAt = measureAddRequest.CreatedAt,
                CreatedBy = createdBy,
                DavNaMkp = measureAddRequest.DavNaMkp,
                DavPosShtut = measureAddRequest.DavPosShtut,
                DeviceId = deviceId,
                Latitude = measureAddRequest.Latitude,
                Longitude = measureAddRequest.Longitude,
                OilWellId = measureAddRequest.OilWellId,
                TempPosShtut = measureAddRequest.TempPosShtut,
                TrubDav = measureAddRequest.TrubDav,
                TrubTemp = measureAddRequest.TrubTemp,
                ZatrubDav = measureAddRequest.ZatrubDav,
                ZatrubTemp = measureAddRequest.ZatrubTemp
            };
        }

        public static MeasureSearchData MapToMeasureSearchData(this MeasureSearchRequest measureSearchRequest)
        {
            return new MeasureSearchData()
            {
                AcceptedAt = measureSearchRequest.AcceptedAt,
                CreatedAt = measureSearchRequest.CreatedAt,
                CreatedBy  = measureSearchRequest.CreatedBy,
                DeviceId = measureSearchRequest.DeviceId,
                OilWellId = measureSearchRequest.OilWellId
            };
        }

    }
}
