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
    public partial class Form7 : Form
    {

        //全局变量，方便类内的不同函数使用
        string PID;



        public Form7()
        {
            InitializeComponent();


            //初始化dataGridView，查看挂号在当前登录医生下的挂号信息
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();
            string selectcmd = "SELECT 病人编号,姓名,性别,挂号科室,医生 FROM hisbook.挂号信息 where 医生编号 = '" + Function.login_ID + "' and " + "是否就诊 = '" + 0 + "';";
            MySqlCommand cmd = new MySqlCommand(selectcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView1.DataSource = ds.Tables[0];//绑定数据源


            //初始化dataGridView，查看药品信息
            string strcmd = @"SELECT  * FROM hisbook.药品信息;";//SQL语句 
            cmd = new MySqlCommand(strcmd, con);//数据库命令
            ada = new MySqlDataAdapter(cmd);//数据适配器
            ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView2.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接

            //刷新诊断记录
            refresh_log();
        }


        //录入按钮功能实现
        private void button5_Click(object sender, EventArgs e)
        {
            //录入诊断信息表
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();//开启连接
            PID = textBox17.Text.ToString();
            string P_name = Function.select_one("hisbook.挂号信息", "姓名", "病人编号", PID);
            if (P_name == "")
            {
                MessageBox.Show("该病患不存在！", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string medicine_ID = textBox7.Text.ToString();
                string num = textBox8.Text.ToString();
                if (medicine_ID.Equals(""))
                {
                    MessageBox.Show("没有选择录入药品！", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                string num_exist = Function.select_one("hisbook.诊断信息", "药品数量", " 病人编号", PID, "药品编号", medicine_ID).ToString();
                bool if_exist = !num_exist.Equals("");
                if (if_exist)
                {
                    if (MessageBox.Show("该药品已录入过，是否再次录入？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        string re_ID = Function.select_one("hisbook.诊断信息", "编号", " 病人编号", PID, "药品编号", medicine_ID);
                        if (num == "")
                        {
                            MessageBox.Show("未填写数量！", "Warning", MessageBoxButtons.OK);
                            return;
                        }
                        string new_num = (Convert.ToInt32(num) + Convert.ToInt32(num_exist)).ToString();
                        string updatecmd = @"update hisbook.诊断信息 set 药品数量 = '" + new_num + "' where 编号 = '" + re_ID + "';";
                        MySqlCommand cmd = new MySqlCommand(updatecmd, con);//数据库命令
                        cmd.ExecuteNonQuery();
                        refresh_log();
                    }
                    else
                        return;
                }
                else
                {
                    string ID = Function.insert_count("hisbook.诊断信息", "Z");
                    string medicine_name = Function.select_one("hisbook.药品信息", "名称", "编号", medicine_ID);//绑定数据源
                    string medicine_value = Function.select_one("hisbook.药品信息", "单价", "编号", medicine_ID);
                    string medicine_num = num.ToString();
                    if (medicine_num == "")
                    {
                        MessageBox.Show("未填写数量！", "Warning", MessageBoxButtons.OK);
                        return;
                    }
                    string medicine_values = (Convert.ToDecimal(medicine_value) * Convert.ToInt32(medicine_num)).ToString();
                    string insertcmd = @"insert into hisbook.诊断信息 VALUES 
                                        ('" + ID + "','" + PID + "','" + medicine_ID + "','" + medicine_name + "','" + num + "','" + medicine_values + "');";
                    MySqlCommand cmd = new MySqlCommand(insertcmd, con);//数据库命令
                    cmd.ExecuteNonQuery();
                    refresh_log();
                }
                con.Close();
                textBox7.Text = "";
                textBox8.Text = "";
                if (MessageBox.Show("录入成功！是否继续录入？", "Successfully", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    return;
                else
                {
                    end();
                }
            }
            con.Close();
        }


        //刷新诊断记录显示
        private void refresh_log()
        {
            string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = @"SELECT  * FROM hisbook.诊断信息 where 病人编号 = '" + PID + "';";//SQL语句 
            MySqlCommand cmd = new MySqlCommand(strcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView3.DataSource = ds.Tables[0];//绑定数据源
            con.Close();//关闭连接
        }


        //点击单元格自动填入功能
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            int column = dataGridView1.CurrentCell.ColumnIndex;
            textBox17.Text = dataGridView1.Rows[row].Cells[column].Value.ToString();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            int column = dataGridView2.CurrentCell.ColumnIndex;
            textBox7.Text = dataGridView2.Rows[row].Cells[column].Value.ToString();
        }


        //点击行头自动填入功能
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            textBox17.Text = dataGridView1.Rows[row].Cells[0].Value.ToString();
        }
        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = dataGridView2.CurrentRow.Index;
            textBox7.Text = dataGridView2.Rows[row].Cells[0].Value.ToString();
        }


        //完成录入按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认是否完成诊断，诊断完成后将不可再添加信息。", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                end();
            else
                return;
        }


        //完成录入，将同一病人的诊断记录合并并显示在下方dataGridView
        private void end()
        {
            if (Function.select_one("hisbook.结算信息", "病人编号", "病人编号", PID).Equals(PID))
            {
                MessageBox.Show("该病例已诊断完毕!", "Warning", MessageBoxButtons.OK);
                return;
            }

            //录入结算信息表
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();//开启连接
            string name = Function.select_one("hisbook.挂号信息", "姓名", "病人编号", PID).ToString();
            string registered_values = Function.select_one("hisbook.挂号信息", "挂号费用", "病人编号", PID).ToString();
            string medicine_data = Function.merge_same("hisbook.诊断信息", new List<string> { "药品名称", "药品数量" }, "病人编号", PID, "*").ToString();
            string medicine_value_sum = Function.sum_same("hisbook.诊断信息", "药品价格", "病人编号", PID).ToString();
            string sum_value = (Convert.ToDecimal(registered_values) + Convert.ToDecimal(medicine_value_sum)).ToString();
            string insertcmd1 = @"insert into hisbook.结算信息 VALUES 
                                ('" + PID + "','" + name + "','" + registered_values + "','" + medicine_value_sum + "','" + medicine_data + "','" + sum_value + "','0" + "');";
            MySqlCommand cmd1 = new MySqlCommand(insertcmd1, con);//数据库命令
            cmd1.ExecuteNonQuery();
            string selectcmd1 = "select 病人编号,姓名,挂号费,药品费,药品名称及数量,总费用 from hisbook.结算信息 where 病人编号='" + PID + "';";
            MySqlCommand cmd2 = new MySqlCommand(selectcmd1, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd2);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView4.DataSource = ds.Tables[0];//绑定数据源


            string updatecmd = "UPDATE `hisbook`.`挂号信息` SET `是否就诊` = '1' WHERE(`病人编号` = '" + PID + "');";
            MySqlCommand cmd3 = new MySqlCommand(updatecmd, con);
            cmd3.ExecuteNonQuery();


            string selectcmd = "SELECT 病人编号,姓名,性别,挂号科室,医生 FROM hisbook.挂号信息 where 医生编号 = '" + Function.login_ID + "' and " + "是否就诊 = '" + 0 + "';";
            MySqlCommand cmd4 = new MySqlCommand(selectcmd, con);//数据库命令
            ada = new MySqlDataAdapter(cmd4);//数据适配器
            ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView1.DataSource = ds.Tables[0];//绑定数据源
            con.Close();
        }

    }
}
