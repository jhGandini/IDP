using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace IDP.Extensions
{
    public static class MigrationExtensions
    {
        public static IServiceProvider MigrateDatabase(this IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                        if (!context.Clients.Any())
                        {
                            foreach (var client in Config.GetClients())
                            {
                                context.Clients.Add(client.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        if (!context.IdentityResources.Any())
                        {
                            foreach (var resource in Config.GetIdentityResources())
                            {
                                context.IdentityResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        if (!context.ApiScopes.Any())
                        {
                            foreach (var apiScope in Config.GetApiScopes())
                            {
                                context.ApiScopes.Add(apiScope.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        if (!context.ApiResources.Any())
                        {
                            foreach (var resource in Config.GetApiResources())
                            {
                                context.ApiResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return provider;
        }
    }
}
