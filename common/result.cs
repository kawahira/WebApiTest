// <copyright file="result.cs">(C)2013</copyright>
// 汎用系機能：Connectionクラス利用時の接続後の情報取得
using System;

namespace Network
{
    /// <summary>
    /// 接続後の結果取得と利用可能状態への変換
    /// </summary>
    public class Result
    {
        /// <summary>
        /// header
        /// </summary>
        public string header { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// returnCode
        /// </summary>
        public int returnCode { get; set; }
        /// <summary>
        /// 初期化 ( refで利用されるので利用側の初期化用）
        /// </summary>
        public void Initialize()
        {
            header = null;
            data = null;
            url = null;
            returnCode = -1;
        }
    }
}
