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

namespace Seçim_Verileriyle_Parti_İstatistik_Analizi
{
    public partial class FrmOyGiris : Form
    {
        public FrmOyGiris()
        {
            InitializeComponent();
        }
        //Data Source=DESKTOP-ERGBAB8;Initial Catalog=DbSecimProje;Integrated Security=True 
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ERGBAB8;Initial Catalog=DbSecimProje;Integrated Security=True");

        private void BtnOyGiriş_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı aç
                baglanti.Open();

                // SQL sorgusu (parantez düzeltildi)
                SqlCommand komut = new SqlCommand("INSERT INTO TBLILCE (ILCEAD, APARTI, BPARTI, CPARTI, DPARTI, EPARTI) VALUES (@P1, @P2, @P3, @P4, @P5, @P6)", baglanti);

                // Parametreler ekleniyor
                komut.Parameters.AddWithValue("@P1", TxtIlceAd.Text);  // İlçe adı
                komut.Parameters.AddWithValue("@P2", int.Parse(TxtA.Text)); // A Parti oyu
                komut.Parameters.AddWithValue("@P3", int.Parse(TxtB.Text)); // B Parti oyu
                komut.Parameters.AddWithValue("@P4", int.Parse(TxtC.Text)); // C Parti oyu
                komut.Parameters.AddWithValue("@P5", int.Parse(TxtD.Text)); // D Parti oyu
                komut.Parameters.AddWithValue("@P6", int.Parse(TxtE.Text)); // E Parti oyu

                // Sorguyu çalıştır
                komut.ExecuteNonQuery();

                // Başarı mesajı göster
                MessageBox.Show("Oy Girişi Başarıyla Gerçekleşti");
            }
            catch (Exception ex)
            {
                // Hata mesajı göster
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                if (baglanti.State == System.Data.ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void BtnGrafik_Click(object sender, EventArgs e)
        {
            FrmGrafikler fr = new FrmGrafikler();
            fr.Show();

        }

        private void BtnCikisYap_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Programdan çıkmak istediğinizden emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                Application.Exit(); // Kullanıcı "Evet" derse program kapanır.
            }
            // Eğer "Hayır" derse hiçbir işlem yapılmaz ve program devam eder.
        }
    }
}
