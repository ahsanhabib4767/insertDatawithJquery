using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsertjQuery.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace InsertjQuery.Controllers
{
   
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        database_Access_layer.db dblayer = new database_Access_layer.db();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add_data(register obj)
        {
            String msg = String.Empty;
            try
            {
                SqlCommand com = new SqlCommand("AddEmp", con);
                //SqlCommand com = new SqlCommand("AddEmp", con);
                com.CommandType = CommandType.StoredProcedure;
               com.Parameters.AddWithValue("@id", obj.id);
                com.Parameters.AddWithValue("@first_name", obj.FirstName);
                com.Parameters.AddWithValue("@last_name", obj.LastName);
                com.Parameters.AddWithValue("@salary", obj.Salary);
                com.Parameters.AddWithValue("@city", obj.City);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "Data Inserted";
                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception) {
                msg = "Data not Inserted";
                return Json(msg, JsonRequestBehavior.AllowGet);

            }
           
        }
        public JsonResult get_data()
        {
            DataSet ds = dblayer.Show_Data();
            List<register> listreg = new List<register>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                listreg.Add(new register
                {
                    id = Convert.ToInt32(dr["id"]),
                FirstName = dr["first_name"].ToString(),
                LastName = dr["last_name"].ToString(),
                Salary = Convert.ToInt32(dr["salary"]),
                City = dr["city"].ToString(),
            });
            }
            return Json(listreg, JsonRequestBehavior.AllowGet);
        }
    }
}