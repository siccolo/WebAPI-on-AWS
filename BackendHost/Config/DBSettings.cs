using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Config
{
    public interface IDBSettings
    {
        string DataSource { get; set; } 
        string Database { get; set; }  
        string UserId { get; set; }  
        string Password { get; set; }

        string Port { get; set; }
    }

    public sealed class DBSettings: IDBSettings
    {
        public string DataSource { get; set; } = "";
        public string Database { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Password { get; set; } = "";

        public string Port { get; set; } = "";

        public string ConnectionInfo
        {
            get
            {
                return $"server={DataSource};user id={UserId};password={Password};port={Port};database={Database};";
            }
        }
    }

}
