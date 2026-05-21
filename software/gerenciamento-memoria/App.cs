using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace gerenciamento_memoria {
    public partial class App : Form {
        
        public Communication com;               // Object that handles a communication
        private string selected_port = null;    // Default port, data_list and 

        private string str_buffer = "";
        private string str_ready = "";
        private int raw_counter = 0;
        private int[] raw_buffer = new int[3];
        private int[] raw_ready = new int[3];
        private STATE read_state = STATE.DONE;

        public App() {
            InitializeComponent();      // APP Init
            com = new Communication();  // Instance Communication onj
            RenderPortBox();            // Init setting
            DoConnection();
        }

        /* ====================================  */
        /* Connection Functions                  */
        /* ====================================  */
        private void DoConnection(string port = "") {
            if (!string.IsNullOrEmpty(port)) {
                Console.WriteLine("here");
                selected_port = port;
            }

            if (com.Open(selected_port)) {
                Console.WriteLine($"Porta [{selected_port}] aberta.");
                // Prevents error on setting callback to break and read
                com.RmvReadCallback(ReadData);
                com.SetReadCallback(ReadData);
            } else {
                Console.WriteLine($"Porta [{selected_port}] não aberta (falha).");
                MessageBox.Show("Não foi possível encontrar a porta.");
            }
        }

        private void DoDesconnection() {
            if (com.IsConnected()) {
                if (com.Close(selected_port)) {
                    Console.WriteLine($"Porta [{selected_port}] encerrada.");
                    selected_port = null;
                } else {
                    Console.WriteLine($"Porta [{selected_port}] não encerrada (falha).");
                }
            }
        }

        private void btnConnected_Click(object sender, EventArgs e) {
            string PORT = comboxPorts.SelectedItem.ToString();
            DoConnection(PORT);
        }

        private void btnDesconnect_Click(object sender, EventArgs e) {
            DoDesconnection();
        }

        /* ====================================  */
        /* Data Functions                        */
        /* ====================================  */

        // Handles the Port Sended Data
        public void ReadData(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            while (sp.BytesToRead > 0) {
                int v = (int)sp.ReadByte();
                //Console.WriteLine(v);

                // Break detection
                if (v == 0) {
                    read_state = STATE.WAITING;
                    continue;
                }

                switch (read_state) {
                    case STATE.WAITING:
                        // If state bit == 1, read_state is STRING
                        // If state bit == 3, read_state is RAW
                        read_state = (STATE) v;
                        RenderBuffer();
                        raw_counter = 0;
                        break;

                    case STATE.STRING:
                        str_buffer += (char) v;
                        break;

                    case STATE.RAW:
                        raw_buffer[raw_counter] = v;
                        raw_counter++;
                        break;
                }
            }
        }
        private void RenderBuffer() {
            if (!string.IsNullOrEmpty(str_buffer)) {
                str_ready = str_buffer;
                str_buffer = "";
                RenderString();
                // Console.WriteLine($"STR: {str_ready}");
            }

            for (int i = 0; i < raw_buffer.Length; i++) {
                raw_ready[i] = raw_buffer[i];
            }
            RenderTable();
            //Console.WriteLine($"BFF: {raw_buffer[0]}");
            //Console.WriteLine($"RAW: {raw_ready.Count}");
            //if (raw_buffer.Length > 0) {
            //    raw_ready = raw_buffer;
            //    raw_buffer.Clear();
            //}
        }

        private void RenderString() {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => { RenderString(); }));
                return;
            }
            textboxMsg.AppendText(str_ready);
        }

        private void RenderTable() {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => { RenderTable(); }));
                return;
            }

            for (int row = 0; row < datagrid.Rows.Count; row++) {
                // Handles "text null" error.
                string text;
                try {
                    // Address column
                    text = datagrid.Rows[row].Cells[0].Value.ToString();
                } catch {
                    text = "";
                }

                if (text.Contains("x")) text = text.Split('x')[1];

                // Accepts the hex notation
                try {
                    if (int.TryParse(text, out int address)) {
                        Console.WriteLine($"row: {row} || add: {address} || {string.Join(",", raw_ready)}");

                        datagrid.Rows[row].Cells[2].Value = raw_ready[address].ToString("X");
                        datagrid.Rows[row].Cells[1].Value = raw_ready[address];
                        
                        //datagrid.Rows[row].Cells[1].Value = raw_ready[address].ToString("X");   // Hex Read column
                        //datagrid.Rows[row].Cells[2].Value = raw_ready[address];                 // Decimal Read column
                    }
                } catch {
                    // Address NaN
                    continue;
                }
            }
        }

        // Send Data to Device
        private void SendData(object sender, EventArgs e) {
            string value = textboxCMD.Text;
            textboxCMD.Clear();
            if (!string.IsNullOrEmpty(value)) com.Write(value);
        }

        /* ====================================  */
        /* Print Functions                        */
        /* ====================================  */

        private void PrintBufferInApp() {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => { PrintBufferInApp(); }));
                return;
            }

            //Console.WriteLine(data_str_buffer);
            //textboxMsg.AppendText(data_str_buffer);
            //data_str_buffer = "";
        }

        private void cmdBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                SendData(sender, e);
                e.SuppressKeyPress = true;
            }
        }

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
            RenderTable();
            //int row = e.RowIndex;
            //if (row < 0) return;
            //// Handles "text null" error.
            //string text;
            //try {
            //    text = datagrid.Rows[row].Cells[0].Value.ToString();
            //} catch {
            //    text = "";
            //}

            //// Accepts the hex notation
            //if (text.Contains("x")) text = text.Split('x')[1];
            //try {
            //    if (int.TryParse(text, out int address)) {
            //        datagrid.Rows[row].Cells[1].Value = raw_ready[address].ToString("X");   // Hex
            //        datagrid.Rows[row].Cells[2].Value = raw_ready[address];                 // Decimal
            //    }
            //} catch {
            //    return; // Couldn't read
            //}
        }

        /* ====================================  */
        /* Render the PortCombobox               */
        /* ====================================  */
        private void RenderPortBox(bool isInit = false) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderPortBox()));
            }

            string[] ports = com.GetPortList();

            comboxPorts.Items.Clear();

            foreach (string port in ports) {
                if (!comboxPorts.Items.Contains(port)) comboxPorts.Items.Add(port);
            }

            // If found only one port, select it.
            if (selected_port == null && ports.Length == 1) {
                comboxPorts.SelectedIndex = 0;
                selected_port = comboxPorts.SelectedItem as string;
            } else if (!com.IsConnected()) {
                comboxPorts.Text = "";
                comboxPorts.SelectedIndex = -1;
                selected_port = null;
            }
        }

        // Update the PortBox every time that a device is detected.
        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);
            // Connected an USB
            if (m.Msg == 0x0219) {
                RenderPortBox();
            }
        }

        private void comboxPorts_SelectedIndexChanged(object sender, EventArgs e) {
            DoDesconnection();
        }
    }
}
