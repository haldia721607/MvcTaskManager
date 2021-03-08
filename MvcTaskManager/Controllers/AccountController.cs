using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcTaskManager.Identity;
using MvcTaskManager.ServiceContracts;
using MvcTaskManager.ViewModels;

namespace MvcTaskManager.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private IUsersService _usersService;
        private readonly UserManager<ApplicationUser> _applicationUserManager;
        private readonly ApplicationDbContext db;
        public AccountController(IUsersService usersService, UserManager<ApplicationUser> applicationUserManager, ApplicationDbContext db)
        {
            this._usersService = usersService;
            this._applicationUserManager = applicationUserManager;
            this.db = db;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel.Username!=null && loginViewModel.Password !=null)
            {
                var user = await _usersService.Authenticate(loginViewModel);
                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                else
                {
                    return Ok(user);
                }
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SignUpViewModel signUpViewModel)
        {
            var user = await _usersService.Register(signUpViewModel);
            if (user == null)
                return BadRequest(new { message = "Invalid Data" });
            return Ok(user);
        }

        [Route("api/getUserByEmail/{Email}")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            var user = await _usersService.GetUserByEmail(Email);
            return Ok(user);
        }

        [Route("api/getallemployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            List<ApplicationUser> users = this.db.Users.ToList();
            List<ApplicationUser> employeeUsers = new List<ApplicationUser>();

            foreach (var item in users)
            {
                if ((await this._applicationUserManager.IsInRoleAsync(item, "Employee")))
                {
                    employeeUsers.Add(item);
                }
            }
            return Ok(employeeUsers);
        }
    }
}
