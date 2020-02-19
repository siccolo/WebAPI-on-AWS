using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

//  using System.Text.Json instead
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

namespace Core
{
    namespace Domain
    {
          // RedeemCode and RedeemResult are under Models
    }

    //  using appsettings.json, see BackendHost.Startup
    /*
    namespace Configuration
    {
        public class DataStoreOptions
        {
            public Db Db { get; set; }
            public string ToJson()
            {
                //return JsonConvert.SerializeObject(this, Formatting.Indented);
                return System.Text.Json.JsonSerializer.Serialize(this
                        , new System.Text.Json.JsonSerializerOptions() { WriteIndented =true}
                        );
            }
        }

        public class Db
        {
            public string ReadDataSource { get; set; }
            public string WriteDataSource { get; set; }
            public string InitialCatalog { get; set; }
            public string UserId { get; set; }
            [System.Text.Json.Serialization.JsonIgnore] public string Password { get; set; }

            public string GetReadOnlyConnectionString() => GetConnectionString(ReadDataSource);

            public string GetWriteConnectionString() => GetConnectionString(WriteDataSource);

            private string GetConnectionString(string dataSource)
            {
                var builder = new SqlConnectionStringBuilder();
                builder.DataSource = dataSource;
                builder.InitialCatalog = InitialCatalog;
                builder.UserID = UserId;
                builder.Password = Password;
                return builder.ConnectionString;
            }
        }
    }
    */
}