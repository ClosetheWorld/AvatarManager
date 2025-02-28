namespace AvatarManager.WinForm.Forms
{
    partial class AuthForm
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
            authTokenInput = new TextBox();
            authButton = new Button();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            linkLabel2 = new LinkLabel();
            label5 = new Label();
            SuspendLayout();
            // 
            // authTokenInput
            // 
            authTokenInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            authTokenInput.Location = new Point(12, 156);
            authTokenInput.Name = "authTokenInput";
            authTokenInput.PlaceholderText = "authcookie_xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            authTokenInput.Size = new Size(360, 23);
            authTokenInput.TabIndex = 0;
            // 
            // authButton
            // 
            authButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            authButton.Font = new Font("Yu Gothic UI", 12F);
            authButton.Location = new Point(12, 201);
            authButton.Name = "authButton";
            authButton.Size = new Size(360, 32);
            authButton.TabIndex = 1;
            authButton.Text = "認証";
            authButton.UseVisualStyleBackColor = true;
            authButton.Click += authButton_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Yu Gothic UI", 12F);
            linkLabel1.Location = new Point(40, 21);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(223, 21);
            linkLabel1.TabIndex = 2;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://vrchat.com/home/login";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F);
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(22, 21);
            label1.TabIndex = 3;
            label1.Text = "1.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F);
            label2.Location = new Point(258, 21);
            label2.Name = "label2";
            label2.Size = new Size(70, 21);
            label2.TabIndex = 4;
            label2.Text = "へログイン";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F);
            label3.Location = new Point(258, 81);
            label3.Name = "label3";
            label3.Size = new Size(61, 21);
            label3.TabIndex = 7;
            label3.Text = "を開き、";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 12F);
            label4.Location = new Point(12, 81);
            label4.Name = "label4";
            label4.Size = new Size(22, 21);
            label4.TabIndex = 6;
            label4.Text = "2.";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Font = new Font("Yu Gothic UI", 12F);
            linkLabel2.Location = new Point(40, 81);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(215, 21);
            linkLabel2.TabIndex = 5;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "https://vrchat.com/api/1/auth";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 12F);
            label5.Location = new Point(40, 114);
            label5.Name = "label5";
            label5.Size = new Size(318, 21);
            label5.TabIndex = 8;
            label5.Text = "authcookie_xxxxxxxxxx をコピーして↓に貼り付け";
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(linkLabel2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(linkLabel1);
            Controls.Add(authButton);
            Controls.Add(authTokenInput);
            MaximumSize = new Size(400, 400);
            MinimumSize = new Size(400, 400);
            Name = "AuthForm";
            Text = "認証";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox authTokenInput;
        private Button authButton;
        private LinkLabel linkLabel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private LinkLabel linkLabel2;
        private Label label5;
    }
}