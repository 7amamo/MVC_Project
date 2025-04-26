using Business.Logic.Layer.interfaces;
using Business.Logic.Layer.Repositories;
using Data.Access.Layer.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.presentaton.Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<DataContext>();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(config =>
            { }).AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            //builder.Services.ConfigureApplicationCookie((config) =>
            //{
                
            //});
			builder.Services.AddAuthentication();


			//builder.Services.AddScoped<IGenearicRepository<Department>, GenericRepository<Department>>();


			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
