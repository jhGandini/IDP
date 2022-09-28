using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
                new IdentityResource("position", "Your position", new List<string> { "position" }),
                new IdentityResource("country", "Your country", new List<string> { "country" }),
                new IdentityResource("medicina.atestados", "Your country", new List<string> { "medicina.atestados.Acesso","medicina.atestados.Perfil" })

            };

        public static List<TestUser> GetUsers() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                    Username = "Mick",
                    Password = "MickPassword",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Mick"),
                        new Claim("family_name", "Mining"),
                        new Claim("address", "Sunny Street 4"),
                        new Claim("role", "Admin"),
                        new Claim("position", "Administrator"),
                        new Claim("country", "USA"),
                        new Claim("medicina.atestados.Acesso", "Sim"),
                        new Claim("medicina.atestados.Perfil", "admin")

                    }
                },
                new TestUser
                {
                    SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                    Username = "Jane",
                    Password = "JanePassword",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Jane"),
                        new Claim("family_name", "Downing"),
                        new Claim("address", "Long Avenue 289"),
                        new Claim("role", "Visitor"),
                        new Claim("position", "Viewer"),
                        new Claim("country", "USA")
                    }
                }
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "company-employee",
                    ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "companyApi" }
                },
                new Client
                {
                    ClientName = "MVC Client",
                    ClientId = "mvc-client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{ "https://localhost:5010/signin-oidc" },
                    RequirePkce = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "companyApi",
                        "position",
                        "country"
                    },
                    ClientSecrets = { new Secret("MVCSecret".Sha512()) },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:5010/signout-callback-oidc" },
                    //FrontChannelLogoutUri = "https://localhost:5010/frontChannello",
                    BackChannelLogoutUri = "https://localhost:5010/Logout"
                },
                new Client
                {
                    ClientName = "medicina.atestados",
                    ClientId = "medicina.atestados",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = new List<string>{ "http://localhost:5001/", "http://localhost:5001/assets/silent-callback.html" },
                    RequirePkce = false,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "medicina.atestados.api",
                        "medicina.atestados",
                        "dadosgestao.api",
                        "auth.api"
                    },
                    AllowedCorsOrigins = { "http://localhost:5001" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string> { "http://localhost:5001/signout-callback" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600
                }
            };
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("companyApi", "CompanyEmployee API"),
                new ApiScope("auth.api", "CompanyEmployee API"),
                new ApiScope("dadosgestao.api", "CompanyEmployee API"),
                new ApiScope("medicina.atestados.api", "CompanyEmployee API")
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" }
                },
                new ApiResource("auth.api", "CompanyEmployee API")
                {
                    Scopes = { "auth.api" }
                },
                new ApiResource("dadosgestao.api", "CompanyEmployee API")
                {
                    Scopes = { "dadosgestao.api" }
                },
                new ApiResource("medicina.atestados.api", "CompanyEmployee API")
                {
                    Scopes = { "medicina.atestados.api" }
                }
            };
    }
}