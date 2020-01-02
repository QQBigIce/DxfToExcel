using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CADToExcel.Model.DAO;
using CADToExcel.Model.Service;

namespace CADToExcel.View
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage 
    {

        public MainPage()
        {
            InitializeComponent();
        }

        // 选择DXF文件按钮
        private void DXFButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.dxfPath.Text = openFileDialog.FileName;
            }
        }

        // 选择Excel模板按钮
        private void FromworkButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.configPath.Text = openFileDialog.FileName;
            }
           
        }

        // 选择配置文件按钮
        private void ConfigButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.configPath.Text = openFileDialog.FileName;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // 读取Dxf文件内块的种类和数量
            DxfMapper dxf = new DxfMapper();
            dxf.ReadFile(dxfPath.Text);
            Dictionary<string, int> blocks = dxf.BlockCount();
            // 读取配置文件中的键值对，把块名替换为编号
            ConfigChange dte = new ConfigChange();
            dte.MainPage = this;    // 把当前页面传给工具类，工具类可以调用本页面内输出窗口
            dte.ChangeMap(configPath.Text);
            Dictionary<string, int> datas = dte.BlockToId(blocks);
            // 把数据写入到模板中，并生成新文件在D盘根目录下
            
            Dictionary<string, int> haveNo = ExcelMapper.WriteData(formworkPath.Text, datas);
            if (haveNo.Count != 0) 
            {
                outputBox.AppendText("=======================\n");
                this.outputBox.AppendText(haveNo + "没有在模板中找到对应的编号\n");
            }
            MessageBox.Show("转换完成");
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
