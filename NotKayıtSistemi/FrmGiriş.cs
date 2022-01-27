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


namespace NotKayıtSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DRTGKRJ\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {

                baglanti.Open();
                string sql = "select * from TBLDERS  where TCKİMLİKNO=@tckimlik and OGRNUMARA=@numara";
                SqlParameter prm1 = new SqlParameter("tckimlik", maskedTextBox2.Text);
                SqlParameter prm2 = new SqlParameter("numara", maskedTextBox1.Text);
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                
                string sql1 = "select * from TBLDERS2  where TCKİMLİKNO=@tckimlik1 and OGRNUMARA=@numara1";
                SqlParameter prm3 = new SqlParameter("tckimlik1", maskedTextBox2.Text);
                SqlParameter prm4 = new SqlParameter("numara1", maskedTextBox1.Text);
                SqlCommand komut1 = new SqlCommand(sql1, baglanti);
                komut1.Parameters.Add(prm3);
                komut1.Parameters.Add(prm4);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(komut1);
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    FrmOgrenciDetay1 frm = new FrmOgrenciDetay1();
                    frm.numara = maskedTextBox1.Text;
                    frm.Show();
                }
                else if (dt.Rows.Count > 0)
                {
                    FrmOgrenciDetay frm = new FrmOgrenciDetay();
                    frm.numara = maskedTextBox1.Text;
                    frm.Show();
                }

                else
                {
                    MessageBox.Show("Hatalı Giriş Yaptınız");

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Veritabanı Bağlantısı Yok");
                baglanti.Close();
            }

            baglanti.Close();
        }
         
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {

                baglanti.Open();
                string sql = "select * from TBLOGRETMEN where KULLANICIADI=@adi and SIFRE=@sifre";
                SqlCommand komut1 = new SqlCommand("select *from TBLDERS", baglanti);
                // string sql2 = "select * from TBLOGRETMEN where KULLANICIADI=" + "'" + textBox1.Text.Trim() + "'" + "and SIFRE="+"'" + textBoxSifre.Text.Trim() + "'" ;
                SqlParameter prm1 = new SqlParameter("adi", textBox1.Text.Trim());
                SqlParameter prm2 = new SqlParameter("sifre", textBoxSifre.Text.Trim());
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    FrmOgretmenDetay1 frm1 = new FrmOgretmenDetay1();

                    frm1.AdSoyad = textBox1.Text;
                    frm1.Show();
                }
                else {
                    MessageBox.Show("Hatalı Giriş Yaptınız");

                }
                
            }
            catch (Exception)
            {

                MessageBox.Show("Veritabanı Bağlantısı Yok");
                baglanti.Close();
            }
            baglanti.Close();
                
        }

        private void btnÖgrenciGirişi_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void btnÖğretmenGirişi_Click(object sender, EventArgs e)
        {
            
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
