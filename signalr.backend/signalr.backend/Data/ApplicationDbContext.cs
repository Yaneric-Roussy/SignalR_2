using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using signalr.backend.Models;

namespace signalr.backend.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // TODO: Ajouter des channels dans le seed

        builder.Entity<Channel>().HasData(new Channel[]
        {
            new Channel(){
                Id = 1,
                Title = "Channel 1"
            },
            new Channel()
            {
                Id = 2,
                Title = "Channel 2"
            },
            new Channel()
            {
                Id = 3,
                Title = "Channel 3"
            }
        });
    }

    public DbSet<Channel> Channel { get; set; } = default!;
}

