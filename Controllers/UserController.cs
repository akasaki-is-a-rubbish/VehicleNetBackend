using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleNetBackend
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : MyControllerBase
    {

        public class UserRegisterVM
        {
            public string username { get; set; }
            public string passwd { get; set; }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterVM userreg)
        {
            if (string.IsNullOrEmpty(userreg.username))
                return GetErrorResult("bad_arg");
            if (_context.Users.Any(u => u.Username == userreg.username))
                return GetErrorResult("dup_user");
            var user = new User
            {
                Username = userreg.username,
                Password = Utils.HashPassword(userreg.passwd),
            };
            _context.Users.Add(user);
            var loginRecord = _context.UserService.CreateLoginRecord_NoSave(user);
            await _context.SaveChangesAsync();
            return GetLoginResult(user, loginRecord);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            var user = await GetLoginUser();
            if (user == null) return GetErrorResult("fail");
            var record = await _context.UserService.CreateLoginRecord(user);
            return GetLoginResult(user, record);
        }

        [HttpPost("{id}/devices")]
        public async Task<IActionResult> GetDevices(int id)
        {
            var login = await GetLoginUser();
            if (login == null) return GetErrorResult("no_login");
            var user = await _context.Users
                .Include(u => u.Vehicles)
                .SingleOrDefaultAsync(u => u.Id == id);
            if (login.Id != user?.Id) return GetErrorResult("no_permission");

            return new JsonResult(user.Vehicles.Select(x => new {
                id = x.Id,
                name = x.Name,
            }));
        }

        private static IActionResult GetLoginResult(User user, LoginRecord loginRecord)
        {
            return new JsonResult(new {
                id = user.Id,
                username = user.Username,
                token = loginRecord.token
            });
        }
    }
}
