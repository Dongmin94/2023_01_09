using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeData.DB
{
    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }



        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Entites.Employee>()
                .HasKey(entity => entity._employeeNo);



			builder.Entity<Entites.Employee>()
                .Property(entity => entity._employeeNo)
			    .ValueGeneratedOnAdd();



			this.OnModelBuilding(builder);
        }




        public DbSet<Entites.Employee> Employees { get; set; }
    }
}