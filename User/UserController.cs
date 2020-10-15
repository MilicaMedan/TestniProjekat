using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Backend.User
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IUserService _userService;
        public UserController(AppDbContext context, IUserService userService )
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var status = await _context.Users.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }



        private bool validTelephoneNo(string telNo)
        {
            return Regex.Match(telNo, @"^\+\d{3}[ .-]\d{2}[ .-]\d{3}[ .-]\d{3}$").Success;
        }

        private bool validMail(string mail)
        {
            return Regex.Match(mail, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Success;
        }

        private string hashPassword(string password)
        {
            byte[] salt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }





        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.passwordHash = hashPassword(user.passwordHash);

            if (validTelephoneNo(user.telephone) && validMail(user.mail)) {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            model.passwordHash = hashPassword(model.passwordHash);
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


    }
}
