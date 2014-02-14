// <copyright file="list.cs">(C)2013</copyright>
using System;
using System.Collections.Generic;

namespace Command
{
    /// <summary>
    /// コマンドとパラメーターをペアで保持する
    /// </summary>
    public class Pair<K, V>
    {
        private K key;
        private V val;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Pair(K k, V v)
        {
            key = k;
            val = v;
        }
        /// <summary>
        /// Key情報(First)
        /// </summary>
        public K Key { get { return this.key; } set { this.key = value; } }
        /// <summary>
        /// Value情報(Sencond)
        /// </summary>
        public V Value { get { return this.val; } set { this.val = value; } }
    }
    /// <summary>
    /// コマンドのリストの文字列変換機能付き
    /// 要求定義：同一コマンドが複数存在しない仕様
    /// 　　　　：パラメーターはテストのために変更可能
    /// </summary>
    public class PairList
    {
        private List<Pair<string, string>> pair = new List<Pair<string, string>>();
        private string separatePair = ",";
        private string separateParam = ":";
        /// <summary>
        /// セパレート文字列を設定する
        /// </summary>
        public void SetSeparate(string sep)
        {
            separatePair = sep;
        }
        /// <summary>
        /// パラメータを設定する string , string
        /// </summary>
        public void Set(string cmdname, string param)
        {
            int index = pair.FindIndex(delegate(Pair<string, string> s)
            {
                return s.Key == cmdname;
            });
            if (index == -1)
            {
                pair.Add(new Pair<string, string>(cmdname, param));
            }
            else
            {
                pair[index].Key = cmdname;
                pair[index].Value = param;
            }
        }
        /// <summary>
        /// パラメータを設定する string , int
        /// </summary>
        public void Set(string cmdname, int param)
        {
            Set(cmdname, param.ToString());
        }
        /// <summary>
        /// パラメータを設定する string , int
        /// </summary>
        public void Set(string cmdname)
        {
            Set(cmdname, null);
        }
        /// <summary>
        /// パラメータを取得する
        /// </summary>
        public string Get()
        {
            string result = null;
            for (int i = 0; i < pair.Count; ++i)
            {
                if (pair[i].Key != null)
                {
                    result += separatePair + pair[i].Key;
                }
                if (pair[i].Value != null)
                {
                    result += separateParam + pair[i].Value;
                }
            }
            return result;
        }
    }
}
