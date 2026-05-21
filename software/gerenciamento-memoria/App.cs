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
        private string selected_port = null;

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
                // Prevents error on setting callback
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

        // --- Variáveis de Inicialização (Devem ser declaradas no escopo da classe) ---
        private long dtp = 200000; // Tempo anterior simulado (ex: 20ms em ticks)
        private long tant = 0;     // Tempo anterior
        private byte c = 0;
        private int s = 0;
        private int i = 0;
        private int j = 0;
        private string s_str = ""; // 'string s;' do quadro
        private char[] str = new char[256]; // Buffer para a string
        private byte[] RAWs = new byte[256]; // Buffer para os dados brutos

        // Substituindo o '#define e dt < 10000' por uma propriedade ou lógica direta.
        // Nota: 1 ms equivale a 10.000 ticks no Stopwatch do C#.
        private bool IsByteReceivedOnTime(long dt) => dt < 10000;

        // Handles the Port Sended Data
        public void ReadData(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            while (sp.BytesToRead > 0) {
                c = (byte)sp.ReadByte();

                long t = timer.ElapsedTicks;
                long dt = t - tant;
                tant = t; // tant = t;
                bool condition_e = IsByteReceivedOnTime(dt);

                if (condition_e) {
                    s = 1;
                    if (i < str.Length) str[i++] = (char)c;
                } else {
                    if (s == 1) {
                        if (i < str.Length) str[i++] = (char)c;
                        if (i < str.Length) str[i] = '\0';
                        Console.WriteLine($"String: {new string(str, 0, i - 1)}");
                        i = 0;
                        s = 0;
                    }
                }

                if (dtp > 110000 && dtp < 210000) {
                    j = 0;
                    if (j < RAWs.Length) RAWs[j++] = c; // Início dos RAWs
                } else if (dtp > 100000 && dtp < 110000) {
                    if (j < RAWs.Length) RAWs[j++] = c; // Não é o início, dado após o início
                    //Console.WriteLine($"Raw: {string.Join(",", RAWs)}");
                }

                // Atualiza o dtp com o dt atual para a próxima iteração
                dtp = dt;
            }
            //SerialPort sp = (SerialPort)sender;
            //char v = (char)sp.ReadByte();
            ////long dt = timer.ElapsedTicks;
            //long dtms = timer.ElapsedMilliseconds;
            //Console.WriteLine(v + " |dtms: " + dtms);
            //timer.Restart();
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
                /*Console.WriteLine("dt: " + dt + " | " + v);
                timer.Restart();
                return;*/

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
            // Handles "text null" error.
            string text;
            try {
                text = datagrid.Rows[row].Cells[0].Value.ToString();
            } catch {
                text = "";
            }

            // Accepts the hex notation
            if (text.Contains("x")) text = text.Split('x')[1];
            try {
                if (int.TryParse(text, out int address)) {
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
