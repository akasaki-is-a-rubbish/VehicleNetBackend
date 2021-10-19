using Microsoft.AspNetCore.Mvc;
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
            return new JsonResult(new {
                id = user.Id,
                username = user.Username,
                token = loginRecord.token
            });
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return null;
        }

        [HttpPost("{id}/devices")]
        public IActionResult GetDevices(int id)
        {
            return null;
        }
    }
}
