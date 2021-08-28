using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserLogin.Data;
using UserLogin.Models;

namespace UserLogin.Controllers
{
    [Route("MoviesUsersApi/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UsersController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> GetUsers()
        {
            return await _context.users.Where(c => c.isDeleted == false).ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<user>> GetUser(int id)
        {
            var my_user = await _context.users.FindAsync(id);

            if (my_user == null)
            {
                return NotFound("no user with specified id");
            }

            return my_user;
        }

        [Route("authenticate")]
        [HttpGet()]
        public async Task<ActionResult<user>> authenticateUser(user my_user)
        {
            String password_encrypted = EnryptString(my_user.password);
            var userName_check = await _context.users.Where(c => (c.userName == my_user.userName && c.password == password_encrypted)).ToListAsync();

            if (IsEmpty(userName_check))
            {
                return NotFound("wrong username or password");
            }

            var my_verified_user = await _context.users.FindAsync(userName_check[0].userId);
            return my_verified_user;
        }

        [HttpPost]
        public async Task<ActionResult<user>> addUser(user my_new_user)
        {
            var username_check = await _context.users.Where(c => c.userName == my_new_user.userName).ToListAsync();
            var email_check = await _context.users.Where(c => c.email == my_new_user.email).ToListAsync();

            Console.WriteLine(username_check);
            Console.WriteLine(email_check);

            if (IsEmpty(username_check) && IsEmpty(email_check) && my_new_user.password != null)
            {
                String encrypted_password = EnryptString(my_new_user.password);
                my_new_user.password = encrypted_password;
                _context.users.Add(my_new_user);
                await _context.SaveChangesAsync();
                return Ok(_context.users);
            }
            return NotFound("please make sure to use unique username and email and do not leave password field empty");
        }

        public static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }

        public string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}