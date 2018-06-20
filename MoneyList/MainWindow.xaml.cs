using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyList
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // 關閉視窗事件
        private void Window_Closed(object sender, EventArgs e)
        {

            // 新增一個串列裝每個項目轉成的文字
            List<string> datas = new List<string>();

            // 轉換每一個項目
            foreach (AddTask item in TaskList.Children)
            {
                // 設置空的字串
                string data = "";

                // 以"|"區隔加入data字串
                data += item.Time.Text + "|" + item.Task.Text + "|" + item.Price.Text;

                // 加入 Datas 的陣列
                datas.Add(data);
            }

            System.IO.File.WriteAllLines(@"D:\data.txt", datas);
        }

        // 按鍵事件
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            // 建立變數
            int Total = 0;

            // 按 enter 計算總支出
            if (e.Key == Key.Return)
            {
                // 計算每一個項目
                foreach (AddTask item in TaskList.Children)
                {
                    // 將價格相加
                    Total += item.itemPriceValue;
                }

                // 顯示價格
                TotalPrice.Text = Total.ToString();
            }

        }

        // 打開視窗事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 開啟檔案
            string[] lines = System.IO.File.ReadAllLines(@"C:\data.txt");
            
            // 分析檔案
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                // 建立項目
                AddTask item = new AddTask();

                // 分別讀取
                item.Time.Text = parts[0];
                item.Task.Text = parts[1];
                item.Price.Text = parts[2];

                // 放入清單
                TaskList.Children.Add(item);
            }
        }

        // 新增項目
        private void addItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 產生項目
            AddTask item = new AddTask();

            // 放入清單
            TaskList.Children.Add(item);
        }
       
    }
}
