using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Thread_Racing_Game.Classes;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Point = System.Drawing.Point;

namespace Thread_Racing_Game.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        CanvasBitmap BG;
        CanvasBitmap car;
        CanvasBitmap finishLine;
        //int timecounter = 10;
        int timecounter = 1;
        DispatcherTimer timer = new DispatcherTimer();
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float DesignWidth = 1800;
        public static float DesignHeght = 720;
        public static float scaleWidth, scaleHeight, pointX, pointY;
        public static float carX = (float)bounds.X / 2;
        public static float carY = (float)bounds.Y;
        public static List<float> carXPOS = new List<float>();
        public static List<float> carYPOS = new List<float>();
        public static List<float> finishLineXPOS = new List<float>();
        public static List<float> finishLineYPOS = new List<float>();
        public static List<float> percent = new List<float>();

        public GameState gameState;
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


            carXPOS.Add((float)90.0);
            carYPOS.Add((float)400.0);

            carXPOS.Add((float)90.0);
            carYPOS.Add((float)650.0);

            percent.Add(0f);
            percent.Add(0f);

            finishLineXPOS.Add((float)1100.0);
            finishLineYPOS.Add((float)250.0);
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaling.setScale();
        }

        private void Timer_Tick(object sender, object e)
        {
            test.Text = DateTime.Now.ToString("h:mm:ss tt");
            if (timecounter % 2 == 0)
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
                car = await CanvasBitmap.LoadAsync(sender, @"Assets/Images/car.png");
                finishLine = await CanvasBitmap.LoadAsync(sender, @"Assets/Images/finish-line.png");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void GameCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(Scaling.img(BG));

            for (int i = 0; i < carXPOS.Count; i++)
            {
                pointX = (carX + (carXPOS[i] - carX) * percent[i]);

                args.DrawingSession.DrawImage(Scaling.img(car), pointX - (256 * scaleWidth), carYPOS[i] - (256 * scaleHeight));

                percent[i] += 0.050f;
           
                if(pointX >= DesignWidth * scaleWidth)
                {
                    carXPOS.RemoveAt(i);
                    percent.RemoveAt(i);
                }
            }


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

        public void weaterButtonClick(object sender, RoutedEventArgs e)
        {
            gameController.gameState.race.weather.getRandomWeather();
            applyWeather();
        }

        private void applyWeather()
        {
            weatherLocation.Text = "Location: " + gameController.gameState.race.weather.LocationName;
            weatherInfo.Text = "Weather condition: " + gameController.gameState.race.weather.Condition;
            Uri imageUri = new Uri(gameController.gameState.race.weather.Icon);
            BitmapImage icon = new BitmapImage(imageUri);
            weatherIcon.Source = icon;
        }
    }
}
