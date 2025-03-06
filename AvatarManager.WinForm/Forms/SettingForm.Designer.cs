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
            avatarGridBindingSource = new BindingSource(components);
            saveButton = new Button();
            introLabel = new Label();
            folderNameLabel = new Label();
            folderNameTextBox = new TextBox();
            searchTextBox = new TextBox();
            avatarSearchLabel = new Label();
            avatarSelectTabControl = new TabControl();
            allAvatarTab = new TabPage();
            uncategorizedAvatarTab = new TabPage();
            uncategorizedAvatarGrid = new DataGridView();
            dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
            dataGridViewImageColumn1 = new DataGridViewImageColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)avatarGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)avatarGridBindingSource).BeginInit();
            avatarSelectTabControl.SuspendLayout();
            allAvatarTab.SuspendLayout();
            uncategorizedAvatarTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)uncategorizedAvatarGrid).BeginInit();
            SuspendLayout();
            // 
            // avatarGrid
            // 
            avatarGrid.AllowUserToAddRows = false;
            avatarGrid.AllowUserToDeleteRows = false;
            avatarGrid.AutoGenerateColumns = false;
            avatarGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            avatarGrid.Columns.AddRange(new DataGridViewColumn[] { isSelectedDataGridViewCheckBoxColumn, avatarThumbnailDataGridViewImageColumn, avatarNameDataGridViewTextBoxColumn, avatarIdDataGridViewTextBoxColumn });
            avatarGrid.DataSource = avatarGridBindingSource;
            avatarGrid.Dock = DockStyle.Fill;
            avatarGrid.Location = new Point(3, 3);
            avatarGrid.MultiSelect = false;
            avatarGrid.Name = "avatarGrid";
            avatarGrid.RowHeadersVisible = false;
            avatarGrid.RowHeadersWidth = 45;
            avatarGrid.RowTemplate.Height = 70;
            avatarGrid.Size = new Size(762, 396);
            avatarGrid.TabIndex = 7;
            avatarGrid.KeyDown += avatarGrid_KeyDown;
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
            // avatarGridBindingSource
            // 
            avatarGridBindingSource.DataSource = typeof(Core.Models.Binding.EditFormAvatarGrid);
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
            // avatarSelectTabControl
            // 
            avatarSelectTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            avatarSelectTabControl.Controls.Add(allAvatarTab);
            avatarSelectTabControl.Controls.Add(uncategorizedAvatarTab);
            avatarSelectTabControl.Location = new Point(12, 46);
            avatarSelectTabControl.Name = "avatarSelectTabControl";
            avatarSelectTabControl.SelectedIndex = 0;
            avatarSelectTabControl.Size = new Size(776, 430);
            avatarSelectTabControl.TabIndex = 13;
            avatarSelectTabControl.SelectedIndexChanged += avatarSelectTabControl_SelectedIndexChanged;
            // 
            // allAvatarTab
            // 
            allAvatarTab.Controls.Add(avatarGrid);
            allAvatarTab.Location = new Point(4, 24);
            allAvatarTab.Name = "allAvatarTab";
            allAvatarTab.Padding = new Padding(3);
            allAvatarTab.Size = new Size(768, 402);
            allAvatarTab.TabIndex = 0;
            allAvatarTab.Text = "全アバター";
            allAvatarTab.UseVisualStyleBackColor = true;
            // 
            // uncategorizedAvatarTab
            // 
            uncategorizedAvatarTab.Controls.Add(uncategorizedAvatarGrid);
            uncategorizedAvatarTab.Location = new Point(4, 24);
            uncategorizedAvatarTab.Name = "uncategorizedAvatarTab";
            uncategorizedAvatarTab.Padding = new Padding(3);
            uncategorizedAvatarTab.Size = new Size(768, 402);
            uncategorizedAvatarTab.TabIndex = 1;
            uncategorizedAvatarTab.Text = "未分類";
            uncategorizedAvatarTab.UseVisualStyleBackColor = true;
            // 
            // uncategorizedAvatarGrid
            // 
            uncategorizedAvatarGrid.AllowUserToAddRows = false;
            uncategorizedAvatarGrid.AllowUserToDeleteRows = false;
            uncategorizedAvatarGrid.AutoGenerateColumns = false;
            uncategorizedAvatarGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            uncategorizedAvatarGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewCheckBoxColumn1, dataGridViewImageColumn1, dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
            uncategorizedAvatarGrid.DataSource = avatarGridBindingSource;
            uncategorizedAvatarGrid.Dock = DockStyle.Fill;
            uncategorizedAvatarGrid.Location = new Point(3, 3);
            uncategorizedAvatarGrid.MultiSelect = false;
            uncategorizedAvatarGrid.Name = "uncategorizedAvatarGrid";
            uncategorizedAvatarGrid.RowHeadersVisible = false;
            uncategorizedAvatarGrid.RowHeadersWidth = 45;
            uncategorizedAvatarGrid.RowTemplate.Height = 70;
            uncategorizedAvatarGrid.Size = new Size(762, 396);
            uncategorizedAvatarGrid.TabIndex = 8;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCheckBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCheckBoxColumn1.DataPropertyName = "IsSelected";
            dataGridViewCheckBoxColumn1.FillWeight = 5F;
            dataGridViewCheckBoxColumn1.Frozen = true;
            dataGridViewCheckBoxColumn1.HeaderText = "";
            dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            dataGridViewCheckBoxColumn1.Resizable = DataGridViewTriState.False;
            dataGridViewCheckBoxColumn1.Width = 37;
            // 
            // dataGridViewImageColumn1
            // 
            dataGridViewImageColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewImageColumn1.DataPropertyName = "AvatarThumbnail";
            dataGridViewImageColumn1.FillWeight = 25F;
            dataGridViewImageColumn1.HeaderText = "サムネイル";
            dataGridViewImageColumn1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            dataGridViewImageColumn1.Resizable = DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn1.DataPropertyName = "AvatarName";
            dataGridViewTextBoxColumn1.FillWeight = 75F;
            dataGridViewTextBoxColumn1.HeaderText = "アバター名";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "AvatarId";
            dataGridViewTextBoxColumn2.HeaderText = "AvatarId";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Visible = false;
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 538);
            Controls.Add(avatarSelectTabControl);
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
            ((System.ComponentModel.ISupportInitialize)avatarGridBindingSource).EndInit();
            avatarSelectTabControl.ResumeLayout(false);
            allAvatarTab.ResumeLayout(false);
            uncategorizedAvatarTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)uncategorizedAvatarGrid).EndInit();
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
        private BindingSource avatarGridBindingSource;
        private DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private DataGridViewImageColumn avatarThumbnailDataGridViewImageColumn;
        private DataGridViewTextBoxColumn avatarNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn avatarIdDataGridViewTextBoxColumn;
        private TabControl avatarSelectTabControl;
        private TabPage allAvatarTab;
        private TabPage uncategorizedAvatarTab;
        private DataGridView uncategorizedAvatarGrid;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridViewImageColumn dataGridViewImageColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}