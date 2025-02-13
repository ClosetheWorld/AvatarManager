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
            isSelectedDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            avatarThumbnailDataGridViewImageColumn = new DataGridViewImageColumn();
            avatarNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            avatarIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            editFormAvatarGridBindingSource = new BindingSource(components);
            saveButton = new Button();
            introLabel = new Label();
            folderNameLabel = new Label();
            folderNameTextBox = new TextBox();
            searchTextBox = new TextBox();
            avatarSearchLabel = new Label();
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
            avatarGrid.Location = new Point(12, 70);
            avatarGrid.MultiSelect = false;
            avatarGrid.Name = "avatarGrid";
            avatarGrid.RowHeadersVisible = false;
            avatarGrid.RowHeadersWidth = 45;
            avatarGrid.RowTemplate.Height = 70;
            avatarGrid.Size = new Size(776, 406);
            avatarGrid.TabIndex = 7;
            // 
            // isSelectedDataGridViewCheckBoxColumn
            // 
            isSelectedDataGridViewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            isSelectedDataGridViewCheckBoxColumn.DataPropertyName = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn.FillWeight = 5F;
            isSelectedDataGridViewCheckBoxColumn.Frozen = true;
            isSelectedDataGridViewCheckBoxColumn.HeaderText = "";
            isSelectedDataGridViewCheckBoxColumn.Name = "isSelectedDataGridViewCheckBoxColumn";
            isSelectedDataGridViewCheckBoxColumn.Resizable = DataGridViewTriState.False;
            isSelectedDataGridViewCheckBoxColumn.Width = 37;
            // 
            // avatarThumbnailDataGridViewImageColumn
            // 
            avatarThumbnailDataGridViewImageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            avatarThumbnailDataGridViewImageColumn.DataPropertyName = "AvatarThumbnail";
            avatarThumbnailDataGridViewImageColumn.FillWeight = 25F;
            avatarThumbnailDataGridViewImageColumn.HeaderText = "サムネイル";
            avatarThumbnailDataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            avatarThumbnailDataGridViewImageColumn.Name = "avatarThumbnailDataGridViewImageColumn";
            avatarThumbnailDataGridViewImageColumn.Resizable = DataGridViewTriState.False;
            // 
            // avatarNameDataGridViewTextBoxColumn
            // 
            avatarNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            avatarNameDataGridViewTextBoxColumn.DataPropertyName = "AvatarName";
            avatarNameDataGridViewTextBoxColumn.FillWeight = 75F;
            avatarNameDataGridViewTextBoxColumn.HeaderText = "アバター名";
            avatarNameDataGridViewTextBoxColumn.Name = "avatarNameDataGridViewTextBoxColumn";
            avatarNameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            // 
            // avatarIdDataGridViewTextBoxColumn
            // 
            avatarIdDataGridViewTextBoxColumn.DataPropertyName = "AvatarId";
            avatarIdDataGridViewTextBoxColumn.HeaderText = "AvatarId";
            avatarIdDataGridViewTextBoxColumn.Name = "avatarIdDataGridViewTextBoxColumn";
            avatarIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // editFormAvatarGridBindingSource
            // 
            editFormAvatarGridBindingSource.DataSource = typeof(Core.Models.Binding.EditFormAvatarGrid);
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            saveButton.Font = new Font("Yu Gothic UI", 12F);
            saveButton.Location = new Point(12, 482);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(776, 44);
            saveButton.TabIndex = 6;
            saveButton.Text = "保存";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // introLabel
            // 
            introLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            introLabel.AutoSize = true;
            introLabel.Font = new Font("Yu Gothic UI", 12F);
            introLabel.Location = new Point(503, 46);
            introLabel.Name = "introLabel";
            introLabel.Size = new Size(285, 21);
            introLabel.TabIndex = 8;
            introLabel.Text = "フォルダに含めるアバターをチェックしてください";
            // 
            // folderNameLabel
            // 
            folderNameLabel.AutoSize = true;
            folderNameLabel.Font = new Font("Yu Gothic UI", 12F);
            folderNameLabel.Location = new Point(12, 9);
            folderNameLabel.Name = "folderNameLabel";
            folderNameLabel.Size = new Size(74, 21);
            folderNameLabel.TabIndex = 9;
            folderNameLabel.Text = "フォルダ名";
            // 
            // folderNameTextBox
            // 
            folderNameTextBox.Font = new Font("Yu Gothic UI", 12F);
            folderNameTextBox.Location = new Point(92, 6);
            folderNameTextBox.Name = "folderNameTextBox";
            folderNameTextBox.Size = new Size(275, 29);
            folderNameTextBox.TabIndex = 10;
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            searchTextBox.Font = new Font("Yu Gothic UI", 12F);
            searchTextBox.Location = new Point(577, 6);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(211, 29);
            searchTextBox.TabIndex = 11;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // avatarSearchLabel
            // 
            avatarSearchLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            avatarSearchLabel.AutoSize = true;
            avatarSearchLabel.Font = new Font("Yu Gothic UI", 12F);
            avatarSearchLabel.Location = new Point(453, 9);
            avatarSearchLabel.Name = "avatarSearchLabel";
            avatarSearchLabel.Size = new Size(118, 21);
            avatarSearchLabel.TabIndex = 12;
            avatarSearchLabel.Text = "アバター名で検索";
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 538);
            Controls.Add(avatarGrid);
            Controls.Add(avatarSearchLabel);
            Controls.Add(searchTextBox);
            Controls.Add(folderNameTextBox);
            Controls.Add(folderNameLabel);
            Controls.Add(introLabel);
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
        private Button saveButton;
        private Label introLabel;
        private Label folderNameLabel;
        private TextBox folderNameTextBox;
        private TextBox searchTextBox;
        private Label avatarSearchLabel;
        private DataGridView avatarGrid;
        private Label label1;
        private Label label2;
        private BindingSource editFormAvatarGridBindingSource;
        private DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private DataGridViewImageColumn avatarThumbnailDataGridViewImageColumn;
        private DataGridViewTextBoxColumn avatarNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn avatarIdDataGridViewTextBoxColumn;
    }
}