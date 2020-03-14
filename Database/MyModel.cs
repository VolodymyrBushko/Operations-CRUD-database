namespace VolodEF.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyModel : DbContext
    {
        public MyModel()
            : base("name=MyModel")
        {
        }

        public virtual DbSet<Department> Departaments { get; set; }
        public virtual DbSet<DepEmp> DepEmps { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.DepEmps)
                .WithRequired(e => e.Departament)
                .HasForeignKey(e => e.idDepartament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Managers)
                .WithRequired(e => e.Departament)
                .HasForeignKey(e => e.idDepartament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.DepEmps)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.idEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Managers)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.idEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Vacations)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.idEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.DepEmps)
                .WithOptional(e => e.Position)
                .HasForeignKey(e => e.idPosition);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Salaries)
                .WithOptional(e => e.Position)
                .HasForeignKey(e => e.idPosition);
        }
    }
}
