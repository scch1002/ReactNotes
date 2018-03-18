using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grace.Core.Repositories
{
    public class TableConfiguration
    {
        public string ConfigurationString { get; set; }

        public string TableName { get; set; }

        public CloudTable GetTableReference()
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationString);

            var client = storageAccount.CreateCloudTableClient();

            var table = client.GetTableReference(TableName);

            table.CreateIfNotExistsAsync().Wait();

            return table;
        }
    }
}
