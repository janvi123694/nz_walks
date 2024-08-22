using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data;

public class NZWalksAuthDbContext : IdentityDbContext
{
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var readerRoleId = "ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c";
        var writerRoleId = "54466f17-02af-48e7-8ed3-5a4a8bfacf6f";
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerRoleId, ConcurrencyStamp = readerRoleId,
                Name = "Reader", NormalizedName = "Reader".ToUpper()
            },
            new IdentityRole
            {
                Id = writerRoleId, ConcurrencyStamp = writerRoleId, 
                Name = "Writer", NormalizedName = "Writer".ToUpper()
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}