namespace AvatarManager.WinForm.Forms
{
    partial class LoadingForm
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
            loadingLabel = new Label();
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // loadingLabel
            // 
            loadingLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            loadingLabel.AutoSize = true;
            loadingLabel.Font = new Font("Yu Gothic UI", 12F);
            loadingLabel.Location = new Point(87, 13);
            loadingLabel.Name = "loadingLabel";
            loadingLabel.Size = new Size(155, 21);
            loadingLabel.TabIndex = 0;
            loadingLabel.Text = "アバターの読み込み中...";
            loadingLabel.UseWaitCursor = true;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(12, 51);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(310, 23);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 1;
            progressBar1.UseWaitCursor = true;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 86);
            Controls.Add(progressBar1);
            Controls.Add(loadingLabel);
            MaximizeBox = false;
            MaximumSize = new Size(350, 125);
            MinimizeBox = false;
            MinimumSize = new Size(350, 125);
            Name = "LoadingForm";
            UseWaitCursor = true;
            FormClosing += LoadingForm_FormClosing;
            Shown += LoadingForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label loadingLabel;
        private ProgressBar progressBar1;
    }
}