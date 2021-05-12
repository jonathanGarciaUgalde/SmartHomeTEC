using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
namespace WebServices.Models
{
    
    public class ServerConexion
    {       
        public ServerConexion()
        {
            init();          
        }

        public string init() 
        {
            return "Server=localhost; Port = 5432;User Id=postgres;Password = 090917; Database = SmartHome";
        }
    }
}
