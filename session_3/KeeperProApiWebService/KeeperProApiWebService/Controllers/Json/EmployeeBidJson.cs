namespace KeeperProApiWebService.Controllers.Json
{
    public class EmployeeBidJson
    {
        public TimeSpan VisitTime { get; set; } = TimeSpan.MinValue;
        public string Type { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}
