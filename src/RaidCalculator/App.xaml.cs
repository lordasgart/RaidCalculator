using System;
using System.Collections.Generic;
using Prism;
using Prism.Ioc;
using RaidCalculator.Models;
using RaidCalculator.ViewModels;
using RaidCalculator.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RaidCalculator
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var raidTypes = new List<RaidType>
            {
                new RaidType() {Level = 1, Title = "1er", DisplayText = "☆", Duration = new TimeSpan(0, 45, 0)},
                new RaidType() {Level = 2, Title = "2er", DisplayText = "☆☆", Duration = new TimeSpan(0, 45, 0)},
                new RaidType() {Level = 3, Title = "3er", DisplayText = "☆☆☆", Duration = new TimeSpan(0, 45, 0)},
                new RaidType() {Level = 4, Title = "4er", DisplayText = "☆☆☆☆", Duration = new TimeSpan(0, 45, 0)},
                new RaidType() {Level = 5, Title = "5er", DisplayText = "☆☆☆☆☆", Duration = new TimeSpan(0, 45, 0)}
            };

            containerRegistry.RegisterInstance(typeof(IEnumerable<RaidType>), raidTypes);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
