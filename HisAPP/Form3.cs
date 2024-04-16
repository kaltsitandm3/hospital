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
    public partial class Form3 : Form
    {
        // 静态变量，便于Form2访问
        public static string ID;


        public Form3()
        {
            InitializeComponent();

            //初始化时对科室的选择框也进行初始化
            List<string> office_list = Function.select_col("hisbook.科室信息", "名称");
            office_list.Insert(0, "");
            comboBox2.DataSource = office_list;
        }


        //在科室的选择项改变时，医生的可选项也进行刷新
        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            string office_name = comboBox2.Text.ToString();
            List<string> doctor_list = Function.select_col("hisbook.医生信息", "姓名", "trim(科室)", office_name);
            doctor_list.Insert(0, "");
            comboBox3.DataSource = doctor_list;
        }


        // 在选择医生后，将挂号费显示在文本框中
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = Function.select_one("hisbook.医生信息", "挂号费", "姓名", comboBox3.SelectedItem.ToString());
            textBox2.Text = value;
        }


        //确认按钮功能实现，将挂号信息写入挂号信息表中
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Function.str);
            //开启连接
            ID = Function.insert_count("hisbook.病人信息库", "P");
            con.Open();
            string name = textBox1.Text.ToString();
            string sex = comboBox1.SelectedItem.ToString();
            string office = comboBox2.SelectedItem.ToString();
            string values = textBox2.Text.ToString();
            string docter_name = comboBox3.SelectedItem.ToString();
            string docter_ID = Function.select_one("hisbook.医生信息", "医生编号", "姓名", docter_name);
            string time = DateTime.Now.ToString();
            string insertcmd = "insert into hisbook.挂号信息 values ('" + ID + "','" + name + "','" + sex + "','" + office + "','" + values + "','" + docter_ID + "','" + docter_name + "','" + time + "','" + "0" + "');";
            MySqlCommand cmd = new MySqlCommand(insertcmd, con);//数据库命令
            cmd.ExecuteNonQuery();
            con.Close();//关闭连接
            MessageBox.Show("挂号成功！您的编号为" + ID + "。请继续填写个人信息。", "Successfully", MessageBoxButtons.OK);
            Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}

