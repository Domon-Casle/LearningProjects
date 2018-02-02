using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TasksWithSecurity.Data
{
    public static class DataBaseDocumentClientSecurity
    {
        public readonly static string DataBaseId = "Task_Security";
        public readonly static string UserCollectionId = "User_Collection";
        public readonly static string RoleCollectionId = "Role_Colleciton";

        public static void InitializeDocumentClient(DocumentClient client)
        {
            InitializeDocumentDataBase(client);
            InitializeDocumentCollection(client);
        }

        private static void InitializeDocumentDataBase(DocumentClient client)
        {
            try
            {
                // Does the DB exist?
                var db = client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DataBaseId)).Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex.GetType() == typeof(DocumentClientException) && ((DocumentClientException)ex).StatusCode == HttpStatusCode.NotFound)
                    {
                        // Create DB
                        var db = client.CreateDatabaseAsync(new Database() { Id = DataBaseId }).Result;
                        return true;
                    }

                    return false;
                });
            }
        }

        private static void InitializeDocumentCollection(DocumentClient client)
        {
            // User 
            try
            {
                // Does the Collection exist?
                var collection = client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DataBaseId, UserCollectionId)).Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex.GetType() == typeof(DocumentClientException) && ((DocumentClientException)ex).StatusCode == HttpStatusCode.NotFound)
                    {
                        DocumentCollection collection = new DocumentCollection() { Id = UserCollectionId };
                        collection = client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DataBaseId), collection).Result;

                        return true;
                    }

                    return false;
                });
            }

            // Role
            try
            {
                // Does the Collection exist?
                var collection = client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DataBaseId, RoleCollectionId)).Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex.GetType() == typeof(DocumentClientException) && ((DocumentClientException)ex).StatusCode == HttpStatusCode.NotFound)
                    {
                        DocumentCollection collection = new DocumentCollection() { Id = RoleCollectionId };
                        collection = client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DataBaseId), collection).Result;

                        return true;
                    }

                    return false;
                });
            }
        }
    }
}
