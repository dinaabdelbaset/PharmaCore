using Microsoft.AspNetCore.Mvc;
using PharmaCore.API.Services;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/branch-employee")] // employee login within branch session
    public class BranchEmployeeController : ControllerBase
    {
        private readonly IEmployeeLoginService _employeeLogin;

        public BranchEmployeeController(IEmployeeLoginService employeeLogin)
        {
            _employeeLogin = employeeLogin;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(EmployeeBranchLoginRequest req)
        {
            var (ok, error, token, permissions, branchIds, employeeId, employeeCode, fullName, branchId, branchName, workstationSessionId, loginTime, message) =
                await _employeeLogin.LoginAsync(req.EmployeeCode, req.Pin, req.BranchId, req.WorkstationSessionId);
            if (!ok) return BadRequest(new { error });
            return Ok(new { token, permissions, branchIds, employeeId, employeeCode, fullName, branchId, branchName, workstationSessionId, loginTime, message });
        }
    }

    public record EmployeeBranchLoginRequest(string EmployeeCode, string Pin, Guid BranchId, Guid WorkstationSessionId);
}