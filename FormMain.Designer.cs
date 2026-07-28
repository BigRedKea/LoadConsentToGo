namespace LoadConsentToGo
{
    partial class FormMain
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
            btnUpload = new Button();
            btnExcelToSqlLite = new Button();
            staff_data_upload_button = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(10, 197);
            btnUpload.Margin = new Padding(1);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(166, 74);
            btnUpload.TabIndex = 1;
            btnUpload.Text = "Upload Consent2Go Data";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // btnExcelToSqlLite
            // 
            btnExcelToSqlLite.Location = new Point(10, 81);
            btnExcelToSqlLite.Margin = new Padding(1);
            btnExcelToSqlLite.Name = "btnExcelToSqlLite";
            btnExcelToSqlLite.Size = new Size(166, 74);
            btnExcelToSqlLite.TabIndex = 2;
            btnExcelToSqlLite.Text = "Student Excel to SqlLite";
            btnExcelToSqlLite.UseVisualStyleBackColor = true;
            btnExcelToSqlLite.Click += btnExcelToSqlLite_Click;
            // 
            // staff_data_upload_button
            // 
            staff_data_upload_button.Location = new Point(232, 81);
            staff_data_upload_button.Margin = new Padding(1);
            staff_data_upload_button.Name = "staff_data_upload_button";
            staff_data_upload_button.Size = new Size(166, 74);
            staff_data_upload_button.TabIndex = 3;
            staff_data_upload_button.Text = "Staff Excel to SqlLite";
            staff_data_upload_button.UseVisualStyleBackColor = true;
            staff_data_upload_button.Click += staff_data_upload_button_Click;
            // 
            // button1
            // 
            button1.Location = new Point(232, 197);
            button1.Margin = new Padding(1);
            button1.Name = "button1";
            button1.Size = new Size(166, 74);
            button1.TabIndex = 4;
            button1.Text = "Upload Staff Data";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 446);
            Controls.Add(button1);
            Controls.Add(staff_data_upload_button);
            Controls.Add(btnExcelToSqlLite);
            Controls.Add(btnUpload);
            Margin = new Padding(1);
            Name = "FormMain";
            Text = "FormMain";
            ResumeLayout(false);
        }

        #endregion

        private Button btnUpload;
        private Button btnExcelToSqlLite;
        private Button staff_data_upload_button;
        private Button button1;
    }
}