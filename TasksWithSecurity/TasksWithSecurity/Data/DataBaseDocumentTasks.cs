using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace TasksWithSecurity.Data
{
    public static class DataBaseDocumentTasks<T> where T : class
    {
        public readonly static string DataBaseId = "Task_Tasks";

        public readonly static string TaskCollectionId = "Tasks_Collection";
        private static DocumentClient documentClient;

        public static void InitializeDocumentClient(DocumentClient client)
        {
            documentClient = client;
            InitializeDocumentDataBase();
            InitializeDocumentCollection();
        }

        private static void InitializeDocumentDataBase()
        {
            try
            {
                // Does the DB exist?
                var db = documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DataBaseId)).Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex.GetType() == typeof(DocumentClientException) && ((DocumentClientException)ex).StatusCode == HttpStatusCode.NotFound)
                    {
                        // Create DB
                        var db = documentClient.CreateDatabaseAsync(new Database() { Id = DataBaseId }).Result;
                        return true;
                    }

                    return false;
                });
            }
        }

        private static void InitializeDocumentCollection()
        {
            // Tasks
            try
            {
                // Does the Collection exist?
                var collection = documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DataBaseId, TaskCollectionId)).Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex.GetType() == typeof(DocumentClientException) && ((DocumentClientException)ex).StatusCode == HttpStatusCode.NotFound)
                    {
                        DocumentCollection collection = new DocumentCollection() { Id = TaskCollectionId };
                        collection = documentClient.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DataBaseId), collection).Result;

                        return true;
                    }

                    return false;
                });
            }
        }

        public static async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await documentClient.ReadDocumentAsync(BuildItemUri(id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = documentClient.CreateDocumentQuery<T>(
                BuildCollectionUri(),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public static async Task<Document> CreateItemAsync(T item)
        {
            return await documentClient.CreateDocumentAsync(
                BuildCollectionUri(),
                item);
        }

        public static async Task<Document> UpdateItemAsync(string id, T item)
        {
            return await documentClient.ReplaceDocumentAsync(
                BuildItemUri(id),
                item);
        }

        public static async Task DeleteItemAsync(string id)
        {
            await documentClient.DeleteDocumentAsync(BuildItemUri(id));
        }

        private static Uri BuildCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(DataBaseId, TaskCollectionId);
        }

        private static Uri BuildItemUri(string itemId)
        {
            return UriFactory.CreateDocumentUri(DataBaseId, TaskCollectionId, itemId);
        }

        private static Uri BuildDataBaseUri()
        {
            return UriFactory.CreateDatabaseUri(DataBaseId);
        }
    }
}
