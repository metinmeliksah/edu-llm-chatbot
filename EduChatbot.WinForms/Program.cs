using System;
using System.Windows.Forms;
using EduChatbot.WinForms;

namespace EduChatbot.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}

