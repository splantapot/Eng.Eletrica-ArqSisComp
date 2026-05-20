using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace gerenciamento_memoria {
    public partial class App : Form {
        // Object that handles a communication
        public Communication com;

        // Default port, data_list and 
        private string default_port = null;

        private readonly Stopwatch timer = new Stopwatch();
        private STATE state = STATE.IDLE;

        private int data_counter = 0;
        private int[] data_list = new int[4];
        private string data_str_buffer = "";
        private char last_v = '\0';

        public App() {
            InitializeComponent();      // APP Init
            com = new Communication();  // Instance Communication onj
            timer.Start();              // Prepares Timer
            RenderPortBox(true);        // Init setting
            TryDefaultInit();
        }

        /* ====================================  */
        /* Tries a default connection            */
        /* ====================================  */
        private void TryDefaultInit() {
            if (!com.isConnected) {
                Console.WriteLine(default_port);
                if (com.Open(default_port)) {
                    Console.WriteLine("Porta aberta: " + default_port);
                    com.SetReadCallback(ReadData);
                } else {
                    Console.WriteLine("Porta não aberta.");
                    MessageBox.Show("Não foi possível encontrar uma porta para inicialização padrão.");
                }
            }
        }

        /* ====================================  */
        /* Data Functions                        */
        /* ====================================  */

        // Handles the Port Sended Data
        public void ReadData(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            char v = (char)sp.ReadByte();
            //long dt = timer.ElapsedTicks;
            long dtms = timer.ElapsedMilliseconds;
            Console.WriteLine(v + " |dtms: " + dtms);
            //Console.WriteLine(dtms);
            //last_v = v;
            timer.Restart();
        }

        public void ReadData1905(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            while (sp.BytesToRead > 0) {
                char v = (char) sp.ReadByte();
                if (v == '\n') v = 'n';
                if (v == '\r') v = 'r';
                long dt = timer.ElapsedTicks;
                bool buffer_ready = false;
                bool write_raw = false;
                Console.WriteLine("dt: " + dt + " | " + v);
                timer.Restart();
                return;

                if (dt >= Config.TIME_NEW_MSG) {
                    // New data coming
                    state = STATE.READING;
                    data_counter = 0;
                }

                if (dt < Config.TIME_STR_READ) {
                    // If a data came so much fast...
                    if (state == STATE.READING) {
                        //... and if it's reading
                        state = STATE.STRING;
                    }
                    if (state == STATE.STRING) {
                        data_str_buffer += v;
                    }
                } else {
                    if (state == STATE.STRING) {
                        state = STATE.READING;
                        buffer_ready = true;
                    }
                }

                if (state == STATE.READING) {
                    data_list[data_counter] = v;
                    data_counter++;
                    if (data_counter >= 4) {
                        data_counter = 0;
                        write_raw = true;
                    }
                }

                RenderData(write_raw);

                if (buffer_ready) {
                    //PrintBufferInApp();
                    Console.WriteLine(data_str_buffer);
                    data_str_buffer = "";
                }

                timer.Restart();
            }
        }

        private void RenderData(bool raw_ready) {
            if (raw_ready) {
                Console.WriteLine("{ " + string.Join(",", data_list) + " }");
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

            Console.WriteLine(data_str_buffer);
            textboxMsg.AppendText(data_str_buffer);
            data_str_buffer = "";
        }

        private void cmdBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                SendData(sender, e);
                e.SuppressKeyPress = true;
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
                    datagrid.Rows[row].Cells[1].Value = data_list[address].ToString("X"); // Hex
                    datagrid.Rows[row].Cells[2].Value = data_list[address];              // Decimal
                }
            } catch {
                return; // Couldn't read
            }
        }

        /* ====================================  */
        /* Render the PortCombobox               */
        /* ====================================  */
        private void RenderPortBox(bool isInit = false) {
            string[] ports = com.GetPortList();

            void RenderFunc() {
                foreach (string port in ports) {
                    if (!comboxPorts.Items.Contains(port)) comboxPorts.Items.Add(port);
                }

                // If found only one port, select it.
                if (default_port == null && ports.Length == 1) {
                    comboxPorts.SelectedIndex = 0;
                    default_port = comboxPorts.SelectedItem as string;
                }
            }

            // Only can use Invoke after Form creation
            if (!isInit) {
                this.BeginInvoke(new Action(() => RenderFunc()));
            } else {
                RenderFunc();
            }
        }
    }
}
