// <copyright file="program.cs">(C)2014</copyright>
// common.libをconsole上で実行するためのアプリケーション
using System;

namespace Application
{
    /// <summary>
    /// アプリケーションクラス
    /// </summary>
    public class consoleConnection
    {
        private const string strParameterIsNotEnough = "URLが指定されていません";
        private const int retParameterIsNotEnough = -1;
        /// <summary>
        /// main proc
        /// </summary>
        public static int Main(string[] args)
        {
            Network.Result result = new Network.Result();
            result.returnCode = retParameterIsNotEnough;
            result.data = strParameterIsNotEnough;
            if (args.Length >= 1)
            {
                Network.Connection con = new Network.Connection(args[0]);
                con.GetStatusCode(null, ref result);
            }
            Console.WriteLine(result.data);
            return result.returnCode;
        }
    }
}
