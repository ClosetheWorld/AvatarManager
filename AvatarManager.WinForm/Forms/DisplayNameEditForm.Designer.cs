namespace AvatarManager.WinForm.Forms
{
    partial class DisplayNameEditForm
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
            introductionLabel = new Label();
            searchTextBox = new TextBox();
            saveButton = new Button();
            SuspendLayout();
            // 
            // introductionLabel
            // 
            introductionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            introductionLabel.AutoSize = true;
            introductionLabel.Font = new Font("Yu Gothic UI", 12F);
            introductionLabel.Location = new Point(12, 9);
            introductionLabel.Name = "introductionLabel";
            introductionLabel.Size = new Size(229, 21);
            introductionLabel.TabIndex = 1;
            introductionLabel.Text = "設定する表示名を入力してください";
            introductionLabel.UseWaitCursor = true;
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            searchTextBox.Font = new Font("Yu Gothic UI", 12F);
            searchTextBox.Location = new Point(12, 33);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(360, 29);
            searchTextBox.TabIndex = 12;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            saveButton.Font = new Font("Yu Gothic UI", 12F);
            saveButton.Location = new Point(12, 67);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(360, 32);
            saveButton.TabIndex = 13;
            saveButton.Text = "保存";
            saveButton.UseVisualStyleBackColor = true;
            // 
            // DisplayNameEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 111);
            Controls.Add(saveButton);
            Controls.Add(searchTextBox);
            Controls.Add(introductionLabel);
            MaximumSize = new Size(400, 150);
            MinimumSize = new Size(400, 150);
            Name = "DisplayNameEditForm";
            Text = "表示名を変更";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label introductionLabel;
        private TextBox searchTextBox;
        private Button saveButton;
    }
}