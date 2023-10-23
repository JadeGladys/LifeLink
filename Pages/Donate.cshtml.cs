using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lifeLINK.Pages
{
    public class DonateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            userInfo.nid = Request.Form["nid"];
            userInfo.fullNames = Request.Form["fullnames"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];
            userInfo.bloodGroup = Request.Form["bg"];
            userInfo.RHfactor = Request.Form["rhfactor"];
            userInfo.organ = Request.Form["organ"];
            userInfo.description = Request.Form["description"];

            if (userInfo.nid.Length == 0 || userInfo.fullNames.Length == 0 || userInfo.email.Length == 0 || userInfo.phone.Length == 0 || userInfo.address.Length == 0 || userInfo.bloodGroup.Length == 0 || userInfo.RHfactor.Length == 0 || userInfo.organ.Length == 0 || userInfo.description.Length == 0)
            {
                errorMessage = "All fields are required MR/MRS";
                return;
            }

            try
            {
                String connectionString = "Data Source=DRKST-MTTR\\SQLEXPRESS;Initial Catalog=lifelink;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO donated " +
                                 "(nid, fullNames, email, phone, address, bloodGroup, RHfactor, organ, description) VALUES " +
                                 "(@nid, @fullnames, @email, @phone, @address, @bg, @rhfactor, @organ, @description);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nid", userInfo.nid);
                        command.Parameters.AddWithValue("@fullnames", userInfo.fullNames);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@address", userInfo.address);
                        command.Parameters.AddWithValue("@bg", userInfo.bloodGroup);
                        command.Parameters.AddWithValue("@rhfactor", userInfo.RHfactor);
                        command.Parameters.AddWithValue("@organ", userInfo.organ);
                        command.Parameters.AddWithValue("@description", userInfo.description);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfo.nid = "";
            userInfo.fullNames = "";
            userInfo.email = "";
            userInfo.phone = "";
            userInfo.address = "";
            userInfo.bloodGroup = "";
            userInfo.RHfactor = "";
            userInfo.organ = "";
            userInfo.description = "";

            successMessage = "Organ Donated Successfully";

            Response.Redirect("/");
        }

        public class UserInfo
        {
            public String? id;
            public String? nid;
            public String? fullNames;
            public String? email;
            public String? phone;
            public String? address;
            public String? bloodGroup;
            public String? RHfactor;
            public String? organ;
            public String? description;
        }
    }
}
