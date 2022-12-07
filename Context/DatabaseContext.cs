using AvaliaAe.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaliaAe.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CodeModel> Code { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<InstitutionModel> Institution { get; set; }

    }
}
