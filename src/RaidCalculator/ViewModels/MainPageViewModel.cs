using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using RaidCalculator.Services;
using Plugin.Clipboard;

namespace RaidCalculator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _messageText = string.Empty;

        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }

        private string _arenaName = string.Empty;

        public string ArenaName
        {
            get => _arenaName;
            set => SetProperty(ref _arenaName, value);
        }

        private int _hours;

        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        private int _minutes;

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        private int _seconds;

        public int Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        private bool _isAlreadyStarted;

        public bool IsAlreadyStarted
        {
            get => _isAlreadyStarted;
            set => SetProperty(ref _isAlreadyStarted, value);
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Calculator";

            GetMessageTextCommand = new DelegateCommand(GetMessageText, CanGetMessageText);
            CopyCommand = new DelegateCommand(Copy, CanCopy);
        }

        #region GetMessageTextCommand

        public ICommand GetMessageTextCommand { get; }

        private void GetMessageText()
        {
            var timeString = "ab";
            if (IsAlreadyStarted) timeString = "bis";

            Calculator calculator = new Calculator();


            TimeSpan timeSpan = new TimeSpan(Hours, Minutes, Seconds);
            var result = calculator.GetStartTimeFromTimeUntilStart(timeSpan);

            MessageText = $"{ArenaName} {timeString} {result.Hour.ToString().PadLeft(2,'0')}:{result.Minute.ToString().PadLeft(2, '0')}";
        }

        private bool CanGetMessageText()
        {
            return true;
        }

        #endregion

        #region CopyCommand

        public ICommand CopyCommand { get; }

        private void Copy()
        {
            CrossClipboard.Current.SetText(MessageText);
        }

        private bool CanCopy()
        {
            return true;
        }

        #endregion
    }
}
