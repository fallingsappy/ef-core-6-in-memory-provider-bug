using System;
using EFCoreInMemoryProviderBug.Entities;
using EFCoreInMemoryProviderBug.Extensions;
using EFCoreInMemoryProviderBug.TypeMapping;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInMemoryProviderBug
{
    public class RatingContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<Department> Departments { get; set; }
        
        public RatingContext()
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapDepartments(modelBuilder);
            MapUnits(modelBuilder);
        }

        private void MapDepartments(ModelBuilder modelBuilder)
        {
	        var builder = modelBuilder.Entity<Department>().ToTable("departments");
	        builder.HasKey(p => p.Id);

	        builder.Property(p => p.Id).HasColumnName("Id");
        }

        private void MapUnits(ModelBuilder modelBuilder)
        {
	        var builder = modelBuilder.Entity<Unit>().ToTable("units");
			builder.HasKey(p => p.Id);
			builder.HasOne(p => p.Department)
				.WithMany()
				.HasForeignKey(x => x.DepartmentId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Property(p => p.Id).HasColumnName("Id");
			
			//TODO: Чтобы убрать баг надо закомментировать следующую строку
			builder.Metadata.FindNavigation(nameof(Unit.Department))?.SetIsEagerLoaded(true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .AddRelationalTypeMappingSourcePlugin<UUIdTypeMapperPlugin>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}