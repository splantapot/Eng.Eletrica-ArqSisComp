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
using System.Security.Cryptography;
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
            //if () MessageBox.Show("Conectado automaticamente!");
        }

        /* ====================================  */
        /* Connection Functions                  */
        /* ====================================  */

        // Open a connection
        private void DoConnection(string port = "") {
            reading_state = STATE.DONE;
            if (com.IsConnected()) {
                MessageBox.Show("Já conectado!");
                return;
            }
            if (!string.IsNullOrEmpty(port)) {
                selected_port = port;
            }

            if (com.Open(selected_port)) {
                Console.WriteLine($"Porta [{selected_port}] aberta.");
                // Prevents error on setting callback to break and read
                com.RmvReadCallback(ReadData);
                com.SetReadCallback(ReadData);
                btnConnected.Enabled = false;
                btnDesconnect.Enabled = true;
            } else {
                Console.WriteLine($"Porta [{selected_port}] não aberta (falha).");
                MessageBox.Show("Não foi possível encontrar a porta.");
            }
        }

        // Close the connection
        private void DoDesconnection() {
            reading_state = STATE.DONE;
            btnDesconnect.Enabled = false;
            btnConnected.Enabled = true;
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
                        str_buffer += (char)v;
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
        private void WriteText(object sender, EventArgs e) {
            try {
                string value = textboxCMD.Text;
                textboxCMD.Clear();
                if (!string.IsNullOrEmpty(value)) com.WriteStr(value);
                AddComandToLog($"Cmd: '{value}'");
            } catch {
                MessageBox.Show("Verifique a conexão da porta.");
            }
        }

        private void WriteCmd(byte cmd, byte address, byte value) {
            try {
                com.WriteBreak();
                com.WriteRaw(cmd);
                com.WriteRaw(address);
                com.WriteRaw(value);
            } catch {
                MessageBox.Show("Verifique a conexão da porta.");
            }
        }
        private void WriteCmd(byte cmd, byte address_low, byte address_high, byte value) {
            try {
                com.WriteBreak();
                com.WriteRaw(cmd);
                com.WriteRaw(address_low);
                com.WriteRaw(address_high);
                com.WriteRaw(value);
            } catch {
                MessageBox.Show("Verifique a conexão da porta.");
            }
        }

        private void WriteByteInIx(byte raw, byte ix) {
            WriteCmd(251, ix, raw);
        }

        /* ====================================  */
        /* Renderers controllers                 */
        /* ====================================  */

        // Render the String Buffer in respective TextBox
        private void RenderStrBuffer(bool isUserStr = false) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderStrBuffer(isUserStr)));
                return;
            }
            str_ready = str_buffer;
            if (isUserStr) {
                //Console.WriteLine($"[USER] {str_ready}");
                textBoxUserMsg.AppendText(str_ready.Replace("\n", "\r\n"));
            } else {
                //Console.WriteLine($"[TEXT] {str_ready}");
                textBoxMsg.SelectionColor = str_ready.Contains("ERRO") ? Color.Red : Color.Black;
                textBoxMsg.AppendText(str_ready);
                textBoxMsg.ScrollToCaret();
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
                return;
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
        /* Log for Commands                      */
        /* ====================================  */

        private void AddComandToLog(string log) {
            textboxCMDLog.AppendText($"{log}\r\n");
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

        // When the user start to edit a cell
        private void dataGrid_CellClick_ClearPaint(object sender, DataGridViewCellEventArgs e) {
            _clearDatagridPaint();
        }

        private void dataGrid_CellBeginEdit_ClearPaint(object sender, DataGridViewCellCancelEventArgs e) {
            _clearDatagridPaint();
        }

        // When the user finish edit a cell
        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            // Clear Colors
            _clearDatagridPaint();
            if (e.ColumnIndex <= 0) return; //blocks for error in edit ix
            int row = e.RowIndex;
            int ix = _getRowIndex(row);
            int value = _getRowValue(row);
            if (value >= 0 && ix >= 0 && ix < 4) {
                WriteByteInIx((byte)value, (byte)ix);
                _clearRowWrite(row);
            } else {
                // Paint the line
                foreach (DataGridViewCell cell in dataGrid.Rows[row].Cells) {
                    cell.Style.BackColor = Color.LightPink;
                }

                // Play sound
                System.Media.SystemSounds.Hand.Play();
            }
        }

        /* ====================================  */
        /* Special Commands                      */
        /* ====================================  */

        // P3OUT is 19 in hex, 
        //Console.WriteLine($"> Address: {address}, Value: {v}");
        private void btnBITSET_Click(object sender, EventArgs e) {
            // bitset: 0000 0000 0000 0100 (address 0x02), 
            // CMD=190, ix (low), ix(high), value
            var (address, value) = _getSpecialCommandData();
            if (address < 0) return;
            WriteCmd(190, (byte)(address % 256), (byte)(address >> 8), value);
            AddComandToLog($"BITSET: Add [hex]: {address.ToString("X")} | Bit: {Math.Log(value, 2)}");
        }

        private void btnBITCLR_Click(object sender, EventArgs e) {
            // bitset: 0000 0000 0000 0100 (address 0x02), 
            // CMD=191, ix (low), ix(high), value
            var (address, value) = _getSpecialCommandData();
            if (address < 0) return;
            WriteCmd(191, (byte)(address % 256), (byte)(address >> 8), value);
            AddComandToLog($"BITCLR: Add [hex]: {address.ToString("X")} | Bit: {Math.Log(value, 2)}");
        }

        private void btnBITINV_Click(object sender, EventArgs e) {
            // bitset: 0000 0000 0000 0100 (address 0x02), 
            // CMD=192, ix (low), ix(high), value
            var (address, value) = _getSpecialCommandData();
            if (address < 0) return;
            WriteCmd(192, (byte)(address % 256), (byte)(address >> 8), value);
            AddComandToLog($"BITINV: Add [hex]: {address.ToString("X")} | Bit: {Math.Log(value, 2)}");
        }

        /* ====================================  */
        /* Cleaning Buttons                      */
        /* ====================================  */
        private void btnClearUserMsg_Click(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderRawBuffer()));
                return;
            }
            textBoxUserMsg.Clear();
        }

        private void btnClearMsg_Click(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderRawBuffer()));
                return;
            }
            textBoxMsg.Clear();
        }

        private void btnClearCMD_Click(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderRawBuffer()));
                return;
            }
            textboxCMDLog.Clear();
        }

        /* ====================================  */
        /* Render the PortCombobox               */
        /* ====================================  */
        private void RenderPortBox(bool isInit = false) {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => RenderPortBox()));
                return;
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
                if (!com.IsConnected()) {
                    DoDesconnection();
                }
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
                return int.Parse(dataGrid.Rows[row].Cells[0].Value.ToString().Trim());
            } catch {
                return -1;
            }
        }

        private int _getRowValue(int row) {
            // Tries to return hex
            try {
                // Cell3 = whex
                string text = dataGrid.Rows[row].Cells[3].Value.ToString().Trim();
                if (text.Contains('x')) text = text.Split('x')[1]; //Accept "0x" notation
                //Console.WriteLine($">{text}");
                return int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            } catch { }

            // Tries to return dec, if hex fails
            try {
                // Cell4 = wdec
                return int.Parse(dataGrid.Rows[row].Cells[4].Value.ToString().Trim());
            } catch {
                return -1;
            }
        }

        private int _getHex(string text) {
            // Tries to return hex
            try {
                // Cell3 = whex
                text = text.Trim();
                if (text.Contains('x')) text = text.Split('x')[1]; //Accept "0x" notation
                //Console.WriteLine($">{text}");
                return int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            } catch { return -1; }
        }

        private int _getDec(string text) {
            // Tries to return dec
            try {
                // Cell4 = wdec
                return int.Parse(text.Trim());
            } catch { return -1; }
        }

        private bool _clearRowWrite(int row) {
            try {
                dataGrid.Rows[row].Cells[3].Value = "";
                dataGrid.Rows[row].Cells[4].Value = "";
                return true;
            } catch {
                return false;
            }
        }

        private void _clearDatagridPaint() {
            foreach (DataGridViewRow row in dataGrid.Rows) {
                foreach (DataGridViewCell cell in row.Cells) {
                    cell.Style.BackColor = Color.Empty;
                }
            }
        }

        private (int address, byte value) _getSpecialCommandData() {
            int address = _getHex(textboxCMDAddress.Text);
            int bit = _getDec(textboxCMDBit.Text);
            if (bit < 0 || bit > 7 || address < 0 || address > 0xFFFF) {
                // Play sound
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show("Insira um endereço (em hexadecimal) e um bit válidos (bit: 0-7, address: 0-65535).");
                return (-1, 0);
            }
            double value = Math.Pow(2, bit);
            byte v = (byte)(value % 256);
            return (address, v);
        }

        // Shortcuts from keyboard
        private void _cmdBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                WriteText(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        /* ====================================  */
        /* Write Code to MSP                     */
        /* ====================================  */
        private void btnWriteMSP_Click(object sender, EventArgs e) {
            // 1. Configura o OpenFileDialog para selecionar o arquivo de gravação
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = @"C:\Users\jvcr\workspace\ufpi\Eng.Eletrica - ArqSisComp\hardware\";
                openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Selecione o arquivo compilado (.txt) para gravar no MSP";

                // Se o usuário cancelar ou fechar a janela de seleção, interrompe a execução
                if (openFileDialog.ShowDialog() != DialogResult.OK) {
                    return;
                }

                // Caminho do arquivo selecionado pelo usuário
                string pathfile = openFileDialog.FileName;
                string execmd = @"C:\BSLDEMO-2.01c.exe";
                string comPort = selected_port;

                // Validação caso não haja porta selecionada
                if (string.IsNullOrEmpty(comPort)) {
                    MessageBox.Show("Por favor, selecione uma porta COM antes de gravar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Monta os argumentos envolvendo o caminho do arquivo entre aspas duplas
                // É como eu pensava, caminhos com espaços tem que estar entre aspas duplas.
                string arguments = $"-c{comPort} -m1 -ij -s2 +epr \"{pathfile}\"";

                try {
                    // Desconecta a porta serial para liberar o BSLDEMO
                    DoDesconnection();

                    // 3. Configura a inicialização do processo externo
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = execmd;       // O executável
                    startInfo.Arguments = arguments;   // Os argumentos/parâmetros

                    // Define o UseShellExecute como false para permitir manipulações limpas de processo
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = false;  // Mantém visível para você ver o progresso do BSLDEMO

                    Console.WriteLine($"Executando:");
                    Console.WriteLine($"{execmd} {arguments}");

                    // 4. Executa o BSLDEMO
                    using (Process process = Process.Start(startInfo)) {
                        if (process != null) {
                            // Espera o BSL terminar antes de reabrir a porta serial no C#
                            process.WaitForExit();
                            MessageBox.Show("Processo de gravação finalizado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Erro ao executar o BSLDEMO: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } finally {
                    // Garante a reconexão automática com o MSP após fechar o processo
                    DoConnection(comPort);
                }
            }
        }
    }
}
