namespace DOF.WebService.Controllers.Request.Device
{
    public class DeviceSearchRequest : RequestWithPaginationAndSorting
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Token { get; set; }
        public int? CreatedBy { get; set; }
    }
}
