using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HisAPP
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();

            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            con.Open();
            string selectcmd = "SELECT * from  药品类型总数;";
            MySqlCommand cmd = new MySqlCommand(selectcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            dataGridView1.DataSource = ds.Tables[0];//绑定数据源





            using (MySqlCommand command = new MySqlCommand("科室挂号数1", con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // 添加参数
                command.Parameters.Add(new MySqlParameter("datestr1", MySqlDbType.VarChar)).Value = textBox1.Text;
                command.Parameters.Add(new MySqlParameter("datestr2", MySqlDbType.VarChar)).Value = textBox2.Text;

                MySqlDataAdapter ada1 = new MySqlDataAdapter(command);//数据适配器
                DataSet ds1 = new DataSet();//数据集
                ada1.Fill(ds1);//查询结果填充数据集
                dataGridView2.DataSource = ds1.Tables[0];//绑定数据源
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Function.str);//实例化链接
            using (MySqlCommand command = new MySqlCommand("科室挂号数1", con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // 添加参数
                command.Parameters.Add(new MySqlParameter("datestr1", MySqlDbType.VarChar)).Value = textBox1.Text;
                command.Parameters.Add(new MySqlParameter("datestr2", MySqlDbType.VarChar)).Value = textBox2.Text;

                MySqlDataAdapter ada1 = new MySqlDataAdapter(command);//数据适配器
                DataSet ds1 = new DataSet();//数据集
                ada1.Fill(ds1);//查询结果填充数据集
                dataGridView2.DataSource = ds1.Tables[0];//绑定数据源
            }
        }
    }
}
