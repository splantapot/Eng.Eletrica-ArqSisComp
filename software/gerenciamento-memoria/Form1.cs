using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gerenciamento_memoria {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e) {
            dataGridView1.Rows.Add("0", "-", "-");
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            
        }

        private void button3_Click(object sender, EventArgs e) {
            if (dataGridView1.SelectedRows.Count > 0) {
                // Limpa todas as colunas, começando pelas últimas
                // Assim, ele garante que chegará ao índice 0 mesmo que a pilha esteja sendo reduzida a cada execução do for
                for (int i = dataGridView1.SelectedRows.Count-1; i >= 0 ; i--) {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
                }
            } else {
                MessageBox.Show("Selecione pelo menos uma linha para remover.");
            }
        }
    }
}
