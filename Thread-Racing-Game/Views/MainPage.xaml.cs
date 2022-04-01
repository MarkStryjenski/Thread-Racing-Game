using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Thread_Racing_Game.Classes;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thread_Racing_Game.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        CanvasBitmap BG;
        int timecounter = 1;
        DispatcherTimer timer = new DispatcherTimer();
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float DesignWidth = 1800;
        public static float DesignHeght = 720;
        public static float scaleWidth, scaleHeight;
        public GameController gameController;

        public MainPage()
        {
            InitializeComponent();
            gameController = new GameController(4);

            Window.Current.SizeChanged += Current_SizeChanged;
            Scaling.setScale();
            DataContext = this;
            //Timer.Tick += Timer_Tick;
            //Timer.Interval = new TimeSpan(0, 0, 1);
            //Timer.Start();
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaling.setScale();
        }

        private void Timer_Tick(object sender, object e)
        {
            test.Text = DateTime.Now.ToString("h:mm:ss tt");
            if (timecounter %2 == 0)
            {
                gameController.ExecuteGameCycle();
            }
            timecounter++;
        }

        private void GameCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreatResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreatResourcesAsync(CanvasControl sender)
        {
            try
            {
                BG = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/race-track-with-start-finish-line.png"));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void GameCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(Scaling.img(BG));

            raceCanvas.Invalidate();
        }

        // navigation
        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }       

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
