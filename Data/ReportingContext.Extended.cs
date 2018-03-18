using Microsoft.EntityFrameworkCore;

namespace Data.Entity
{
    public partial class ReportingContext : DbContext
    {
        public ReportingContext()
        {
        }

        public ReportingContext(DbContextOptions<ReportingContext> options)
        : base(options)
        {
        }
    }
}