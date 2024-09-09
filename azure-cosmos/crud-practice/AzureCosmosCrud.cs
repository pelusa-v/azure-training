using System;
using Microsoft.Azure.Cosmos;

namespace crud_practice;

public class AzureCosmosCrud
{
    private readonly CosmosClient _client;

    public AzureCosmosCrud(string endpoint, string key)
    {
        _client = new CosmosClient(endpoint, key);
    }

    public async Task<DatabaseResponse> CreateDatabase(string dbId)
    {
        var res = await _client.CreateDatabaseAsync(dbId);
        return res;
    }

    public async Task<Database> GetDatabase(string dbId)
    {
        var database = _client.GetDatabase(dbId);
        if (database == null)
        {
            throw new Exception("Database not found");
        }
        return await database.ReadAsync();
    }

    public async Task DeleteDatabase(string dbId)
    {
        var database = await GetDatabase(dbId);
        await database.DeleteAsync();
    }

    public async Task<Container> CreateContainer(string dbId, string containerId, string partitionKey)
    {
        var database = await GetDatabase(dbId);
        var container = await database.CreateContainerAsync(containerId, partitionKey);
        return container;
    }

    public async Task<Container> GetContainer(string dbId, string containerId)
    {
        var database = await GetDatabase(dbId);
        var container = database.GetContainer(containerId);
        if (container == null)
        {
            throw new Exception("Container not found");
        }
        return container;
    }

    public async Task DeleteContainer(string dbId, string containerId)
    {
        var container = await GetContainer(dbId, containerId);
        await container.DeleteContainerAsync();
    }

    public async Task CreateItem<T>(string dbId, string containerId, T item)
    {
        var container = await GetContainer(dbId, containerId);
        try
        {
            var res = await container.CreateItemAsync<T>(item);
            Console.WriteLine(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<T> ReadItem<T>(string dbId, string containerId, string itemId, string partitionKeyValue)
    {
        var container = await GetContainer(dbId, containerId);
        ItemResponse<T> item = await container.ReadItemAsync<T>(itemId, new PartitionKey(partitionKeyValue));
        return item;
    }

    public async Task<List<T>> QueryItem<T>(string dbId, string containerId, string query, string partitionKeyValue)
    {
        var container = await GetContainer(dbId, containerId);
        var iterator = container.GetItemQueryIterator<T>(
            query,
            requestOptions: new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey(partitionKeyValue) 
            });
        var items = new List<T>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            items.AddRange(response.ToList());
        }
        return items;
    }
}
