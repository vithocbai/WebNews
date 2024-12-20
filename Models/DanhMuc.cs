namespace WebNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMuc")]
    public partial class DanhMuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMuc()
        {
            BaiViet = new HashSet<BaiViet>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDanhMuc { get; set; }

        public string MoTa { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? NgayTao { get; set; }

        [StringLength(255)]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViet { get; set; }
    }
}
