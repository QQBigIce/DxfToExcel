using netDxf;
using netDxf.Entities;
using netDxf.Header;
using System;
using System.Collections.Generic;
using System.Text;

namespace CADToExcel.Model.DAO
{
    class DxfMapper
    {
        public DxfDocument Dxf { get; set; }

        // 读取Dxf文件
        public void ReadFile(string path)
        {
            DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(path);
            if (dxfVersion < DxfVersion.AutoCad2000) return;
            this.Dxf = DxfDocument.Load(path);
        }

        // 读取Dxf文件内设备块并计数，返回计数的字典
        public Dictionary<string, int> BlockCount()
        {
            IEnumerable<Insert> inserts = Dxf.Inserts;
            List<string> list = new List<string>();
            foreach (Insert item in inserts)
            {
                list.Add(item.Block.Name);
            }
            Dictionary<string, int> blockCount = ItemCount(list);
            return blockCount;
        }

        // 统计集合内每种元素的个数
        private Dictionary<string, int> ItemCount(List<string> list)
        {
            Dictionary<string, int> countDict = new Dictionary<string, int>();
            foreach (string item in list)
            {
                if (countDict.ContainsKey(item))
                {
                    countDict[item]++;
                }
                else
                {
                    countDict[item] = 1;
                }
            }
            return countDict;
        }
    }
}
