using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testCommon
{
    /// <summary>
    /// commonライブラリのテスト
    /// </summary>
    [TestClass]
    public class connection
    {
        /// <summary>
        /// connectionクラスのテスト
        /// </summary>
        [TestMethod]
        public void StatusCode()
        {
            string testurl = "http://google.co.jp";
            Network.Result result     = new Network.Result();
            Network.Connection con = new Network.Connection(testurl);

            // 200 test
            con.GetStatusCode(null, ref result);
            Assert.AreSame(testurl, result.url);
            Assert.IsTrue(result.data.Length > 0);
            Assert.IsNull(result.header);
            Assert.AreEqual(200, result.returnCode);

            // 404 test
            testurl += "/aaaa";
            con.ChangeBaseURL(testurl);
            con.GetStatusCode(null, ref result);
            Assert.AreSame(testurl, result.url);
            Assert.IsTrue(result.data.Length > 0);
            Assert.IsNull(result.header);
            Assert.AreEqual(404, result.returnCode);

            // remote名解決エラー
            testurl = "http://hogehoge"; // 存在しないドメインを指定する
            con.ChangeBaseURL(testurl);
            con.GetStatusCode(null, ref result);
            Assert.AreSame(testurl, result.url);
            Assert.IsTrue(result.data.Length > 0);
            Assert.IsNull(result.header);
            Assert.AreEqual(-1, result.returnCode);
        }
        /// <summary>
        /// Listとの対比用の文字列生成
        /// </summary>
        private string ListBuild(string[] passStrings, int max, string separateList, string separateCmd, int ofsIndex, string strChange)
        {
            string sameCheck = null;

            for (int i = 0; i < max; ++i)
            {
                string temp = i == ofsIndex ? strChange : passStrings[1];
                sameCheck += separateList + passStrings[0] + i.ToString() + separateCmd + temp + i.ToString();
            }
            return sameCheck;
        }
        /// <summary>
        /// コマンドリストのテスト
        /// </summary>
        [TestMethod]
        public void Lists()
        {
            // パラメータ入力テスト
            string separateList = ",";
            string separateCmd = ":";
            string[] passStrings = { "cmd", "param" };
            string strChange = null;
            int max = 3;
            int ofsIndex = -1;
            Command.cList list = new Command.cList();

            // 初期化状態のテスト
            Assert.IsNull(list.GetParameter());

            // パラメータの作成テスト
            for (int i = 0; i < max; ++i)
            {
                list.SetParameter(passStrings[0] + i.ToString(), passStrings[1] + i.ToString());
            }
            Assert.AreEqual(ListBuild(passStrings, max, separateList, separateCmd, ofsIndex, strChange), list.GetParameter());

            // セパレートを入れ替えるテスト
            separateList = "&";
            list.SetSeparate(separateList);
            Assert.AreEqual(ListBuild(passStrings, max, separateList, separateCmd, ofsIndex, strChange), list.GetParameter());

            // パラメーターを入れ替えるテスト
            strChange = "changed";
            ofsIndex = 1;
            list.SetParameter(passStrings[0] + ofsIndex.ToString(), strChange + ofsIndex.ToString());
            Assert.AreEqual(ListBuild(passStrings, max, separateList, separateCmd, ofsIndex, strChange), list.GetParameter());

            // パラメータを削除するテスト
            strChange = null;
            ofsIndex = 1;
            list.SetParameter(passStrings[0] + ofsIndex.ToString(), strChange + ofsIndex.ToString());
            Assert.AreEqual(ListBuild(passStrings, max, separateList, separateCmd, ofsIndex, strChange), list.GetParameter());
        }
    }
}
