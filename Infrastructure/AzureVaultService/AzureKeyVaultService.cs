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
            var keyVaultEndpoint = configuration["KeyVault:Endpoint"];
            var credential = new DefaultAzureCredential();
            _secretClient = new SecretClient(new Uri(keyVaultEndpoint), credential);
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
            return secret.Value;
        }
    }
}
