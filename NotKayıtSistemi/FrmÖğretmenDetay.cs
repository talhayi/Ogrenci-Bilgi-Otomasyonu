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
    public partial class FrmÖğretmenDetay : Form
    {
        public FrmÖğretmenDetay()
        {
            InitializeComponent();
        }

        
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DRTGKRJ\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True");
        public void verileriGöster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler,baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
              
        bool durum1 = true;
        private void kayıtkontrol()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLDERS", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (mskNumara.Text == oku["OGRNUMARA"].ToString() || maskedTextBox2.Text == oku["TCKİMLİKNO"].ToString())
                {
                    durum1 = false;

                }
            }
               baglanti.Close();
        }
        public string AdSoyad;
    
        private void FrmÖğretmenDetay_Load(object sender, EventArgs e)
        {

            // TODO: Bu kod satırı 'dbNotKayıtDataSet3.TBLDERS' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBLDERSTableAdapter1.Fill(this.dbNotKayıtDataSet3.TBLDERS);


            lblAdSoyad.Text = AdSoyad;
                       
            baglanti.Open();  
            SqlCommand komut = new SqlCommand("select * From TBLOGRETMEN where KULLANICIADI=@p1", baglanti);


            komut.Parameters.AddWithValue(@"p1", AdSoyad);
        
            SqlDataReader dr = komut.ExecuteReader();
            
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[1].ToString() + " " + dr[2].ToString();
            }

           
            baglanti.Close();
          
            // TODO: Bu kod satırı 'dbNotKayıtDataSet.TBLDERS' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            durum1 = true;
            kayıtkontrol();
            if (durum1 == true)
            {          
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS(OGRNUMARA,OGRAD,OGRSOYAD,TCKİMLİKNO) values(@P1,@P2,@P3,@P4)",baglanti);
            komut.Parameters.AddWithValue(@"p1", mskNumara.Text);
            komut.Parameters.AddWithValue(@"p2", textAd.Text);
            komut.Parameters.AddWithValue(@"p3", textSoyAd.Text);
            komut.Parameters.AddWithValue(@"p4", maskedTextBox2.Text);
            komut.ExecuteNonQuery();
            verileriGöster("select * From TBLDERS");
            mskNumara.Clear();
            textAd.Clear();
            textSoyAd.Clear();
            maskedTextBox2.Clear();
            baglanti.Close();
            mskNumara.Focus();
            MessageBox.Show("öğrenci sisteme eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
            }
            else
            {
                MessageBox.Show("Kayıtlı Öğrenci Ekleyemezsiniz!!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(textSınav1.Text);
            s2 = Convert.ToDouble(textSınav2.Text);
            s3 = Convert.ToDouble(textSınav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblOrtalama.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                durum = "TRUE";
                
            }
            else
                durum = "FALSE";



            if (Convert.ToDouble(textSınav1.Text) < 100 && Convert.ToDouble(textSınav1.Text) > 0&& Convert.ToDouble(textSınav2.Text) < 100 && Convert.ToDouble(textSınav2.Text) > 0&&Convert.ToDouble(textSınav3.Text) < 100 && Convert.ToDouble(textSınav3.Text) > 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5,OGRDEVAM=@P6 WHERE OGRNUMARA=@P7", baglanti);


                komut.Parameters.AddWithValue(@"p1", textSınav1.Text);
                komut.Parameters.AddWithValue(@"p2", textSınav2.Text);
                komut.Parameters.AddWithValue(@"p3", textSınav3.Text);
                komut.Parameters.AddWithValue(@"p4", decimal.Parse(lblOrtalama.Text));
                komut.Parameters.AddWithValue(@"p5", durum);
                komut.Parameters.AddWithValue(@"p6", textDevam.Text);
                komut.Parameters.AddWithValue(@"p7", mskNumara.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Öğrenci Notları ve Devamsızlık Durumu Güncellendi");
           
                SqlCommand komut1 = new SqlCommand("select *from TBLDERS", baglanti);
                SqlDataAdapter da = new SqlDataAdapter(komut1);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            else
                MessageBox.Show("Lütfen 0 ile 100 arasında bir değer giriniz");
            baglanti.Close();
            
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textSoyAd.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textSınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textSınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            textSınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            textDevam.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();

        }
        
      
        private void btnÖğrenciSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from TBLDERS where OGRNUMARA=@numara", baglanti);
            komut.Parameters.AddWithValue("@numara", maskedTBOgrenciSil.Text);
            komut.ExecuteNonQuery();
            verileriGöster("select * from TBLDERS");          
            baglanti.Close();
            maskedTBOgrenciSil.Clear();

        }

        private void textBoxAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from TBLDERS where OGRNUMARA like '%" + textBoxAra.Text + "%'or OGRAD like '%" + textBoxAra.Text + "%'or OGRSOYAD like '%" + textBoxAra.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];                   
       
            baglanti.Close();
           
           
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            baglanti.Open();


            SqlCommand komut = new SqlCommand("select *from TBLDERS",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
            
        }

        private void textAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textSoyAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textSınav1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&&e.KeyChar!=8)
            {
                e.Handled = true;
            }
        }

        private void textSınav2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textSınav3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textDevam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textSınav1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPageBSubesi_Click(object sender, EventArgs e)
        {

        }

        private void tabPageAsubesi_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxAra_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}

