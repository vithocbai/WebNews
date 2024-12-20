﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebNews.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
		public string TenDangNhap { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
		[DataType(DataType.Password)]
		public string MatKhau { get; set; }

	}
}