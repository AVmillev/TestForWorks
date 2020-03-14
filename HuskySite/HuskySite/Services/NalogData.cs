using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HuskySite.Services
{
    public class NalogData : INalogData
    {
        public Task<string> GetReceiptAsinch(string fd, string fn, string fp)
        {
            return Task.Run(() => { return HttpPost(@"https://proverkacheka.nalog.ru:9999/v1/inns/*/kkts/*/fss/"+fn+@"/tickets/"+fd+@"?fiscalSign="+fp+"&sendToEmail=no", ""); });
        }

        private static string HttpPost(string URI, string Parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(URI);

            request.Method = "get";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", "Basic Kzc5MDI0NDI4OTU1OjQ1MTk2NQ==");
            request.Headers.Add("Device-Id", "fotwRYR-Psg:APA91bHKrWBVXLLguRcqE1NhZ01m0ZAXkXEEdci_FsS1EqjObXCX56oBVAOhwZXViG7fBQREUg20t5zpjo4hsVbh1lEoforsH-V967dypgJ9HCRALn1BWH63ZkpUUqI40dil5NLALdxb");
            request.Headers.Add("Device-OS", "Adnroid 5.1.1");
            request.Headers.Add("Version", "2");
            request.Headers.Add("ClientVersion", "1.4.4.1");
            //request.Headers.Add("Connection", "Keep-Alive");
            request.Headers.Add("Accept-Encoding", "gzip");
            request.UserAgent = "okhttp/3.0.1";

            var response = (HttpWebResponse)request.GetResponse();

            

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
