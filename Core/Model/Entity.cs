using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Entity
    {
        [Key]
        public string Id { get; set; }
    }
}
