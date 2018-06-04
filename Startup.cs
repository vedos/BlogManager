using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManager.DAL;
using BlogManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlogManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            
            services.AddMvc();

            // Added - uses IOptions<T> for your settings.
            services.AddOptions();

            // Added - Confirms that we have a home for our DemoSettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = serviceProvider.GetService<ApiContext>();
            AddTestData(context);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

        }

        private static void AddTestData(ApiContext context)
        {
            var testUser1 = new User
            {
                Id = 1,
                FirstName = "Luke",
                LastName = "Skywalker",
                Username="admin",
                Password="admin",
                Age = "22"
            };

            var testUser2 = new User
            {
                Id = 2,
                FirstName = "Leon",
                LastName = "Leon",
                Username = "user1",
                Password = "test",
                Age = "27"
            };

            var testUser3 = new User
            {
                Id = 3,
                FirstName = "Nagasaki",
                LastName = "Kojima",
                Username = "user2",
                Password = "test",
                Age = "20"
            };

            var testUser4 = new User
            {
                Id = 4,
                FirstName = "Steve",
                LastName = "Aoki",
                Username = "user3",
                Password = "test",
                Age = "26"
            };

            var testUser5 = new User
            {
                Id = 5,
                FirstName = "Wade",
                LastName = "Wilson",
                Username = "user4",
                Password = "test",
                Age = "32"
            };

            var testUser6 = new User
            {
                Id = 6,
                FirstName = "John",
                LastName = "Smith",
                Username = "user5",
                Password = "test",
                Age = "24"
            };

            context.Users.Add(testUser1);
            context.Users.Add(testUser2);
            context.Users.Add(testUser3);
            context.Users.Add(testUser4);
            context.Users.Add(testUser5);
            context.Users.Add(testUser6);

            var testBlog1 = new Blog
            {
                Id = 1111,
                UserId = testUser2.Id,
                Summary = "What do you think about that?",
                Title = "My New Blog",
                PublishingDateTime = DateTime.Now
            };

            var testBlog2 = new Blog
            {
                Id = 1112,
                UserId = testUser1.Id,
                Summary = "Yes, I started with...",
                Title = "My New Blog",
                PublishingDateTime = DateTime.Now
            };

            context.Blogs.Add(testBlog1);
            context.Blogs.Add(testBlog2);
            context.SaveChanges();
        }

        public class AppSettings
        {
            public string ItemsPerPage { get; set; }
        }
    }
}
