using Microsoft.Data.SqlClient;
using System.Data;


namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        SqlConnection conn = new SqlConnection(
    "Data Source=DESKTOP-SQTO64F\\LOCAL;Initial Catalog=ek;Integrated Security=True;TrustServerCertificate=True;");
        void listele()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from persons", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            dataGridView1.DataSource = dt;
            conn.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO persons VALUES(@ad, @soyad, @eposta)"
, conn);
            cmd.Parameters.AddWithValue("@ad", txtName.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSurname.Text);
            cmd.Parameters.AddWithValue("@eposta", txtemail.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            listele();

        }
        int secilen;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilen = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtemail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from persons where Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", secilen);
            cmd.ExecuteNonQuery();
            conn.Close();
            listele();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE persons SET ad=@ad, soyad=@soyad, eposta=@eposta WHERE Id=@id", conn);

            cmd.Parameters.AddWithValue("@ad", txtName.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSurname.Text);
            cmd.Parameters.AddWithValue("@eposta", txtemail.Text);
            cmd.Parameters.AddWithValue("@id", secilen);

            cmd.ExecuteNonQuery();

            conn.Close();

            listele();
        }
        private void btnAra_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM persons WHERE ad LIKE @ad", conn);

            cmd.Parameters.AddWithValue("@ad", "%" + txtAra.Text + "%");

            SqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rdr);

            dataGridView1.DataSource = dt;

            conn.Close();
        }
    }
}
