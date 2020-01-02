using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CADToExcel.View;

namespace CADToExcel.Model.Service
{
    class ConfigChange
    {
        public Dictionary<string, string> ConfigDict { get; set; }
        public MainPage MainPage { get; set; }

        // 获得配置文件里面的块名和型号的键值对
        public void ChangeMap(string path)
        {
            ConfigDict = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                for (string line = ""; (line = sr.ReadLine()) != null;)
                {
                    string k = line.Split(' ')[0];
                    string v = line.Split(' ')[1];
                    ConfigDict.Add(k, v);
                }
            }
        }

        // 把块和块数量的字典转化为型号和数量的字典
        public Dictionary<string, int> BlockToId(Dictionary<string, int> blockDict)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kvp in blockDict)
            {
                bool result = false;
                foreach (KeyValuePair<string, string> configKvp in this.ConfigDict)
                {
                    if (kvp.Key.Equals(configKvp.Key))
                    {
                        dict.Add(configKvp.Value, kvp.Value);
                        result = true;
                        break;
                    }
                 
                }
                if (result == false) MainPage.outputBox.AppendText(kvp.Key + "没有在配置文件中找到\n");
            }
            return dict;
        }

    }
}
