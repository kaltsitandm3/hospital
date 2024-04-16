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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            //只显示主界面
            hide_all();
            tabPage0.Parent = tabControl1;
        }


        //信息查询界面的一键查询功能实现
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = @"SELECT  * FROM hisbook.病人信息库;";//SQL语句 
            MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView1.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接
        }


        //文本框失去焦点后变灰体默认提示词
        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "请输入姓名";
                textBox1.ForeColor = Color.Gray;
            }
            else
            {
                textBox1.ForeColor = Color.Black;
            }
        }
        private void textBox2_LostFocus(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "请输入年龄";
                textBox2.ForeColor = Color.Gray;
            }
            else
            {
                textBox2.ForeColor = Color.Black;
            }
        }
        private void textBox3_LostFocus(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "请输入编号";
                textBox3.ForeColor = Color.Gray;
            }
            else
            {
                textBox3.ForeColor = Color.Black;
            }
        }
        private void textBox5_LostFocus(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "请输入电话";
                textBox5.ForeColor = Color.Gray;
            }
            else
            {
                textBox5.ForeColor = Color.Black;
            }
        }
        private void textBox6_LostFocus(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "请输入药品名称";
                textBox6.ForeColor = Color.Gray;
            }
        }


        //文本框被选中后默认提示词消失，字体变黑色
        private void textBox1_GotFocus(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "请输入姓名")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }
        private void textBox2_GotFocus(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "请输入年龄")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }
        private void textBox3_GotFocus(object sender, MouseEventArgs e)
        {
            if (textBox3.Text == "请输入编号")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }
        private void textBox5_GotFocus(object sender, MouseEventArgs e)
        {
            if (textBox5.Text == "请输入电话")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }
        private void textBox6_GotFocus(object sender, MouseEventArgs e)
        {
            if (textBox6.Text == "请输入药品名称")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }


        //信息查询的性别框，失去焦点时选择框提示默认字体
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Text = "请输入性别";
                comboBox1.ForeColor = Color.Gray;
            }
        }


        //信息查询的性别框，选中项更换时，将字体改回黑色
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.ForeColor = Color.Black;
        }


        //信息查询界面的具体查询功能实现
        private void button3_Click(object sender, EventArgs e)
        {
            string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string ID = textBox3.Text.ToString();
            string name = textBox1.Text.ToString();
            string sex = comboBox1.Text.ToString();
            string year = textBox2.Text.ToString();
            string phone = textBox5.Text.ToString();
            string[] Text = { ID, name, sex, year, phone };
            string[] Textname = { "编号", "姓名", "性别", "年龄", "电话" };
            int num = 0;
            string strcmd = "SELECT * FROM hisbook.病人信息库";//SQL语句
            for (int i = 0; i < 5; i++)
            {
                if (Text[i].Equals("请输入" + Textname[i]))
                    continue;
                if (num == 0)
                {
                    strcmd += " where";
                    num++;
                }
                else
                    strcmd += " and";
                if (i <= 1)
                {
                    strcmd = strcmd + " " + Textname[i] + " like '%" + Text[i] + "%'";
                }
                else
                    strcmd = strcmd + " " + Textname[i] + " = '" + Text[i] + "'";
            }
            strcmd += ";";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView1.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接
        }


        //管理系统的查看全部药品的功能实现
        private void button4_Click(object sender, EventArgs e)
        {
            string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = @"SELECT  * FROM hisbook.药品信息;";//SQL语句 
            MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView2.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接
        }


        //管理系统的药品编号查询点击功能，将隐藏的确认查询显示
        private void button6_Click(object sender, EventArgs e)
        {
            textBox6.Visible = true;
            button7.Visible = true;
        }


        //管理系统的确认查询，查询药品编号，支持模糊查询
        private void button7_Click(object sender, EventArgs e)
        {
            string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string Text = textBox6.Text.ToString();
            string strcmd = @"SELECT  * FROM hisbook.药品信息 where 编号 like '%" + (Text.Equals("请输入药品名称") ? "" : Text) + "%';";//SQL语句 
            MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView2.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接
            textBox6.Visible = false;
            button7.Visible = false;
        }


        //查询系统的返回键
        private void only_parent0()
        {
            hide_all();
            tabPage0.Parent = tabControl1;
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void button22_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void button24_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void button25_Click(object sender, EventArgs e)
        {
            only_parent0();
        }


        //挂号系统的我要挂号，弹出填写挂号信息的窗口form3
        private void button12_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }


        //信息查询页面的确认修改按钮，支持在dataGridView的修改返回到数据库
        private void button13_Click(object sender, EventArgs e)
        {
            string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();
            string insertQuery0 = string.Format("truncate table 病人信息库;");
            MySqlCommand command0 = new MySqlCommand(insertQuery0, con);
            command0.ExecuteNonQuery();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // 构建INSERT语句
                if (!row.IsNewRow)
                {
                    if ((row.Cells[0].Value != null && row.Cells[0].Value.ToString() != "") && (row.Cells[1].Value != null && row.Cells[1].Value.ToString() != ""))
                    {
                        string insertQuery = string.Format(@"INSERT INTO hisbook.病人信息库
                              VALUES('{0}', '{1}', '{2}', '{3}', '{4}'); ",
                            row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value);

                        // 执行INSERT语句
                        MySqlCommand command = new MySqlCommand(insertQuery, con);
                        command.ExecuteNonQuery();
                    }
                    else if ((row.Cells[0].Value == null || row.Cells[0].Value.ToString() == "") && (row.Cells[1].Value != null || row.Cells[1].Value.ToString() != ""))
                    {
                        row.Cells[0].Value = Function.insert_count("hisbook.病人信息库", "P");
                        string insertQuery = string.Format(@"INSERT INTO hisbook.病人信息库
                              VALUES('{0}', '{1}', '{2}', '{3}', '{4}'); ",
                            row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value);

                        // 执行INSERT语句
                        MySqlCommand command = new MySqlCommand(insertQuery, con);
                        command.ExecuteNonQuery();
                    }
                }
            }
            button13.Visible = false;
        }


        //在数据库的内容被更改后，显示出隐藏的确认修改按钮
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            button13.Visible = true;
        }


        //管理系统中查看全部医生信息的功能实现
        private void button17_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();//开启连接
            string strcmd = @"SELECT  * FROM hisbook.医生信息;";//SQL语句 
            MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView2.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接
        }


        //管理系统中查看全部科室信息的功能实现
        private void button18_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();//开启连接
            string strcmd = @"SELECT  * FROM hisbook.科室信息;";//SQL语句 
            MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView2.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接
        }

        //管理系统的添加信息按钮，点击后打开负责添加信息的新界面form4
        private void button15_Click(object sender, EventArgs e)
        {
            Form Form4 = new Form4();
            Form4.ShowDialog();
        }


        //管理系统的删除信息按钮，点击后打开负责添删除信息的新界面form5
        private void button16_Click(object sender, EventArgs e)
        {
            Form Form5 = new Form5();
            Form5.ShowDialog();
        }


        //结算系统的查询按钮，要求编号与姓名匹配时才可查询
        private void button19_Click(object sender, EventArgs e)
        {
            string ID = textBox4.Text;
            string name = textBox18.Text;
            if (!name.Equals(Function.select_one("hisbook.结算信息", "姓名", "病人编号", ID)))
                MessageBox.Show("编号与姓名不匹配！", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                textBox19.Text = Function.select_one("hisbook.结算信息", "挂号费", "病人编号", ID);
                textBox20.Text = Function.select_one("hisbook.结算信息", "药品费", "病人编号", ID);
                textBox21.Text = Function.select_one("hisbook.结算信息", "总费用", "病人编号", ID);
                string IF = Function.select_one("hisbook.结算信息", "是否结算", "病人编号", ID);
                if (IF.Equals("1"))
                {
                    textBox22.Text = "已结算";
                    button20.Visible = false;
                }
                else
                {
                    textBox22.Text = "未结算";
                    button20.Visible = true;
                }
            }

        }


        //结算系统的点击支付按钮，弹出一个新界面
        private void button20_Click(object sender, EventArgs e)
        {
            Form form6 = new Form6();
            form6.ShowDialog();
        }


        //诊断系统的登录功能实现
        private void button5_Click(object sender, EventArgs e)
        {
            string ID = textBox7.Text.ToString(); ;
            string password = textBox8.Text.ToString();
            if ((!ID.Equals("")) && password.Equals(Function.select_one("hisbook.医生密码表", "密码", "医生编号", ID)))
            {
                Function.login_if = true;
                Function.login_ID = ID;
                Form form7 = new Form7();
                form7.ShowDialog();
            }
            else if (Function.login_if)
            {
                Form form7 = new Form7();
                form7.ShowDialog();
            }
            else if (Function.select_one("hisbook.医生密码表", "密码", "医生编号", ID).Equals(""))
            {
                MessageBox.Show("账号错误！", "Fail", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("密码错误！", "Fail", MessageBoxButtons.OK);
            }
            textBox7.Text = "";
            textBox8.Text = "";
        }


        //管理系统的医生注册按钮，弹出注册界面
        private void button21_Click(object sender, EventArgs e)
        {
            Form form8 = new Form8();
            form8.ShowDialog();
        }


        //隐藏所有选项卡
        private void hide_all()
        {
            tabPage0.Parent = null;
            tabPage1.Parent = null;
            tabPage2.Parent = null;
            tabPage3.Parent = null;
            tabPage4.Parent = null;
            tabPage5.Parent = null;
        }


        //切换密码的可视状态
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (textBox8.PasswordChar == '*')
                textBox8.PasswordChar = '\0';
            else
                textBox8.PasswordChar = '*';
        }


        //绘制选项卡背景颜色 
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush tab_blackColr = new SolidBrush(Color.Black);     // 笔刷背景
            Rectangle recMain = tabControl1.ClientRectangle;        //获取Table控件的工作区域
            e.Graphics.FillRectangle(tab_blackColr, recMain);          //绘制TabControl背景

            //设置笔刷
            SolidBrush red = new SolidBrush(Color.OrangeRed);              // 红色
            SolidBrush yellow = new SolidBrush(Color.Yellow);        //黄色
            SolidBrush blue = new SolidBrush(Color.LightSkyBlue);             //蓝色
            SolidBrush green = new SolidBrush(Color.GreenYellow);            //黑色
            SolidBrush pink = new SolidBrush(Color.Pink);
            SolidBrush orange = new SolidBrush(Color.Orange);
            SolidBrush black = new SolidBrush(Color.Black);


            //绘制红色背景
            if (tabControl1.TabPages[0].Name.Equals("tabPage0"))
            {
                Rectangle rectangleRed = tabControl1.GetTabRect(0);
                e.Graphics.FillRectangle(red, rectangleRed);
            }
            //绘制黄色背景
            else if (tabControl1.TabPages[0].Name.Equals("tabPage1"))
            {
                Rectangle rectangleYellow = tabControl1.GetTabRect(0);
                e.Graphics.FillRectangle(yellow, rectangleYellow);
            }
            else if (tabControl1.TabPages[0].Name.Equals("tabPage2"))
            {
                Rectangle rectangleLightSkyBlue = tabControl1.GetTabRect(0);
                e.Graphics.FillRectangle(blue, rectangleLightSkyBlue);
            }
            else if (tabControl1.TabPages[0].Name.Equals("tabPage3"))
            {
                Rectangle rectangleGreen = tabControl1.GetTabRect(0);
                e.Graphics.FillRectangle(green, rectangleGreen);
            }
            else if (tabControl1.TabPages[0].Name.Equals("tabPage4"))
            {
                Rectangle rectanglePink = tabControl1.GetTabRect(0);
                e.Graphics.FillRectangle(pink, rectanglePink);
            }
            else if (tabControl1.TabPages[0].Name.Equals("tabPage5"))
            {
                Rectangle rectangleOrange = tabControl1.GetTabRect(0);
                e.Graphics.FillRectangle(orange, rectangleOrange);
            }


            //设置标签文字居中对齐
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            //设置标签文字
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                Rectangle rec = tabControl1.GetTabRect(i);
                //设置文字字体和文字大小
                e.Graphics.DrawString(tabControl1.TabPages[i].Text, new Font("宋体", 10), black, rec, stringFormat);
            }

        }


        //选项卡的隐藏、切换和返回键
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            only_parent0();
        }
        private void label30_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            hide_all();
            tabPage2.Parent = tabControl1;
        }
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            hide_all();
            tabPage1.Parent = tabControl1;
        }
        private void label29_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            hide_all();
            tabPage1.Parent = tabControl1;
        }
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            hide_all();
            tabPage2.Parent = tabControl1;
        }
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
            hide_all();
            tabPage4.Parent = tabControl1;
        }
        private void label31_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
            hide_all();
            tabPage4.Parent = tabControl1;
        }
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            hide_all();
            tabPage3.Parent = tabControl1;
        }
        private void label32_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            hide_all();
            tabPage3.Parent = tabControl1;
        }
        private void pictureBox16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
            hide_all();
            tabPage5.Parent = tabControl1;
        }
        private void label33_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
            hide_all();
            tabPage5.Parent = tabControl1;
        }


        //打开备份界面
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.ShowDialog();
        }


        //打开恢复界面
        private void button8_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            form11.ShowDialog();
        }
    }
}
