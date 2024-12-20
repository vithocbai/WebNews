namespace WebNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongKe")]
    public partial class ThongKe
    {
        public int Id { get; set; }

        public int? BaiVietId { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotThich { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? NgayCapNhat { get; set; }

        public virtual BaiViet BaiViet { get; set; }
    }
}
