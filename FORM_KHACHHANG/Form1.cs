using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace FORM_KHACHHANG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string duongdan = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\HINHANH\";
        private void btn_them_Click(object sender, EventArgs e)
        {
            string chuoikn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\source\repos\FORM_KHACHHANG\FORM_KHACHHANG\QLCONGTY.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(chuoikn);
            string cmd = "INSERT INTO KHACHHANG VALUES(N'"+txt_makh.Text+"',N'"+txt_hoten.Text+"',N'"+txt_tuoi.Text+"',N'"+txt_hinhanh.Text+"')";
            SqlCommand comm = new SqlCommand(cmd, cnn);
            cnn.Open();
            int kq = comm.ExecuteNonQuery();
            cnn.Close();
            if (kq >= 1)
            {
                MessageBox.Show("Thêm Thành Công");
                //MessageBox.Show("Update Thành Công");
                //pictureBox1.Image = new Bitmap(Image.FromFile(duongdan + txt_hinhanh.Text));
                //Bitmap bm = new Bitmap(pictureBox1.Image);
                //bm.Save(duongdan + txt_hinhanh.Text, ImageFormat.Bmp);
                pictureBox1.Image.Save(duongdan + txt_hinhanh.Text);
                txt_makh.Text = "";
                txt_hoten.Text = "";
                txt_tuoi.Text = "";
                txt_hinhanh.Text = "";
                pictureBox1.Image = null; 
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại");
            }
            string sql = "SELECT * FROM KHACHHANG";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            da.Fill(dt);
            dataGridView_khachhang.DataSource = dt;

            
            
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string chuoikn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\source\repos\FORM_KHACHHANG\FORM_KHACHHANG\QLCONGTY.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(chuoikn);
            string cmd = "UPDATE KHACHHANG SET HOTEN = N'" + txt_hoten.Text + "', TUOI= N'" + txt_tuoi.Text + "',HINHANH = N'" + txt_hinhanh.Text + "' WHERE MAKH= N'" + txt_makh.Text + "'";
            SqlCommand comm = new SqlCommand(cmd, cnn);
            cnn.Open();
            int kq = comm.ExecuteNonQuery();
            cnn.Close();
            if (kq >= 1)
            {
                MessageBox.Show("Update Thành Công");
                pictureBox1.Image.Save(duongdan + txt_hinhanh.Text);
                txt_makh.Text = "";
                txt_hoten.Text = "";
                txt_tuoi.Text = "";
                txt_hinhanh.Text = "";
                pictureBox1.Image = null;
            }
            else
            {
                MessageBox.Show("Update Thất Bại");
            }
            string sql = "SELECT * FROM KHACHHANG";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            da.Fill(dt);
            dataGridView_khachhang.DataSource = dt;
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bạn có thật sự muốn xóa?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dialog == DialogResult.OK)
            {
                string chuoikn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\source\repos\FORM_KHACHHANG\FORM_KHACHHANG\QLCONGTY.mdf;Integrated Security=True";
                SqlConnection cnn = new SqlConnection(chuoikn);
                string cmd = "DELETE FROM KHACHHANG WHERE MAKH= N'" + txt_makh.Text + "'";
                SqlCommand comm = new SqlCommand(cmd, cnn);
                cnn.Open();
                int kq = comm.ExecuteNonQuery();
                cnn.Close();
                if (kq >= 1)
                {
                    MessageBox.Show("Xóa Thành Công");
                    File.Delete(duongdan + txt_hinhanh.Text);
                    txt_makh.Text = "";
                    txt_hoten.Text = "";
                    txt_tuoi.Text = "";
                    txt_hinhanh.Text = "";
                    pictureBox1.Image = null;
                }
                else
                {
                    MessageBox.Show("Xóa Thất Bại");
                }
                string sql = "SELECT * FROM KHACHHANG";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
                da.Fill(dt);
                dataGridView_khachhang.DataSource = dt;
            }
            
        }

        private void btn_dong_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bạn có thật sự muốn thoát?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dialog == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Hãy chọn ảnh khách hàng";
            open.Filter = "JEPG|*.JEPG|BMP|*.bmp|Tất cả ảnh|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string chuoikn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\source\repos\FORM_KHACHHANG\FORM_KHACHHANG\QLCONGTY.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(chuoikn);
            string sql = "SELECT * FROM KHACHHANG";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            da.Fill(dt);
            dataGridView_khachhang.DataSource = dt;
        }

        private void dataGridView_khachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_makh.Text = dataGridView_khachhang.CurrentRow.Cells["MAKH"].Value.ToString();
            txt_hoten.Text = dataGridView_khachhang.CurrentRow.Cells["HOTEN"].Value.ToString();
            txt_tuoi.Text = dataGridView_khachhang.CurrentRow.Cells["TUOI"].Value.ToString();
            txt_hinhanh.Text = dataGridView_khachhang.CurrentRow.Cells["HINHANH"].Value.ToString();
            pictureBox1.ImageLocation = duongdan + txt_hinhanh.Text;
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            string chuoikn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\source\repos\FORM_KHACHHANG\FORM_KHACHHANG\QLCONGTY.mdf;Integrated Security=True";
            string sql = "Select * from KHACHHANG where MAKH LIKE '%" + txt_timkiem.Text + "%'or HOTEN LIKE '%" + txt_timkiem.Text + "%'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, chuoikn);
            da.Fill(dt);
            dataGridView_khachhang.DataSource = dt;

        }

        private void rdo_tang_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView_khachhang.Sort(dataGridView_khachhang.Columns[2], ListSortDirection.Ascending);
        }

        private void rdo_giam_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView_khachhang.Sort(dataGridView_khachhang.Columns[2], ListSortDirection.Descending);
        }
    }
}
