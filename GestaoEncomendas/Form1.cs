using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoEncomendas
{
    public partial class frmRegisto : Form
    {
        private struct Encomenda
        {
            public int Id;
            public DateTime Data;
            public string Cliente;
            public string Produto;
            public double Valor;
        }
        private List<Encomenda> encomendas = new List<Encomenda>();
        private List<Encomenda> entregues = new List<Encomenda>();
        int id = 1;

        public frmRegisto()
        {
            InitializeComponent();
            dgvRegisto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRegisto.MultiSelect = false;
            dgvRegisto.CellClick += dgvRegisto_CellClick;
        }

        private void dgvRegisto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvRegisto.Rows[e.RowIndex].Selected=true;                
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Inserir();
        }

        private void Inserir()
        {
            if (string.IsNullOrWhiteSpace(txtCliente.Text) || string.IsNullOrWhiteSpace(txtProduto.Text) || string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preencha todos os campos", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Encomenda novaEncomenda = new Encomenda
            {
                Id = id++,
                Data = dtpData.Value,
                Cliente = txtCliente.Text,
                Produto = txtProduto.Text,
                Valor = double.Parse(txtValor.Text)
            };

            encomendas.Add(novaEncomenda);
            Atualizar();

            dtpData.Value = DateTime.Now;
            txtCliente.Text = "";
            txtProduto.Text = "";
            txtValor.Text = "";
        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {
            Entregar();
        }

        private void Entregar()
        {
            if (dgvRegisto.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma encomenda", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = dgvRegisto.SelectedRows[0].Index;
            Encomenda selectedEncomenda = encomendas[selectedIndex];

            encomendas.RemoveAt(selectedIndex);
            entregues.Add(selectedEncomenda);

            Atualizar();

        }

        private void btnVerEntregas_Click(object sender, EventArgs e)
        {
            Ver();
        }

        private void Ver()
        {
            throw new NotImplementedException();
        }

        private void Atualizar()
        {
            dgvRegisto.Rows.Clear();
            foreach (var encomenda in encomendas)
            {
                dgvRegisto.Rows.Add(encomenda.Id, encomenda.Data.ToString("dd/MM/yyyy"), encomenda.Cliente, encomenda.Produto, encomenda.Valor);
            }
        }

    }
}

