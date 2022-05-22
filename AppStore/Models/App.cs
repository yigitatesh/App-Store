using System.ComponentModel.DataAnnotations;

namespace AppStore.Models
{
    public class App
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select a category.")]
        [Display(Name = "Category")]
        public string? Category { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
