namespace gerenciamento_memoria {
    partial class App {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.btnAddRow = new System.Windows.Forms.Button();
            this.panelDatagrid = new System.Windows.Forms.Panel();
            this.labelWrite = new System.Windows.Forms.Label();
            this.datagrid = new System.Windows.Forms.DataGridView();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReadHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReadDec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWriteHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWriteDec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelRead = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.comboxPorts = new System.Windows.Forms.ComboBox();
            this.btnRmvRow = new System.Windows.Forms.Button();
            this.panelMsg = new System.Windows.Forms.Panel();
            this.splitMsg = new System.Windows.Forms.SplitContainer();
            this.textboxMsg = new System.Windows.Forms.TextBox();
            this.labelMsg = new System.Windows.Forms.Label();
            this.textboxCMDReg = new System.Windows.Forms.TextBox();
            this.labelCMDReg = new System.Windows.Forms.Label();
            this.btnBITSET = new System.Windows.Forms.Button();
            this.btnBITCLR = new System.Windows.Forms.Button();
            this.textboxCMDBit = new System.Windows.Forms.TextBox();
            this.labelSetBit = new System.Windows.Forms.Label();
            this.labelSetAddress = new System.Windows.Forms.Label();
            this.textboxCMDAddress = new System.Windows.Forms.TextBox();
            this.labelSpCMD = new System.Windows.Forms.Label();
            this.btnSendCmd = new System.Windows.Forms.Button();
            this.textboxCMD = new System.Windows.Forms.TextBox();
            this.labelCMD = new System.Windows.Forms.Label();
            this.btnConnected = new System.Windows.Forms.Button();
            this.panelDatagrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMsg)).BeginInit();
            this.splitMsg.Panel1.SuspendLayout();
            this.splitMsg.Panel2.SuspendLayout();
            this.splitMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(8, 6);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(109, 34);
            this.btnAddRow.TabIndex = 1;
            this.btnAddRow.Text = "Adicionar Linha";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.addRowBtn_Click);
            // 
            // panelDatagrid
            // 
            this.panelDatagrid.Controls.Add(this.labelWrite);
            this.panelDatagrid.Controls.Add(this.datagrid);
            this.panelDatagrid.Controls.Add(this.labelRead);
            this.panelDatagrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDatagrid.Location = new System.Drawing.Point(0, 0);
            this.panelDatagrid.Name = "panelDatagrid";
            this.panelDatagrid.Size = new System.Drawing.Size(684, 207);
            this.panelDatagrid.TabIndex = 7;
            // 
            // labelWrite
            // 
            this.labelWrite.AutoSize = true;
            this.labelWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWrite.Location = new System.Drawing.Point(409, 4);
            this.labelWrite.Name = "labelWrite";
            this.labelWrite.Size = new System.Drawing.Size(65, 16);
            this.labelWrite.TabIndex = 2;
            this.labelWrite.Text = "ESCRITA";
            // 
            // datagrid
            // 
            this.datagrid.AllowUserToAddRows = false;
            this.datagrid.AllowUserToDeleteRows = false;
            this.datagrid.AllowUserToResizeColumns = false;
            this.datagrid.AllowUserToResizeRows = false;
            this.datagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datagrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAddress,
            this.colReadHex,
            this.colReadDec,
            this.colWriteHex,
            this.colWriteDec});
            this.datagrid.GridColor = System.Drawing.SystemColors.Control;
            this.datagrid.Location = new System.Drawing.Point(8, 23);
            this.datagrid.Name = "datagrid";
            this.datagrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.datagrid.Size = new System.Drawing.Size(668, 184);
            this.datagrid.TabIndex = 0;
            this.datagrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellEndEdit);
            // 
            // colAddress
            // 
            this.colAddress.HeaderText = "Endereços";
            this.colAddress.Name = "colAddress";
            this.colAddress.Width = 80;
            // 
            // colReadHex
            // 
            this.colReadHex.HeaderText = "Hex";
            this.colReadHex.Name = "colReadHex";
            this.colReadHex.ReadOnly = true;
            this.colReadHex.Width = 80;
            // 
            // colReadDec
            // 
            this.colReadDec.HeaderText = "Dec";
            this.colReadDec.Name = "colReadDec";
            this.colReadDec.ReadOnly = true;
            this.colReadDec.Width = 80;
            // 
            // colWriteHex
            // 
            this.colWriteHex.HeaderText = "Hex";
            this.colWriteHex.Name = "colWriteHex";
            this.colWriteHex.Width = 80;
            // 
            // colWriteDec
            // 
            this.colWriteDec.HeaderText = "Dec";
            this.colWriteDec.Name = "colWriteDec";
            this.colWriteDec.Width = 80;
            // 
            // labelRead
            // 
            this.labelRead.AutoSize = true;
            this.labelRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRead.Location = new System.Drawing.Point(211, 4);
            this.labelRead.Name = "labelRead";
            this.labelRead.Size = new System.Drawing.Size(64, 16);
            this.labelRead.TabIndex = 1;
            this.labelRead.Text = "LEITURA";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnConnected);
            this.panelButtons.Controls.Add(this.comboxPorts);
            this.panelButtons.Controls.Add(this.btnAddRow);
            this.panelButtons.Controls.Add(this.btnRmvRow);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 207);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(684, 46);
            this.panelButtons.TabIndex = 8;
            // 
            // comboxPorts
            // 
            this.comboxPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboxPorts.FormattingEnabled = true;
            this.comboxPorts.Location = new System.Drawing.Point(285, 12);
            this.comboxPorts.Name = "comboxPorts";
            this.comboxPorts.Size = new System.Drawing.Size(121, 23);
            this.comboxPorts.TabIndex = 5;
            // 
            // btnRmvRow
            // 
            this.btnRmvRow.Location = new System.Drawing.Point(123, 6);
            this.btnRmvRow.Name = "btnRmvRow";
            this.btnRmvRow.Size = new System.Drawing.Size(156, 34);
            this.btnRmvRow.TabIndex = 4;
            this.btnRmvRow.Text = "Remover Linha Selecionada";
            this.btnRmvRow.UseVisualStyleBackColor = true;
            this.btnRmvRow.Click += new System.EventHandler(this.rmvRowBtn_Click);
            // 
            // panelMsg
            // 
            this.panelMsg.Controls.Add(this.splitMsg);
            this.panelMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMsg.Location = new System.Drawing.Point(0, 253);
            this.panelMsg.Name = "panelMsg";
            this.panelMsg.Size = new System.Drawing.Size(684, 228);
            this.panelMsg.TabIndex = 9;
            // 
            // splitMsg
            // 
            this.splitMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMsg.IsSplitterFixed = true;
            this.splitMsg.Location = new System.Drawing.Point(0, 0);
            this.splitMsg.Name = "splitMsg";
            // 
            // splitMsg.Panel1
            // 
            this.splitMsg.Panel1.Controls.Add(this.textboxMsg);
            this.splitMsg.Panel1.Controls.Add(this.labelMsg);
            this.splitMsg.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitMsg.Panel2
            // 
            this.splitMsg.Panel2.Controls.Add(this.textboxCMDReg);
            this.splitMsg.Panel2.Controls.Add(this.labelCMDReg);
            this.splitMsg.Panel2.Controls.Add(this.btnBITSET);
            this.splitMsg.Panel2.Controls.Add(this.btnBITCLR);
            this.splitMsg.Panel2.Controls.Add(this.textboxCMDBit);
            this.splitMsg.Panel2.Controls.Add(this.labelSetBit);
            this.splitMsg.Panel2.Controls.Add(this.labelSetAddress);
            this.splitMsg.Panel2.Controls.Add(this.textboxCMDAddress);
            this.splitMsg.Panel2.Controls.Add(this.labelSpCMD);
            this.splitMsg.Panel2.Controls.Add(this.btnSendCmd);
            this.splitMsg.Panel2.Controls.Add(this.textboxCMD);
            this.splitMsg.Panel2.Controls.Add(this.labelCMD);
            this.splitMsg.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitMsg.Size = new System.Drawing.Size(684, 228);
            this.splitMsg.SplitterDistance = 332;
            this.splitMsg.SplitterWidth = 10;
            this.splitMsg.TabIndex = 0;
            // 
            // textboxMsg
            // 
            this.textboxMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxMsg.Location = new System.Drawing.Point(8, 25);
            this.textboxMsg.Multiline = true;
            this.textboxMsg.Name = "textboxMsg";
            this.textboxMsg.ReadOnly = true;
            this.textboxMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxMsg.Size = new System.Drawing.Size(316, 195);
            this.textboxMsg.TabIndex = 0;
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMsg.Location = new System.Drawing.Point(5, 5);
            this.labelMsg.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(78, 16);
            this.labelMsg.TabIndex = 0;
            this.labelMsg.Text = "Mensagens";
            // 
            // textboxCMDReg
            // 
            this.textboxCMDReg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxCMDReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxCMDReg.Location = new System.Drawing.Point(11, 163);
            this.textboxCMDReg.Multiline = true;
            this.textboxCMDReg.Name = "textboxCMDReg";
            this.textboxCMDReg.ReadOnly = true;
            this.textboxCMDReg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxCMDReg.Size = new System.Drawing.Size(317, 54);
            this.textboxCMDReg.TabIndex = 1;
            // 
            // labelCMDReg
            // 
            this.labelCMDReg.AutoSize = true;
            this.labelCMDReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCMDReg.Location = new System.Drawing.Point(8, 144);
            this.labelCMDReg.Name = "labelCMDReg";
            this.labelCMDReg.Size = new System.Drawing.Size(146, 16);
            this.labelCMDReg.TabIndex = 13;
            this.labelCMDReg.Text = "Registro de Comandos";
            // 
            // btnBITSET
            // 
            this.btnBITSET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBITSET.Location = new System.Drawing.Point(261, 82);
            this.btnBITSET.Name = "btnBITSET";
            this.btnBITSET.Size = new System.Drawing.Size(67, 52);
            this.btnBITSET.TabIndex = 12;
            this.btnBITSET.Text = "BITSET";
            this.btnBITSET.UseVisualStyleBackColor = true;
            // 
            // btnBITCLR
            // 
            this.btnBITCLR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBITCLR.Location = new System.Drawing.Point(188, 82);
            this.btnBITCLR.Name = "btnBITCLR";
            this.btnBITCLR.Size = new System.Drawing.Size(67, 52);
            this.btnBITCLR.TabIndex = 11;
            this.btnBITCLR.Text = "BITCLR";
            this.btnBITCLR.UseVisualStyleBackColor = true;
            // 
            // textboxCMDBit
            // 
            this.textboxCMDBit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxCMDBit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textboxCMDBit.Location = new System.Drawing.Point(47, 111);
            this.textboxCMDBit.Name = "textboxCMDBit";
            this.textboxCMDBit.Size = new System.Drawing.Size(135, 23);
            this.textboxCMDBit.TabIndex = 10;
            // 
            // labelSetBit
            // 
            this.labelSetBit.AutoSize = true;
            this.labelSetBit.Font = new System.Drawing.Font("Microsoft PhagsPa", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSetBit.Location = new System.Drawing.Point(12, 114);
            this.labelSetBit.Name = "labelSetBit";
            this.labelSetBit.Size = new System.Drawing.Size(29, 16);
            this.labelSetBit.TabIndex = 9;
            this.labelSetBit.Text = "Bit :";
            // 
            // labelSetAddress
            // 
            this.labelSetAddress.AutoSize = true;
            this.labelSetAddress.Font = new System.Drawing.Font("Microsoft PhagsPa", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSetAddress.Location = new System.Drawing.Point(12, 86);
            this.labelSetAddress.Name = "labelSetAddress";
            this.labelSetAddress.Size = new System.Drawing.Size(33, 16);
            this.labelSetAddress.TabIndex = 8;
            this.labelSetAddress.Text = "End :";
            // 
            // textboxCMDAddress
            // 
            this.textboxCMDAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxCMDAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textboxCMDAddress.Location = new System.Drawing.Point(47, 82);
            this.textboxCMDAddress.Name = "textboxCMDAddress";
            this.textboxCMDAddress.Size = new System.Drawing.Size(135, 23);
            this.textboxCMDAddress.TabIndex = 7;
            // 
            // labelSpCMD
            // 
            this.labelSpCMD.AutoSize = true;
            this.labelSpCMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpCMD.Location = new System.Drawing.Point(8, 63);
            this.labelSpCMD.Name = "labelSpCMD";
            this.labelSpCMD.Size = new System.Drawing.Size(136, 16);
            this.labelSpCMD.TabIndex = 6;
            this.labelSpCMD.Text = "Comandos Especiais";
            // 
            // btnSendCmd
            // 
            this.btnSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendCmd.Location = new System.Drawing.Point(261, 25);
            this.btnSendCmd.Name = "btnSendCmd";
            this.btnSendCmd.Size = new System.Drawing.Size(67, 23);
            this.btnSendCmd.TabIndex = 5;
            this.btnSendCmd.Text = "Enviar";
            this.btnSendCmd.UseVisualStyleBackColor = true;
            this.btnSendCmd.Click += new System.EventHandler(this.SendData);
            // 
            // textboxCMD
            // 
            this.textboxCMD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxCMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textboxCMD.Location = new System.Drawing.Point(11, 25);
            this.textboxCMD.Name = "textboxCMD";
            this.textboxCMD.Size = new System.Drawing.Size(244, 23);
            this.textboxCMD.TabIndex = 1;
            this.textboxCMD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmdBox_KeyDown);
            // 
            // labelCMD
            // 
            this.labelCMD.AutoSize = true;
            this.labelCMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCMD.Location = new System.Drawing.Point(8, 6);
            this.labelCMD.Name = "labelCMD";
            this.labelCMD.Size = new System.Drawing.Size(73, 16);
            this.labelCMD.TabIndex = 0;
            this.labelCMD.Text = "Comandos";
            // 
            // btnConnected
            // 
            this.btnConnected.Location = new System.Drawing.Point(412, 6);
            this.btnConnected.Name = "btnConnected";
            this.btnConnected.Size = new System.Drawing.Size(97, 34);
            this.btnConnected.TabIndex = 6;
            this.btnConnected.Text = "Conectar";
            this.btnConnected.UseVisualStyleBackColor = true;
            // 
            // App
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(684, 481);
            this.Controls.Add(this.panelMsg);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelDatagrid);
            this.MinimumSize = new System.Drawing.Size(600, 520);
            this.Name = "App";
            this.Text = "Gerenciamento de Memória do MSP430";
            this.panelDatagrid.ResumeLayout(false);
            this.panelDatagrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelMsg.ResumeLayout(false);
            this.splitMsg.Panel1.ResumeLayout(false);
            this.splitMsg.Panel1.PerformLayout();
            this.splitMsg.Panel2.ResumeLayout(false);
            this.splitMsg.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMsg)).EndInit();
            this.splitMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Panel panelDatagrid;
        private System.Windows.Forms.DataGridView datagrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReadHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReadDec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWriteHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWriteDec;
        private System.Windows.Forms.Label labelWrite;
        private System.Windows.Forms.Label labelRead;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelMsg;
        private System.Windows.Forms.TextBox textboxMsg;
        private System.Windows.Forms.SplitContainer splitMsg;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Label labelCMD;
        private System.Windows.Forms.Button btnRmvRow;
        private System.Windows.Forms.TextBox textboxCMD;
        private System.Windows.Forms.Button btnSendCmd;
        private System.Windows.Forms.ComboBox comboxPorts;
        private System.Windows.Forms.Label labelSetAddress;
        private System.Windows.Forms.TextBox textboxCMDAddress;
        private System.Windows.Forms.Label labelSpCMD;
        private System.Windows.Forms.TextBox textboxCMDReg;
        private System.Windows.Forms.Label labelCMDReg;
        private System.Windows.Forms.Button btnBITSET;
        private System.Windows.Forms.Button btnBITCLR;
        private System.Windows.Forms.TextBox textboxCMDBit;
        private System.Windows.Forms.Label labelSetBit;
        private System.Windows.Forms.Button btnConnected;
    }
}

