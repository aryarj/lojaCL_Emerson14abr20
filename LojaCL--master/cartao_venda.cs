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


namespace LojaCL
{
    public partial class cartao_venda : Form
    {
        public cartao_venda()
        {
            InitializeComponent();
        }

        private void dgvCartaovenda_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        public void CarregacbxUsuario()
        {
            String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
            String usu = "select * from usuario";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(usu, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(usu, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "nome");
            cbxUsuario.ValueMember = "Id";
            cbxUsuario.DisplayMember = "nome";
            cbxUsuario.DataSource = ds.Tables["nome"];
            con.Close();
        }

        public void CarregadgvCartaovenda()
        {
            String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
            String query = "select * from cartaovenda";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable cartaovenda = new DataTable();
            da.Fill(cartaovenda);
            dgvCartaovenda.DataSource = cartaovenda;
            con.Close();

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Inserir_Cartaovenda";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@numero", txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", cbxUsuario.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                CarregadgvCartaovenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregadgvPripedido();
                MessageBox.Show("Registro inserido com sucesso!", "Cadastro", MessageBoxButtons.OK);
                con.Close();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
                            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Localizar_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["Id"].ToString();
                    txtNumero.Text = rd["numero"].ToString();
                    cbxUsuario.Text = rd["usuario"].ToString();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Atualizar_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@numero", this.txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", this.cbxUsuario.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                CarregadgvCartaovenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregadgvPripedido();
                MessageBox.Show("Registro atualizado com sucesso!", "Atualizar Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void dgvCartaovenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvCartaovenda.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNumero.Text = row.Cells[1].Value.ToString();
                cbxUsuario.Text = row.Cells[2].Value.ToString();
                
                
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Excluir_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                CarregadgvCartaovenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregadgvPripedido();
                MessageBox.Show("Registro apagado com sucesso!", "Excluir Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            

        }

        private void cartao_venda_Load(object sender, EventArgs e)
        {
            CarregacbxUsuario();
        }
    }
}
