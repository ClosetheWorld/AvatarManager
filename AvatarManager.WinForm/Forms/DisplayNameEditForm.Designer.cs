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
            displayNameTextBox = new TextBox();
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
            // displayNameTextBox
            // 
            displayNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            displayNameTextBox.Font = new Font("Yu Gothic UI", 12F);
            displayNameTextBox.Location = new Point(12, 33);
            displayNameTextBox.Name = "displayNameTextBox";
            displayNameTextBox.Size = new Size(360, 29);
            displayNameTextBox.TabIndex = 12;
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
            saveButton.Click += saveButton_Click;
            // 
            // DisplayNameEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 111);
            Controls.Add(saveButton);
            Controls.Add(displayNameTextBox);
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
        private TextBox displayNameTextBox;
        private Button saveButton;
    }
}