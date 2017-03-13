namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Display(Name ="ID")]
        public long ProductID { get; set; }
        [Display(Name = "Thể loại")]
        public long CategoryID { get; set; }

        [Display(Name = "Tên sách")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập tên sách")]
        [StringLength(250)]
        public string ProductName { get; set; }

        [Display(Name = "Mã sách")]
        [StringLength(50)]
        public string ProductCode { get; set; }

        [Display(Name = "Tác giả")]
        [StringLength(250)]
        public string Author { get; set; }

        [Display(Name = "Nhà xuất bản")]
        [StringLength(250)]
        public string Publisher { get; set; }

        [Display(Name = "Seo title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập seo title")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Display(Name = "Miêu tả")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập mô tả")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Display(Name = "Giá bìa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập giá")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mại")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Số lượng")]
        public int Quanlity { get; set; }

        [Display(Name = "Chi tiết")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập chi tiêt")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public long ViewCount { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Trạng thai")]
        public bool Status { get; set; }
    }
}
