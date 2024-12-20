namespace WebNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiViet()
        {
            BinhLuan = new HashSet<BinhLuan>();
            ThongKe = new HashSet<ThongKe>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string TieuDe { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string NoiDung { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? NgayDang { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public int? NguoiDungId { get; set; }

        public int? DanhMucId { get; set; }

		public string Anh { get; set; }
		public virtual DanhMuc DanhMuc { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongKe> ThongKe { get; set; }
    }
}
