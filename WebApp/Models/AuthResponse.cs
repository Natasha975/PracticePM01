using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
	public class AuthResponse
	{
		public string Token { get; set; }
		public int Role { get; set; }
		public int UserId { get; set; }
		public bool RequiresTwoFactor { get; set; }
	}
}