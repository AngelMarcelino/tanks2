using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using System.Diagnostics;
using Windows.Foundation.Collections;
using Windows.System;
using Tanks.Controlls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Tanks
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public int X { get; set; }
        private Environment env { get; }
        public MainWindow()
        {
            env = Environment.Init();
            this.InitializeComponent();
            this.InitializeThread();
        }

        void InitializeThread()
        {
            Task.Run(() =>
            {
                while(true)
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    env.UpdateState();
                    this.Canvas.Invalidate();
                    Thread.Sleep(1);
                    sw.Stop();
                    GlobalTimer.ElapsedTimeInSeconds = sw.Elapsed.TotalSeconds;
                }
            });
        }

        void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            env.Draw(args);
        }

        void Grid_KeyUp(object sender, KeyRoutedEventArgs args)
        {
            env.ControllSet.HandleKeyUp(args.Key);
        }

        void Grid_KeyDown(object sender, KeyRoutedEventArgs args)
        {
            env.ControllSet.HandleKeyDown(args.Key);
        }
    }
}
