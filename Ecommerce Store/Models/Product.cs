using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Store.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You Have To Provide a Valid  Code.")]
        [MinLength(12, ErrorMessage = "Code can`t be less than 12 Numbers .")]
        [DataType(DataType.Password)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [DisplayName("Confirm Product Code")]
        [Compare("ConfirmCode", ErrorMessage = "Product Code and Confirm Product Code don`t Match")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmCode { get; set; }


        [Required(ErrorMessage = "You Have To Provide a Valid  Name.")]
        [MinLength(3, ErrorMessage = "Name can`t be less than 3 charachers.")]
        [MaxLength(50, ErrorMessage = "Name Mustn`t exceed 50 charachers.")]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "You Have To Provide a Valid  Color.")]
        [MinLength(3, ErrorMessage = "Color can`t be less than 3 charachers.")]
        [MaxLength(50, ErrorMessage = "Color Mustn`t exceed 50 charachers.")]
        [Display(Name = "Color")]
        public string Color { get; set; }


        [Required(ErrorMessage = "You Have To Provide a Valid  Location.")]
        [MinLength(8, ErrorMessage = "Location can`t be less than 8 charachers.")]
        [MaxLength(50, ErrorMessage = "Location Mustn`t exceed 50 charachers.")]
        [Display(Name = "Location")]
        public string Location { get; set; }


        [Range(200, 10000, ErrorMessage = "Price Must be btween 200 EGP and 10000 EGP")]
        public decimal Price { get; set; }


        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }


        [DisplayName("IS Avaliable")]
        public bool ISAvaliable { get; set; }


        [ValidateNever]
        public string? Notes { get; set; }


        // Foreign key
        [DisplayName("Section")]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a Valid Section")]
        public int SectionId { get; set; }


        // Navigation Property 
        [ValidateNever]
        public Section Section { get; set; }


        [ValidateNever]
        public string ImagePath { get; set; }


        [DisplayName("Image")]
        [NotMapped]
        [ValidateNever]
        public IFormFile ImageFile { get; set; }

    }
}
