using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tablut.Model.GameModel;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class InitGameViewModel: ApplicationViewModel
    {
        public List<string> SavedGames;
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
            SavedGames = new List<string>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            foreach (string filepath in Directory.GetFiles(path))
            {
                if (Path.GetExtension(filepath) == ".tablut")
                {
                    SavedGames.Add(Path.GetFileNameWithoutExtension(filepath));
                }
            }
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
            string fname = FileName.ToLower();
            string p1name = string.Empty;
            string p2name = string.Empty;
         
            if (string.IsNullOrEmpty(fname))
            {
                FileNameError = "Give me a filename.";
                HasFileNameError = true;
            }
            else if (fname.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
            {
                FileNameError = "The given filename contains invalid filename characters.";
                HasFileNameError = true;
            }
            else if (SavedGames.Contains(fname))
            {
                FileNameError = "The given filename is used already.";
                HasFileNameError = true;
            }

            if (string.IsNullOrEmpty(P1Name) || P1Name.Length < 4)
            {
                P1NameError = "The attacker player's name should be at least 4 characters long";
                HasP1NameError = true;
            }
            if (!HasP1NameError)
            {
                p1name = P1Name.Substring(0, 1).ToUpper() + P1Name.Substring(1).ToLower();
                if (p1name.Any(c => !char.IsLetter(c)))
                {
                    P1NameError = "Player name can only contain letters.";
                    HasP1NameError = true;
                }
            }

            if (string.IsNullOrEmpty(P2Name) || P2Name.Length < 4)
            {
                P2NameError = "The defender player's name should be at least 4 characters long";
                HasP2NameError = true;
            }
            if (!HasP2NameError)
            {
                p2name = P2Name.Substring(0, 1).ToUpper() + P2Name.Substring(1).ToLower();
                if (p2name.Any(c => !char.IsLetter(c)))
                {
                    P2NameError = "Player name can only contain letters.";
                    HasP2NameError = true;
                }
                else if (p2name == p1name)
                {
                    P2NameError = "The Attacker's name and the Defender's name can't be the same.";
                    HasP2NameError = true;
                }
            }
            if (!HasFileNameError && !HasP1NameError && !HasP2NameError)
            {
                OnPushState?.Invoke(new GameViewModel(new GameModel(p1name, p2name), fname));
            }
        }

        private void Command_Back(object obj)
        {
            OnPushState?.Invoke(new MainMenuViewModel());
        }
    }
}
