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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();

            //初始化时对科室的选择框也进行初始化
            List<string> office_list = Function.select_col("hisbook.科室信息", "名称");
            office_list.Insert(0, "");
            comboBox2.DataSource = office_list;
        }


        //确认按钮功能实现，实现注册功能
        private void button1_Click(object sender, EventArgs e)
        {
            string ID = "";
            ID += textBox1.Text.ToString();
            string password = "";
            password += textBox2.Text.ToString();
            string re_password = "";
            re_password += textBox3.Text.ToString();
            if (Function.select_one("hisbook.医生信息", "医生编号", "医生编号", ID).Equals(""))
            {
                //MessageBox.Show("医生信息未录入！", "Fail", MessageBoxButtons.OK);
            }
            if (!password.Equals(re_password))
            {
                MessageBox.Show("两次密码不一致！", "Fail", MessageBoxButtons.OK);

            }
            else
            {
                MessageBox.Show("注册成功", "Successfully", MessageBoxButtons.OK);
                MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
                con.Open();//开启连接
                string insertcmd = @"insert into hisbook.医生密码表 VALUES ('" + ID + "','" + password + "');";
                MySqlCommand cmd = new MySqlCommand(insertcmd, con);//数据库命令
                cmd.ExecuteNonQuery();


                string insertcmd1 = @"insert into hisbook.医生信息 VALUES ('" + ID + "','" + comboBox2.SelectedItem.ToString() + "','36','"+textBox4.Text+"');";
                MySqlCommand cmd1 = new MySqlCommand(insertcmd1, con);//数据库命令
                cmd1.ExecuteNonQuery();



                con.Close();
                Hide();
            }
        }


        //密码可视状态的切换
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
                textBox2.PasswordChar = '\0';
            else
                textBox2.PasswordChar = '*';
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
                textBox3.PasswordChar = '\0';
            else
                textBox3.PasswordChar = '*';
        }
    }
}
