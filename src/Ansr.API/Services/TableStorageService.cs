using ansr.API.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ansr.API.Services
{
	public class TableStorageService : ITableStorageService
	{
		private const string TableName = "questions";
		private readonly IConfiguration _configuration;

		public TableStorageService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<QuestionEntity> RetrieveAsync(string category, string id)
		{
			var retrieveOperation = TableOperation.Retrieve<QuestionEntity>(category, id);
			return await ExecuteTableOperation(retrieveOperation) as QuestionEntity;
		}

		public async Task<QuestionEntity> InsertOrMergeAsync(QuestionEntity entity)
		{
			var insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
			return await ExecuteTableOperation(insertOrMergeOperation) as QuestionEntity;
		}

		public async Task<QuestionEntity> DeleteAsync(QuestionEntity entity)
		{
			var deleteOperation = TableOperation.Delete(entity);
			return await ExecuteTableOperation(deleteOperation) as QuestionEntity;
		}

		private async Task<object> ExecuteTableOperation(TableOperation tableOperation)
		{
			var table = await GetCloudTable();
			var tableResult = await table.ExecuteAsync(tableOperation);
			return tableResult.Result;
		}

		private async Task<CloudTable> GetCloudTable()
		{
			var storageAccount = CloudStorageAccount.Parse(_configuration["StorageConnectionString"]);
			var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
			var table = tableClient.GetTableReference(TableName);
			await table.CreateIfNotExistsAsync();
			return table;
		}
	}
}
