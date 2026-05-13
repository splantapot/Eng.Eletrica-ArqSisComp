using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gerenciamento_memoria {
    public partial class App : Form {
        public Comunication com;
        private string default_port = null;
        private List<int> data = new List<int>();

        public App() {
            InitializeComponent();
            com = new Comunication();
            RenderPortBox();
            Console.WriteLine(default_port);
            if (com.Open(default_port)) {
                Console.WriteLine("Porta aberta: " + default_port);
                com.SetReadCallback(PortMessageHandler);
            } else {
                MessageBox.Show("Erro ao abrir porta " + default_port);
            }
        }

        // Handles the Port Sended Data
        public void PortMessageHandler(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            string dt = sp.ReadExisting();
            ReadData(dt);
        }

        // Main pool data
        private void ReadData(string value) {
            // Cancel if value is empty or the window hasn't started.
            if (string.IsNullOrEmpty(value)) return;
            if (this.IsDisposed || !this.IsHandleCreated) return;
            // Read the data in UI Thread
            this.BeginInvoke(new Action(() => {
                try {
                    if (value.Contains("\n")) {
                        textboxMsg.AppendText(value);
                    } else {
                        char v = value[0];
                        if (!data.Contains(v)) {
                            Console.WriteLine("Received: " + v);
                            data.Add(v);
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error in ReadData: " + ex.Message);
                }
            }));
        }

        // Send Data to Device
        private void SendData(object sender, EventArgs e) {
            string value = textboxCMD.Text;
            textboxCMD.Clear();
            if (!string.IsNullOrEmpty(value)) com.Write(value);
        }

        private void cmdBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                SendData(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        // Render the PortBox combobox
        private void RenderPortBox() {
            string[] ports = com.GetPortList();
            foreach (string port in ports) {
                if (!comboxPorts.Items.Contains(port)) comboxPorts.Items.Add(port);
            }
            // If found port, select it; Else, show notification.
            if (default_port == null && ports.Length > 0) {
                comboxPorts.SelectedIndex = 0;
                default_port = comboxPorts.SelectedItem as string;
            }
        }

        // Update the PortBox every time that a device is detected.
        //protected override void WndProc(ref Message m) {
        //    base.WndProc(ref m);
        //    // Connected an USB
        //    if (m.Msg == 0x0219) {
        //        RenderPortBox();
        //    }
        //}

        public void addRowBtn_Click(object sender, EventArgs e) {
            datagrid.Rows.Add("x", "-", "-");
        }

        // Clear rows button
        private void rmvRowBtn_Click(object sender, EventArgs e) {
            if (datagrid.SelectedRows.Count > 0) {
                // Clear all rows, starting by the last
                // This way, the rows will be removed without index error
                for (int i = datagrid.SelectedRows.Count - 1; i >= 0; i--) {
                    datagrid.Rows.RemoveAt(datagrid.SelectedRows[i].Index);
                }
            } else {
                MessageBox.Show("Selecione pelo menos uma linha para remover.");
            }
        }

        // When the user finish edit a cell
        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            int row = e.RowIndex;
            if (row < 0) return;
            string value = datagrid.Rows[row].Cells[0].Value?.ToString();
            // Accepts the hex notation
            if (value.Contains("x")) value = value.Split('x')[1];
            try {
                if (int.TryParse(value, out int address)) {
                    datagrid.Rows[row].Cells[1].Value = data[address].ToString("X"); // Hex
                    datagrid.Rows[row].Cells[2].Value = data[address];              // Decimal
                }
            } catch {
                return; // Couldn't read
            }
        }
    }
}
