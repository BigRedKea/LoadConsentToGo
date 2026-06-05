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
            SuspendLayout();
            // 
            // txtGuardian1Email
            // 
            txtGuardian1Email.Location = new Point(391, 542);
            txtGuardian1Email.Name = "txtGuardian1Email";
            txtGuardian1Email.Size = new Size(503, 47);
            txtGuardian1Email.TabIndex = 0;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(391, 452);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(503, 47);
            txtLastName.TabIndex = 1;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(391, 361);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(503, 47);
            txtFirstName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(114, 360);
            label1.Name = "label1";
            label1.Size = new Size(160, 41);
            label1.TabIndex = 3;
            label1.Text = "First Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(114, 452);
            label2.Name = "label2";
            label2.Size = new Size(157, 41);
            label2.TabIndex = 4;
            label2.Text = "Last Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(117, 548);
            label3.Name = "label3";
            label3.Size = new Size(224, 41);
            label3.TabIndex = 5;
            label3.Text = "Guardian1Email";
            // 
            // BtnOk
            // 
            BtnOk.Location = new Point(138, 62);
            BtnOk.Name = "BtnOk";
            BtnOk.Size = new Size(213, 101);
            BtnOk.TabIndex = 6;
            BtnOk.Text = "OK";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += button1_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(463, 62);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(213, 101);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += ButtonCreate_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(117, 267);
            label4.Name = "label4";
            label4.Size = new Size(102, 41);
            label4.TabIndex = 8;
            label4.Text = "Group";
            // 
            // txtGroup
            // 
            txtGroup.Location = new Point(391, 261);
            txtGroup.Name = "txtGroup";
            txtGroup.Size = new Size(503, 47);
            txtGroup.TabIndex = 9;
            // 
            // FormSMSData
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1418, 1424);
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
    }
}