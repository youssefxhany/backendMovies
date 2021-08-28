using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserLogin.Models;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class userController : ControllerBase
    {
        private List<user> users = new List<user> {
            new user {userId = 1, userName = "youssef", password = "joeHany1998" ,email = "youssef.hossam1998@gmail.com"},
            new user {userId = 2, userName = "yassmine", password = "yassmineHany1998" ,email = "yassmine.hossam1998@gmail.com"}
        };

        // GET: api/<userController>
        [HttpGet]
        public ActionResult<IEnumerable<user>> Get()
        {
            //Console.WriteLine("entered");
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<user> Get(int id)
        {
            user my_user = users.FirstOrDefault(c => c.userId == id);
            if(my_user == null)
            {
                return NotFound("user not available");
            }
            return Ok(my_user);
        }

        [HttpPost]
        public ActionResult<IEnumerable<user>> Post(user my_user)
        {
            users.Add(my_user);
            return users;
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<user>> Put(int id,user updated_user)
        {
            user my_user = users.FirstOrDefault(c => c.userId == id);

            if(my_user == null)
            {
                return NotFound("no user with such id");
            }

            if(updated_user.userName != null)
                my_user.userName = updated_user.userName;
            if (updated_user.userName != null)
                my_user.email = updated_user.email;

            return users;
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<user>> Delete(int id)
        {
            user my_user = users.FirstOrDefault(c => c.userId == id);
            if(my_user == null)
            {
                return NotFound("no user with specified id");
            }
            users.Remove(my_user);
            return users;
        }
    }
}

