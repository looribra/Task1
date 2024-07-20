using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Task1;

namespace Task1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private static List<User> users = new List<User> {

            new User{
                FirstName= "Loor", 
                LastName= "Azem",
                DateOfBirth=new DateTime(2002, 8 ,16),
                Email="looribrahim2@gmail.com", 
                id= 1 },

            new User{ 
                FirstName="Bissan",
                LastName= "Ibrahim", 
                DateOfBirth=new DateTime(1998, 7 ,24),
                Email= "bissan2@gmail.com", 
                id=2
            },
            new User{
                FirstName="Ibrahim", 
                LastName= "Ahmad", 
                DateOfBirth= new DateTime(1999, 7 ,24), 
                Email="ibrahim@gmail.com", 
                id=3 }
        };


        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        // GET All Data
        [HttpGet("GetAllData")]
        public IEnumerable<User> GetAllData()
        {
            return users;
        }

        // GET Data By Id
        [HttpGet("GetById")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            return Ok(user);
        }

        //Post 
        [HttpPost("Add")]
        public ActionResult<User> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Bad request");
            }
            user.id = users.Max(u => u.id) + 1;
            users.Add(user);
            return Ok("User Added Successfully");
        }

        //Put
        [HttpPut("UpdateUser")]
        public IActionResult PutUser(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound("User Not Found");
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.DateOfBirth = updatedUser.DateOfBirth;
            user.Email = updatedUser.Email;

            return Ok("User Updated Successfully");
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(d => d.id == id);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            users.Remove(user);
            return Ok("User deleted Successfully");
        }


    }
}
