using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsForms.SanPham_ServiceReferences;

namespace WindowsForms
{
    public partial class SanPhamChiTiet_Form : Form
    {
        SanPham_ServiceReferences.SanPham_Service sanpham = new SanPham_ServiceReferences.SanPham_Service();

        public SanPhamChiTiet_Form()
        {
            InitializeComponent();
        }

        private void btBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.png; *.jpeg; *.bmp)|*.jpg; *.jpeg; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                pictureBoxSP.Image = new Bitmap(open.FileName);
                // image file path
                txtUrl.Text = open.SafeFileName;
                string newPathToFile = System.IO.Directory.GetCurrentDirectory().Replace("\\WindowsForms\\bin\\Debug", "\\Website\\Images\\SanPham\\" + open.SafeFileName);

                System.IO.File.Copy(open.FileName, newPathToFile, true);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {

        }

        private void SanPhamChiTiet_Form_Load(object sender, EventArgs e)
        {
            if (SanPham_Form.id_sp != 0)
            {
                LoadData(SanPham_Form.id_sp);
            }
            else
            {
                btSave.Text = "Thêm";
            }
        }

        private void LoadData(int ma_sp)
        {
            DataRow dr = sanpham.SanPham_GetByID(ma_sp).Rows[0];
            txtMaSP.Text = dr["ma_sp"].ToString();
            txtTenSP.Text = dr["ten_sp"].ToString();
            txtMota.Text = dr["mo_ta"].ToString();
            txtGia.Text = dr["gia"].ToString();
            cbDanhmuc.Text = sanpham.GetDanhMuc(int.Parse(dr["phan_loai"].ToString()));
            //pictureBoxSP.Image = Image.FromFile(Application.StartupPath.Replace(Ultilities.WindowsForms, Ultilities.Website + dr["hinh"].ToString()));
        }

        private void Load_cbDanhMuc()
        {
            DataRow dr = sanpham.DanhMuc_Load().Rows[0];

            cbDanhmuc.DataSource = sanpham.DanhMuc_Load();
            cbDanhmuc.ValueMember = "ma_loai";
            cbDanhmuc.DisplayMember = "ten_loai";
        }

        private void cbDanhmuc_MouseClick(object sender, MouseEventArgs e)
        {
            Load_cbDanhMuc();
        }
    }
}
