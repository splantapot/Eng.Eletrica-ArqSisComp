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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.colIndexes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReadHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReadDec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWriteHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWriteDec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnDesconnect = new System.Windows.Forms.Button();
            this.btnConnected = new System.Windows.Forms.Button();
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
            this.splitTop = new System.Windows.Forms.SplitContainer();
            this.textBoxUserMsg = new System.Windows.Forms.TextBox();
            this.labelUserMsg = new System.Windows.Forms.Label();
            this.btnAdd4Rows = new System.Windows.Forms.Button();
            this.btnClearMsg = new System.Windows.Forms.Button();
            this.btnClearUserMsg = new System.Windows.Forms.Button();
            this.panelDatagrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMsg)).BeginInit();
            this.splitMsg.Panel1.SuspendLayout();
            this.splitMsg.Panel2.SuspendLayout();
            this.splitMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTop)).BeginInit();
            this.splitTop.Panel1.SuspendLayout();
            this.splitTop.Panel2.SuspendLayout();
            this.splitTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(8, 6);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 34);
            this.btnAddRow.TabIndex = 1;
            this.btnAddRow.Text = "Adicionar Linha";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // panelDatagrid
            // 
            this.panelDatagrid.Controls.Add(this.splitTop);
            this.panelDatagrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDatagrid.Location = new System.Drawing.Point(0, 0);
            this.panelDatagrid.Name = "panelDatagrid";
            this.panelDatagrid.Size = new System.Drawing.Size(684, 207);
            this.panelDatagrid.TabIndex = 7;
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeColumns = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndexes,
            this.colReadHex,
            this.colReadDec,
            this.colWriteHex,
            this.colWriteDec});
            this.dataGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dataGrid.Location = new System.Drawing.Point(3, 32);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 30;
            this.dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid.Size = new System.Drawing.Size(336, 172);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellEndEdit);
            // 
            // colIndexes
            // 
            this.colIndexes.HeaderText = "Índices";
            this.colIndexes.Name = "colIndexes";
            this.colIndexes.Width = 60;
            // 
            // colReadHex
            // 
            this.colReadHex.HeaderText = "Hex";
            this.colReadHex.Name = "colReadHex";
            this.colReadHex.ReadOnly = true;
            this.colReadHex.ToolTipText = "Ler Hexadecimal";
            this.colReadHex.Width = 55;
            // 
            // colReadDec
            // 
            this.colReadDec.HeaderText = "Dec";
            this.colReadDec.Name = "colReadDec";
            this.colReadDec.ReadOnly = true;
            this.colReadDec.ToolTipText = "Ver Decimal";
            this.colReadDec.Width = 55;
            // 
            // colWriteHex
            // 
            this.colWriteHex.HeaderText = "Hex";
            this.colWriteHex.Name = "colWriteHex";
            this.colWriteHex.ToolTipText = "Escrever Hexadecimal";
            this.colWriteHex.Width = 55;
            // 
            // colWriteDec
            // 
            this.colWriteDec.HeaderText = "Dec";
            this.colWriteDec.Name = "colWriteDec";
            this.colWriteDec.ToolTipText = "Escrever Decimal";
            this.colWriteDec.Width = 55;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnAdd4Rows);
            this.panelButtons.Controls.Add(this.btnDesconnect);
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
            // btnDesconnect
            // 
            this.btnDesconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDesconnect.Location = new System.Drawing.Point(601, 6);
            this.btnDesconnect.Name = "btnDesconnect";
            this.btnDesconnect.Size = new System.Drawing.Size(80, 34);
            this.btnDesconnect.TabIndex = 7;
            this.btnDesconnect.Text = "Desconectar";
            this.btnDesconnect.UseVisualStyleBackColor = true;
            this.btnDesconnect.Click += new System.EventHandler(this.btnDesconnect_Click);
            // 
            // btnConnected
            // 
            this.btnConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnected.Location = new System.Drawing.Point(515, 6);
            this.btnConnected.Name = "btnConnected";
            this.btnConnected.Size = new System.Drawing.Size(80, 34);
            this.btnConnected.TabIndex = 6;
            this.btnConnected.Text = "Conectar";
            this.btnConnected.UseVisualStyleBackColor = true;
            this.btnConnected.Click += new System.EventHandler(this.btnConnected_Click);
            // 
            // comboxPorts
            // 
            this.comboxPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboxPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboxPorts.FormattingEnabled = true;
            this.comboxPorts.Location = new System.Drawing.Point(405, 12);
            this.comboxPorts.Name = "comboxPorts";
            this.comboxPorts.Size = new System.Drawing.Size(104, 23);
            this.comboxPorts.TabIndex = 5;
            this.comboxPorts.SelectedIndexChanged += new System.EventHandler(this.comboxPorts_SelectedIndexChanged);
            // 
            // btnRmvRow
            // 
            this.btnRmvRow.Location = new System.Drawing.Point(123, 6);
            this.btnRmvRow.Name = "btnRmvRow";
            this.btnRmvRow.Size = new System.Drawing.Size(99, 34);
            this.btnRmvRow.TabIndex = 4;
            this.btnRmvRow.Text = "Remover Linha Selecionada";
            this.btnRmvRow.UseVisualStyleBackColor = true;
            this.btnRmvRow.Click += new System.EventHandler(this.btnRmvRow_Click);
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
            this.splitMsg.Panel1.Controls.Add(this.btnClearMsg);
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
            this.splitMsg.SplitterDistance = 342;
            this.splitMsg.SplitterWidth = 6;
            this.splitMsg.TabIndex = 0;
            // 
            // textboxMsg
            // 
            this.textboxMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxMsg.Location = new System.Drawing.Point(8, 34);
            this.textboxMsg.Multiline = true;
            this.textboxMsg.Name = "textboxMsg";
            this.textboxMsg.ReadOnly = true;
            this.textboxMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxMsg.Size = new System.Drawing.Size(326, 186);
            this.textboxMsg.TabIndex = 0;
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMsg.Location = new System.Drawing.Point(8, 11);
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
            this.textboxCMDReg.Size = new System.Drawing.Size(263, 54);
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
            this.btnBITSET.Location = new System.Drawing.Point(207, 82);
            this.btnBITSET.Name = "btnBITSET";
            this.btnBITSET.Size = new System.Drawing.Size(67, 52);
            this.btnBITSET.TabIndex = 12;
            this.btnBITSET.Text = "BITSET";
            this.btnBITSET.UseVisualStyleBackColor = true;
            // 
            // btnBITCLR
            // 
            this.btnBITCLR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBITCLR.Location = new System.Drawing.Point(134, 82);
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
            this.textboxCMDBit.Size = new System.Drawing.Size(81, 23);
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
            this.textboxCMDAddress.Size = new System.Drawing.Size(81, 23);
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
            this.btnSendCmd.Location = new System.Drawing.Point(207, 25);
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
            this.textboxCMD.Size = new System.Drawing.Size(190, 23);
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
            // splitTop
            // 
            this.splitTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTop.Location = new System.Drawing.Point(0, 0);
            this.splitTop.Name = "splitTop";
            // 
            // splitTop.Panel1
            // 
            this.splitTop.Panel1.Controls.Add(this.dataGrid);
            // 
            // splitTop.Panel2
            // 
            this.splitTop.Panel2.Controls.Add(this.btnClearUserMsg);
            this.splitTop.Panel2.Controls.Add(this.labelUserMsg);
            this.splitTop.Panel2.Controls.Add(this.textBoxUserMsg);
            this.splitTop.Size = new System.Drawing.Size(684, 207);
            this.splitTop.SplitterDistance = 342;
            this.splitTop.SplitterWidth = 6;
            this.splitTop.TabIndex = 1;
            // 
            // textBoxUserMsg
            // 
            this.textBoxUserMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUserMsg.Location = new System.Drawing.Point(3, 32);
            this.textBoxUserMsg.Multiline = true;
            this.textBoxUserMsg.Name = "textBoxUserMsg";
            this.textBoxUserMsg.ReadOnly = true;
            this.textBoxUserMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUserMsg.Size = new System.Drawing.Size(330, 169);
            this.textBoxUserMsg.TabIndex = 1;
            // 
            // labelUserMsg
            // 
            this.labelUserMsg.AutoSize = true;
            this.labelUserMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserMsg.Location = new System.Drawing.Point(7, 9);
            this.labelUserMsg.Name = "labelUserMsg";
            this.labelUserMsg.Size = new System.Drawing.Size(147, 16);
            this.labelUserMsg.TabIndex = 14;
            this.labelUserMsg.Text = "Mensagens do Usuário";
            // 
            // btnAdd4Rows
            // 
            this.btnAdd4Rows.Location = new System.Drawing.Point(89, 6);
            this.btnAdd4Rows.Name = "btnAdd4Rows";
            this.btnAdd4Rows.Size = new System.Drawing.Size(30, 34);
            this.btnAdd4Rows.TabIndex = 9;
            this.btnAdd4Rows.Text = "x4";
            this.btnAdd4Rows.UseVisualStyleBackColor = true;
            this.btnAdd4Rows.Click += new System.EventHandler(this.btnAdd4Rows_Click);
            // 
            // btnClearMsg
            // 
            this.btnClearMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearMsg.Location = new System.Drawing.Point(267, 8);
            this.btnClearMsg.Name = "btnClearMsg";
            this.btnClearMsg.Size = new System.Drawing.Size(67, 23);
            this.btnClearMsg.TabIndex = 14;
            this.btnClearMsg.Text = "Limpar";
            this.btnClearMsg.UseVisualStyleBackColor = true;
            this.btnClearMsg.Click += new System.EventHandler(this.btnClearMsg_Click);
            // 
            // btnClearUserMsg
            // 
            this.btnClearUserMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearUserMsg.Location = new System.Drawing.Point(266, 6);
            this.btnClearUserMsg.Name = "btnClearUserMsg";
            this.btnClearUserMsg.Size = new System.Drawing.Size(67, 23);
            this.btnClearUserMsg.TabIndex = 15;
            this.btnClearUserMsg.Text = "Limpar";
            this.btnClearUserMsg.UseVisualStyleBackColor = true;
            this.btnClearUserMsg.Click += new System.EventHandler(this.btnClearUserMsg_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelMsg.ResumeLayout(false);
            this.splitMsg.Panel1.ResumeLayout(false);
            this.splitMsg.Panel1.PerformLayout();
            this.splitMsg.Panel2.ResumeLayout(false);
            this.splitMsg.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMsg)).EndInit();
            this.splitMsg.ResumeLayout(false);
            this.splitTop.Panel1.ResumeLayout(false);
            this.splitTop.Panel2.ResumeLayout(false);
            this.splitTop.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTop)).EndInit();
            this.splitTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Panel panelDatagrid;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndexes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReadHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReadDec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWriteHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWriteDec;
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
        private System.Windows.Forms.Button btnDesconnect;
        private System.Windows.Forms.SplitContainer splitTop;
        private System.Windows.Forms.Label labelUserMsg;
        private System.Windows.Forms.TextBox textBoxUserMsg;
        private System.Windows.Forms.Button btnAdd4Rows;
        private System.Windows.Forms.Button btnClearUserMsg;
        private System.Windows.Forms.Button btnClearMsg;
    }
}

