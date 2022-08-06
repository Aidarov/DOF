using DOF.WebService.Common;
using DOF.WebService.Database;
using DOF.WebService.Exceptions.OilField;
using DOF.WebService.Models;
using DOF.WebService.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.OilField
{
    public class OilFieldService : IOilFieldService
    {
        private MainDbContext _context { get; set; }
        private IUserService _userService { get; set; }
        public OilFieldService(MainDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<int> Add(OilFieldAddData oilFieldAddData)
        {
            var fieldExist = await _context.OilFields.AnyAsync(c => c.Code.Equals(oilFieldAddData.Code) || c.Name.Equals(oilFieldAddData.Name));

            if (!fieldExist)
            {
                var oilField = new Database.Models.OilField()
                {
                    Code = oilFieldAddData.Code,
                    Name = oilFieldAddData.Name,
                    ShortName = oilFieldAddData.ShortName
                };

                await _context.OilFields.AddAsync(oilField);

                await _context.SaveChangesAsync();

                return oilField.Id;
            }

            throw new OilFieldAllreadyExistException();
            
        }

        public async Task Delete(int id, int deletedBy, DateTime deletedAt)
        {
            var oilField = await _context.OilFields.SingleAsync(c => c.Id == id && c.IsDeleted == false);

            oilField.IsDeleted = true;
            oilField.DeletedAt = deletedAt;
            oilField.DeletedBy = deletedBy;

            await _context.SaveChangesAsync();            
        }

        public async Task<IEnumerable<OilFieldModel>> Get(OilFieldSearchData oilFieldSearchData, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<OilFieldModel> result = null;

            var set = _context.OilFields.AsQueryable();

            if (!String.IsNullOrEmpty(oilFieldSearchData.Name))
                set = set.Where(c => c.Name.StartsWith(oilFieldSearchData.Name, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(oilFieldSearchData.ShortName))
                set = set.Where(c => c.ShortName.StartsWith(oilFieldSearchData.ShortName, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(oilFieldSearchData.Code))
                set = set.Where(c => c.Code.StartsWith(oilFieldSearchData.Code, StringComparison.OrdinalIgnoreCase));

            if (oilFieldSearchData.IsDeleted != null)
                set = set.Where(c => c.IsDeleted == oilFieldSearchData.IsDeleted.Value);


            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(OilFieldModel.Name)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Name) : set.OrderByDescending(c => c.Name);

                if (sort.FieldName.Equals(nameof(OilFieldModel.ShortName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.ShortName) : set.OrderByDescending(c => c.ShortName);

                if (sort.FieldName.Equals(nameof(OilFieldModel.Code)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Code) : set.OrderByDescending(c => c.Code);

                if (sort.FieldName.Equals(nameof(OilFieldModel.CreatedAt)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedAt) : set.OrderByDescending(c => c.CreatedAt);

                if (sort.FieldName.Equals(nameof(OilFieldModel.CreatedBy)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedBy) : set.OrderByDescending(c => c.CreatedBy);

                if (sort.FieldName.Equals(nameof(OilFieldModel.Id)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Id) : set.OrderByDescending(c => c.Id);

                if (sort.FieldName.Equals(nameof(OilFieldModel.IsDeleted)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.IsDeleted) : set.OrderByDescending(c => c.IsDeleted);

                if (sort.FieldName.Equals(nameof(OilFieldModel.DeletedBy)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.DeletedBy) : set.OrderByDescending(c => c.DeletedBy);
            }

            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            if (resultSet.Any())
                result = new List<OilFieldModel>();

            foreach (var rawOilField in resultSet)
                result.Add(await Convert(rawOilField));

            return result;
        }

        public async Task<OilFieldModel> GetById(int id)
        {
            var oilField = await _context.OilFields.SingleOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            return await Convert(oilField);
        }

        public async Task Update(OilFieldUpdateData oilFieldUpdateData)
        {
            var oilFieldExist = await _context.OilFields.AnyAsync(c => c.Name.Equals(oilFieldUpdateData.Name, StringComparison.OrdinalIgnoreCase) || c.Code.Equals(oilFieldUpdateData.Code, StringComparison.OrdinalIgnoreCase));

            if (!oilFieldExist)
                throw new OilFieldAllreadyExistException();

            var oilFieldNameOrCodeIsBusy = await _context.OilFields.AnyAsync(c => c.Id != oilFieldUpdateData.Id && (c.Code.Equals(oilFieldUpdateData.Code, StringComparison.OrdinalIgnoreCase) || c.Name.Equals(oilFieldUpdateData.Name, StringComparison.OrdinalIgnoreCase)));

            if (!oilFieldNameOrCodeIsBusy)
                throw new OilFieldCodeOrNameIsBusyException();


            var currentOilField = await _context.OilFields.SingleAsync(c => c.Id == oilFieldUpdateData.Id);

            currentOilField.Name = oilFieldUpdateData.Name;
            currentOilField.Code = oilFieldUpdateData.Code;
            currentOilField.ShortName = oilFieldUpdateData.ShortName;

            await _context.SaveChangesAsync();

        }

        private async Task<OilFieldModel> Convert(Database.Models.OilField oilField)
        {
            UserModel deletedBy = null;

            if(oilField.DeletedBy != null)
                deletedBy = await _userService.GetById(oilField.DeletedBy.Value);

            return new OilFieldModel()
            {
                Id = oilField.Id,
                CreatedAt = oilField.CreatedAt,
                //CreatedBy = await _userService.GetById(oilField.CreatedBy),
                Code = oilField.Code,
                Name = oilField.Name,
                ShortName = oilField.ShortName,
                IsDeleted = oilField.IsDeleted,
                DeletedAt = oilField.DeletedAt,
                //DeletedBy = deletedBy
                CreatedBy = oilField.CreatedBy,
                //CreatedByModel = await _userService.GetById(oilField.CreatedBy),
                DeletedBy = oilField.DeletedBy,
                //DeletedByModel = deletedBy
            };
        }
    }
}
