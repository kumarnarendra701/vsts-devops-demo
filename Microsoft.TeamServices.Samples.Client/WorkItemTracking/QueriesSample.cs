using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.TeamServices.Samples.Client.WorkItemTracking
{
    [ClientSample(WitConstants.WorkItemTrackingWebConstants.RestAreaName, WitConstants.WorkItemTrackingRestResources.Queries)]
    public class QueriesSample : ClientSample
    {

        [ClientSampleMethod]
        public List<QueryHierarchyItem> GetListOfQueries()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            
            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            List<QueryHierarchyItem> queries = workItemTrackingClient.GetQueriesAsync(projectId, QueryExpand.None, 1).Result;

            if (queries == null)
            {
                Console.WriteLine("No queries found");
            }
            else
            {
                Console.WriteLine("Queries:");

                foreach(var query in queries)
                {
                    Console.WriteLine("  {0} - {1}", query.Name, query.Path);
                }
            }

            return queries;
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetQueryOrFolderByFolderPath()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            string queryName = "Shared Queries/Current Iteration";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(project, queryName, null, 2).Result;

            if (query == null)
            {
                Console.WriteLine("No queries found for path '{0}'", queryName);
            }
            else
            {
                Console.WriteLine("Queries:");

                foreach (var item in query.Children)
                {
                    Console.WriteLine("{0}", item.Name);
                    Console.WriteLine("  {0}", item.Id);
                    Console.WriteLine("  {0}", item.Path);
                    Console.WriteLine();
                }
            }

            return query;
        }

        [ClientSampleMethod]
        public List<QueryHierarchyItem> GetListOfQueriesAndFoldersWithOptions()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            List<QueryHierarchyItem> queries = workItemTrackingClient.GetQueriesAsync(projectId, QueryExpand.All, 1).Result;

            if (queries == null)
            {
                Console.WriteLine("No queries found");
            }
            else
            {
                Console.WriteLine("Queries:");

                foreach (var query in queries)
                {
                    Console.WriteLine("  {0} - {1}", query.Name, query.Path);
                }
            }

            return queries;
        }

        [ClientSampleMethod]
        public List<QueryHierarchyItem> GetListOfQueriesAndFoldersIncludeDeleted()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            List<QueryHierarchyItem> queries = workItemTrackingClient.GetQueriesAsync(projectId, QueryExpand.None, 1, true).Result;

            if (queries == null)
            {
                Console.WriteLine("No queries found");
            }
            else
            {
                Console.WriteLine("Queries:");

                foreach (var query in queries)
                {
                    Console.WriteLine("  {0} - {1}", query.Name, query.Path);
                }
            }

            return queries;
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetQueryOrFolderById()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(projectId, queryId).Result;

            if (query == null)
            {
                Console.WriteLine("Query not found");
            }
            else
            {
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);         
            }

            return query;
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetQueryOrFolderByName()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryName = "Shared Queries/Current Iteration/Active Bugs";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(projectId, queryName).Result;

            if (query == null)
            {
                Console.WriteLine("Query not found");
            }
            else
            {
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);              
            }

            return query;
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetDeletedQueryOrFolderById()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(projectId, queryId, null, 1, true).Result;

            if (query == null)
            {
                Console.WriteLine("Query not found");
            }
            else
            {
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);       
            }

            return query;
        }

        [ClientSampleMethod]
        public QueryHierarchyItem CreateQuery()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryPath = "Shared Queries";

            QueryHierarchyItem postedQuery = new QueryHierarchyItem()
            {
                Name = "Sample",   
                Wiql = "Select [System.Id], [System.Title], [System.State] From WorkItems Where [System.WorkItemType] = 'Bug' order by [Microsoft.VSTS.Common.Priority] asc, [System.CreatedDate] desc"
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                QueryHierarchyItem query = workItemTrackingClient.CreateQueryAsync(postedQuery, projectId, queryPath).Result;

                Console.WriteLine("Query Successfully Created");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }
            catch (System.AggregateException ex)
            {
                if (ex.InnerException.Message.Contains("TF237018"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Error creating query: Query name in specified path already exists");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return null;
            }         
        }

        [ClientSampleMethod]
        public QueryHierarchyItem CreateFolder()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryPath = "Shared Queries";

            QueryHierarchyItem postedQuery = new QueryHierarchyItem()
            {
                Name = "Folder " + System.Guid.NewGuid().ToString(), //random folder name
                IsFolder = true
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem query = workItemTrackingClient.CreateQueryAsync(postedQuery, projectId, queryPath).Result;

            if (query == null)
            {
                Console.WriteLine("Error creating folder");
            }
            else
            {
                Console.WriteLine("Folder Successfully Created");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);
            }

            return query;
        }

        [ClientSampleMethod]
        public QueryHierarchyItem UpdateQuery()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

            QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
            {               
                Wiql = "Select [System.Id], [System.Title], [System.State] From WorkItems Where [System.WorkItemType] = 'Bug' AND [System.State] = 'Active' order by [Microsoft.VSTS.Common.Priority] asc, [System.CreatedDate] desc"
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem query = workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId).Result;

            if (query == null)
            {
                Console.WriteLine("Error updating query");
            }
            else
            {
                Console.WriteLine("Query updated successfully");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);                
            }

            return query;
        }

        [ClientSampleMethod]
        public QueryHierarchyItem RenameQueryOrFolder()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

            QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
            {
                Name = "Active Bugs"  
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem query = workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId).Result;

            if (query == null)
            {
                Console.WriteLine("Error updating query");
            }
            else
            {
                Console.WriteLine("Query renamed successfully");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);         
            }

            return query;
        }

        //[ClientSampleMethod]
        //public QueryHierarchyItem MoveQueryOrFolder()
        //{
        //    System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
        //    string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

        //    QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
        //    {
        //        Id = new System.Guid("8a8c8212-15ca-41ed-97aa-1d6fbfbcd581") //where you want to move the queryId too
        //    };

        //    VssConnection connection = Context.Connection;
        //    WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

        //    QueryHierarchyItem query = workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId).Result;

        //    if (query == null)
        //    {
        //        Console.WriteLine("Error moving query");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Query/folder moved successfully");
        //        Console.WriteLine("Id:         {0}", query.Id);
        //        Console.WriteLine("Name:       {0}", query.Name);
        //        Console.WriteLine("Path:       {0}", query.Path);
        //    }

        //    return query;
        //}

        [ClientSampleMethod]
        public void DeleteQueryOrFolderById()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";            

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            workItemTrackingClient.DeleteQueryAsync(projectId, queryId);

            Console.WriteLine("Query/folder deleted");                
        }

        [ClientSampleMethod]
        public void DeleteQueryOrFolderByPath()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string path = "My Queries/Sample";         

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();
            
            workItemTrackingClient.DeleteQueryAsync(projectId, path);

            Console.WriteLine("Query/folder deleted");
        }

        [ClientSampleMethod]
        public void UnDeleteQueryOrFolder()
        {
            System.Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

            QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
            {
                IsDeleted = false
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId, true);

            Console.WriteLine("Query/folder deleted");
        }

        [ClientSampleMethod]
        public WorkItemQueryResult ExecuteQuery()
        {
            Guid queryId = Guid.Parse("6e511ae8-aafe-455a-b318-a4158bbd0f1e"); // TODO

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                WorkItemQueryResult queryResult = workItemTrackingClient.QueryByIdAsync(queryId).Result;

                return queryResult;
            }
            catch (System.AggregateException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ex.InnerException.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return null;
            }           
        }

        [ClientSampleMethod]
        public WorkItemQueryResult ExecuteByWiql()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            Wiql wiql = new Wiql()
            {
                Query = "Select ID, Title from Issue where (State = 'Active') order by Title"
            };            

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            WorkItemQueryResult queryResult = workItemTrackingClient.QueryByWiqlAsync(wiql, project).Result;

            return queryResult;
        }

        [ClientSampleMethod]
        public IEnumerable<WorkItem> GetWorkItemsFromQuery()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            string queryName = "Shared Queries/Current Sprint";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem queryItem;

            try
            {
                // get the query object based on the query name and project
                queryItem = workItemTrackingClient.GetQueryAsync(project, queryName).Result;
            }
            catch (Exception ex)
            {
                // query was likely not found
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ex.InnerException.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return null;
            }

            // now we have the query, so let'ss execute it and get the results
            WorkItemQueryResult queryResult = workItemTrackingClient.QueryByIdAsync(queryItem.Id).Result;

            if (queryResult.WorkItems.Count() == 0)
            {
                return new List<WorkItem>();
            }
            else
            {
                // need to get the list of our work item id's and put them into an array
                int[] workItemIds = queryResult.WorkItems.Select<WorkItemReference, int>(wif => { return wif.Id; }).ToArray();

                // build a list of the fields we want to see
                string[] fields = new []
                    {
                        "System.Id",
                        "System.Title",
                        "System.State"
                    };

                IEnumerable<WorkItem> workItems = workItemTrackingClient.GetWorkItemsAsync(workItemIds, fields, queryResult.AsOf).Result;

                return workItems;
            }
        }

        public IEnumerable<WorkItem> GetWorkItemsFromWiql()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;

            // create a query to get your list of work items needed
            Wiql wiql = new Wiql()
            {
                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        "Where [Work Item Type] = 'Bug' " +
                        "And [System.TeamProject] = '" + project + "' " +
                        "And [System.State] = 'New' " +
                        "Order By [State] Asc, [Changed Date] Desc"
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            // execute the query
            WorkItemQueryResult queryResult = workItemTrackingClient.QueryByWiqlAsync(wiql).Result;

            // check to make sure we have some results
            if (queryResult.WorkItems.Count() == 0)
            {
                return new List<WorkItem>();
            }
            else
            {
                // need to get the list of our work item id's and put them into an array
                int[] workItemIds = queryResult.WorkItems.Select<WorkItemReference, int>(wif => { return wif.Id; }).ToArray();

                // build a list of the fields we want to see
                string[] fields = new []
                    {
                        "System.Id",
                        "System.Title",
                        "System.State"
                    };

                IEnumerable<WorkItem> workItems = workItemTrackingClient.GetWorkItemsAsync(
                    workItemIds, 
                    fields, 
                    queryResult.AsOf).Result;

                return workItems;
            }
        }

    }
}
