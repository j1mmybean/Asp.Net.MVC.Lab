using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMvcDemo.Models
{
	public class CShoppingCartItem
	{
		public int productId { get; set; }
		public int count { get; set; }
		public decimal price { get; set; }
	}
}