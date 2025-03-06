namespace AvatarManager.WinForm.Forms
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            userName = new Label();
            loginPlaceHolder = new Label();
            avatarGrid = new DataGridView();
            avatarGridBindingSource = new BindingSource(components);
            folderGrid = new DataGridView();
            folderNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            folderGridBindingSource = new BindingSource(components);
            settingButton = new Button();
            folderRightClickMenu = new ContextMenuStrip(components);
            editMenuItem = new ToolStripMenuItem();
            deleteMenuItem = new ToolStripMenuItem();
            avatarRightClickMenu = new ContextMenuStrip(components);
            editAvatarDisplayNameMenuItem = new ToolStripMenuItem();
            avatarThumbnailDataGridViewImageColumn = new DataGridViewImageColumn();
            avatarNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            avatarIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)avatarGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)avatarGridBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)folderGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)folderGridBindingSource).BeginInit();
            folderRightClickMenu.SuspendLayout();
            avatarRightClickMenu.SuspendLayout();
            SuspendLayout();
            // 
            // userName
            // 
            userName.AutoSize = true;
            userName.Font = new Font("Yu Gothic UI", 12F);
            userName.Location = new Point(157, 8);
            userName.Name = "userName";
            userName.Size = new Size(82, 21);
            userName.TabIndex = 0;
            userName.Text = "userName";
            // 
            // loginPlaceHolder
            // 
            loginPlaceHolder.AutoSize = true;
            loginPlaceHolder.Font = new Font("Yu Gothic UI", 12F);
            loginPlaceHolder.Location = new Point(12, 8);
            loginPlaceHolder.Name = "loginPlaceHolder";
            loginPlaceHolder.Size = new Size(139, 21);
            loginPlaceHolder.TabIndex = 1;
            loginPlaceHolder.Text = "ログイン中のユーザー: ";
            // 
            // avatarGrid
            // 
            avatarGrid.AllowUserToAddRows = false;
            avatarGrid.AllowUserToDeleteRows = false;
            avatarGrid.AllowUserToResizeRows = false;
            avatarGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            avatarGrid.AutoGenerateColumns = false;
            avatarGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            avatarGrid.Columns.AddRange(new DataGridViewColumn[] { avatarThumbnailDataGridViewImageColumn, avatarNameDataGridViewTextBoxColumn, avatarIdDataGridViewTextBoxColumn });
            avatarGrid.DataSource = avatarGridBindingSource;
            avatarGrid.Location = new Point(245, 32);
            avatarGrid.MultiSelect = false;
            avatarGrid.Name = "avatarGrid";
            avatarGrid.ReadOnly = true;
            avatarGrid.RowHeadersVisible = false;
            avatarGrid.RowHeadersWidth = 45;
            avatarGrid.Size = new Size(527, 517);
            avatarGrid.TabIndex = 2;
            avatarGrid.CellClick += avatarGrid_CellClick;
            // 
            // avatarGridBindingSource
            // 
            avatarGridBindingSource.DataSource = typeof(Core.Models.Binding.MainWindowAvatarGrid);
            // 
            // folderGrid
            // 
            folderGrid.AllowUserToAddRows = false;
            folderGrid.AllowUserToDeleteRows = false;
            folderGrid.AllowUserToResizeRows = false;
            folderGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            folderGrid.AutoGenerateColumns = false;
            folderGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            folderGrid.ColumnHeadersVisible = false;
            folderGrid.Columns.AddRange(new DataGridViewColumn[] { folderNameDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn });
            folderGrid.DataSource = folderGridBindingSource;
            folderGrid.Location = new Point(12, 32);
            folderGrid.MultiSelect = false;
            folderGrid.Name = "folderGrid";
            folderGrid.ReadOnly = true;
            folderGrid.RowHeadersVisible = false;
            folderGrid.Size = new Size(227, 477);
            folderGrid.TabIndex = 3;
            folderGrid.CellClick += folderGrid_CellClick;
            // 
            // folderNameDataGridViewTextBoxColumn
            // 
            folderNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            folderNameDataGridViewTextBoxColumn.DataPropertyName = "FolderName";
            folderNameDataGridViewTextBoxColumn.HeaderText = "";
            folderNameDataGridViewTextBoxColumn.Name = "folderNameDataGridViewTextBoxColumn";
            folderNameDataGridViewTextBoxColumn.ReadOnly = true;
            folderNameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            idDataGridViewTextBoxColumn.Visible = false;
            // 
            // folderGridBindingSource
            // 
            folderGridBindingSource.DataSource = typeof(Core.Models.Binding.MainWindowFolderGrid);
            // 
            // settingButton
            // 
            settingButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            settingButton.Font = new Font("Yu Gothic UI Light", 12F);
            settingButton.Location = new Point(12, 515);
            settingButton.Name = "settingButton";
            settingButton.Size = new Size(227, 34);
            settingButton.TabIndex = 4;
            settingButton.Text = "フォルダ追加";
            settingButton.UseVisualStyleBackColor = true;
            settingButton.Click += settingButton_Click;
            // 
            // folderRightClickMenu
            // 
            folderRightClickMenu.Items.AddRange(new ToolStripItem[] { editMenuItem, deleteMenuItem });
            folderRightClickMenu.Name = "contextMenuStrip1";
            folderRightClickMenu.RenderMode = ToolStripRenderMode.System;
            folderRightClickMenu.Size = new Size(99, 48);
            folderRightClickMenu.Opening += folderRightClickMenu_Opening;
            // 
            // editMenuItem
            // 
            editMenuItem.Name = "editMenuItem";
            editMenuItem.Size = new Size(98, 22);
            editMenuItem.Text = "編集";
            editMenuItem.Click += editMenuItem_Click;
            // 
            // deleteMenuItem
            // 
            deleteMenuItem.Name = "deleteMenuItem";
            deleteMenuItem.Size = new Size(98, 22);
            deleteMenuItem.Text = "削除";
            deleteMenuItem.Click += deleteMenuItem_Click;
            // 
            // avatarRightClickMenu
            // 
            avatarRightClickMenu.Items.AddRange(new ToolStripItem[] { editAvatarDisplayNameMenuItem });
            avatarRightClickMenu.Name = "avatarRightClickMenu";
            avatarRightClickMenu.RenderMode = ToolStripRenderMode.System;
            avatarRightClickMenu.Size = new Size(144, 26);
            avatarRightClickMenu.Opening += avatarRightClickMenu_Opening;
            // 
            // editAvatarDisplayNameMenuItem
            // 
            editAvatarDisplayNameMenuItem.Name = "editAvatarDisplayNameMenuItem";
            editAvatarDisplayNameMenuItem.Size = new Size(143, 22);
            editAvatarDisplayNameMenuItem.Text = "表示名を編集";
            editAvatarDisplayNameMenuItem.Click += editAvatarDisplayNameMenuItem_Click;
            // 
            // avatarThumbnailDataGridViewImageColumn
            // 
            avatarThumbnailDataGridViewImageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            avatarThumbnailDataGridViewImageColumn.DataPropertyName = "AvatarThumbnail";
            avatarThumbnailDataGridViewImageColumn.FillWeight = 20F;
            avatarThumbnailDataGridViewImageColumn.HeaderText = "サムネイル";
            avatarThumbnailDataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            avatarThumbnailDataGridViewImageColumn.Name = "avatarThumbnailDataGridViewImageColumn";
            avatarThumbnailDataGridViewImageColumn.ReadOnly = true;
            avatarThumbnailDataGridViewImageColumn.Resizable = DataGridViewTriState.False;
            // 
            // avatarNameDataGridViewTextBoxColumn
            // 
            avatarNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            avatarNameDataGridViewTextBoxColumn.DataPropertyName = "AvatarName";
            avatarNameDataGridViewTextBoxColumn.FillWeight = 80F;
            avatarNameDataGridViewTextBoxColumn.HeaderText = "アバター名";
            avatarNameDataGridViewTextBoxColumn.Name = "avatarNameDataGridViewTextBoxColumn";
            avatarNameDataGridViewTextBoxColumn.ReadOnly = true;
            avatarNameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            // 
            // avatarIdDataGridViewTextBoxColumn
            // 
            avatarIdDataGridViewTextBoxColumn.DataPropertyName = "AvatarId";
            avatarIdDataGridViewTextBoxColumn.HeaderText = "AvatarId";
            avatarIdDataGridViewTextBoxColumn.Name = "avatarIdDataGridViewTextBoxColumn";
            avatarIdDataGridViewTextBoxColumn.ReadOnly = true;
            avatarIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(settingButton);
            Controls.Add(folderGrid);
            Controls.Add(avatarGrid);
            Controls.Add(loginPlaceHolder);
            Controls.Add(userName);
            MaximumSize = new Size(800, 2160);
            MinimumSize = new Size(800, 600);
            Name = "MainWindow";
            Text = "AvatarManager";
            Shown += MainWindow_Shown;
            ((System.ComponentModel.ISupportInitialize)avatarGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)avatarGridBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)folderGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)folderGridBindingSource).EndInit();
            folderRightClickMenu.ResumeLayout(false);
            avatarRightClickMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label userName;
        private Label loginPlaceHolder;
        private DataGridView avatarGrid;
        private DataGridView folderGrid;
        private Button settingButton;
        private ContextMenuStrip folderRightClickMenu;
        private ToolStripMenuItem editMenuItem;
        private ToolStripMenuItem deleteMenuItem;
        private ContextMenuStrip avatarRightClickMenu;
        private ToolStripMenuItem editAvatarDisplayNameMenuItem;
        private BindingSource avatarGridBindingSource;
        private BindingSource folderGridBindingSource;
        private DataGridViewTextBoxColumn folderNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewImageColumn avatarThumbnailDataGridViewImageColumn;
        private DataGridViewTextBoxColumn avatarNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn avatarIdDataGridViewTextBoxColumn;
    }
}
