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
using Plugin.Share;
using Plugin.Share.Abstractions;
using RaidCalculator.Models;

namespace RaidCalculator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IEnumerable<RaidType> _raidTypes;
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

        public MainPageViewModel(INavigationService navigationService, IEnumerable<RaidType> raidTypes)
            : base(navigationService)
        {
            _raidTypes = raidTypes;

            Title = "Calculator";

            GetMessageTextCommand = new DelegateCommand(GetMessageText, CanGetMessageText);

            var copyCommand = new DelegateCommand(Copy, CanCopy);
            copyCommand.ObservesProperty(() => MessageText);
            CopyCommand = copyCommand;

            var shareCommand = new DelegateCommand(Share, CanShare);
            shareCommand.ObservesProperty(() => MessageText);
            ShareCommand = shareCommand;
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
            return !string.IsNullOrWhiteSpace(MessageText);
        }

        #endregion

        #region ShareCommand

        public ICommand ShareCommand { get; }

        private void Share()
        {
            var shareMessage = new ShareMessage { Text = MessageText };
            CrossShare.Current.Share(shareMessage);
        }

        private bool CanShare()
        {
            return !string.IsNullOrWhiteSpace(MessageText);
        }

        #endregion
    }
}
