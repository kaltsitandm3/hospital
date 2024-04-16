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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            //初始化三个groupBox都不可视
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;

            //初始化科室选择框
            List<string> office_list = Function.select_col("hisbook.科室信息", "名称");
            office_list.Insert(0, "");
            comboBox1.DataSource = office_list;
        }


        //根据选择的按钮显示对应的groupBox
        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            groupBox4.Visible = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = true;
        }


        //确认添加的功能实现，向对应的数据库中插入数据
        private void button4_Click(object sender, EventArgs e)
        {
            string ID;
            if (Function.medicine_vacancy.Count() > 0)
            {
                ID = Function.medicine_vacancy[0].ToString();
                Function.medicine_vacancy.RemoveAt(0);
            }
            else
                ID = Function.insert_count("hisbook.药品信息", "M");
            string name = textBox1.Text;
            string value = textBox2.Text;
            string num = textBox6.Text;
            string type = comboBox2.SelectedItem.ToString();
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();//开启连接
            string str = "insert into hisbook.药品信息 VALUES('" + ID + "','" + name + "','" + value + "','"+num+"','"+ type + "');";
            MySqlCommand cmd = new MySqlCommand(str, con);//数据库命令
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("添加成功！", "Successfully", MessageBoxButtons.OK);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string ID;
            if (Function.department_vacancy.Count() > 0)
            {
                ID = Function.department_vacancy[0].ToString();
                Function.department_vacancy.RemoveAt(0);
            }
            else
                ID = Function.insert_count("hisbook.科室信息", "R");
            string name = textBox3.Text;
            string comment = "";
            comment += textBox4.Text;
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();//开启连接
            string str = "insert into hisbook.科室信息 VALUES('" + ID + "','" + name + "','" + comment + "');";
            MySqlCommand cmd = new MySqlCommand(str, con);//数据库命令
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("添加成功！", "Successfully", MessageBoxButtons.OK);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string ID;
            if (Function.docter_vacancy.Count() > 0)
            {
                ID = Function.docter_vacancy[0].ToString();
                Function.docter_vacancy.RemoveAt(0);
            }
            else
                ID = Function.insert_count("hisbook.医生信息", "D");
            string name = textBox5.Text;
            string department = comboBox1.Text;
            string value = textBox7.Text;
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();//开启连接
            string str = "insert into hisbook.医生信息 VALUES('" + ID + "','" + department + "','" + value + "','" + name + "');";
            MySqlCommand cmd = new MySqlCommand(str, con);//数据库命令
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("添加成功！", "Successfully", MessageBoxButtons.OK);
        }
    }
}
