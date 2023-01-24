using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Models
{
    [Table("cars")]
    public class Car:BaseEntity
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        [MaxLength(12)]
        [Unique] 

        public string Vin { get; set; }

    }
    //public class CarMart
    //{
    //    public int Id { get; set; }
    //    public List<Car> Cars { get; set; }

    //}
}
