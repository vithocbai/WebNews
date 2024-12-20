namespace WebNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string NoiDung { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? NgayBinhLuan { get; set; }

        public int? NguoiDungId { get; set; }

        public int? BaiVietId { get; set; }

        public virtual BaiViet BaiViet { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
