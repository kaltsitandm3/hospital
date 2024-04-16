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
using HisAPP;

namespace HisAPP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            //将挂号生成的编号直接填入此处
            textBox2.Text = Form3.ID;
        }


        //确认按钮，实现了将信息录入病人信息库的功能
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string ID = textBox2.Text.ToString();
            string name = Function.select_one("hisbook.挂号信息", "姓名", "病人编号", ID);
            if (name == "")
                MessageBox.Show("未查询到该编号的挂号信息！", "Fail", MessageBoxButtons.OK);
            else
            {
                string sex = Function.select_one("hisbook.挂号信息", "性别", "病人编号", ID);
                string year = textBox4.Text.ToString();
                string phone = textBox6.Text.ToString();
                string strcmd = @"insert into hisbook.病人信息库 VALUES ('" + ID + "','" + name + "','" + sex + "','" + year + "','" + phone + "');";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
                cmd.ExecuteNonQuery();
                MessageBox.Show("录入成功！", "Successfully", MessageBoxButtons.OK);
            }
            con.Close();
            Hide();
        }
    }
}
