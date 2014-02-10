// <copyright file="security.cs">(C)2013</copyright>
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Security
{
    /// <summary>
    /// SSL通信で信頼されない証明書を回避するための設定
    /// http://www.atmarkit.co.jp/fdotnet/dotnettips/867sslavoidverify/sslavoidverify.html
    /// </summary>
    public class Settings
    {
        private static Settings instance_ = new Settings();
        /// <summary>
        /// public class Connectionのコンストラクターで一度だけ呼びだしたいのでSingletonにする
        /// </summary>
        public static Settings GetInstance()
        {
            return instance_;
        }
        /// <summary>
        /// コンストラクター
        /// </summary>
        public Settings()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);
        }
        /// <summary>
        /// trueだけを返すだけのコールバック
        /// </summary>
        private bool OnRemoteCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;  // 「SSL証明書の使用は問題なし」と示す
        }
    }
}
