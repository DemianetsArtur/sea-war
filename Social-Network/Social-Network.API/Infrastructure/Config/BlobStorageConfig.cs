using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Social_Network.API.Infrastructure.Config
{
    public static class BlobStorageConfig
    {
        public static void SetBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionString");
            services.AddSingleton(_ => new BlobServiceClient(connectionString));
        }
    }
}