// <copyright file="commandParam.cs">(C)2013</copyright>
using System;

namespace Command
{
    /// <summary>
    /// コマンドとパラメーターのセットを管理するクラス
    /// </summary>
    public class cParam
    {
        private string command = null;
        private string parameter = null;
        private string separate   = ":";
        public cParam()
        {
            Set(null, null);
        }
        public cParam(string cmd, string param)
        {
            Set(cmd, param);
        }
        /// <summary>
        /// コマンドとパラメーターを設定する
        /// </summary>
        public void Set(string cmd, string param)
        {
            command = cmd;
            parameter = param;
        }
        /// <summary>
        /// セパレート文字列を設定する
        /// </summary>
        public void SetSeparate(string sep)
        {
            separate = sep;
        }
        /// <summary>
        /// コマンドを取得する(判定用）
        /// </summary>
        public string GetCommand()
        {
            return command;
        }
        /// <summary>
        /// コマンドとパラメーターを利用可能な状態で取得する
        /// </summary>
        public string Get()
        {
            if (parameter != null)
            {
                return GetCommand() + separate + parameter;
            }
            else
            {
                return GetCommand();
            }
        }
    }
}
