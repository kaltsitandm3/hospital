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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();

            //初始化三个groupBox都不可视
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
        }


       
        //确认删除的功能实现，向对应的数据库中插入数据
        private void button4_Click(object sender, EventArgs e)
        {
            string ID = textBox1.Text;
            string name = textBox2.Text;
            if (name.Equals(Function.select_one("hisbook.药品信息", "名称", "编号", ID)))
            {
                Function.delete_data("hisbook.药品信息", "编号", ID);
                Function.medicine_vacancy.Add(ID);
                MessageBox.Show("删除成功！", "Successfully", MessageBoxButtons.OK);
                Hide();
            }
            else
            {
                MessageBox.Show("删除失败！请确定编号和名称是否对应。", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string ID = textBox3.Text;
            string name = textBox4.Text;
            string docter_name = Function.select_one("hisbook.医生信息", "姓名", "科室", name); 
            if(!docter_name.Equals(""))
            {
                MessageBox.Show("该科室下有医生，无法删除！", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (name.Equals(Function.select_one("hisbook.科室信息", "名称", "编号", ID)))
            {
                Function.delete_data("hisbook.科室信息", "编号", ID);
                Function.department_vacancy.Add(ID);
                MessageBox.Show("删除成功！", "Successfully", MessageBoxButtons.OK);
                Hide();
            }
            else
            {
                MessageBox.Show("删除失败！请确定编号和名称是否对应。", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string ID = textBox5.Text;
            string name = textBox6.Text;
            if (name.Equals(Function.select_one("hisbook.医生信息", "姓名", "医生编号", ID)))
            {
                Function.delete_data("hisbook.医生信息", "医生编号", ID);
                Function.docter_vacancy.Add(ID);
                MessageBox.Show("删除成功！", "Successfully", MessageBoxButtons.OK);
                Hide();
            }
            else
            {
                MessageBox.Show("删除失败！请确定编号和名称是否对应。", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

    }
}
