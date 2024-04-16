using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HisAPP
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }


        
        //检测radiobutton的选择并执行对应操作
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if(MessageBox.Show(@"备份路径默认在当前程序下", @"提示", MessageBoxButtons.YesNo)== DialogResult.Yes)
                    Export();
            }
            else if (radioButton2.Checked)
            {

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "请选择存储备份的文件夹";
                //设置默认文件类型显示顺序
                saveFileDialog1.FilterIndex = 1;
                //保存对话框是否记忆上次打开的目录
                saveFileDialog1.RestoreDirectory = true;
                //设置文件类型
                saveFileDialog1.Filter = "文本文件(*.txt)|*.txt|SQL Text File(*.sql)|*.sql|所有文件(*.*)|*.*";
                saveFileDialog1.FileName= DateTime.Now.ToString().Replace("/", "-").Replace(":",".")+"_backup.sql";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //获得文件路径
                    Export(saveFileDialog1.FileName);
                }
            }
        }


        //导出数据库
        public void Export(params string[] path)
        {
            string file_path;
            if (path.Length==0)
            {
                // 显示备份路径提示信息
                file_path = ".//mysql/default_backup.sql";
            }
            else
            {
                file_path = path[0];
            }
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();
            // 获取当前时间并将斜杠替换为短横线
            // 设置备份文件路径
            using (var cmd = new MySqlCommand())
            {
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    // 设置数据库连接
                    cmd.Connection = con;
                    // 导出数据库到文件
                    mb.ExportToFile(file_path);
                    con.Close();
                    // 显示备份成功信息
                    MessageBox.Show("备份成功！");
                }
            }
            Hide();
        }

    }
}
