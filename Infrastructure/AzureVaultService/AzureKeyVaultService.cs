using Application.Interfaces;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.AzureVaultService
{
    public class AzureKeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;
        private readonly IMemoryCache _cache;

        public AzureKeyVaultService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            try
            {
                var keyVaultEndpoint = configuration["Azure:KeyVaultUri"];
                var credential = new DefaultAzureCredential();
                _secretClient = new SecretClient(new Uri(keyVaultEndpoint), credential);
                _cache = memoryCache;
            }
            catch (Exception ex)
            {
                // log exception
                Console.WriteLine($"An error occurred while setting up AzureKeyVaultService: {ex.Message}");
                throw; // rethrow the exception after logging
            }
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            try
            {
                if (!_cache.TryGetValue(secretName, out string cachedSecret))
                {
                    KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
                    cachedSecret = secret.Value;
                    _cache.Set(secretName, cachedSecret);
                }

                return cachedSecret;
            }
            catch (Exception ex)
            {
                // log exception
                Console.WriteLine($"An error occurred while retrieving the secret: {ex.Message}");
                throw; // rethrow the exception after logging
            }
        }
    }
}
