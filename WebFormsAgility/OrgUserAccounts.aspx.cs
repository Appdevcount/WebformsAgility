using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebFormsAgility
{
    public partial class OrgUserAccounts : System.Web.UI.Page
    {
        protected DataTable dtUsers;

        protected void Page_Load(object sender, EventArgs e)
        {

            
           
            String strConnString = ConfigurationManager.ConnectionStrings["TEST"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "OrgGetInformation";
            cmd1.Parameters.Add("@OrgType", SqlDbType.Int).Value = 1;
            cmd1.Connection = con;

            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "OrgGetAccountDetails";
            cmd2.Parameters.Add("@userId", SqlDbType.Int).Value = 1;
            cmd2.Connection = con;

            try
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                sda1.Fill(ds, "AcctInfo");

                //string AutoId= ds.Tables[0].Rows[0][0].ToString();
                Int16 OrgType = Convert.ToInt16( ds.Tables[0].Rows[0][1]);
                string OrgNameAr = ds.Tables[0].Rows[0][2].ToString();
                string OrgTradeLicenseNo = ds.Tables[0].Rows[0][3].ToString();
                string OrgCode = ds.Tables[0].Rows[0][4].ToString();

                OrgName.Text = OrgNameAr;
                TrdLicNo.Text = OrgTradeLicenseNo;
                OrgCd.Text = OrgCode;

                switch(OrgType)
                {
                    case 1:
                        CheckBox0.Checked = true;
                        break;
                    case 2:
                        CheckBox1.Checked = true;
                        break;
                    case 3:
                        CheckBox2.Checked = true;
                        break;
                    case 4:
                        CheckBox3.Checked = true;
                        break;
                    case 5:
                        CheckBox4.Checked = true;
                        break;
                }


                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                sda2.Fill(ds, "AcctUserDet");

                //UserDetailsRptr.DataSource = ds.Tables[1];
                dtUsers = ds.Tables[1];

                //UserDetailsRptr.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("Some minor issue has occured. Please contact IT Admin");
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}