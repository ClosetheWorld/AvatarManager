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
            components = new System.ComponentModel.Container();
            avatarGrid = new DataGridView();
            editFormAvatarGridBindingSource = new BindingSource(components);
            saveButton = new Button();
            label1 = new Label();
            label2 = new Label();
            folderNameTextBox = new TextBox();
            isSelectedDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            avatarThumbnailDataGridViewImageColumn = new DataGridViewImageColumn();
            avatarNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            avatarIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)avatarGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)editFormAvatarGridBindingSource).BeginInit();
            SuspendLayout();
            // 
            // avatarGrid
            // 
            avatarGrid.AllowUserToAddRows = false;
            avatarGrid.AllowUserToDeleteRows = false;
            avatarGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            avatarGrid.AutoGenerateColumns = false;
            avatarGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            avatarGrid.Columns.AddRange(new DataGridViewColumn[] { isSelectedDataGridViewCheckBoxColumn, avatarThumbnailDataGridViewImageColumn, avatarNameDataGridViewTextBoxColumn, avatarIdDataGridViewTextBoxColumn });
            avatarGrid.DataSource = editFormAvatarGridBindingSource;
            avatarGrid.Location = new Point(12, 42);
            avatarGrid.MultiSelect = false;
            avatarGrid.Name = "avatarGrid";
            avatarGrid.ReadOnly = true;
            avatarGrid.RowHeadersVisible = false;
            avatarGrid.RowHeadersWidth = 45;
            avatarGrid.RowTemplate.Height = 70;
            avatarGrid.Size = new Size(776, 346);
            avatarGrid.TabIndex = 7;
            // 
            // editFormAvatarGridBindingSource
            // 
            editFormAvatarGridBindingSource.DataSource = typeof(Core.Models.Binding.EditFormAvatarGrid);
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
            // isSelectedDataGridViewCheckBoxColumn
            // 
            isSelectedDataGridViewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            isSelectedDataGridViewCheckBoxColumn.DataPropertyName = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn.FillWeight = 5F;
            isSelectedDataGridViewCheckBoxColumn.HeaderText = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn.Name = "isSelectedDataGridViewCheckBoxColumn";
            isSelectedDataGridViewCheckBoxColumn.ReadOnly = true;
            isSelectedDataGridViewCheckBoxColumn.Resizable = DataGridViewTriState.False;
            // 
            // avatarThumbnailDataGridViewImageColumn
            // 
            avatarThumbnailDataGridViewImageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            avatarThumbnailDataGridViewImageColumn.DataPropertyName = "AvatarThumbnail";
            avatarThumbnailDataGridViewImageColumn.FillWeight = 25F;
            avatarThumbnailDataGridViewImageColumn.HeaderText = "AvatarThumbnail";
            avatarThumbnailDataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            avatarThumbnailDataGridViewImageColumn.Name = "avatarThumbnailDataGridViewImageColumn";
            avatarThumbnailDataGridViewImageColumn.ReadOnly = true;
            // 
            // avatarNameDataGridViewTextBoxColumn
            // 
            avatarNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            avatarNameDataGridViewTextBoxColumn.DataPropertyName = "AvatarName";
            avatarNameDataGridViewTextBoxColumn.FillWeight = 75F;
            avatarNameDataGridViewTextBoxColumn.HeaderText = "AvatarName";
            avatarNameDataGridViewTextBoxColumn.Name = "avatarNameDataGridViewTextBoxColumn";
            avatarNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // avatarIdDataGridViewTextBoxColumn
            // 
            avatarIdDataGridViewTextBoxColumn.DataPropertyName = "AvatarId";
            avatarIdDataGridViewTextBoxColumn.HeaderText = "AvatarId";
            avatarIdDataGridViewTextBoxColumn.Name = "avatarIdDataGridViewTextBoxColumn";
            avatarIdDataGridViewTextBoxColumn.ReadOnly = true;
            avatarIdDataGridViewTextBoxColumn.Visible = false;
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
            ((System.ComponentModel.ISupportInitialize)editFormAvatarGridBindingSource).EndInit();
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
        private BindingSource editFormAvatarGridBindingSource;
        private DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private DataGridViewImageColumn avatarThumbnailDataGridViewImageColumn;
        private DataGridViewTextBoxColumn avatarNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn avatarIdDataGridViewTextBoxColumn;
    }
}