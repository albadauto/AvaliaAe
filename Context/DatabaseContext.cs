using Microsoft.EntityFrameworkCore;

namespace AvaliaAe.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
                
        }
    }
}
