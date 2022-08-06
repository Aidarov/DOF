using DOF.WebService.Common;
using DOF.WebService.Database;
using DOF.WebService.Models;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.OilWell;
using DOF.WebService.Services.TabletUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Measure
{
    public class MeasureService : IMeasureService
    {
        private MainDbContext _context;
        private ITabletUserService _tabletUserService;
        private IDeviceService _deviceService;
        private IOilWellService _oilWellService;

        public MeasureService(MainDbContext context, 
            ITabletUserService tabletUserService,
            IDeviceService deviceService,
             IOilWellService oilWellService)
        {
            _context = context;
            _tabletUserService = tabletUserService;
            _deviceService = deviceService;
            _oilWellService = oilWellService;
        }

        public async Task<IEnumerable<MeasureModel>> Get(MeasureSearchData filter, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<MeasureModel> result = null;

            var set = _context.Measures.AsQueryable();

            if (filter.AcceptedAt != null && filter.AcceptedAt.From != null)
                set = set.Where(c => c.AcceptedAt >= filter.AcceptedAt.From.Value);

            if (filter.AcceptedAt != null && filter.AcceptedAt.To != null)
                set = set.Where(c => c.AcceptedAt <= filter.AcceptedAt.To.Value);

            if (filter.CreatedAt != null && filter.CreatedAt.From != null)
                set = set.Where(c => c.CreatedAt >= filter.CreatedAt.From.Value);

            if (filter.CreatedAt != null && filter.CreatedAt.To != null)
                set = set.Where(c => c.CreatedAt <= filter.CreatedAt.To.Value);

            if (filter.DeviceId != null)
                set = set.Where(c => c.DeviceId == filter.DeviceId.Value);

            if (filter.OilWellId != null)
                set = set.Where(c => c.OilWellId == filter.OilWellId.Value);

            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(MeasureModel.AcceptedAt)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.AcceptedAt) : set.OrderByDescending(c => c.AcceptedAt);

                if (sort.FieldName.Equals(nameof(MeasureModel.CreatedAt)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedAt) : set.OrderByDescending(c => c.CreatedAt);

                if (sort.FieldName.Equals(nameof(MeasureModel.CreatedBy)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedBy) : set.OrderByDescending(c => c.CreatedBy);
            }

            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            if (resultSet.Any())
                result = new List<MeasureModel>();

            foreach (var rawOilField in resultSet)
                result.Add(await Convert(rawOilField));

            return result;
        }

        private async Task<MeasureModel> Convert(Database.Models.Measure rawMeasure)
        {
            return new MeasureModel()
            {
                Id = rawMeasure.Id,
                AcceptedAt = rawMeasure.AcceptedAt,
                CreatedAt = rawMeasure.CreatedAt,
                DavNaMkp = rawMeasure.DavNaMkp,
                DavPosShtut = rawMeasure.DavPosShtut,
                Latitude = rawMeasure.Latitude,
                Longitude = rawMeasure.Longitude,
                TempPosShtut = rawMeasure.TempPosShtut,
                TrubDav = rawMeasure.TrubDav,
                TrubTemp = rawMeasure.TrubTemp,
                ZatrubDav = rawMeasure.ZatrubDav,
                ZatrubTemp = rawMeasure.ZatrubTemp,
                CreatedBy = await _tabletUserService.GetById(rawMeasure.CreatedBy),
                Device = await _deviceService.GetById(rawMeasure.DeviceId),
                OilWell = await _oilWellService.GetById(rawMeasure.OilWellId)
            };
        }

        public async Task AddRange( IEnumerable<MeasureAddData> measureAddCollections)
        {
            foreach (var measureAddData in measureAddCollections)
            {
                await _context.Measures.AddAsync(new Database.Models.Measure()
                {                    
                    DavNaMkp = measureAddData.DavNaMkp,
                    DavPosShtut = measureAddData.DavPosShtut,
                    TempPosShtut = measureAddData.TempPosShtut,
                    TrubDav = measureAddData.TrubDav,
                    TrubTemp = measureAddData.TrubTemp,
                    ZatrubDav = measureAddData.ZatrubDav,
                    ZatrubTemp = measureAddData.ZatrubTemp,
                    Latitude = measureAddData.Latitude,
                    Longitude = measureAddData.Longitude,
                    OilWellId = measureAddData.OilWellId,
                    DeviceId = measureAddData.DeviceId,
                    AcceptedAt = measureAddData.AcceptedAt,
                    CreatedAt = measureAddData.CreatedAt,
                    CreatedBy = measureAddData.CreatedBy
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
