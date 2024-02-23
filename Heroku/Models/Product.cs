using System.ComponentModel.DataAnnotations;

namespace Heroku.Models
{
    public class Product:IEntity
    {
     
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
