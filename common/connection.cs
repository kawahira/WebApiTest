// <copyright file="connection.cs">(C)2013</copyright>
// �ėp�n�@�\�F�ʐMwrapper
using System;
using System.Net;
using System.IO;
using System.Text;

namespace Network
{
    /// <summary>
    /// testRunner�̎d�l����ۂɂ͓����ɓ����Ȃ����Ƃ肠��������instance�O���
    /// ������s�\�ɂ��Ă����B
    /// </summary>
    public class Connection
    {
        private const int timeoutLimit = 1000000;   // 16�����ő�ɂ���
        /// <summary>
        /// �R���X�g���N�^�[
        /// </summary>
        public Connection(string url)
        {
            Security.Settings.GetInstance();
            ChangeBaseURL(url);
        }
        /// <summary>
        /// �Ăяo��URL��ύX����
        /// </summary>
        public void ChangeBaseURL(string url) { baseurl = url; }
        /// <summary>
        /// �w��REST API�̌Ăяo�����s��
        /// </summary>
        public int GetStatusCode(string extend, ref Result result)
        {
            result.Initialize();
            result.url = baseurl;
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(result.url);
            //���\�b�h��POST���w��
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
                    // ���������z�X�g���Ȃǂ������ł��Ȃ����
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
