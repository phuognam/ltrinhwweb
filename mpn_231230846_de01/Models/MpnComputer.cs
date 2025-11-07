using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace mpn_231230846_de01.Models
{
    public class MpnComputer
    {
        [Key]
        public int mpnComId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(200)]
        public string mpnComName { get; set; }

        [Required]
        [Range(1, 5000, ErrorMessage = "Giá phải là số và nằm trong khoảng 1 đến 5000")]
        [Display(Name = "Giá (triệu)")]

        public decimal mpnComPrice { get; set; }
        [Display(Name = "Ảnh")]
        public string mpnComImage { get; set; }
        [Display(Name = "Trạng thái")]
        public bool mpnComStatus { get; set; }
        [NotMapped]
        public IFormFile UploatImage { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
