namespace LoadConsentToGo
{
    partial class FormSMSData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtGuardian1Email = new TextBox();
            txtLastName = new TextBox();
            txtFirstName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            BtnOk = new Button();
            btnCreate = new Button();
            label4 = new Label();
            txtGroup = new TextBox();
            txtUniqueId = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // txtGuardian1Email
            // 
            txtGuardian1Email.Location = new Point(161, 198);
            txtGuardian1Email.Margin = new Padding(1);
            txtGuardian1Email.Name = "txtGuardian1Email";
            txtGuardian1Email.Size = new Size(209, 23);
            txtGuardian1Email.TabIndex = 0;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(161, 165);
            txtLastName.Margin = new Padding(1);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(209, 23);
            txtLastName.TabIndex = 1;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(161, 132);
            txtFirstName.Margin = new Padding(1);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(209, 23);
            txtFirstName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 132);
            label1.Margin = new Padding(1, 0, 1, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 3;
            label1.Text = "First Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 165);
            label2.Margin = new Padding(1, 0, 1, 0);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 4;
            label2.Text = "Last Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(48, 200);
            label3.Margin = new Padding(1, 0, 1, 0);
            label3.Name = "label3";
            label3.Size = new Size(90, 15);
            label3.TabIndex = 5;
            label3.Text = "Guardian1Email";
            // 
            // BtnOk
            // 
            BtnOk.Location = new Point(57, 23);
            BtnOk.Margin = new Padding(1);
            BtnOk.Name = "BtnOk";
            BtnOk.Size = new Size(88, 37);
            BtnOk.TabIndex = 6;
            BtnOk.Text = "Skip";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += button1_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(191, 23);
            btnCreate.Margin = new Padding(1);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(88, 37);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += ButtonCreate_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 98);
            label4.Margin = new Padding(1, 0, 1, 0);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 8;
            label4.Text = "Group";
            // 
            // txtGroup
            // 
            txtGroup.Location = new Point(161, 95);
            txtGroup.Margin = new Padding(1);
            txtGroup.Name = "txtGroup";
            txtGroup.Size = new Size(209, 23);
            txtGroup.TabIndex = 9;
            // 
            // txtUniqueId
            // 
            txtUniqueId.Location = new Point(161, 238);
            txtUniqueId.Margin = new Padding(1);
            txtUniqueId.Name = "txtUniqueId";
            txtUniqueId.Size = new Size(209, 23);
            txtUniqueId.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(48, 238);
            label5.Margin = new Padding(1, 0, 1, 0);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 11;
            label5.Text = "UniqueId";
            label5.Click += label5_Click;
            // 
            // FormSMSData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 521);
            Controls.Add(label5);
            Controls.Add(txtUniqueId);
            Controls.Add(txtGroup);
            Controls.Add(label4);
            Controls.Add(btnCreate);
            Controls.Add(BtnOk);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtFirstName);
            Controls.Add(txtLastName);
            Controls.Add(txtGuardian1Email);
            Margin = new Padding(1);
            Name = "FormSMSData";
            Text = "FormSMSData";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtGuardian1Email;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button BtnOk;
        private Button btnCreate;
        private Label label4;
        private TextBox txtGroup;
        private TextBox txtUniqueId;
        private Label label5;
    }
}