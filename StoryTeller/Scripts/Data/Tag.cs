using System;
using System.Collections.Generic;
using System.Text;

//TODO : namespace StoryTeller.Data
namespace StoryTeller
{
    //コンポーネントのパラメータとなる
    /// <summary>
    /// コンポーネント用のパラメータ
    /// </summary>
    public class Tag
    {
        private string name = "";
        private string original = "";
        private Dictionary<string, string> dicParam = new Dictionary<string, string>();

        public static Tag Parse(string str)
        {
            Tag returnTag = new Tag();

            returnTag.original = str;
            StringBuilder tempStr = new StringBuilder();

            var contentStr = str.TrimStart('[').TrimEnd(']').Trim();

            int strLen = contentStr.Length;
            int charIdx = 0;

            int end_tag_index = 0;

            while (charIdx < strLen && contentStr[charIdx] != ' ')
            {
                char c = contentStr[charIdx];
                if (c == ' ')
                {
                    returnTag.name = tempStr.ToString();
                    end_tag_index = charIdx;
                    break;
                }
                else
                {
                    tempStr.Append(c);
                    charIdx++;
                }
            }

            if (returnTag.name == "")
            {
                returnTag.name = tempStr.ToString();
            }

            tempStr.Clear();
            if (charIdx < strLen)
            {
                bool equalSeen = false;
                bool inQuote = false;

                string key = "";

                for (int i = charIdx; i < strLen; i++)
                {
                    char c = contentStr[i];

                    switch(c)
                    {
                        case ' ':
                            if(inQuote)
                            {
                                tempStr.Append(c);
                            }
                            else
                            {
                                equalSeen = false;
                                if (!string.IsNullOrEmpty(key))
                                {
                                    returnTag.dicParam[key] = tempStr.ToString();
                                }
                                tempStr.Clear();
                                key = string.Empty;
                            }
                            break;
                        case '=':
                            if (inQuote)
                            {
                                tempStr.Append(c);
                            }
                            else
                            {
                                if (!equalSeen)
                                {
                                    equalSeen = true;
                                    key = tempStr.ToString();
                                    tempStr.Clear();
                                }
                            }
                            break;
                        case '\"':
                        case '\'':
                            if (!inQuote)
                            {
                                inQuote = true;
                                continue;
                            }
                            else
                            {
                                //パラメータ設定の終わり
                                equalSeen = false;
                                inQuote = false;

                                //値を登録
                                //UnityEngine.Debug.Log(string.Format("{0} : {1}", key, tempStr));
                                returnTag.dicParam[key] = tempStr.ToString();
                                tempStr.Clear();
                                key = string.Empty;
                            }
                            break;
                        default:
                            tempStr.Append(c);
                            break;
                    }



                    if (i == strLen - 1)
                    {
                        //最後の文字の場合
                        if (!string.IsNullOrEmpty(key))
                        {
                            //UnityEngine.Debug.Log(string.Format("{0} : {1}", key, tempStr));
                            returnTag.dicParam[key] = tempStr.ToString();

                        }
                    }

                }
                tempStr.Clear();
            }

            return returnTag;
        }



        public string Original
        {
            get { return this.original; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string getTagName()
        {
            return this.name;
        }

        public string getParam(string key)
        {

            if (this.dicParam.ContainsKey(key))
            {
                return this.dicParam[key];
            }
            else
            {
                return null;
            }

        }

        public Dictionary<string, string> getParamByDictionary()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> pair in this.dicParam)
            {
                dic[pair.Key] = pair.Value;
            }

            return dic;

        }
    }
}
