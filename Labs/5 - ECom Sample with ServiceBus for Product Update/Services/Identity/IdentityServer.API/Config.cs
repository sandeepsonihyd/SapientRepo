using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API
{
    public class Config
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("catalog", "Catalog API"),
                new ApiScope("basket", "Basket API")
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("catalog", "Catalog API")
                {
                    Scopes = new [] {"catalog" }
                },
                new ApiResource("basket", "Basket API")
                  {
                    Scopes = new [] {"basket" }
                },
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                    new Client
                    {
                        ClientId = "client",
                        ClientName = "WebMVC",
                        AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                        RequirePkce = false,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        ClientUri = configuration["MVCClientUrl"] +  "/signin-oidc",
                        RedirectUris = { configuration["MVCClientUrl"] +  "/signin-oidc", "https://localhost:5101/signin-oidc" },
                        PostLogoutRedirectUris = { configuration["MVCClientUrl"] + "/signout-callback-oidc" },

                        // scopes that client has access to
                        AllowedScopes = new List<string>
                            {
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                "catalog",
                                "basket"
                            },
                         AllowOfflineAccess = true,
                         RequireConsent = false
                    }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

    }

}
