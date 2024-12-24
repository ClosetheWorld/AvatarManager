namespace AvatarManager.WinForm.Forms
{
    partial class SettingForm
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
            avatarGrid = new DataGridView();
            ContainsCheck = new DataGridViewCheckBoxColumn();
            AvatarThumbnail = new DataGridViewImageColumn();
            AvatarName = new DataGridViewTextBoxColumn();
            AvatarId = new DataGridViewTextBoxColumn();
            saveButton = new Button();
            label1 = new Label();
            label2 = new Label();
            folderNameTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)avatarGrid).BeginInit();
            SuspendLayout();
            // 
            // avatarGrid
            // 
            avatarGrid.AllowUserToAddRows = false;
            avatarGrid.AllowUserToDeleteRows = false;
            avatarGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            avatarGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            avatarGrid.Columns.AddRange(new DataGridViewColumn[] { ContainsCheck, AvatarThumbnail, AvatarName, AvatarId });
            avatarGrid.Location = new Point(12, 42);
            avatarGrid.MultiSelect = false;
            avatarGrid.Name = "avatarGrid";
            avatarGrid.RowHeadersVisible = false;
            avatarGrid.RowHeadersWidth = 45;
            avatarGrid.Size = new Size(776, 346);
            avatarGrid.TabIndex = 7;
            // 
            // ContainsCheck
            // 
            ContainsCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ContainsCheck.FillWeight = 5F;
            ContainsCheck.HeaderText = "";
            ContainsCheck.Name = "ContainsCheck";
            ContainsCheck.Resizable = DataGridViewTriState.False;
            // 
            // AvatarThumbnail
            // 
            AvatarThumbnail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AvatarThumbnail.FillWeight = 25F;
            AvatarThumbnail.HeaderText = "サムネイル";
            AvatarThumbnail.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AvatarThumbnail.Name = "AvatarThumbnail";
            AvatarThumbnail.Resizable = DataGridViewTriState.False;
            // 
            // AvatarName
            // 
            AvatarName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AvatarName.FillWeight = 70F;
            AvatarName.HeaderText = "アバター名";
            AvatarName.Name = "AvatarName";
            // 
            // AvatarId
            // 
            AvatarId.HeaderText = "";
            AvatarId.Name = "AvatarId";
            AvatarId.Visible = false;
            AvatarId.Width = 5;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            saveButton.Font = new Font("Yu Gothic UI", 12F);
            saveButton.Location = new Point(12, 394);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(776, 44);
            saveButton.TabIndex = 6;
            saveButton.Text = "保存";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F);
            label1.Location = new Point(503, 9);
            label1.Name = "label1";
            label1.Size = new Size(285, 21);
            label1.TabIndex = 8;
            label1.Text = "フォルダに含めるアバターをチェックしてください";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F);
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(74, 21);
            label2.TabIndex = 9;
            label2.Text = "フォルダ名";
            // 
            // folderNameTextBox
            // 
            folderNameTextBox.Font = new Font("Yu Gothic UI", 12F);
            folderNameTextBox.Location = new Point(92, 6);
            folderNameTextBox.Name = "folderNameTextBox";
            folderNameTextBox.Size = new Size(275, 29);
            folderNameTextBox.TabIndex = 10;
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(folderNameTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(avatarGrid);
            Controls.Add(saveButton);
            Name = "SettingForm";
            Text = "フォルダ追加";
            Shown += SettingForm_Shown;
            ((System.ComponentModel.ISupportInitialize)avatarGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView avatarGrid;
        private DataGridViewCheckBoxColumn ContainsCheck;
        private DataGridViewImageColumn AvatarThumbnail;
        private DataGridViewTextBoxColumn AvatarName;
        private DataGridViewTextBoxColumn AvatarId;
        private Button saveButton;
        private Label label1;
        private Label label2;
        private TextBox folderNameTextBox;
    }
}