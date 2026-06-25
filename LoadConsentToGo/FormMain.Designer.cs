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
            SuspendLayout();

            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(244, 735);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(602, 201);
            btnUpload.TabIndex = 1;
            btnUpload.Text = "Upload Consent2Go Data";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // btnExcelToSqlLite
            // 
            btnExcelToSqlLite.Location = new Point(244, 447);
            btnExcelToSqlLite.Name = "btnExcelToSqlLite";
            btnExcelToSqlLite.Size = new Size(602, 201);
            btnExcelToSqlLite.TabIndex = 2;
            btnExcelToSqlLite.Text = "Excel to SqlLite";
            btnExcelToSqlLite.UseVisualStyleBackColor = true;
            btnExcelToSqlLite.Click += btnExcelToSqlLite_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 1220);
            Controls.Add(btnExcelToSqlLite);
            Controls.Add(btnUpload);
            Name = "FormMain";
            Text = "FormMain";
            ResumeLayout(false);
        }

        #endregion

        private Button btnUpload;
        private Button btnExcelToSqlLite;
    }
}