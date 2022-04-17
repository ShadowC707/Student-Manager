using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class StudentContext : DbContext
    {   
        public DbSet<Student> Students { get; set; }

        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().OwnsOne(s => s.Mother);
            builder.Entity<Student>().OwnsOne(s => s.Father);
            builder.Entity<Student>().Property(p => p.Address).HasColumnName("addr");
        }

    }
}
