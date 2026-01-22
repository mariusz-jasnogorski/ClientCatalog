namespace ClientCatalog.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer? components = null;

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblSearch;
        internal System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSort;
        internal System.Windows.Forms.ComboBox cmbSortBy;
        internal System.Windows.Forms.CheckBox chkDesc;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Button btnEdit;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.DataGridView gridCustomers;

        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblSort = new Label();
            cmbSortBy = new ComboBox();
            chkDesc = new CheckBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            gridCustomers = new DataGridView();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCustomers).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblSearch);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(lblSort);
            pnlTop.Controls.Add(cmbSortBy);
            pnlTop.Controls.Add(chkDesc);
            pnlTop.Controls.Add(btnAdd);
            pnlTop.Controls.Add(btnEdit);
            pnlTop.Controls.Add(btnDelete);
            pnlTop.Controls.Add(btnRefresh);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Margin = new Padding(3, 4, 3, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(11, 13, 11, 13);
            pnlTop.Size = new Size(1257, 54);
            pnlTop.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblSearch.Location = new Point(11, 16);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(59, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(80, 11);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Name / NIP / Email / Phone / Address...";
            txtSearch.Size = new Size(365, 27);
            txtSearch.TabIndex = 1;
            // 
            // lblSort
            // 
            lblSort.AutoSize = true;
            lblSort.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblSort.Location = new Point(469, 16);
            lblSort.Name = "lblSort";
            lblSort.Size = new Size(42, 20);
            lblSort.TabIndex = 2;
            lblSort.Text = "Sort:";
            // 
            // cmbSortBy
            // 
            cmbSortBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSortBy.Items.AddRange(new object[] { "Name", "Nip", "Email", "Phone", "Address", "CreatedAtUtc", "UpdatedAtUtc" });
            cmbSortBy.Location = new Point(514, 11);
            cmbSortBy.Margin = new Padding(3, 4, 3, 4);
            cmbSortBy.Name = "cmbSortBy";
            cmbSortBy.Size = new Size(182, 28);
            cmbSortBy.TabIndex = 3;
            // 
            // chkDesc
            // 
            chkDesc.AutoSize = true;
            chkDesc.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            chkDesc.Location = new Point(709, 13);
            chkDesc.Margin = new Padding(3, 4, 3, 4);
            chkDesc.Name = "chkDesc";
            chkDesc.Size = new Size(64, 24);
            chkDesc.TabIndex = 4;
            chkDesc.Text = "DESC";
            chkDesc.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(822, 8);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(91, 37);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(926, 8);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(91, 37);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1029, 8);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(91, 37);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(1131, 8);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(91, 37);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // gridCustomers
            // 
            gridCustomers.AllowUserToAddRows = false;
            gridCustomers.AllowUserToDeleteRows = false;
            gridCustomers.Dock = DockStyle.Fill;
            gridCustomers.Location = new Point(0, 54);
            gridCustomers.Margin = new Padding(3, 4, 3, 4);
            gridCustomers.MultiSelect = false;
            gridCustomers.Name = "gridCustomers";
            gridCustomers.ReadOnly = true;
            gridCustomers.RowHeadersVisible = false;
            gridCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCustomers.Size = new Size(1257, 879);
            gridCustomers.TabIndex = 0;
            gridCustomers.CellContentClick += gridCustomers_CellContentClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 933);
            Controls.Add(gridCustomers);
            Controls.Add(pnlTop);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Client Catalog";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridCustomers).EndInit();
            ResumeLayout(false);
        }
    }
}
