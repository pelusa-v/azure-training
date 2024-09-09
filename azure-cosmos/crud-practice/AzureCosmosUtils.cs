using System;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;

namespace crud_practice;

public class AzureCosmosUtils : AzureCosmosCrud
{
    public AzureCosmosUtils(string endpoint, string key) : base(endpoint, key)
    {
    }

    public async Task ExecuteSP<T>(string dbId, string containerId, string spId, string partitionKeyValue, T input)
    {
        try
        {
            var container = await GetContainer(dbId, containerId);
            var res = await container.Scripts.ExecuteStoredProcedureAsync<T>(spId, new PartitionKey(partitionKeyValue), [input]);
            Console.WriteLine(res.Resource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}