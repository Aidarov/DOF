using DOF.WebService.Common;
using DOF.WebService.Database;
using DOF.WebService.Exceptions.Device;
using DOF.WebService.Models;
using DOF.WebService.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Device
{
    public class DeviceService : IDeviceService
    {
        private readonly MainDbContext _context;
        private readonly IUserService _userService;

        public DeviceService(MainDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;

        }

        public async Task Activate(Guid token, string clientAppVersion)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(c => c.Token == token);

            if (String.IsNullOrEmpty(device.ClientAppVersion))
                throw new Exception("");

            if (device.Activated != null || device.ActivatedAt != null)
                throw new Exception("");

            if (device.DeletedAt != null || device.DeletedBy != null || device.IsDeleted == false)
                throw new Exception("");

            device.ClientAppVersion = clientAppVersion;
            device.Activated = true;
            device.ActivatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<int> Add(DeviceAddData deviceAddData)
        {
            var deviceAllreadyExist = await _context.Devices.AnyAsync(c => c.Name.Equals(deviceAddData.Name) || c.SerialNumber.Equals(deviceAddData.SerialNumber));

            if (deviceAllreadyExist)  throw new DeviceAllreadyExistException();

            var newDevice = new Database.Models.Device()
            {
                Name = deviceAddData.Name,
                Model = deviceAddData.Model,
                SerialNumber = deviceAddData.SerialNumber,
                Token = deviceAddData.Token,
                CreatedAt = deviceAddData.CreatedAt,
                CreatedBy = deviceAddData.CreatedBy,
                IsDeleted = deviceAddData.IsDeleted,
                PhotoPath = deviceAddData.PhotoPath
            };

            await _context.Devices.AddAsync(newDevice);

            await _context.SaveChangesAsync();

            return newDevice.Id;
        }

        public async Task Delete(int id, int deletedBy, DateTime deletedAt)
        {
            var device = await _context.Devices.SingleAsync(c => c.Id == id && c.IsDeleted == false);

            device.IsDeleted = true;
            device.DeletedBy = deletedBy;
            device.DeletedAt = deletedAt;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DeviceModel>> Get(DeviceSearchData deviceSearchData, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<DeviceModel> result = null;

            var set = _context.Devices.AsQueryable();

            if (!String.IsNullOrEmpty(deviceSearchData.Name))
                set = set.Where(c => c.Name.StartsWith(deviceSearchData.Name, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(deviceSearchData.SerialNumber))
                set = set.Where(c => c.SerialNumber.StartsWith(deviceSearchData.SerialNumber, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(deviceSearchData.Model))
                set = set.Where(c => c.Model.StartsWith(deviceSearchData.Model, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(deviceSearchData.Token))
                set = set.Where(c => c.Model.StartsWith(deviceSearchData.Token, StringComparison.OrdinalIgnoreCase));

            if (deviceSearchData.CreatedBy != null)
                set = set.Where(c => c.CreatedBy == deviceSearchData.CreatedBy.Value);

            
            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(DeviceModel.Name)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Name) : set.OrderByDescending(c => c.Name);

                if (sort.FieldName.Equals(nameof(DeviceModel.SerialNumber)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.SerialNumber) : set.OrderByDescending(c => c.SerialNumber);

                if (sort.FieldName.Equals(nameof(DeviceModel.Model)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Model) : set.OrderByDescending(c => c.Model);

                if (sort.FieldName.Equals(nameof(DeviceModel.PhotoPath)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.PhotoPath) : set.OrderByDescending(c => c.PhotoPath);

                if (sort.FieldName.Equals(nameof(DeviceModel.CreatedBy)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedBy) : set.OrderByDescending(c => c.CreatedBy);

                if (sort.FieldName.Equals(nameof(DeviceModel.Id)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Id) : set.OrderByDescending(c => c.Id);

                if (sort.FieldName.Equals(nameof(DeviceModel.IsDeleted)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.IsDeleted) : set.OrderByDescending(c => c.IsDeleted);

                if (sort.FieldName.Equals(nameof(DeviceModel.DeletedBy)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.DeletedBy) : set.OrderByDescending(c => c.DeletedBy);

            }

            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            if (resultSet.Any())
                result = new List<DeviceModel>();

            foreach (var rawDevice in resultSet)
                result.Add(await Convert(rawDevice));

            return result;
        }

        public async Task<DeviceModel> GetById(int id)
        {
            var deviceRaw = await _context.Devices.SingleAsync(c => c.Id == id && c.IsDeleted == false);

            return await Convert(deviceRaw);
        }

        public async Task<DeviceModel> GetByToken(Guid token)
        {
            var deviceRaw = await _context.Devices.SingleAsync(c => c.Token == token && c.IsDeleted == false);

            return await Convert(deviceRaw);
        }

        public async Task Update(DeviceUpdateData deviceUpdateData)
        {
            var deviceAllreadyExist = await _context.Devices.AnyAsync(c => c.Id != deviceUpdateData.Id && (c.Name.Equals(deviceUpdateData.Name, StringComparison.OrdinalIgnoreCase ) || c.SerialNumber.Equals(deviceUpdateData.SerialNumber, StringComparison.OrdinalIgnoreCase)));
            
            if (deviceAllreadyExist)
                throw new DeviceNameOrSerialNumberBusyException();

            var device = await _context.Devices.SingleAsync(c => c.Id == deviceUpdateData.Id && c.IsDeleted == false);

            device.Name = deviceUpdateData.Name;
            device.Model = deviceUpdateData.Model;
            device.SerialNumber = deviceUpdateData.SerialNumber;            
            device.PhotoPath = deviceUpdateData.PhotoPath;
            device.Token = deviceUpdateData.Token;

            await _context.SaveChangesAsync();

        }

        private async Task<DeviceModel> Convert(Database.Models.Device rawDevice)
        {
            UserModel deletedBy = null;

            if(rawDevice.DeletedBy != null)
                deletedBy = await _userService.GetById(rawDevice.DeletedBy.Value);

            return new DeviceModel()
            {
                Id = rawDevice.Id,
                Name = rawDevice.Name,
                Model = rawDevice.Model,
                SerialNumber = rawDevice.SerialNumber,
                Token = rawDevice.Token,
                CreatedAt = rawDevice.CreatedAt,
                //CreatedByModel = await _userService.GetById(rawDevice.CreatedBy),
                DeletedAt = rawDevice.DeletedAt,
                //DeletedByModel = deletedBy,
                IsDeleted = rawDevice.IsDeleted,
                PhotoPath = rawDevice.PhotoPath
            };
        }
    }
}
