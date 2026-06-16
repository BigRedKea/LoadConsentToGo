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
            btnDownload = new Button();
            btnUpload = new Button();
            SuspendLayout();
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(244, 157);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(602, 201);
            btnDownload.TabIndex = 0;
            btnDownload.Text = "Download Consent2Go Data";
            btnDownload.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(227, 510);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(602, 201);
            btnUpload.TabIndex = 1;
            btnUpload.Text = "Upload Consent2Go Data";
            btnUpload.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 1220);
            Controls.Add(btnUpload);
            Controls.Add(btnDownload);
            Name = "FormMain";
            Text = "FormMain";
            ResumeLayout(false);
        }

        #endregion

        private Button btnDownload;
        private Button btnUpload;
    }
}