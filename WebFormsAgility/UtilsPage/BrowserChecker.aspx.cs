using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsAgility.UtilsPage
{
    public partial class BrowserChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    public static class BrowserInfoHelper
    {
        public static string GetUserAgent()
        {
            return HttpContext.Current.Request.UserAgent;
        }

        public static HttpBrowserCapabilities GetCapabilities()
        {
            var capabilities = new HttpBrowserCapabilities();

            var hashtable = new Hashtable(180, StringComparer.OrdinalIgnoreCase);
            hashtable[string.Empty] = GetUserAgent();
            capabilities.Capabilities = hashtable;

            var capabilitiesFactory = new BrowserCapabilitiesFactory();
            var headers = new NameValueCollection();
            capabilitiesFactory.ConfigureBrowserCapabilities(headers, capabilities);
            capabilitiesFactory.ConfigureCustomCapabilities(headers, capabilities);

            return capabilities;
        }
    }
}