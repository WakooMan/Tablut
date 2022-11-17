using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tablut.Model.GameModel;

namespace Tablut.ViewModel
{
    public class InitGameViewModel: ApplicationViewModel
    {
        public string TitleText => "Tablut";
        public string P1Text => "Attacker Player";
        public string P2Text => "Defender Player";
        public string StartText => "Start New Game";
        public string BackText => "Go Back To Menu";
        public string FileNameText => "Save Filename";
        public string FileName { get; set; } = "";
        public string P1Name { get; set; } = "";
        public string P2Name { get; set; } = "";
        public DelegateCommand StartCommand { get; }
        public DelegateCommand BackCommand { get; }

        private bool _hasFileNameError = false;
        private bool _hasP1NameError = false;
        private bool _hasP2NameError = false;
        private string _fileNameError = "";
        private string _p1NameError = "";
        private string _p2NameError = "";

        public bool HasFileNameError 
        {
            get 
            {
                return _hasFileNameError;
            }
            set 
            {
                if (_hasFileNameError != value)
                {
                    _hasFileNameError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FileNameError
        {
            get
            {
                return _fileNameError;
            }
            set
            {
                if (_fileNameError != value)
                {
                    _fileNameError = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasP1NameError
        {
            get
            {
                return _hasP1NameError;
            }
            set
            {
                if (_hasP1NameError != value)
                {
                    _hasP1NameError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string P1NameError
        {
            get
            {
                return _p1NameError;
            }
            set
            {
                if (_p1NameError != value)
                {
                    _p1NameError = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasP2NameError
        {
            get
            {
                return _hasP2NameError;
            }
            set
            {
                if (_hasP2NameError != value)
                {
                    _hasP2NameError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string P2NameError
        {
            get
            {
                return _p2NameError;
            }
            set
            {
                if (_p2NameError != value)
                {
                    _p2NameError = value;
                    OnPropertyChanged();
                }
            }
        }
        public InitGameViewModel()
        {
            StartCommand = new DelegateCommand(Command_Start);
            BackCommand = new DelegateCommand(Command_Back);
        }

        private void Command_Start(object obj)
        {
            HasFileNameError = false;
            HasP1NameError = false;
            HasP2NameError = false;
            FileNameError = "";
            P1NameError = "";
            P2NameError = "";
            if (string.IsNullOrEmpty(FileName))
            {
                FileNameError = "Give me a filename.";
                HasFileNameError = true;
            }
            else if (FileName.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
            {
                FileNameError = "The given filename contains invalid filename characters.";
                HasFileNameError = true;
            }
            if (string.IsNullOrEmpty(P1Name))
            {
                P1NameError = "Give me the Attacker player's name.";
                HasP1NameError = true;
            }
            else if (P1Name.Any(c => !char.IsLetter(c)))
            {
                P1NameError = "Player name can only contain letters.";
                HasP1NameError = true;
            }
            if (string.IsNullOrEmpty(P2Name))
            {
                P2NameError = "Give me the Defender player's name.";
                HasP2NameError = true;
            }
            else if (P2Name.Any(c => !char.IsLetter(c)))
            {
                P2NameError = "Player name can only contain letters.";
                HasP2NameError = true;
            }
            else if (P2Name == P1Name)
            {
                P2NameError = "The Attacker's name and the Defender's name can't be the same.";
                HasP2NameError = true;
            }
            if (!HasFileNameError && !HasP1NameError && !HasP2NameError)
            {
                OnPushState?.Invoke(new GameViewModel(new GameModel(P1Name, P2Name), FileName));
            }
        }

        private void Command_Back(object obj)
        {
            OnPushState?.Invoke(new MainMenuViewModel());
        }
    }
}
