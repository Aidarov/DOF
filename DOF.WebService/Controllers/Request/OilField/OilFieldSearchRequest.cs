namespace DOF.WebService.Controllers.Request.OilField
{
    public class OilFieldSearchRequest : RequestWithPaginationAndSorting
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
    }
}
