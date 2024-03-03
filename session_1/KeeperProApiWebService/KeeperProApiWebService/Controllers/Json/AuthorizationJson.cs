using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.Json
{
    public class AuthorizationJson
    {
        [FromHeader]
        public string Authorization { get; set; } = string.Empty;
    }
}
