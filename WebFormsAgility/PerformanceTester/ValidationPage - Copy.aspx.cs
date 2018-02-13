using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebFormsAgility.PerformanceTester
{
    public partial class ValidationPage : System.Web.UI.Page
    {
        Stopwatch t = Stopwatch.StartNew();

        protected void Page_PreInit(object sender, EventArgs e)
        {

            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " PreInit";
            t.Reset();
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " Init";
            t.Reset();
        }
        protected void Page_InitComplete(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " InitComplete";
            t.Reset();
        }
        protected override void OnPreLoad(EventArgs e)
        {
            //Work and It will assign the values to label.  
            //If the page is post back, then label contrl values will be loaded from view state.  
            //E.g: If you string str = lblName.Text, then str will contain viewstate values.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " PreLoad";
            t.Reset();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " Load";
            t.Reset();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " btnSubmit_Click";
            t.Reset();
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " LoadComplete";
            t.Reset();
        }
        protected override void OnPreRender(EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " PreRender";
            t.Reset();
        }
        protected override void OnSaveStateComplete(EventArgs e)
        {
            //Work and It will assign the values to label.  
            //But "SaveStateComplete" values will not be available during post back. i.e. View state.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " SaveStateComplete";
            t.Reset();
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            //Work and it will not effect label contrl, view stae and post back data.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " UnLoad";
            t.Reset();
        }
        protected void ValidateData_Click(object sender, EventArgs e)
        {
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " Button Clicked";
            t.Reset();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(@"..\Data\Sampleservicedocument.xml");
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " File Loaded";
            t.Reset();
            ValidateRequest(xmldoc);
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " Validate Completed";
            t.Reset();
            lblName.Text = lblName.Text + "<br/>" + "IsFreefromSQLIn =" + IsFreefromSQLIn;
        }

        bool IsFreefromSQLIn;
        /// <summary>
        /// The request will be validated once the service document is loaded to from client request.
        /// Prevention of basic SQL injection.
        ///Prevention of Session hijacking.
        ///Special character validation in data node.
        ///CSRF prevention in an alternative way by using Session object.
        /// </summary>
        /// <param name="xml">Service Docuemnt which is loaded from client request.</param>
        private void ValidateRequest(XmlDocument xml)
        {
            if (xml != null)
            {
                #region  Validating and preventing SQL Injection
                ////
                //Validating and preventing SQL basic commands and other criteria like 1=1, 'i'='i'
                ////
                //XmlNode actionControl = xml.SelectSingleNode("//actioncontrol");
                XmlNode actionControl = xml.SelectSingleNode("/servicedocument/actionservice/actioncontrol");
                XmlNode dataNode = xml.SelectSingleNode("/servicedocument/data");

                string pageSession = Session["pageSession"].ToString();

                if (actionControl != null)
                {
                    IsFreefromSQLIn = IsFreefromSQLInjection(actionControl);
                }
                #endregion
                //string aId = xml.SelectSingleNode("//actioncontrol").Attributes["actionid"].Value;
                string actionid = actionControl.Attributes["actionid"].Value;

                if (string.IsNullOrEmpty(actionid) || actionid == "LogOn" || actionid == "SaveLanguage")
                {
                    Session["pageSession"] = null;
                }
                else if (actionid == "LogOut")
                {
                    //UserLogOut();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                }
                //else if (xml.SelectSingleNode("//data/*") != null)

                else if (dataNode.HasChildNodes)
                {
                    //Preventing below special charectors in data node, means preventing below special charectors from client input..
                    //Regex sCharacters = new Regex(@"[~`!%^*|\{}]");
                    //if (sCharacters.IsMatch(xml.SelectSingleNode("//data/*").OuterXml) || (xml.SelectSingleNode("//actioncontrol").Attributes["sert"].Value != HttpContext.Current.Session.SessionID.ToString()))
                    //    Response.Redirect("~/SSOLogout.aspx?error=1");


                    //if (xml.SelectSingleNode("//data/*/*") != null && Session["pageSession"] != null)
                    if (dataNode.FirstChild.HasChildNodes && pageSession != null)
                    {
                        ///
                        //Validating Cross site request using open time session object.
                        //if open time is not valid, application will be logging out. In some exceptional cases request will be allowed if open time is empty
                        ///

                        //string pageSession = Session["pageSession"].ToString();

                        //string openTime = xml.SelectSingleNode("//data/*/*").Attributes["OpenTime"] == null ? string.Empty : xml.SelectSingleNode("//data/*/*").Attributes["OpenTime"].Value;
                        string openTime = dataNode.FirstChild.Attributes["OpenTime"] == null ? string.Empty : dataNode.FirstChild.Attributes["OpenTime"].Value;
                        if (openTime != string.Empty)
                        {
                            //if (!pageSession.Contains(openTime))
                            //{
                            //    //UserLogOut();
                            //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                            //}
                            //else if (pageSession.IndexOf("$" + openTime) > -1)
                            //{
                            //    Session["pageSession"] = pageSession.Replace("$" + openTime, ""); //removing stored page open time from session
                            //}
                            //else Session["pageSession"] = pageSession.Replace(openTime, ""); //removing stored page open time from session

                            SetPageSessionObject(true, pageSession, openTime);
                        }
                        //else
                        //    Response.Redirect("~/SSOLogout.aspx?error=1");                  
                    }
                }
                //else if (xml.SelectSingleNode("//data").Attributes["isn"] != null && xml.SelectSingleNode("//data").Attributes["isn"].Value == "y" && Session["pageSession"] != null)
                else if (dataNode.Attributes["isn"] != null && dataNode.Attributes["isn"].Value == "y" && pageSession != null)
                {
                    ///
                    //Making pageSession object null and removing all opened page related information
                    ///
                    //string s = Session["pageSession"].ToString();

                    //if (pageSession.IndexOf("$") > -1)
                    //    Session["pageSession"] = pageSession.Substring(0, pageSession.IndexOf("$"));


                    SetPageSessionObject(false, pageSession, "");
                }
            }
        }


        private bool IsFreefromSQLInjection(XmlNode actionControl)
        {
            string actioncriteria = Convert.ToString(actionControl.Attributes["actioncriteria"].Value).ToLower();
            string nextactioncriteria = Convert.ToString(actionControl.Attributes["nextactioncriteria"].Value).ToLower();

            if (actioncriteria == string.Empty || nextactioncriteria == string.Empty)
            {
                return true;
            }
            string ACriterias = actioncriteria.Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ") + " " +
                            nextactioncriteria.Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ");

            Regex SQLmatchChars = new Regex(@"((( |0)*\[?(.+)\]?( )*=( |0)*\[?\4\]?( )*(?<=\b|\]|\s)or)|(or(?=\b|\[|\s)( |0)*\[?(.+)\]?( )*=( |0)*\[?\10\]?( )*))|(--|/\*)|(\b|^)(select(\s|\t)|insert(\s|\t)|update(\s|\t)|delete(\s|\t)|drop(\s|\t)|alter(\s|\t)|sp_|xp_|exec(ute){0,1}(\s|\t)|sleep(\s|\t)|grant(\s|\t)|restore(\s|\t)|waitfor(\s|\t)|dbcc(\s|\t)|revoke(\s|\t)|truncate(\s|\t)|shutdown)", RegexOptions.Compiled);

            if (SQLmatchChars.IsMatch(ACriterias))
            {
                Session["IsFirst"] = null;
                //UserLogOut();


                //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetPageSessionObject(bool dataNodeHasChildNodes, string pageSession,string openTime)
        {
            if(dataNodeHasChildNodes)
            {
                if (!pageSession.Contains(openTime))
                {
                    //UserLogOut();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                }
                else if (pageSession.IndexOf("$" + openTime) > -1)
                {
                    Session["pageSession"] = pageSession.Replace("$" + openTime, ""); //removing stored page open time from session
                }
                else Session["pageSession"] = pageSession.Replace(openTime, ""); //removing stored page open time from session

            }
            else
            {
                if (pageSession.IndexOf("$") > -1)
                    Session["pageSession"] = pageSession.Substring(0, pageSession.IndexOf("$"));
            }

        }


    }


}


//                        Regex sCharacters =
//                            new Regex(@"
//((( |0)*\[?(.+)\]?( )*=( |0)*\[?\4\]?( )*(?<=\b|\]|\s)or) |
//(or(?=\b|\[|\s) ( |0)*\[?(.+)\]?( )*=( |0)*\[?\10\]?( )*)) |
//(--|/\*)|
//(\b|^)
//(
//select(\s|\t)|
//insert(\s|\t)|
//update(\s|\t)|
//delete(\s|\t)|
//drop(\s|\t)|
//alter(\s|\t)|
//sp_|
//xp_|
//exec(ute){0,1}(\s|\t)|
//sleep(\s|\t)|
//grant(\s|\t)|
//restore(\s|\t)|
//waitfor(\s|\t)|
//dbcc(\s|\t)|
//revoke(\s|\t)|
//truncate(\s|\t)|
//shutdown)");

//                        regex for detection of SQL meta - characters
// / ((\% 3D)| (=))[^\n]* ((\%27)|(\')|(\-\-)|(\%3B)|(;))/i

//            Regex for typical SQL Injection attack
///\w* ((\%27)|(\'))((\%6F)|o|(\%4F))((\%72)|r|(\%52))/ix

//            Regex for detecting SQL Injection attacks on a MS SQL Server
///exec(\s|\+)+(s|x)p\w+/ix