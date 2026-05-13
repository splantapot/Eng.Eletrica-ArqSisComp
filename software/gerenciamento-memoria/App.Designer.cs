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
            this.addRowBtn = new System.Windows.Forms.Button();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.writeLabel = new System.Windows.Forms.Label();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.addressCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readHexCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readDec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.writeHexCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.writeDecCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readLabel = new System.Windows.Forms.Label();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.portBox = new System.Windows.Forms.ComboBox();
            this.rmvRowBtn = new System.Windows.Forms.Button();
            this.boxPanel = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.msgBox = new System.Windows.Forms.TextBox();
            this.msgLabel = new System.Windows.Forms.Label();
            this.sendCmdBtn = new System.Windows.Forms.Button();
            this.cmdBox = new System.Windows.Forms.TextBox();
            this.cmdLabel = new System.Windows.Forms.Label();
            this.dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.btnPanel.SuspendLayout();
            this.boxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // addRowBtn
            // 
            this.addRowBtn.Location = new System.Drawing.Point(8, 6);
            this.addRowBtn.Name = "addRowBtn";
            this.addRowBtn.Size = new System.Drawing.Size(109, 34);
            this.addRowBtn.TabIndex = 1;
            this.addRowBtn.Text = "Adicionar Linha";
            this.addRowBtn.UseVisualStyleBackColor = true;
            this.addRowBtn.Click += new System.EventHandler(this.addRowBtn_Click);
            // 
            // dataPanel
            // 
            this.dataPanel.Controls.Add(this.writeLabel);
            this.dataPanel.Controls.Add(this.dataGrid);
            this.dataPanel.Controls.Add(this.readLabel);
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataPanel.Location = new System.Drawing.Point(0, 0);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(687, 207);
            this.dataPanel.TabIndex = 7;
            // 
            // writeLabel
            // 
            this.writeLabel.AutoSize = true;
            this.writeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeLabel.Location = new System.Drawing.Point(409, 4);
            this.writeLabel.Name = "writeLabel";
            this.writeLabel.Size = new System.Drawing.Size(65, 16);
            this.writeLabel.TabIndex = 2;
            this.writeLabel.Text = "ESCRITA";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeColumns = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.addressCol,
            this.readHexCol,
            this.readDec,
            this.writeHexCol,
            this.writeDecCol});
            this.dataGrid.GridColor = System.Drawing.SystemColors.Control;
            this.dataGrid.Location = new System.Drawing.Point(8, 23);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid.Size = new System.Drawing.Size(671, 184);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellEndEdit);
            // 
            // addressCol
            // 
            this.addressCol.HeaderText = "Endereços";
            this.addressCol.Name = "addressCol";
            // 
            // readHexCol
            // 
            this.readHexCol.HeaderText = "Hex";
            this.readHexCol.Name = "readHexCol";
            this.readHexCol.ReadOnly = true;
            // 
            // readDec
            // 
            this.readDec.HeaderText = "Dec";
            this.readDec.Name = "readDec";
            this.readDec.ReadOnly = true;
            // 
            // writeHexCol
            // 
            this.writeHexCol.HeaderText = "Hex";
            this.writeHexCol.Name = "writeHexCol";
            // 
            // writeDecCol
            // 
            this.writeDecCol.HeaderText = "Dec";
            this.writeDecCol.Name = "writeDecCol";
            // 
            // readLabel
            // 
            this.readLabel.AutoSize = true;
            this.readLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readLabel.Location = new System.Drawing.Point(211, 4);
            this.readLabel.Name = "readLabel";
            this.readLabel.Size = new System.Drawing.Size(64, 16);
            this.readLabel.TabIndex = 1;
            this.readLabel.Text = "LEITURA";
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.portBox);
            this.btnPanel.Controls.Add(this.addRowBtn);
            this.btnPanel.Controls.Add(this.rmvRowBtn);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPanel.Location = new System.Drawing.Point(0, 207);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(687, 46);
            this.btnPanel.TabIndex = 8;
            // 
            // portBox
            // 
            this.portBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portBox.FormattingEnabled = true;
            this.portBox.Location = new System.Drawing.Point(285, 12);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(121, 23);
            this.portBox.TabIndex = 5;
            // 
            // rmvRowBtn
            // 
            this.rmvRowBtn.Location = new System.Drawing.Point(123, 6);
            this.rmvRowBtn.Name = "rmvRowBtn";
            this.rmvRowBtn.Size = new System.Drawing.Size(156, 34);
            this.rmvRowBtn.TabIndex = 4;
            this.rmvRowBtn.Text = "Remover Linha Selecionada";
            this.rmvRowBtn.UseVisualStyleBackColor = true;
            this.rmvRowBtn.Click += new System.EventHandler(this.rmvRowBtn_Click);
            // 
            // boxPanel
            // 
            this.boxPanel.Controls.Add(this.splitContainer);
            this.boxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxPanel.Location = new System.Drawing.Point(0, 253);
            this.boxPanel.Name = "boxPanel";
            this.boxPanel.Size = new System.Drawing.Size(687, 197);
            this.boxPanel.TabIndex = 9;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.msgBox);
            this.splitContainer.Panel1.Controls.Add(this.msgLabel);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.sendCmdBtn);
            this.splitContainer.Panel2.Controls.Add(this.cmdBox);
            this.splitContainer.Panel2.Controls.Add(this.cmdLabel);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer.Size = new System.Drawing.Size(687, 197);
            this.splitContainer.SplitterDistance = 334;
            this.splitContainer.SplitterWidth = 10;
            this.splitContainer.TabIndex = 0;
            // 
            // msgBox
            // 
            this.msgBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgBox.Location = new System.Drawing.Point(8, 25);
            this.msgBox.Multiline = true;
            this.msgBox.Name = "msgBox";
            this.msgBox.ReadOnly = true;
            this.msgBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.msgBox.Size = new System.Drawing.Size(318, 164);
            this.msgBox.TabIndex = 0;
            // 
            // msgLabel
            // 
            this.msgLabel.AutoSize = true;
            this.msgLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLabel.Location = new System.Drawing.Point(5, 5);
            this.msgLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(78, 16);
            this.msgLabel.TabIndex = 0;
            this.msgLabel.Text = "Mensagens";
            // 
            // sendCmdBtn
            // 
            this.sendCmdBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendCmdBtn.Location = new System.Drawing.Point(243, 52);
            this.sendCmdBtn.Name = "sendCmdBtn";
            this.sendCmdBtn.Size = new System.Drawing.Size(76, 28);
            this.sendCmdBtn.TabIndex = 5;
            this.sendCmdBtn.Text = "Enviar";
            this.sendCmdBtn.UseVisualStyleBackColor = true;
            this.sendCmdBtn.Click += new System.EventHandler(this.SendData);
            // 
            // cmdBox
            // 
            this.cmdBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBox.Location = new System.Drawing.Point(11, 25);
            this.cmdBox.Name = "cmdBox";
            this.cmdBox.Size = new System.Drawing.Size(308, 21);
            this.cmdBox.TabIndex = 1;
            this.cmdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmdBox_KeyDown);
            // 
            // cmdLabel
            // 
            this.cmdLabel.AutoSize = true;
            this.cmdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLabel.Location = new System.Drawing.Point(8, 6);
            this.cmdLabel.Name = "cmdLabel";
            this.cmdLabel.Size = new System.Drawing.Size(73, 16);
            this.cmdLabel.TabIndex = 0;
            this.cmdLabel.Text = "Comandos";
            // 
            // App
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(687, 450);
            this.Controls.Add(this.boxPanel);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.dataPanel);
            this.Name = "App";
            this.Text = "Gerenciamento de Memória do MSP430";
            this.dataPanel.ResumeLayout(false);
            this.dataPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.btnPanel.ResumeLayout(false);
            this.boxPanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addRowBtn;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn readHexCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn readDec;
        private System.Windows.Forms.DataGridViewTextBoxColumn writeHexCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn writeDecCol;
        private System.Windows.Forms.Label writeLabel;
        private System.Windows.Forms.Label readLabel;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Panel boxPanel;
        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label msgLabel;
        private System.Windows.Forms.Label cmdLabel;
        private System.Windows.Forms.Button rmvRowBtn;
        private System.Windows.Forms.TextBox cmdBox;
        private System.Windows.Forms.Button sendCmdBtn;
        private System.Windows.Forms.ComboBox portBox;
    }
}

