using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Domain.AzureVault;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AzureVaultService
{
    public class AzureKeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public AzureKeyVaultService(IConfiguration configuration)
        {
            try
            {
                var keyVaultEndpoint = configuration["Azure:KeyVaultUri"];
                var credential = new DefaultAzureCredential();
                _secretClient = new SecretClient(new Uri(keyVaultEndpoint), credential);
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
                KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
                return secret.Value;
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
