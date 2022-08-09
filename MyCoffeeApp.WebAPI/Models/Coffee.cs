using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoffeeApp.WebAPI.Models
{
    public class Coffee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Roaster { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
