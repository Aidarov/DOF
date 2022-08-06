

using DOF.WebService.Common;
using DOF.WebService.Database;
using DOF.WebService.Exceptions.DevicePath;
using DOF.WebService.Models;
using DOF.WebService.Services.Mappings.DevicePath;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Device.DevicePath
{
    public class DevicePathService : IDevicePathService
    {
        private MainDbContext _context;
        public DevicePathService(MainDbContext context)
        {
            _context = context;
        }

        private async Task<Database.Models.DevicePath> AddNewPath(DevicePathAddData devicePathAddData)
        {
            var pathAdded = await _context.DevicePaths.AnyAsync(c => c.Uid == devicePathAddData.Uid);

            if (pathAdded)
                throw new DevicePathAllreadyExistException();

            var path = devicePathAddData.MapToDbModel();

            await _context.DevicePaths.AddAsync(path);

            return path;
        }


        public async Task<int> Add(DevicePathAddData devicePathAddData)
        {
            var path = await AddNewPath(devicePathAddData);

            await _context.SaveChangesAsync();

            return path.Id;
        }

        public async Task AddRange(IEnumerable<DevicePathAddData> devicePathAddData)
        {
            foreach (var data in devicePathAddData)
                await AddNewPath(data);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DevicePathModel>> Get(DevicePathSearchData filter, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<DevicePathModel> result = null;

            var set = _context.DevicePaths.AsQueryable();

            if (filter.DeviceId != null)
                set = set.Where(c => c.DeviceId == filter.DeviceId);

            if (filter.AcceptedAt != null)
            {
                if (filter.AcceptedAt.From != null)
                    set = set.Where(c => c.AcceptedAt >= filter.AcceptedAt.From.Value);

                if (filter.AcceptedAt.To != null)
                    set = set.Where(c => c.AcceptedAt <= filter.AcceptedAt.To.Value);
            }

            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(DevicePathModel.DeviceId)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.DeviceId) : set.OrderByDescending(c => c.DeviceId);

                if (sort.FieldName.Equals(nameof(DevicePathModel.AcceptedAt)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.AcceptedAt) : set.OrderByDescending(c => c.AcceptedAt);
            }

            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            if (resultSet.Any())
                result = new List<DevicePathModel>();

            foreach (var rawPath in resultSet)
                result.Add(await Convert(rawPath));

            return result;
        }

        private async Task<DevicePathModel> Convert(Database.Models.DevicePath rawPath)
        {
            var device = await _context.Devices.SingleAsync(c => c.Id == rawPath.DeviceId);

            return new DevicePathModel
            {
                Id = rawPath.Id,
                AcceptedAt = rawPath.AcceptedAt,
                DeviceId = rawPath.DeviceId,
                DeviceName = device.Name,
                Latitude = rawPath.Latitude,
                Longitude = rawPath.Longitude,
                TimeStamp = rawPath.TimeStamp,
                Uid = rawPath.Uid
            };
        }
    }
}
