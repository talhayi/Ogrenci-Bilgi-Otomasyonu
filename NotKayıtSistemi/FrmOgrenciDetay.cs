﻿using System;
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
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        public string numara;
      

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DRTGKRJ\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True");

        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * From TBLDERS where OGRNUMARA=@p1", baglanti);
            
            komut.Parameters.AddWithValue(@"p1", numara);
            
            SqlDataReader dr = komut.ExecuteReader();
           
            while (dr.Read())
                {
                    lblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                    lblSınav1.Text = dr[4].ToString();
                    lblSınav2.Text = dr[5].ToString();
                    lblSınav3.Text = dr[6].ToString();
                    lblOrtalama.Text = dr[7].ToString();
                    lblDurum.Text = dr[8].ToString();
                    lbldevam.Text = dr[11].ToString();

                }
           
            string a = lblDurum.Text;
            if (a == "True")
            {
                lblDurum.Text = "GEÇTİ";
            }
            else
                lblDurum.Text = "KALDI";

            baglanti.Close();

       
           

        }
        
    }
}
