using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CodeCheckerDbContext : DbContext
    {
        public CodeCheckerDbContext()
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> Homework { get; set; }
        public DbSet<SubmittedHomework> SubmittedHomework { get; set; }
        public DbSet<HomeworkRule> HomeworkRules { get; set; }


        public CodeCheckerDbContext(DbContextOptions<CodeCheckerDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectModels;Initial Catalog=CodeChecker;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
