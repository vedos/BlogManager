using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManager.DAL;
using BlogManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Controllers
{
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly ApiContext _context;

        public LoginController(ApiContext context)
        {
            _context = context;
        }
        
        [HttpGet("[action]")]
        public User Login(string username, string password)
        {

            User user = _context
                .Users
                .Where(x=>x.Username == username && x.Password == password)
                .SingleOrDefault();

            return user;
        }

    }
}