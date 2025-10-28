using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Updater
{

    public class GithubRelease
    {
        public class VersionInfo
        {
            /// <summary>
            /// 版本标题
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 版本号
            /// </summary>
            public string Version { get; set; }
            /// <summary>
            /// 是否是预览版本
            /// </summary>
            public bool IsPre { get; set; }
            /// <summary>
            /// 下载路径
            /// </summary>
            public string DownloadUrl { get; set; }
            /// <summary>
            /// 版本更新内容网页链接
            /// </summary>
            public string HtmlUrl { get; set; }
        }
        public class GithubModel
        {
            public string Tag_name { get; set; }
            public string Html_url { get; set; }
            public string Name { get; set; }
            public bool Prerelease { get; set; }

            public List<GithubAssetsModel> Assets { get; set; }
        }
        public class GithubAssetsModel
        {
            public string Browser_download_url { get; set; }
        }
        private   string githubUrl;
        private   string nowVersion;
        public VersionInfo Info { get; set; }

        //public event UpdaterEventHandler RequestCompleteEvent;
        //public event UpdaterEventHandler RequestErrorEvent;
        //public delegate void UpdaterEventHandler(object sender, object value);
        public GithubRelease(string githubUrl, string nowVersion)
        {
            this.githubUrl = githubUrl;
            this.nowVersion = nowVersion;
            Info = new VersionInfo();
        }
        public bool IsCanUpdate()
        {
            return !(nowVersion == Info.Version);
        }
        public async Task<VersionInfo> GetRequest()
        {
            var result = await Task.Run(() =>
              {
                  HttpWebResponse httpWebRespones = null;
                  try
                  {
                      System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                      HttpWebRequest httpWebRequest = WebRequest.Create(githubUrl) as HttpWebRequest;
                      httpWebRequest.Timeout = 60 * 1000;
                      httpWebRequest.ReadWriteTimeout = 60000;
                      httpWebRequest.AllowAutoRedirect = true;
                      httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
                      httpWebRespones = (HttpWebResponse)httpWebRequest.GetResponse();


                      using (Stream stream = httpWebRespones.GetResponseStream())
                      {
                          List<byte> lst = new List<byte>();
                          int nRead = 0;
                          while ((nRead = stream.ReadByte()) != -1) lst.Add((byte)nRead);
                          byte[] bodyBytes = lst.ToArray();

                          string body = Encoding.UTF8.GetString(bodyBytes, 0, bodyBytes.Length);

                          var data = JsonConvert.DeserializeObject<GithubModel>(body);
                          Info.IsPre = data.Prerelease;
                          Info.Title = data.Name;
                          Info.Version = data.Tag_name;
                          Info.DownloadUrl = data.Assets[0].Browser_download_url;
                          Info.HtmlUrl = data.Html_url;
                          return Info;
                      }

                  }
                  catch (Exception)
                  {
                      return null;
                  }

              });
            return result;
        }
    }
}
