using KafeRest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace KafeRest.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Menu> Yemekler { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Galery> Galery { get; set; }
    public DbSet<About> Abouts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
	public DbSet<Connection> Connections { get; set; }
	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}

