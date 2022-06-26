using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui_3_system_timer
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // https://stackoverflow.com/a/71074431/5438626
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this); // m_window in App.cs
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            var size = new Windows.Graphics.SizeInt32();
            size.Width = 480;
            size.Height = 800;

            appWindow.Resize(size);
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

            SystemTimer.PropertyChanged += (sender, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(SystemTimer.Second):
                        dispatcherQueue.TryEnqueue(() =>
                        {
                            textboxClock.Text = SystemTimer.Second.ToString("hh:mm:ss");
                        });
                        break;
                }
            };

            this.Closed += (sender, e) =>
            {
                SystemTimer.Dispose();
            };
        }
    }
}
