using Assessment03.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment03.Context;

public class ProjectContext : DbContext
{
    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    { }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Contact> Contacts { get; set; }
}

