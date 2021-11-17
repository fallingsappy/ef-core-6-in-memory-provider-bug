using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreInMemoryProviderBug.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder AddRelationalTypeMappingSourcePlugin<TPlugin>(
            this DbContextOptionsBuilder builder
        ) where TPlugin : class, ITypeMappingSourcePlugin, IRelationalTypeMappingSourcePlugin
        {
            ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(
                new TypeMappingSourcePluginExtension<TPlugin>());
            return builder;
        }

        public static DbContextOptionsBuilder<TContext> AddRelationalTypeMappingSourcePlugin<TPlugin, TContext>(
            this DbContextOptionsBuilder<TContext> builder
        ) where TPlugin : class, ITypeMappingSourcePlugin, IRelationalTypeMappingSourcePlugin
            where TContext : DbContext
        {
            ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(
                new TypeMappingSourcePluginExtension<TPlugin>());
            return builder;
        }
    }
    
    public class TypeMappingSourcePluginExtension<TPlugin> : IDbContextOptionsExtension
        where TPlugin : class, ITypeMappingSourcePlugin, IRelationalTypeMappingSourcePlugin
    {
        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<ITypeMappingSourcePlugin, TPlugin>();
            services.AddSingleton<IRelationalTypeMappingSourcePlugin, TPlugin>();
        }

        public void Validate(IDbContextOptions options) { }

        private DbContextOptionsExtensionInfo _info;
        public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);
    }
    
    public class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension) { }
        public override int GetServiceProviderHashCode() => 0;
        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => true;
        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo) { }
        public override bool IsDatabaseProvider => false;
        public override string LogFragment => string.Empty;
    }
}