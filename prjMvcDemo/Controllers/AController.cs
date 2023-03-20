using MauiApp1.NewFolder;
using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
	public class AController : Controller
	{
		static int count = 0;
		public ActionResult showCountByCookie()
		{
			int count = 0;
			HttpCookie c = Request.Cookies["Count"];
			if (c != null)
			{
				count = Convert.ToInt32(c.Value);
			}
			count++;
			c = new HttpCookie("Count");
			c.Value = count.ToString();
			c.Expires = DateTime.Now.AddSeconds(20);
			Response.Cookies.Add(c);
			ViewBag.Count = count;
			return View();
		}
		public ActionResult showCountBySession()
		{
			int count = 0;
			if (Session["Count"] != null)
			{
				count = (int)Session["Count"];
			}
			count++;
			Session["Count"] = count;
			ViewBag.Count = count;
			return View();
		}
		public ActionResult showCount()
		{
			count++;
			ViewBag.Count = count;
			return View();
		}
		public ActionResult fileUploadDemo()
		{
			return View();
		}
		[HttpPost]
		public ActionResult fileUploadDemo(HttpPostedFileBase photo)
		{
			photo.SaveAs(@"C:\MVCLab\01.jpg");
			return View();
		}
		public ActionResult demoForm()
		{
			double a = Convert.ToDouble(Request.Form["txtA"]);
			double b = Convert.ToDouble(Request.Form["txtB"]);
			double c = Convert.ToDouble(Request.Form["txtC"]);
			double ans1 = (-b - Math.Sqrt(b * b - 4 * a * c)) / 2 * a;
			double ans2 = (-b + Math.Sqrt(b * b - 4 * a * c)) / 2 * a;

			double[] numbers = { a, b, c, ans1, ans2 };
			ViewBag.NUM = numbers;
			return View();
		}
		public string testingInsert()
		{
			CCustomer customer = new CCustomer()
			{
				fAddress = "Kaoshung",
				//fEmail = "tom@gmail.com",
				fName = "Tom",
				fPassword = "1234",
				fPhone = "0923451512",
			};
			(new CCustomerFactory()).create(customer);
			return "新增資料成功";
		}
		public int testingQueryAll()
		{
			return (new CCustomerFactory()).queryAll().Count;
		}
		public CCustomer testingQueryById(int id)
		{
			return (new CCustomerFactory()).queryById(id);
		}
		public string testingUpdate()
		{
			CCustomer customer = new CCustomer()
			{
				fId = 7,
				fAddress = "Taipei",
				fEmail = "John@gmail.com",
				fName = "John",
				fPassword = "1234",
				fPhone = "0912345678",
			};
			(new CCustomerFactory()).update(customer);
			return "更新資料成功";
		}
		public string testingDelete(int id)
		{
			(new CCustomerFactory()).delete(id);

			return "資料已刪除";
		}

		public ActionResult bindingById(int? id)
		{
			CCustomer cCustomer = null;
			if (id == null) return View();

			SqlConnection con = new SqlConnection();
			con.ConnectionString = @"Data Source =.; Initial Catalog = dbDemo; Integrated Security = True";
			using (con)
			{
				con.Open();
				SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
				using (cmd)
				{
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						cCustomer = new CCustomer()
						{
							fId = (int)reader["fId"],
							fName = reader["fName"].ToString(),
							fPhone = reader["fPhone"].ToString(),
						};
						ViewBag.KK = cCustomer;
					}
				}
			}
			return View(cCustomer);
		}
		public ActionResult showById(int? id)
		{
			if (id == null) return View();

			SqlConnection con = new SqlConnection();
			con.ConnectionString = @"Data Source =.; Initial Catalog = dbDemo; Integrated Security = True";
			using (con)
			{
				con.Open();
				SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
				using (cmd)
				{
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						CCustomer cCustomer = new CCustomer()
						{
							fId = (int)reader["fId"],
							fName = reader["fName"].ToString(),
							fPhone = reader["fPhone"].ToString(),
						};
						ViewBag.KK = cCustomer;
					}
				}
			}
			return View();
		}
		public string queryById(int? id)
		{
			if (id == null) return "沒有指定id";
			SqlConnection con = new SqlConnection();
			con.ConnectionString = @"Data Source =.; Initial Catalog = dbDemo; Integrated Security = True";
			con.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
			SqlDataReader reader = cmd.ExecuteReader();
			string s = "查無任何資料";
			if (reader.Read())
			{
				s = reader["fName"].ToString() + " / " + reader["fPhone"].ToString();
			}
			con.Close();
			return s;
		}

		public string demoServer()
		{
			return "目前伺服器上的實體位置：" + Server.MapPath(".");
		}

		public string demoRequest()
		{
			string id = Request.QueryString["pid"];
			if (id == "1")
				return "XBox 加入購物車成功";
			else if (id == "2")
				return "PS5 加入購物車成功";
			return "找不到該產品資料";
		}
		public string demoQueryString1(int? pid)
		{
			string id = pid.ToString();
			if (id == "1")
				return "XBox 加入購物車成功";
			else if (id == "2")
				return "PS5 加入購物車成功";
			return "找不到該產品資料";
		}
		public string demoQueryString2(int? id)
		{
			if (id == 1)
				return "XBox 加入購物車成功";
			else if (id == 2)
				return "PS5 加入購物車成功";
			return "找不到該產品資料";
		}

		public void DemoResponse()
		{
			Response.Clear();
			Response.ContentType = "application/octet-stream";
			Response.Filter.Close();
			Response.WriteFile(@"C:\Users\User\Pictures\Acer\HelloWorld.png");
			Response.End();
		}
		public string lotto(int count, int max)
		{
			string numbers = string.Empty;
			CLottoGen lottoGen = new CLottoGen();
			List<int> numberIist = lottoGen.GetLottoNumbers(count, max);

			for (int i = 0; i < numberIist.Count(); i++)
			{
				numbers += numberIist[i] + ", ";
			}
			return numbers;
		}
		public string sayHello()
		{
			return "Hello Asp.Net MVC";
		}

		// GET: A
		public ActionResult Index()
		{
			return View();
		}
	}
}