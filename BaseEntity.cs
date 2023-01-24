using SQLite;

namespace CarListApp.Maui.Models
{
    public abstract class BaseEntity
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
    }
    //public class CarMart
    //{
    //    public int Id { get; set; }
    //    public List<Car> Cars { get; set; }

    //}
}
