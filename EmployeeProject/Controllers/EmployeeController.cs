using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Employee")]
    public class EmployeeController : ControllerBase
    {
    }
}
