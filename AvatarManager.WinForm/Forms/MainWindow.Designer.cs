namespace AvatarManager.Winform
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
            userName = new Label();
            loginPlaceHolder = new Label();
            avatarGrid = new DataGridView();
            AvatarThumbnail = new DataGridViewImageColumn();
            AvatarName = new DataGridViewTextBoxColumn();
            AvatarId = new DataGridViewTextBoxColumn();
            folderGrid = new DataGridView();
            Folders = new DataGridViewTextBoxColumn();
            FolderId = new DataGridViewTextBoxColumn();
            settingButton = new Button();
            ((System.ComponentModel.ISupportInitialize)avatarGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)folderGrid).BeginInit();
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
            avatarGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            avatarGrid.Columns.AddRange(new DataGridViewColumn[] { AvatarThumbnail, AvatarName, AvatarId });
            avatarGrid.Location = new Point(245, 32);
            avatarGrid.MultiSelect = false;
            avatarGrid.Name = "avatarGrid";
            avatarGrid.ReadOnly = true;
            avatarGrid.RowHeadersVisible = false;
            avatarGrid.RowHeadersWidth = 45;
            avatarGrid.Size = new Size(527, 392);
            avatarGrid.TabIndex = 2;
            avatarGrid.CellClick += avatarGrid_CellClick;
            // 
            // AvatarThumbnail
            // 
            AvatarThumbnail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AvatarThumbnail.FillWeight = 25F;
            AvatarThumbnail.HeaderText = "サムネイル";
            AvatarThumbnail.ImageLayout = DataGridViewImageCellLayout.Zoom;
            AvatarThumbnail.Name = "AvatarThumbnail";
            AvatarThumbnail.ReadOnly = true;
            AvatarThumbnail.Resizable = DataGridViewTriState.False;
            // 
            // AvatarName
            // 
            AvatarName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AvatarName.FillWeight = 75F;
            AvatarName.HeaderText = "アバター名";
            AvatarName.Name = "AvatarName";
            AvatarName.ReadOnly = true;
            // 
            // AvatarId
            // 
            AvatarId.HeaderText = "";
            AvatarId.Name = "AvatarId";
            AvatarId.ReadOnly = true;
            AvatarId.Visible = false;
            // 
            // folderGrid
            // 
            folderGrid.AllowUserToAddRows = false;
            folderGrid.AllowUserToDeleteRows = false;
            folderGrid.AllowUserToResizeRows = false;
            folderGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            folderGrid.ColumnHeadersVisible = false;
            folderGrid.Columns.AddRange(new DataGridViewColumn[] { Folders, FolderId });
            folderGrid.Location = new Point(12, 32);
            folderGrid.MultiSelect = false;
            folderGrid.Name = "folderGrid";
            folderGrid.ReadOnly = true;
            folderGrid.RowHeadersVisible = false;
            folderGrid.Size = new Size(227, 362);
            folderGrid.TabIndex = 3;
            folderGrid.CellClick += folderGrid_CellClick;
            // 
            // Folders
            // 
            Folders.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Folders.HeaderText = "";
            Folders.Name = "Folders";
            Folders.ReadOnly = true;
            Folders.Resizable = DataGridViewTriState.False;
            Folders.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // FolderId
            // 
            FolderId.HeaderText = "";
            FolderId.Name = "FolderId";
            FolderId.ReadOnly = true;
            FolderId.Visible = false;
            // 
            // settingButton
            // 
            settingButton.Font = new Font("Yu Gothic UI Light", 12F);
            settingButton.Location = new Point(12, 400);
            settingButton.Name = "settingButton";
            settingButton.Size = new Size(227, 34);
            settingButton.TabIndex = 4;
            settingButton.Text = "フォルダ追加";
            settingButton.UseVisualStyleBackColor = true;
            settingButton.Click += settingButton_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 436);
            Controls.Add(settingButton);
            Controls.Add(folderGrid);
            Controls.Add(avatarGrid);
            Controls.Add(loginPlaceHolder);
            Controls.Add(userName);
            MinimumSize = new Size(800, 475);
            Name = "MainWindow";
            Text = "AvatarManager";
            Shown += MainWindow_Shown;
            ((System.ComponentModel.ISupportInitialize)avatarGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)folderGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label userName;
        private Label loginPlaceHolder;
        private DataGridView avatarGrid;
        private DataGridView folderGrid;
        private Button settingButton;
        private DataGridViewTextBoxColumn Folders;
        private DataGridViewTextBoxColumn FolderId;
        private DataGridViewImageColumn AvatarThumbnail;
        private DataGridViewTextBoxColumn AvatarName;
        private DataGridViewTextBoxColumn AvatarId;
    }
}
