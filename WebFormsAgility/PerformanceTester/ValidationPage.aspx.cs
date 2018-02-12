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
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds+ " PreInit";
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " Init";
        }
        protected void Page_InitComplete(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " InitComplete";
        }
        protected override void OnPreLoad(EventArgs e)
        {
            //Work and It will assign the values to label.  
            //If the page is post back, then label contrl values will be loaded from view state.  
            //E.g: If you string str = lblName.Text, then str will contain viewstate values.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " PreLoad";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " Load";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " btnSubmit_Click";
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " LoadComplete";
        }
        protected override void OnPreRender(EventArgs e)
        {
            //Work and It will assign the values to label.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " PreRender";
        }
        protected override void OnSaveStateComplete(EventArgs e)
        {
            //Work and It will assign the values to label.  
            //But "SaveStateComplete" values will not be available during post back. i.e. View state.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " SaveStateComplete";
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            //Work and it will not effect label contrl, view stae and post back data.  
            lblName.Text = lblName.Text + "<br/>" + t.ElapsedMilliseconds + " UnLoad";
        }

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
                XmlNode xlAcnCtrl = xml.SelectSingleNode("//actioncontrol");
                if (xlAcnCtrl != null)
                {
                    if (Convert.ToString(xlAcnCtrl.Attributes["actioncriteria"].Value) != string.Empty || Convert.ToString(xlAcnCtrl.Attributes["nextactioncriteria"].Value) != string.Empty)
                    {
                        string sCri = xlAcnCtrl.Attributes["actioncriteria"].Value.ToLower().Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ") + " " + xml.SelectSingleNode("//actioncontrol").Attributes["nextactioncriteria"].Value.ToLower().Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ");
                        Regex sCharacters = new Regex(@"((( |0)*\[?(.+)\]?( )*=( |0)*\[?\4\]?( )*(?<=\b|\]|\s)or)|(or(?=\b|\[|\s)( |0)*\[?(.+)\]?( )*=( |0)*\[?\10\]?( )*))|(--|/\*)|(\b|^)(select(\s|\t)|insert(\s|\t)|update(\s|\t)|delete(\s|\t)|drop(\s|\t)|alter(\s|\t)|sp_|xp_|exec(ute){0,1}(\s|\t)|sleep(\s|\t)|grant(\s|\t)|restore(\s|\t)|waitfor(\s|\t)|dbcc(\s|\t)|revoke(\s|\t)|truncate(\s|\t)|shutdown)");
                        if (sCharacters.IsMatch(sCri))
                        {
                            Session["IsFirst"] = null;
                            //UserLogOut();
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                        }
                    }
                }
                #endregion
                string aId = xml.SelectSingleNode("//actioncontrol").Attributes["actionid"].Value;
                if (string.IsNullOrEmpty(aId) || aId == "LogOn" || aId == "SaveLanguage")
                {
                    Session["pageSession"] = null;
                }
                else if (aId == "LogOut")
                {
                    //UserLogOut();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                }
                else if (xml.SelectSingleNode("//data/*") != null)
                {
                    //Preventing below special charectors in data node, means preventing below special charectors from client input..
                    //Regex sCharacters = new Regex(@"[~`!%^*|\{}]");
                    //if (sCharacters.IsMatch(xml.SelectSingleNode("//data/*").OuterXml) || (xml.SelectSingleNode("//actioncontrol").Attributes["sert"].Value != HttpContext.Current.Session.SessionID.ToString()))
                    //    Response.Redirect("~/SSOLogout.aspx?error=1");
                    if (xml.SelectSingleNode("//data/*/*") != null && Session["pageSession"] != null)
                    {
                        ///
                        //Validating Cross site request using open time session object.
                        //if open time is not valid, application will be logging out. In some exceptional cases request will be allowed if open time is empty
                        ///
                        string s = Session["pageSession"].ToString();
                        string openTime = xml.SelectSingleNode("//data/*/*").Attributes["OpenTime"] == null ? string.Empty : xml.SelectSingleNode("//data/*/*").Attributes["OpenTime"].Value;
                        if (openTime != string.Empty)
                        {
                            if (!s.Contains(openTime))
                            {
                                //UserLogOut();
                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                            }
                            else if (s.IndexOf("$" + openTime) > -1)
                            {
                                Session["pageSession"] = s.Replace("$" + openTime, ""); //removing stored page open time from session
                            }
                            else Session["pageSession"] = s.Replace(openTime, ""); //removing stored page open time from session
                        }
                        //else
                        //    Response.Redirect("~/SSOLogout.aspx?error=1");                  
                    }
                }
                else if (xml.SelectSingleNode("//data").Attributes["isn"] != null && xml.SelectSingleNode("//data").Attributes["isn"].Value == "y" && Session["pageSession"] != null)
                {
                    ///
                    //Making pageSession object null and removing all opened page related information
                    ///
                    string s = Session["pageSession"].ToString();
                    if (s.IndexOf("$") > -1)
                        Session["pageSession"] = s.Substring(0, s.IndexOf("$"));
                }
            }
        }

    }
}