namespace ClientCatalog.WinForms
{
    partial class CustomerDetailForm
    {
        private System.ComponentModel.IContainer? components = null;

        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.TextBox txtNip;
        internal System.Windows.Forms.TextBox txtAddress;
        internal System.Windows.Forms.TextBox txtPhone;
        internal System.Windows.Forms.TextBox txtEmail;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNip;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtName = new TextBox();
            txtNip = new TextBox();
            txtAddress = new TextBox();
            txtPhone = new TextBox();
            txtEmail = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            tableLayoutPanel = new TableLayoutPanel();
            lblName = new Label();
            lblNip = new Label();
            lblAddress = new Label();
            lblPhone = new Label();
            lblEmail = new Label();
            flowButtons = new FlowLayoutPanel();
            tableLayoutPanel.SuspendLayout();
            flowButtons.SuspendLayout();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Location = new Point(154, 20);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(515, 27);
            txtName.TabIndex = 1;
            // 
            // txtNip
            // 
            txtNip.Dock = DockStyle.Fill;
            txtNip.Location = new Point(154, 71);
            txtNip.Margin = new Padding(3, 4, 3, 4);
            txtNip.Name = "txtNip";
            txtNip.Size = new Size(515, 27);
            txtNip.TabIndex = 3;
            // 
            // txtAddress
            // 
            txtAddress.Dock = DockStyle.Fill;
            txtAddress.Location = new Point(154, 122);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(515, 27);
            txtAddress.TabIndex = 5;
            // 
            // txtPhone
            // 
            txtPhone.Dock = DockStyle.Fill;
            txtPhone.Location = new Point(154, 173);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(515, 27);
            txtPhone.TabIndex = 7;
            // 
            // txtEmail
            // 
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.Location = new Point(154, 224);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(515, 27);
            txtEmail.TabIndex = 9;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(546, 4);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 31);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(437, 4);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(103, 31);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.Controls.Add(txtName, 1, 0);
            tableLayoutPanel.Controls.Add(lblNip, 0, 1);
            tableLayoutPanel.Controls.Add(txtNip, 1, 1);
            tableLayoutPanel.Controls.Add(lblAddress, 0, 2);
            tableLayoutPanel.Controls.Add(txtAddress, 1, 2);
            tableLayoutPanel.Controls.Add(lblPhone, 0, 3);
            tableLayoutPanel.Controls.Add(txtPhone, 1, 3);
            tableLayoutPanel.Controls.Add(lblEmail, 0, 4);
            tableLayoutPanel.Controls.Add(txtEmail, 1, 4);
            tableLayoutPanel.Controls.Add(flowButtons, 0, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(14, 16, 14, 16);
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 67F));
            tableLayoutPanel.Size = new Size(686, 340);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Left;
            lblName.AutoSize = true;
            lblName.Location = new Point(17, 27);
            lblName.Name = "lblName";
            lblName.Padding = new Padding(0, 8, 0, 0);
            lblName.Size = new Size(59, 28);
            lblName.TabIndex = 0;
            lblName.Text = "Name *";
            // 
            // lblNip
            // 
            lblNip.Anchor = AnchorStyles.Left;
            lblNip.AutoSize = true;
            lblNip.Location = new Point(17, 78);
            lblNip.Name = "lblNip";
            lblNip.Padding = new Padding(0, 8, 0, 0);
            lblNip.Size = new Size(42, 28);
            lblNip.TabIndex = 2;
            lblNip.Text = "NIP *";
            // 
            // lblAddress
            // 
            lblAddress.Anchor = AnchorStyles.Left;
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(17, 129);
            lblAddress.Name = "lblAddress";
            lblAddress.Padding = new Padding(0, 8, 0, 0);
            lblAddress.Size = new Size(62, 28);
            lblAddress.TabIndex = 4;
            lblAddress.Text = "Address";
            // 
            // lblPhone
            // 
            lblPhone.Anchor = AnchorStyles.Left;
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(17, 180);
            lblPhone.Name = "lblPhone";
            lblPhone.Padding = new Padding(0, 8, 0, 0);
            lblPhone.Size = new Size(50, 28);
            lblPhone.TabIndex = 6;
            lblPhone.Text = "Phone";
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Left;
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(17, 231);
            lblEmail.Name = "lblEmail";
            lblEmail.Padding = new Padding(0, 8, 0, 0);
            lblEmail.Size = new Size(46, 28);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "Email";
            // 
            // flowButtons
            // 
            tableLayoutPanel.SetColumnSpan(flowButtons, 2);
            flowButtons.Controls.Add(btnSave);
            flowButtons.Controls.Add(btnCancel);
            flowButtons.Dock = DockStyle.Fill;
            flowButtons.FlowDirection = FlowDirection.RightToLeft;
            flowButtons.Location = new Point(17, 275);
            flowButtons.Margin = new Padding(3, 4, 3, 4);
            flowButtons.Name = "flowButtons";
            flowButtons.Size = new Size(652, 59);
            flowButtons.TabIndex = 10;
            // 
            // CustomerDetailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 340);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Customer";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            flowButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
