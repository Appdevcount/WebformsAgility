﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace WebFormsAgility.PerformanceTester
{
    public partial class ValidationPage : System.Web.UI.Page
    {
        Stopwatch t = Stopwatch.StartNew();
        bool IsFreefromSQLIn;
        bool PageSessionObjectReset;
        string pageSession;
        string openTime;
        string ActionCriterias;

        protected void Page_PreInit(object sender, EventArgs e)
        {

            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " PreInit";
            t.Reset(); t.Start();
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " Init";
            t.Reset(); t.Start();
        }
        protected void Page_InitComplete(object sender, EventArgs e)
        {
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " InitComplete";
            t.Reset(); t.Start();
        }
        protected override void OnPreLoad(EventArgs e)
        {
            //Work and It will assign the values to label.  
            //If the page is post back, then label contrl values will be loaded from view state.  
            //E.g: If you string str = Status.Text, then str will contain viewstate values.  
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " PreLoad";
            t.Reset(); t.Start();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " Load";
            t.Reset(); t.Start();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " btnSubmit_Click";
            t.Reset(); t.Start();
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " LoadComplete";
            t.Reset(); t.Start();
        }
        protected override void OnPreRender(EventArgs e)
        {
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " PreRender";
            t.Reset(); t.Start();
        }
        protected override void OnSaveStateComplete(EventArgs e)
        {
            //Work and It will assign the values to label.  
            //But "SaveStateComplete" values will not be available during post back. i.e. View state.  
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " SaveStateComplete";
            t.Reset(); t.Start();
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            //Work and it will not effect label contrl, view stae and post back data.  
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " UnLoad";
            t.Reset(); t.Start();
        }
        protected void ValidateData_Click(object sender, EventArgs e)
        {
            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " Button Clicked";
            t.Reset(); t.Start();

            string FileFullPath = Server.MapPath(@"~/Data/Sampleservicedocument.xml");
            //XmlTextReader reader = new XmlTextReader(FileFullPath);

            XPathDocument xmlDoc = new XPathDocument(FileFullPath);

            XPathNavigator nav = xmlDoc.CreateNavigator();

            XmlDocument xmldoc1 = new XmlDocument();
            xmldoc1.LoadXml(nav.InnerXml.ToString());

            XmlReaderSettings rs = new XmlReaderSettings();
            rs.ProhibitDtd = true;//This property will prevent XML External Entity reference ie..when the XML input contains a reference to an external entity like below
                                  //<!DOCTYPE test SYSTEM "file:///C:/test/test.txt">
                                  //The XmlDocument object will throw an exception on loading the XML content containing this vulnerable external reference

            XmlReader reader = XmlReader.Create(FileFullPath, rs);
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(reader);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("DTD is prohibited"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('DTD Content found')", true);
                    return;
                }
            }

            Status.Text = Status.Text + "<br/>" + t.ElapsedMilliseconds + " File Loaded";
            t.Reset(); t.Start();

            int i = 0;
            // Task.Run(() =>
            //{
            while (i <= 10)
            {
                Task.Run(() =>
               {
                   //Method Call for validation
                   ValidateRequest(xmldoc);
                   Status.Text = Status.Text + "<br/><br/>  New Request " + i.ToString();
                   Status.Text = Status.Text + "<br/> " + t.ElapsedMilliseconds + "=================";
                   t.Reset(); t.Start();
                   Status.Text = Status.Text + "<br/>" + " ActionCriterias = " + ActionCriterias + "<br/> openTime = " + openTime + "<br/> IsFreefromSQLIn = " + IsFreefromSQLIn + "<br/> PageSessionObjectReset = " + PageSessionObjectReset + "<br/> pageSession = " + pageSession;

               });
                ++i;
            }
            //});

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

                XmlNode actionControl = xml.SelectSingleNode("XML/servicedocument/actionservice/actioncontrol");
                XmlNode dataNode = xml.SelectSingleNode("XML/servicedocument/data");
                pageSession = "";// Session["pageSession"].ToString()==null ?"": Session["pageSession"].ToString();

                if (actionControl != null)
                {
                    IsFreefromSQLIn = IsFreefromSQLInjection(actionControl);
                    if (!IsFreefromSQLIn)
                        return;
                }

                string actionid = actionControl.Attributes["actionid"].Value;

                if (string.IsNullOrEmpty(actionid) || actionid == "LogOn" || actionid == "SaveLanguage")
                {
                    Session["pageSession"] = null;
                }
                else if (actionid == "LogOut")
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                    return;
                }
                else if (dataNode.HasChildNodes)
                {
                    if (dataNode.FirstChild.HasChildNodes && pageSession != null)
                    {
                        ///
                        //Validating Cross site request using open time session object.
                        //if open time is not valid, application will be logging out. In some exceptional cases request will be allowed if open time is empty
                        ///
                        openTime = dataNode.FirstChild.FirstChild.Attributes["OpenTime"] == null ? string.Empty : dataNode.FirstChild.FirstChild.Attributes["OpenTime"].Value;
                        if (openTime != string.Empty)
                        {
                            PageSessionObjectReset = SetPageSessionObject(true, pageSession, openTime);
                        }
                    }
                }
                else if (dataNode.Attributes["isn"] != null && dataNode.Attributes["isn"].Value == "y" && pageSession != null)
                {
                    PageSessionObjectReset = SetPageSessionObject(false, pageSession, "");
                }
            }
        }


        //Will return true only if the actioncriteria and nextactioncriteria are valid texts
        //Validating and preventing SQL basic commands and other criteria like 1=1, 'i'='i'
        private bool IsFreefromSQLInjection(XmlNode actionControl)
        {
            string actioncriteria = Convert.ToString(actionControl.Attributes["actioncriteria"].Value).ToLower();
            string nextactioncriteria = Convert.ToString(actionControl.Attributes["nextactioncriteria"].Value).ToLower();

            ActionCriterias = actioncriteria.Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ") + " " +
                            nextactioncriteria.Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ");

            if (actioncriteria == string.Empty || nextactioncriteria == string.Empty)
            {
                return true;
            }
            //ActionCriterias = actioncriteria.Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ") + " " +
            //                nextactioncriteria.Replace(Environment.NewLine, " ").Replace("[", " ").Replace("]", " ");

            Regex SQLmatchChars = new Regex(@"((( |0)*\[?(.+)\]?( )*=( |0)*\[?\4\]?( )*(?<=\b|\]|\s)or)|(or(?=\b|\[|\s)( |0)*\[?(.+)\]?( )*=( |0)*\[?\10\]?( )*))|(--|/\*)|(\b|^)(select(\s|\t)|insert(\s|\t)|update(\s|\t)|delete(\s|\t)|drop(\s|\t)|alter(\s|\t)|sp_|xp_|exec(ute){0,1}(\s|\t)|sleep(\s|\t)|grant(\s|\t)|restore(\s|\t)|waitfor(\s|\t)|dbcc(\s|\t)|revoke(\s|\t)|truncate(\s|\t)|shutdown)", RegexOptions.Compiled);

            if (SQLmatchChars.IsMatch(ActionCriterias))
            {
                //Session["IsFirst"] = null;
                //UserLogOut();
                //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                return false;
            }
            else
            {
                return true;
            }
        }

        //Will return true only if the Session["pageSession"] is altered
        private bool SetPageSessionObject(bool dataNodeHasChildNodes, string pageSession, string openTime)
        {
            if (dataNodeHasChildNodes)
            {
                if (!pageSession.Contains(openTime))
                {
                    //UserLogOut();
                    //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", "alert('UserLogOut')", true);
                    return false;
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
            return true;
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