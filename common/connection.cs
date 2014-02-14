// <copyright file="connection.cs">(C)2013</copyright>
// 汎用系機能：通信wrapper
using System;
using System.Net;
using System.IO;
using System.Text;

namespace Network
{
    /// <summary>
    /// testRunnerの仕様上実際には同時に動かないがとりあえず複数instance前提で
    /// 並列実行可能にしておく。
    /// </summary>
    public class Connection
    {
        private const int timeoutLimit = 1000000;   // 16分を最大にする
        /// <summary>
        /// コンストラクター
        /// </summary>
        public Connection(string url)
        {
            Security.Settings.GetInstance();
            ChangeBaseURL(url);
        }
        /// <summary>
        /// 呼び出しURLを変更する
        /// </summary>
        public void ChangeBaseURL(string url) { baseurl = url; }
        /// <summary>
        /// 指定REST APIの呼び出しを行う
        /// </summary>
        public int GetStatusCode(string extend, ref Result result)
        {
            result.Initialize();
            result.url = baseurl;
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(result.url);
            //メソッドにPOSTを指定
            webreq.Timeout = timeoutLimit;
            webreq.Method = "GET";
            if (extend != null && extend.Length > 0)
            {
                webreq.Method = "POST";
                webreq.ContentLength = extend.Length;
                System.IO.Stream reqStream = webreq.GetRequestStream();
                reqStream.Write(Encoding.UTF8.GetBytes(extend), 0, extend.Length);
                reqStream.Close();
            }
            HttpWebResponse webres = null;
            try
            {
                webres = (HttpWebResponse)webreq.GetResponse();
                Stream st = webres.GetResponseStream();
                Encoding enc = Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(st, enc);
                result.data = sr.ReadToEnd();
                sr.Close();
                result.returnCode = (int)webres.StatusCode;
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errres = (HttpWebResponse)ex.Response;
                    result.returnCode = (int)errres.StatusCode;
                }
                else
                {
                    // そもそもホスト名などが解決できない問題
                    result.returnCode = -1;
                }
                result.data = ex.Message;
            }
            finally
            {
                if (webres != null)
                {
                    webres.Close();
                }
            }
            return result.returnCode;
        }
        private string baseurl;
    }
}
