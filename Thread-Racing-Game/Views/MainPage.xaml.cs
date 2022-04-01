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
using Windows.UI.Xaml.Media.Imaging;

namespace Thread_Racing_Game.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        CanvasBitmap BG;
        //int timecounter = 10;
        DispatcherTimer timer = new DispatcherTimer();
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float DesignWidth = 1800;
        public static float DesignHeght = 720;
        public static float scaleWidth, scaleHeight;
        public GameState gameState;
        public Weather weather = new Weather();

        public MainPage()
        {
            InitializeComponent();
            User user = new User(100);

            RepairTeam repairTeam = new RepairTeam(10);
            Car car1 = new Car(100);
            car1.Name = "ALFA";
            car1.WheelHealth = 90;
            car1.EngineHealth = 90;

            Car car2 = new Car(100);
            car2.Name = "BETA";
            car2.WheelHealth = 30;
            car2.EngineHealth = 30;

            Car car3 = new Car(100);
            car3.Name = "OMEGA";
            car3.WheelHealth = 40;
            car3.EngineHealth = 50;

            Team alfaTeam = new Team("Alfa", repairTeam, car1,null);
            Team betaTeam = new Team("Beta", repairTeam, car2, null);
            Team gammaTeam = new Team("Gamma", repairTeam, car1, null);
            Team omegaTeam = new Team("Omega", repairTeam, car3, null);
            List<Team> teamsList = new List<Team>();
            teamsList.Add(alfaTeam);
            teamsList.Add(betaTeam);
            teamsList.Add(gammaTeam);
            teamsList.Add(omegaTeam);
            Weather weather = new Weather();
            Race race = new Race(150,teamsList);
            //race.testEventHandler();

            this.gameState = new GameState(race, null);

            GameController gameController = new GameController(4);
            gameController.ExecuteGameCycle();

            Window.Current.SizeChanged += Current_SizeChanged;
            Scaling.setScale();
            DataContext = this;
            //Timer.Tick += Timer_Tick;
            //Timer.Interval = new TimeSpan(0, 0, 1);
            //Timer.Start();
            timer.Tick += Timer_Tick;
            timer.Start();
            applyWeather();
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaling.setScale();
        }

        private void Timer_Tick(object sender, object e)
        {
            test.Text = DateTime.Now.ToString("h:mm:ss tt");
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

        public void weaterButtonClick(object sender, RoutedEventArgs e)
        {
            weather.getRandomWeather();
            applyWeather();
        }

        private void applyWeather()
        {
            weatherLocation.Text = "Location: " + weather.LocationName;
            weatherInfo.Text = "Weather condition: " + weather.Condition;
            Uri imageUri = new Uri(weather.Icon);
            BitmapImage icon = new BitmapImage(imageUri);
            weatherIcon.Source = icon;
        }
    }
}
