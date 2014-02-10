// <copyright file="commandList.cs">(C)2013</copyright>
using System;
using System.Collections.Generic;

namespace Command
{
    /// <summary>
    /// コマンドのリスト管理
    /// 要求定義：同一コマンドが複数存在しない仕様
    /// 　　　　：パラメーターはテストのために変更可能
    /// </summary>
    public class cList
    {
        private List<cParam> cmdList = new List<cParam>();
        private string separate = ",";
        /// <summary>
        /// コンストラクター
        /// </summary>
        public cList()
        {
        }
        /// <summary>
        /// セパレート文字列を設定する
        /// </summary>
        public void SetSeparate(string sep)
        {
            separate = sep;
        }
        /// <summary>
        /// パラメータを設定する string , string
        /// </summary>
        public void SetParameter(string cmdname, string param)
        {
            int index = cmdList.FindIndex(delegate(cParam s)
            {
                return s.GetCommand() == cmdname;
            });
            if (index == -1)
            {
                cmdList.Add(new cParam(cmdname, param));
            }
            else
            {
                cmdList[index].Set(cmdname, param);
            }
        }
        /// <summary>
        /// パラメータを設定する string , int
        /// </summary>
        public void SetParameter(string cmdname, int param)
        {
            SetParameter(cmdname, param.ToString());
        }
        /// <summary>
        /// パラメータを設定する string , int
        /// </summary>
        public void SetParameter(string cmdname)
        {
            SetParameter(cmdname, null);
        }
        /// <summary>
        /// パラメータを取得する
        /// </summary>
        public string GetParameter()
        {
            string result = null;
            for (int i = 0; i < cmdList.Count ; ++i)
            {
                if (cmdList[i].GetCommand() != null)
                {
                    result += separate + cmdList[i].Get();
                }
            }
            return result;
        }
    }
}
