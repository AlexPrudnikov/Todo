using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoCSharp.ErrorDao;
using TodoCSharp.Models;
using TodoCSharp.RoleDao;
using TodoCSharp.RolePresentationService;
using TodoCSharp.TodoDao;
using TodoCSharp.TodoErrorPresentationService;
using TodoCSharp.TodoPresentationService;
using TodoCSharp.TodoStyleDao;
using TodoCSharp.UserAccountDao;
using TodoCSharp.UserAccountPresentationService;
using TodoCSharp.UserRoleDao;
using TodoCSharp.UserRolePresentationService;

namespace TodoCSharp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration confugaration)
        {
            Configuration = confugaration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Получаем строку подключения из файла конфигурации
            String connection = Configuration.GetConnectionString("DefaultConnection");

            // Добавляем контекст Mobilecontext в качестве сервиса в приложение
            // Добавление контекста данных в виде сервиса позволит затем получать его в конструкторе контроллера через механизм внедрения зависимостей.
            services.AddDbContextPool<TodoContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<TodoContext>();

            // DependencyInjection
            services.AddTransient<ITodoDao, TodoDao.TodoDao>();
            services.AddTransient<ITodoPresentationService, TodoPresentationService.TodoPresentationService>();
            services.AddTransient<ITodoErrorDao, TodoErrorDao.TodoErrorDao>();
            services.AddTransient<ITodoErrorPresentationService, TodoErrorPresentationService.TodoErrorPresentationService>();
            services.AddTransient<IRoleDao, RoleDao.RoleDao>();
            services.AddTransient<IRolePresentationService, RolePresentationService.RolePresentationService>();
            services.AddTransient<IUserAccountDao, UserAccountDao.UserAccountDao>();
            services.AddTransient<IUserAccountPresentationService, UserAccountPresentationService.UserAccountPresentationService>();
            services.AddTransient<IUserRoleDao, UserRoleDao.UserRoleDao>();
            services.AddTransient<IUserRolePresentationService, UserRolePresentationService.UserRolePresentationService>();
            services.AddTransient<ITodoStyleDao, TodoStyleDao.TodoStyleDao>();
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();          
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Create}/{id?}"
                    );
            });     
        }
    }
}
