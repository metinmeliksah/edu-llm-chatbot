namespace EduChatbot.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label apiKeyLabel;
        private System.Windows.Forms.TextBox apiKeyTextBox;
        private System.Windows.Forms.Panel chatScrollPanel;
        private System.Windows.Forms.FlowLayoutPanel chatPanel;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendButton;

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
            headerPanel = new Panel();
            titleLabel = new Label();
            apiKeyLabel = new Label();
            apiKeyTextBox = new TextBox();
            chatScrollPanel = new Panel();
            chatPanel = new FlowLayoutPanel();
            inputPanel = new Panel();
            sendButton = new Button();
            messageTextBox = new TextBox();
            headerPanel.SuspendLayout();
            chatScrollPanel.SuspendLayout();
            inputPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(37, 37, 38);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(apiKeyLabel);
            headerPanel.Controls.Add(apiKeyTextBox);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(10);
            headerPanel.Size = new Size(800, 50);
            headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(10, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Gemini Chat";
            // 
            // apiKeyLabel
            // 
            apiKeyLabel.AutoSize = true;
            apiKeyLabel.ForeColor = Color.FromArgb(170, 170, 170);
            apiKeyLabel.Location = new Point(522, 21);
            apiKeyLabel.Name = "apiKeyLabel";
            apiKeyLabel.Size = new Size(50, 15);
            apiKeyLabel.TabIndex = 1;
            apiKeyLabel.Text = "API Key:";
            // 
            // apiKeyTextBox
            // 
            apiKeyTextBox.BackColor = Color.FromArgb(51, 51, 51);
            apiKeyTextBox.BorderStyle = BorderStyle.None;
            apiKeyTextBox.ForeColor = Color.White;
            apiKeyTextBox.Location = new Point(578, 20);
            apiKeyTextBox.Name = "apiKeyTextBox";
            apiKeyTextBox.Size = new Size(200, 16);
            apiKeyTextBox.TabIndex = 2;
            // 
            // chatScrollPanel
            // 
            chatScrollPanel.AutoScroll = true;
            chatScrollPanel.BackColor = Color.FromArgb(30, 30, 30);
            chatScrollPanel.Controls.Add(chatPanel);
            chatScrollPanel.Dock = DockStyle.Fill;
            chatScrollPanel.Location = new Point(0, 50);
            chatScrollPanel.Name = "chatScrollPanel";
            chatScrollPanel.Padding = new Padding(10);
            chatScrollPanel.Size = new Size(800, 490);
            chatScrollPanel.TabIndex = 1;
            chatScrollPanel.Resize += ChatScrollPanel_Resize;
            // 
            // chatPanel
            // 
            chatPanel.AutoSize = true;
            chatPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            chatPanel.BackColor = Color.FromArgb(30, 30, 30);
            chatPanel.Dock = DockStyle.Top;
            chatPanel.FlowDirection = FlowDirection.TopDown;
            chatPanel.Location = new Point(10, 10);
            chatPanel.Name = "chatPanel";
            chatPanel.Padding = new Padding(10);
            chatPanel.Size = new Size(780, 20);
            chatPanel.TabIndex = 0;
            chatPanel.WrapContents = false;
            // 
            // inputPanel
            // 
            inputPanel.BackColor = Color.FromArgb(37, 37, 38);
            inputPanel.Controls.Add(sendButton);
            inputPanel.Controls.Add(messageTextBox);
            inputPanel.Dock = DockStyle.Bottom;
            inputPanel.Location = new Point(0, 540);
            inputPanel.Name = "inputPanel";
            inputPanel.Padding = new Padding(10);
            inputPanel.Size = new Size(800, 60);
            inputPanel.TabIndex = 2;
            // 
            // sendButton
            // 
            sendButton.BackColor = Color.FromArgb(0, 120, 215);
            sendButton.Dock = DockStyle.Right;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.ForeColor = Color.White;
            sendButton.Location = new Point(690, 10);
            sendButton.Margin = new Padding(10, 0, 0, 0);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(100, 40);
            sendButton.TabIndex = 1;
            sendButton.Text = "Gönder";
            sendButton.UseVisualStyleBackColor = false;
            // 
            // messageTextBox
            // 
            messageTextBox.BackColor = Color.FromArgb(51, 51, 51);
            messageTextBox.BorderStyle = BorderStyle.None;
            messageTextBox.Dock = DockStyle.Fill;
            messageTextBox.Font = new Font("Segoe UI", 9F);
            messageTextBox.ForeColor = Color.White;
            messageTextBox.Location = new Point(10, 10);
            messageTextBox.Multiline = true;
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(780, 40);
            messageTextBox.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(800, 600);
            Controls.Add(chatScrollPanel);
            Controls.Add(inputPanel);
            Controls.Add(headerPanel);
            ForeColor = Color.White;
            Name = "MainForm";
            Text = "C# Gemini Chatbot";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            chatScrollPanel.ResumeLayout(false);
            chatScrollPanel.PerformLayout();
            inputPanel.ResumeLayout(false);
            inputPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}

