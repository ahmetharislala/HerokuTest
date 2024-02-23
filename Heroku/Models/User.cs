namespace Heroku.Models
{
    public class User:IEntity
    {
        public string? UserName { get; set; }  
        public string? Password { get; set; }
    }
}
