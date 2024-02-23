using System.ComponentModel.DataAnnotations;

namespace Heroku.Models
{
    public abstract class IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
