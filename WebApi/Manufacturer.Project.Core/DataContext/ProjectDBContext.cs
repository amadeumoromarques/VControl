namespace Manufacturer.Project.Core.DataContext
{
    using Manufacturer.Project.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;
    using System.Reflection.Emit;
    using TrackerEnabledDbContext.Core;

    /// <summary>
    /// Database context of the Generator.
    /// </summary>
    public class ProjectDBContext : TrackerContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDBContext"/> class.
        /// </summary>
        /// <param name="options">the options.</param>
        public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
             : base(options)
        {
        }

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.ReadClassesMap(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Read all classes map on project and create a model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public void ReadClassesMap(ModelBuilder modelBuilder)
        {
            var typesToRegister = this.GetType()
                .Assembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic val = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(val);
            }
        }
    }
}