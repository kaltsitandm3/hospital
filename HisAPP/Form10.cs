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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }



        // 检测radiobutton的选择并执行对应操作
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (MessageBox.Show(@"当前数据库将被覆盖！", @"提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Import();
                }
            }
            else if (radioButton2.Checked)
            {

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "请选择存储备份的文件夹";
                //设置默认文件类型显示顺序
                openFileDialog1.FilterIndex = 1;
                //保存对话框是否记忆上次打开的目录
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Import(openFileDialog1.FileName);
                }
            }
        }


        //导入备份
        private void Import(params string[] path)
        {
            string file_path;
            if (path.Length == 0)
            {
                file_path = ".//mysql/default_backup.sql";
            }
            else
            {
                file_path = path[0];
            }
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = con;
                        //执行还原
                        mb.ImportFromFile(file_path);
                        con.Close();
                        MessageBox.Show("已还原");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Hide();
        }

    }
}
