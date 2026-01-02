using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using EduChatbot.WinForms.ViewModels;
using EduLlmChatbot.Core.Models;

namespace EduChatbot.WinForms;

public partial class MainForm : Form
{
    private MainViewModel? _viewModel;
    private Panel _loadingPanel;
    private Label _loadingLabel;
    private readonly System.Windows.Forms.Timer _loadingTimer;
    private int _loadingDots;

    // Designer'dan gelen kontroller için field'lar (Designer dosyasında tanımlı)
    // Bu field'lar Designer tarafından otomatik oluşturulur
    private TextBox _apiKeyTextBox => apiKeyTextBox;
    private TextBox _messageTextBox => messageTextBox;
    private Button _sendButton => sendButton;
    private FlowLayoutPanel _chatPanel => chatPanel;
    private Panel _headerPanel => headerPanel;
    private Panel _inputPanel => inputPanel;

    public MainForm()
    {
        _loadingTimer = new System.Windows.Forms.Timer { Interval = 500 };
        _loadingTimer.Tick += LoadingTimer_Tick;
        
        InitializeComponent();
        
        _viewModel = new MainViewModel();
        
        SetupBindings();
        
        // Auto scroll and update display
        ((INotifyCollectionChanged)_viewModel.Messages).CollectionChanged += Messages_CollectionChanged;
        
        // Initial display - biraz bekle ki UI hazır olsun
        this.Load += (s, e) => 
        {
            UpdateChatDisplay();
        };
    }

    private void Messages_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add || 
            e.Action == NotifyCollectionChangedAction.Reset ||
            e.Action == NotifyCollectionChangedAction.Replace)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    UpdateChatDisplay();
                    ScrollToBottom();
                }));
            }
            else
            {
                UpdateChatDisplay();
                ScrollToBottom();
            }
        }
    }

    private void SetupBindings()
    {
        _apiKeyTextBox.TextChanged += (s, e) => _viewModel!.ApiKey = _apiKeyTextBox.Text;
        _messageTextBox.TextChanged += (s, e) => _viewModel!.CurrentMessage = _messageTextBox.Text;
        _sendButton.Click += async (s, e) => 
        {
            if (!_viewModel!.IsLoading && !string.IsNullOrWhiteSpace(_viewModel.CurrentMessage))
            {
                await _viewModel.SendMessageAsync();
            }
        };
        _messageTextBox.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter && !_viewModel!.IsLoading && !string.IsNullOrWhiteSpace(_viewModel.CurrentMessage))
            {
                e.SuppressKeyPress = true;
                await _viewModel.SendMessageAsync();
            }
        };

        // Update button state and loading animation
        _viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(_viewModel.IsLoading))
            {
                _sendButton.Enabled = !_viewModel.IsLoading;
                _sendButton.Text = _viewModel.IsLoading ? "..." : "Gönder";
                
                if (_viewModel.IsLoading)
                {
                    ShowLoadingAnimation();
                }
                else
                {
                    HideLoadingAnimation();
                }
            }
            else if (e.PropertyName == nameof(_viewModel.CurrentMessage))
            {
                _sendButton.Enabled = !_viewModel.IsLoading && !string.IsNullOrWhiteSpace(_viewModel.CurrentMessage);
            }
        };
        
        _sendButton.Enabled = false;
        InitializeLoadingAnimation();
    }

    private void InitializeLoadingAnimation()
    {
        _loadingPanel = new Panel
        {
            Height = 30,
            BackColor = Color.FromArgb(45, 45, 48),
            Visible = false,
            Dock = DockStyle.Top
        };

        _loadingLabel = new Label
        {
            Text = "Yükleniyor",
            Font = new Font("Segoe UI", 9F, FontStyle.Italic),
            ForeColor = Color.FromArgb(150, 150, 150),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };

        _loadingPanel.Controls.Add(_loadingLabel);
        _inputPanel.Controls.Add(_loadingPanel);
    }

    private void LoadingTimer_Tick(object? sender, EventArgs e)
    {
        _loadingDots = (_loadingDots + 1) % 4;
        _loadingLabel.Text = "Yükleniyor" + new string('.', _loadingDots);
    }

    private void ShowLoadingAnimation()
    {
        if (_loadingPanel == null) return;
        
        _loadingDots = 0;
        _loadingPanel.Visible = true;
        _loadingPanel.BringToFront();
        _loadingTimer.Start();
    }

    private void HideLoadingAnimation()
    {
        if (_loadingPanel == null) return;
        
        _loadingTimer.Stop();
        _loadingPanel.Visible = false;
    }

    private void UpdateChatDisplay()
    {
        if (_chatPanel == null || _viewModel == null) 
        {
            return;
        }
        
        if (this.InvokeRequired)
        {
            this.BeginInvoke(new Action(UpdateChatDisplay));
            return;
        }
        
        try
        {
            _chatPanel.SuspendLayout();
            _chatPanel.Controls.Clear();
            
            int panelWidth = 600;
            if (_chatPanel.Parent is Panel parent && parent.ClientSize.Width > 40)
            {
                panelWidth = parent.ClientSize.Width - 40;
            }
            
            foreach (var message in _viewModel.Messages)
            {
                if (string.IsNullOrEmpty(message.Content)) continue;
                
                var container = new Panel
                {
                    Width = panelWidth,
                    AutoSize = true,
                    Margin = new Padding(0, 5, 0, 5),
                    BackColor = Color.Transparent,
                    Padding = new Padding(0) 
                };

                var msgPanel = new Panel
                {
                    AutoSize = true,
                    MaximumSize = new Size(panelWidth - 60, 0), // Daha dar yap ki kenarlardan boşluk kalsın
                    Padding = new Padding(12),
                    BorderStyle = BorderStyle.None
                };

                var label = new Label
                {
                    Text = message.Content,
                    AutoSize = true,
                    MaximumSize = new Size(panelWidth - 90, 0), // Panel padding'leri hesaba kat
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.TopLeft
                };

                // Önce label'ı ekle ki panelin boyutu hesaplansın
                msgPanel.Controls.Add(label);

                // Renk ayarları
                switch (message.Role)
                {
                    case Role.User:
                        msgPanel.BackColor = Color.FromArgb(0, 120, 215);
                        break;
                    case Role.System:
                        msgPanel.BackColor = Color.FromArgb(255, 140, 0);
                        label.TextAlign = ContentAlignment.TopCenter;
                        break;
                    default:
                        msgPanel.BackColor = Color.FromArgb(45, 45, 48);
                        break;
                }

                container.Controls.Add(msgPanel);
                
                // Layout hesaplamasını zorla
                msgPanel.PerformLayout();
                
                // Konumlandırma (Dock yerine Manuel Location)
                switch (message.Role)
                {
                    case Role.User:
                        // Sağ tarafa yasla
                        msgPanel.Location = new Point(container.Width - msgPanel.Width, 0);
                        break;
                    case Role.System:
                        // Ortala
                        msgPanel.Location = new Point((container.Width - msgPanel.Width) / 2, 0);
                        break;
                    default:
                        // Sol tarafa yasla
                        msgPanel.Location = new Point(0, 0);
                        break;
                }

                _chatPanel.Controls.Add(container);
            }
            
            _chatPanel.ResumeLayout(true);
            _chatPanel.PerformLayout();
            
            // Scroll to bottom after layout update
            ScrollToBottom();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"UpdateChatDisplay Hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ScrollToBottom()
    {
        if (_chatPanel?.Parent is Panel scrollable && scrollable.AutoScroll)
        {
            try
            {
                scrollable.VerticalScroll.Value = scrollable.VerticalScroll.Maximum;
                scrollable.Invalidate();
            }
            catch
            {
                // Scroll hatası görmezden gel
            }
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _loadingTimer?.Stop();
        _loadingTimer?.Dispose();
        _viewModel?.Dispose();
        base.OnFormClosed(e);
    }

    private void ChatScrollPanel_Resize(object? sender, EventArgs e)
    {
        if (sender is Panel scrollPanel && _chatPanel != null && scrollPanel.ClientSize.Width > 20)
        {
            _chatPanel.Width = scrollPanel.ClientSize.Width - 20;
        }
    }
}

