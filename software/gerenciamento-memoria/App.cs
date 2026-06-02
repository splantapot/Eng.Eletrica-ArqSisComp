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
        private int[] raw_buffer = new int[4];
        private int[] raw_ready = new int[4];
        private int raw_counter = 0;
        private STATE reading_state = STATE.DONE;

        public App() {
            InitializeComponent();      // APP Init
            com = new Communication();  // Instance Communication Obj
            RenderPortBox();            // Init setting
            DoConnection();
        }

        /* ====================================  */
        /* Connection Functions                  */
        /* ====================================  */
        
        // Open a connection
        private void DoConnection(string port = "") {
            if (!string.IsNullOrEmpty(port)) {
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

        // Close the connection
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

        // Connect button
        private void btnConnected_Click(object sender, EventArgs e) {
            string PORT = comboxPorts.SelectedItem.ToString();
            DoConnection(PORT);
        }

        // Disconnect button
        private void btnDesconnect_Click(object sender, EventArgs e) {
            DoDesconnection();
        }

        /* ====================================  */
        /* Get Serial Data Functions             */
        /* ====================================  */

        // Handles the Port Sended Data
        public void ReadData(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            while (sp.BytesToRead > 0) {
                byte v = (byte)sp.ReadByte();
                ProcessByte(v);
            }
        }

        public void ProcessByte(byte v) {
            switch (reading_state) {
                case STATE.DONE:
                    if (v == Communication.dummyByte) {
                        // Receive 'U'
                        reading_state = STATE.WAITING_ID;
                    } else {
                        // Start the buffer for UserMsg (Header Failure)
                        str_buffer = ((char)v).ToString();
                        reading_state = STATE.USER_STRING;
                    }
                    break;

                case STATE.WAITING_ID:
                    if (v == (byte)STATE.STRING) {
                        // Header to System String
                        str_buffer = "";
                        reading_state = STATE.STRING;
                    } else if (v == (byte)STATE.RAW) {
                        // Header to Raw Data
                        raw_counter = 0;
                        reading_state = STATE.RAW;
                    } else {
                        // Wasn't a valid header, start the buffer for UserMsg (Header Failure)
                        // Recoveries the first dummy byte and add the current byte to the buffer
                        str_buffer = ((char)Communication.dummyByte).ToString() + (char)v;
                        reading_state = STATE.USER_STRING;
                    }
                    break;

                case STATE.STRING:
                case STATE.USER_STRING:
                    if (v == '\n') {
                        // Calls the Buffer Render
                        RenderStrBuffer(reading_state == STATE.USER_STRING);
                        reading_state = STATE.DONE;
                    } else {
                        str_buffer += (char)v;
                    }
                    break;

                case STATE.RAW:
                    raw_buffer[raw_counter++] = v;
                    if (raw_counter >= 4) {
                        // Calls the Buffer Render
                        RenderRawBuffer();
                        reading_state = STATE.DONE;
                    }
                    break;

                default:
                    reading_state = STATE.DONE;
                    break;
            }
        }

        /* ====================================  */
        /* Write Serial Data Functions           */
        /* ====================================  */

        // Send Data to Device
        private void SendCmd(object sender, EventArgs e) {
            string value = textboxCMD.Text;
            textboxCMD.Clear();
            if (!string.IsNullOrEmpty(value)) com.WriteStr(value);
        }

        private void btnDebug_Click(object sender, EventArgs e) {
            com.WriteBreak();
            com.WriteRaw(251);
            com.WriteRaw(0);
            com.WriteRaw(10);
        }

        private void WriteInMSPArray(byte i, byte raw) {
            com.WriteBreak();
            com.WriteRaw(251); // Cmd 251
            com.WriteRaw(i);
            com.WriteRaw(raw);
        }

        /* ====================================  */
        /* Renderers controllers                 */
        /* ====================================  */

        // Render the String Buffer in respective TextBox
        private void RenderStrBuffer(bool isUserStr = false) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderStrBuffer(isUserStr)));
            }

            str_ready = str_buffer;
            if (isUserStr) {
                //Console.WriteLine($"[USER] {str_ready}");
                textBoxUserMsg.AppendText(str_ready+"\n");
            } else {
                //Console.WriteLine($"[TEXT] {str_ready}");
                textboxMsg.AppendText(str_ready+"\n");
            }
        }

        // Organizes the Raw Buffer and calls the render of the Data Grid
        private void RenderRawBuffer() {
            raw_ready = (int[])raw_buffer.Clone();
            //Console.WriteLine($"{raw_ready}");
            RenderUpdatedTable();
        }

        // Render Raw Buffer in Data Grid
        private void RenderUpdatedTable() {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderRawBuffer()));
            }

            for (int row = 0; row < dataGrid.RowCount; row++) {
                // ix rhex rdec whex wdec
                int ix = _getRowIndex(row);
                try {
                    dataGrid.Rows[row].Cells[1].Value = raw_ready[ix].ToString("X");    //Hex
                    dataGrid.Rows[row].Cells[2].Value = raw_ready[ix];                  //Dec
                } catch {
                    dataGrid.Rows[row].Cells[1].Value = "?";    //Hex
                    dataGrid.Rows[row].Cells[2].Value = "?";    //Dec
                }
            }
        }

        /* ====================================  */
        /* Datagrid buttons                      */
        /* ====================================  */

        private void btnAddRow_Click(object sender, EventArgs e) {
            dataGrid.Rows.Add("x", "?", "?");
        }

        private void btnAdd4Rows_Click(object sender, EventArgs e) {
            for (int i = 0; i < 4; i++) {
                dataGrid.Rows.Add(i.ToString(), "?", "?");
            }
        }

        // Clear rows button
        private void btnRmvRow_Click(object sender, EventArgs e) {
            if (dataGrid.SelectedRows.Count > 0) {
                // Clear all rows, starting by the last
                // This way, the rows will be removed without index error
                for (int i = dataGrid.SelectedRows.Count - 1; i >= 0; i--) {
                    dataGrid.Rows.RemoveAt(dataGrid.SelectedRows[i].Index);
                }
            } else {
                MessageBox.Show("Selecione pelo menos uma linha para remover.");
            }
        }

        // When the user finish edit a cell
        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            
        }

        /* ====================================  */
        /* Cleaning Buttons                      */
        /* ====================================  */
        private void btnClearUserMsg_Click(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderRawBuffer()));
            }
            textBoxUserMsg.Clear();
        }

        private void btnClearMsg_Click(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderRawBuffer()));
            }
            textboxMsg.Clear();
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

        /* ====================================  */
        /* Utils                                 */
        /* ====================================  */

        // Get the index of the row, that is in the first cell of the row, -1 if error
        private int _getRowIndex(int row) {
            try {
                // Cell0 = ix
                return int.Parse(dataGrid.Rows[row].Cells[0].Value.ToString());
            } catch {
                return -1;
            }
        }

        // Shortcuts from keyboard
        private void _cmdBox_KeyDown(object sender, KeyEventArgs e) {
            //if (e.KeyCode == Keys.Enter) {
            //    SendData(sender, e);
            //    e.SuppressKeyPress = true;
            //}
        }
    }
}
