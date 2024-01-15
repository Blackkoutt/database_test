using Biblioteka.Context;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IAuthorRepository, DbAuthorRepository>();
builder.Services.AddScoped<IBookRepository, DbBookRepository>();
builder.Services.AddScoped<IBookTypeRepository, DbBookTypeRepository>();
builder.Services.AddScoped<IGenreRepository, DbGenreRepository>();
builder.Services.AddScoped<IPublisherRepository, DbPublisherRepository>();
builder.Services.AddScoped<IRoomRepository, DbRoomRepository>();
builder.Services.AddScoped<IRoomReservationRepository, DbRoomReservationRepository>();
builder.Services.AddScoped<ITagRepository, DbTagRepository>();
builder.Services.AddScoped<IEmployeeDataRepository, EmployeeDataRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IReader_BorrowingsRepository, Reader_BorrowingsRepository>();
builder.Services.AddScoped<IBookOpinionRepository, BookOpinionsRepository>();


/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "mydatabase.db");
    
}*/

builder.Services.AddDbContext<BibContext>(options =>
{
    var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "mydatabase.db");
    options.UseSqlite($"Data Source={dbPath}");
    //options.UseOracle(builder.Configuration.GetConnectionString("Database"), b=>b.UseOracleSQLCompatibility("11"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnectionString"));
});

// Configuring ASP Identity
builder.Services.AddDefaultIdentity<BibUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;

})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<BibContext>()
.AddDefaultTokenProviders()
.AddErrorDescriber<BibErrorDescriber>();

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



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
app.MapDefaultControllerRoute();

app.UseRouting();

app.UseAuthorization();

/*app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");*/
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] { "Admin", "Employee", "Reader" };

	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
			await roleManager.CreateAsync(new IdentityRole(role));
	}
}

using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BibUser>>();

	string email = "admin@admin.com";
	string password = "admin123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new BibUser();

		user.name = "Admin";
		user.surname = "Admin";
		user.UserName = email;
		user.Email = email;
		user.EmailConfirmed = true;

		await userManager.CreateAsync(user, password);

		await userManager.AddToRoleAsync(user, "Admin");
	}
	//email = "employee@employee.com";
	//password = "employee123";
	//if (await userManager.FindByEmailAsync(email) == null)
	//{
	//	var user = new BibUser();

	//	user.name = "Employee";
	//	user.surname = "Employee";
	//	user.UserName = email;
	//	user.Email = email;
	//	user.EmailConfirmed = true;

	//	await userManager.CreateAsync(user, password);

	//	await userManager.AddToRoleAsync(user, "Employee");
	//}
}

app.Run();
