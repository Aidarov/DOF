using DOF.WebService.Common;
using DOF.WebService.Database;
using DOF.WebService.Exceptions.OilWell;
using DOF.WebService.Models;
using DOF.WebService.Services.OilField;
using DOF.WebService.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.OilWell
{
    public class OilWellService : IOilWellService
    {
        private MainDbContext _context;
        private IUserService _userService;
        private IOilFieldService _oilFieldService;
        public OilWellService(MainDbContext context, IUserService userService, IOilFieldService oilFieldService)
        {
            _context = context;
            _userService = userService;
            _oilFieldService = oilFieldService;
        }
        public async Task<int> Add(OilWellAddData oilWellAddData)
        {
            var fieldExist = await _context.OilWells.AnyAsync(c => c.Code.Equals(oilWellAddData.Code) || c.Name.Equals(oilWellAddData.Name));

            if (!fieldExist)
            {
                var oilWell = new Database.Models.OilWell()
                {
                    Code = oilWellAddData.Code,
                    Name = oilWellAddData.Name,
                    ShortName = oilWellAddData.ShortName,
                    CreatedAt = oilWellAddData.CreatedAt,
                    CreatedBy = oilWellAddData.CreatedBy,
                    Latitude = oilWellAddData.Latitude,
                    Longitude = oilWellAddData.Longitude,
                    OilFieldId = oilWellAddData.OilField,
                    IsDeleted = oilWellAddData.IsDeleted
                };

                await _context.OilWells.AddAsync(oilWell);

                await _context.SaveChangesAsync();

                return oilWell.Id;
            }

            throw new OilWellAllreadyExistException();
        }

        public async Task Delete(int id, int deletedBy, DateTime deletedAt)
        {
            var oilWell = await _context.OilWells.SingleAsync(c => c.Id == id && !c.IsDeleted);

            oilWell.IsDeleted = true;
            oilWell.DeletedAt = deletedAt;
            oilWell.DeletedBy = deletedBy;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OilWellModel>> Get(OilWellSearchData oilWellSearchData, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<OilWellModel> result = null;

            var set = _context.OilWells.AsQueryable();

            if (!String.IsNullOrEmpty(oilWellSearchData.Name))
                set = set.Where(c => c.Name.StartsWith(oilWellSearchData.Name, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(oilWellSearchData.ShortName))
                set = set.Where(c => c.ShortName.StartsWith(oilWellSearchData.ShortName, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(oilWellSearchData.Code))
                set = set.Where(c => c.Code.StartsWith(oilWellSearchData.Code, StringComparison.OrdinalIgnoreCase));

            if (oilWellSearchData.OilFieldId != null)
                set = set.Where(c => c.OilFieldId == oilWellSearchData.OilFieldId);

            if (oilWellSearchData.IsDeleted != null)
                set = set.Where(c => c.IsDeleted == oilWellSearchData.IsDeleted.Value);


            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(OilWellModel.Name)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Name) : set.OrderByDescending(c => c.Name);

                if (sort.FieldName.Equals(nameof(OilWellModel.ShortName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.ShortName) : set.OrderByDescending(c => c.ShortName);

                if (sort.FieldName.Equals(nameof(OilWellModel.Code)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Code) : set.OrderByDescending(c => c.Code);

                if (sort.FieldName.Equals(nameof(OilWellModel.CreatedAt)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedAt) : set.OrderByDescending(c => c.CreatedAt);

                if (sort.FieldName.Equals(nameof(OilWellModel.CreatedBy)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedBy) : set.OrderByDescending(c => c.CreatedBy);

                if (sort.FieldName.Equals(nameof(OilWellModel.Id)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Id) : set.OrderByDescending(c => c.Id);

                if (sort.FieldName.Equals(nameof(OilWellModel.IsDeleted)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.IsDeleted) : set.OrderByDescending(c => c.IsDeleted);

                if (sort.FieldName.Equals(nameof(OilWellModel.DeletedBy)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.DeletedBy) : set.OrderByDescending(c => c.DeletedBy);

                if (sort.FieldName.Equals(nameof(OilWellModel.OilField)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.OilFieldId) : set.OrderByDescending(c => c.OilFieldId);
            }

            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            if (resultSet.Any())
                result = new List<OilWellModel>();

            foreach (var rawOilWell in resultSet)
                result.Add(await Convert(rawOilWell));

            return result;
        }

        private async Task<OilWellModel> Convert(Database.Models.OilWell rawOilWell)
        {
            UserModel deletedBy = null;

            if (rawOilWell.DeletedBy != null)
                deletedBy = await _userService.GetById(rawOilWell.DeletedBy.Value);

            return new OilWellModel()
            {
                Id = rawOilWell.Id,
                CreatedAt = rawOilWell.CreatedAt,
                CreatedBy = rawOilWell.CreatedBy,
                //CreatedByModel = await _userService.GetById(rawOilWell.CreatedBy),
                Code = rawOilWell.Code,
                Name = rawOilWell.Name,
                ShortName = rawOilWell.ShortName,
                IsDeleted = rawOilWell.IsDeleted,
                DeletedAt = rawOilWell.DeletedAt,
                DeletedBy = rawOilWell.DeletedBy,
                //DeletedByModel = deletedBy,
                Latitude = rawOilWell.Latitude,
                Longitude = rawOilWell.Longitude,
                OilField = await _oilFieldService.GetById(rawOilWell.OilFieldId)
            };
        }

        public async Task<OilWellModel> GetById(int id)
        {
            var oilWell = await _context.OilWells.SingleOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            return await Convert(oilWell);
        }

        public async Task Update(OilWellUpdateData oilWellUpdate)
        {
            var oilWellExist = await _context.OilWells.AnyAsync(c => c.Name.Equals(oilWellUpdate.Name, StringComparison.OrdinalIgnoreCase) || c.Code.Equals(oilWellUpdate.Code, StringComparison.OrdinalIgnoreCase));

            if (!oilWellExist)
                throw new OilWellAllreadyExistException();

            var oilWellNameOrCodeIsBusy = await _context.OilWells.AnyAsync(c => c.Id != oilWellUpdate.Id && (c.Code.Equals(oilWellUpdate.Code, StringComparison.OrdinalIgnoreCase) || c.Name.Equals(oilWellUpdate.Name, StringComparison.OrdinalIgnoreCase)));

            if (!oilWellNameOrCodeIsBusy)
                throw new OilWellCodeOrNameBusyException();


            var currentOilWell = await _context.OilWells.SingleAsync(c => c.Id == oilWellUpdate.Id);

            currentOilWell.Name = oilWellUpdate.Name;
            currentOilWell.Code = oilWellUpdate.Code;
            currentOilWell.ShortName = oilWellUpdate.ShortName;
            currentOilWell.OilFieldId = oilWellUpdate.OilField;
            currentOilWell.Latitude = oilWellUpdate.Latitude;
            currentOilWell.Longitude = oilWellUpdate.Longitude;

            await _context.SaveChangesAsync();
        }
    }
}
