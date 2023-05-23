using CosmosTodoApi.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosTodoApi.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<QAItem>> GetItemsAsync(string query);
        Task<QAItem?> GetItemAsync(string id);
        Task AddItemAsync(QAItem item);
        Task UpdateItemAsync(string id, QAItem item);
        Task DeleteItemAsync(string id);
    }
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;
        private static IEnumerable<QAItem> ctresults;
        
        public IEnumerable<QAItem>GetItems()
        {
            return ctresults;
        }
        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<QAItem>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<QAItem>(new QueryDefinition(queryString));

            var results = new List<QAItem>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            ctresults = results;

            return results;
        }

        public async Task<QAItem?> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<QAItem> response = await _container.ReadItemAsync<QAItem>(id, new PartitionKey(id));
                return response.Resource;
            }
            // 404 Not Foundの場合はnullを返す
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddItemAsync(QAItem item)
        {
            await _container.CreateItemAsync<QAItem>(item, new PartitionKey(item.Id));
        }

        public async Task UpdateItemAsync(string id, QAItem item)
        {
           await _container.UpsertItemAsync<QAItem>(item, new PartitionKey(id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<QAItem>(id, new PartitionKey(id));
        }

        // Cosmosクライアントの初期化処理
        public static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync()
        {
            // appsettings.jsonからCosmosDBの設定値を取得
            var databaseName = "webservicecontainers";
            var containerName = "QAItems";
            var account = "https://webaservicedb.documents.azure.com:443/";
            string? key = Environment.GetEnvironmentVariable("COSMOS_DB_KEY");

            // CosmosClientとCosmosDbServiceのインスタンスを生成
            var client = new CosmosClient(account, key);
            var cosmosDbService = new CosmosDbService(client, databaseName, containerName);

            // データベースが存在しない場合は新たに作成する
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

            // コンテナが存在しない場合は新たに作成する
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return cosmosDbService;
        }
    }


}

