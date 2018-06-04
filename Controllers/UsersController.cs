using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManager.DAL;
using BlogManager.Models;
using BlogManager.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static BlogManager.Startup;

namespace BlogManager.Controllers
{
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly ApiContext _context;
        private AppSettings ConfigSettings { get; set; }


        public UsersController(ApiContext context, IOptions<AppSettings> settings)
        {
            _context = context;
            ConfigSettings = settings.Value;
        }

        [HttpGet("[action]")]
        public List<UserVM> GetUsersList()
        {
            var result = _context
                .Users
                .Include(x => x.Blogs)
                .Select(x => new UserVM {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName,
                    Age = x.Age,
                    Email = x.FirstName.ToLower() + "_" + x.LastName.ToLower() + "@mail.com",
                    NumberOfBlogs = x.Blogs.Count().ToString()
                })
                .OrderBy(x => x.Name)
                .ToList();
            return result;
        }

        [HttpGet("[action]")]
        public List<UserVM> SearchUsers(string query)
        {
            var result = _context
                .Users
                .Include(x => x.Blogs)
                .Select(x => new UserVM
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName,
                    Age = x.Age,
                    Email = x.FirstName.ToLower() + "_" + x.LastName.ToLower() + "@mail.com",
                    NumberOfBlogs = x.Blogs.Count().ToString()
                })
                .Where(x => (x.Name.Contains(query) || x.Email.Contains(query)) || query == null)
                .OrderBy(x => x.Name)
                .ToList();
            return result;
        }

        [HttpGet("[action]")]
        public UserDetailsVM GetUserDetails(int id)
        {
            string format = "hh:mm, dd MMM yyyy";
            var result = _context
                .Users
                .Include(x => x.Blogs)
                .Where(x=>x.Id==id)
                .Select(x => new UserDetailsVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    Email = x.FirstName.ToLower() + "_" + x.LastName.ToLower() + "@mail.com",
                    Blogs = x.Blogs.Select(y => new BlogVM {
                        Id = y.Id,
                        Title = y.Title,
                        PublishingDateTime = y.PublishingDateTime.ToString(format),
                        Summary = y.Summary
                    })
                    .OrderBy(y=>y.PublishingDateTime)
                    .ToList()
                })
                .SingleOrDefault();
            return result;
        }
    }
}