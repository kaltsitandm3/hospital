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
    public class Function
    {
        //连接数据库的字符串
        public static string str = "server=localhost;user id=root;password=123456;persistsecurityinfo=True;database=hisbook";



        //记录删除的编号，方便之后添加时优先补充删除的编号
        public static List<string> medicine_vacancy = new List<string>();
        public static List<string> docter_vacancy = new List<string>();
        public static List<string> department_vacancy = new List<string>();


        //判断本次运行是否登录过
        public static bool login_if = false;


        //记录登录的ID
        public static string login_ID;


        //查询单个元素，params的参数顺序为两个一组，列名在前，数据在后 
        public static string select_one(string table_name, string select_col, params string[] limited)
        {
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();
            int num = limited.Length;
            string selectcmd = "SELECT " + select_col + " FROM " + table_name;
            for (int i = 0; i < num; i = i + 2)
            {
                if (i == 0)
                {
                    selectcmd += " where ";
                }
                else
                {
                    selectcmd += " and ";
                }
                selectcmd += limited[i] + " = '" + limited[i + 1] + "'";
            }
            selectcmd += ";";
            MySqlCommand cmd = new MySqlCommand(selectcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            con.Close();
            if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
            {
                return "";
            }
            string select_data = ds.Tables[0].Rows[0][0].ToString();
            return select_data;
        }


        //将某一列相同的行合并到一行
        public static string merge_same(string table_name, List<string> merge_col, string limited_col, string limited_data, string connector)
        {
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();
            string selectcmd = "SELECT ";
            for (int i = 0; i < merge_col.Count; i++)
            {
                if (i > 0)
                    selectcmd += ",";
                selectcmd += merge_col[i];
            }
            selectcmd += " FROM " + table_name;
            if (limited_col == "")
            {
                selectcmd += ";";
            }
            else
            {
                selectcmd = selectcmd + " where " + limited_col + " = " + "'" + limited_data + "';";
            }
            MySqlCommand cmd = new MySqlCommand(selectcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            con.Close();
            if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
            {
                return "";
            }
            string merge_data = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    merge_data += ",";
                for (int j = 0; j < merge_col.Count; j++)
                {
                    if (j > 0)
                        merge_data += connector;
                    merge_data += ds.Tables[0].Rows[i][j].ToString();
                }
            }
            return merge_data;
        }

        //查询一列内满足条件的所有元素
        public static List<string> select_col(string table_name, string col_name, params string[] limited)
        {
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            int num = limited.Length;
            string selectcmd = "SELECT " + col_name + " FROM " + table_name;
            for (int i = 0; i < num; i = i + 2)
            {
                if (i == 0)
                {
                    selectcmd += " where ";
                }
                else
                {
                    selectcmd += " and ";
                }
                selectcmd += limited[i] + " = '" + limited[i + 1] + "'";
            }
            selectcmd += ";";
            MySqlCommand cmd = new MySqlCommand(selectcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            List<string> col_data = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                col_data.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            con.Close();//关闭连接
            return col_data;
        }


        //计算插入编号，表名，编号前缀字符
        public static string insert_count(string table_name, string pre)
        {
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();
            string countcmd = "select count(*)+1 from " + table_name + ";";
            MySqlDataAdapter ada = new MySqlDataAdapter(countcmd, con);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            string ID = ds.Tables[0].Rows[0][0].ToString();
            for (int i = ID.Length; i < 5; i++)
            {
                ID = '0' + ID;
            }
            ID = pre + ID;
            con.Close();
            return ID;
        }


        //将某一元素相同的行求和
        public static string sum_same(string table_name, string sum_col, string limit_col, string limit_data)
        {
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string sumcmd = "select sum(" + sum_col + ") from " + table_name;
            if (limit_col == "")
            {
                sumcmd += " ;";
            }
            else
            {
                sumcmd += " where " + limit_col + " = '" + limit_data + "';";
            }
            MySqlCommand cmd = new MySqlCommand(sumcmd, con);//数据库命令
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);//数据适配器
            DataSet ds = new DataSet();//数据集
            ada.Fill(ds);//查询结果填充数据集
            con.Close();//关闭连接
            return ds.Tables[0].Rows[0][0].ToString();
        }


        //删除满足条件的一行
        public static void delete_data(string table_name, string limited_col, string limited_data)
        {
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();
            string deletestr = "delete from " + table_name + " where " + limited_col + "='" + limited_data + "';";
            MySqlCommand cmd = new MySqlCommand(deletestr, con);//数据库命令
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}