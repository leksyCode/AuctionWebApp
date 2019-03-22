using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AuctionWebApp.Common
{
    public class GetImageFromGoogle
    {
        public static string GetHtmlCode(string slotQuety)
        {
            string url = "https://www.google.com/search?q=" + slotQuety  + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }

        public static string GetUrls(string html)
        {
            var urls = new List<string>();

            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
            ndx++;
            int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
            return html.Substring(ndx, ndx2 - ndx);
        }
    }
}
