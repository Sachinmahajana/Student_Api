using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Student_Api.Model;

namespace Student_Api.Data
{
    public class DefaultDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(GetSqlServerConnection()));
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employe> Employees { get; set; }
        public DbSet<Emp> emps { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<User> users { get; set; }
        //  public DbSet<ExceptionLog> Exceptionlogs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<Customer> customers { get; set; }

        // Add the constructor that accepts DbContextOptions
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        {
        }

        public DefaultDbContext()
        {
        }

        //public DefaultDbContext( )
        //{
        //}

        private static string GetSqlServerConnection()
        {
            SqlConnectionStringBuilder connectionBuilder = new()
            {
                
                DataSource = "AVNI\\SQLEXPRESS ",
                InitialCatalog = "StudentDbCore",
                TrustServerCertificate = true,
                IntegratedSecurity = true
            };
            return connectionBuilder.ConnectionString;
        }
    }
}
