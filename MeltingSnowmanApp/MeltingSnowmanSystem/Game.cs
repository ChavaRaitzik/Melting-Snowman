using CrypticWizard.RandomWordGenerator;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeltingSnowmanSystem
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? ScoreChanged;
        public enum GameStatusEnum { NotStarted, Playing, GameWon, GameLost, GiveUp }

        WordGenerator wordgenerator = new();

        Random rnd = new();

        GameStatusEnum _gamestatus = GameStatusEnum.NotStarted;

        string _dash = "";

        ObservableCollection<string> _pictures;

        ObservableCollection<String> _pictureswithfulllocation;

        Word _worddisplay = new Word();

        private static int _score = 0;

        Message _message = new Message();

        Word _word = new Word();

        string path = "C:\\Users\\chava\\repos\\Melting-Snowman\\MeltingSnowmanApp\\MeltingSnowmanApp\\Images\\";

        private static int numgames;

        public Game()
        {
            numgames++;
            this.GameName = "GAME " + numgames;
            for (int i = 0; i < 26; i++)
            {
                this.Letters.Add(new Letter());
            }
            Letters[0].LetterValue = "A";
            Letters[1].LetterValue = "B";
            Letters[2].LetterValue = "C";
            Letters[3].LetterValue = "D";
            Letters[4].LetterValue = "E";
            Letters[5].LetterValue = "F";
            Letters[6].LetterValue = "G";
            Letters[7].LetterValue = "H";
            Letters[8].LetterValue = "I";
            Letters[9].LetterValue = "J";
            Letters[10].LetterValue = "K";
            Letters[11].LetterValue = "L";
            Letters[12].LetterValue = "M";
            Letters[13].LetterValue = "N";
            Letters[14].LetterValue = "O";
            Letters[15].LetterValue = "P";
            Letters[16].LetterValue = "Q";
            Letters[17].LetterValue = "R";
            Letters[18].LetterValue = "S";
            Letters[19].LetterValue = "T";
            Letters[20].LetterValue = "U";
            Letters[21].LetterValue = "V";
            Letters[22].LetterValue = "W";
            Letters[23].LetterValue = "X";
            Letters[24].LetterValue = "Y";
            Letters[25].LetterValue = "Z";
            this.Letters.ForEach(l => l.BackColor = LetterStandardColor);

            MysteryWords = wordgenerator.GetWords(WordGenerator.PartOfSpeech.noun, 1000);
            Pictures = new() { "snowman1picture.png", "snowman2picture.png", "snowman3picture.png", "snowman4picture.png", "snowman5picture.png", "snowman6picture.png" };
            PicturesWithFullLocation = new() { path + "snowman1picture.png", path + "snowman2picture.png", path + "snowman3picture.png", path + "snowman4picture.png", path + "snowman5picture.png", path + "snowman6picture.png" };
        }

        public string GameName { get; private set; }
        public GameStatusEnum GameStatus
        {
            get => _gamestatus; private set
            {
                _gamestatus = value;
                this.InvokePropertyChanged();
            }
        }
        public List<String> MysteryWords { get; private set; } = new();
        public List<Letter> Letters {  get; private set; } = new(); 
        public List<String> GameWonMessages { get; private set; } = new() { "You Won!", "Great Job!", "Congratulations!" };
        public List<String> GameLostMessages { get; private set; } = new() { "Better Luck Next Time", "Too Many Incorrect Guesses", "Try Again" };
        public List<String> GiveUpMessages { get; private set; } = new() { "Never give up", "Next time, just try your best", "Was that really so hard?" };
        public ObservableCollection<String> Pictures
        {   get => _pictures;
            private set 
            { 
                _pictures = value;
                this.InvokePropertyChanged();
            } 
        }

        public ObservableCollection<String> PicturesWithFullLocation
        {
            get => _pictureswithfulllocation;
            private set
            {
                _pictureswithfulllocation = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(Picture1);
            }
        }

        public string Picture1
        {
            get => PicturesWithFullLocation[0];
        }

        public Word Word
        {
            get => _word;
            private set
            {
                _word = value;
                this.InvokePropertyChanged();
            }
        }

        public string Dash 
        { 
            get => _dash;
            private set
            {
                _dash = value;
                this.InvokePropertyChanged();
            }
        }

        public Word WordDisplay
        {
            get => _worddisplay;
            set
            {
                _worddisplay = value;
                this.InvokePropertyChanged();
            }
        }

        public static int Score
        {
            get => _score;
            private set
            {
                _score = value;
            }
        }

        public Message Message
        {
            get => _message;
            private set
            {
                _message = value;
                this.InvokePropertyChanged();
            }
        }

        public string StartButtonText
        {
            get => "START " + this.GameName;
        }

        public System.Drawing.Color DisplayStandardColor { get; private set; } = System.Drawing.Color.Black;
        public System.Drawing.Color DisplayWinningColor { get; private set; } = System.Drawing.Color.LimeGreen;
        public System.Drawing.Color DisplayLosingColor { get; private set; } = System.Drawing.Color.Red;
        public System.Drawing.Color DisplayGiveUpColor { get; private set; } = System.Drawing.Color.Orange;
        public System.Drawing.Color LetterStandardColor { get; private set; } = System.Drawing.Color.DarkSlateBlue;
        public System.Drawing.Color LetterCorrectColor { get; private set; } = System.Drawing.Color.LimeGreen;
        public System.Drawing.Color LetterIncorrectColor { get; private set; } = System.Drawing.Color.Red;

        public void GetMysteryWord()
        {
            Word.WordValue = this.MysteryWords[rnd.Next(0, this.MysteryWords.Count)].ToUpper();
            Dash = new string('-', this.Word.WordValue.Length);
            this.WordDisplay.WordValue = Dash;
            this.WordDisplay.Color = DisplayStandardColor;
        }

        public void NewGame()
        {
            this.GameStatus = GameStatusEnum.Playing;
            this.Letters.ForEach(l => 
            {
                l.BackColor = LetterStandardColor;
                l.IsEnabled = true;
            });
            this.Message.MessageText = "";
            this.Pictures = new() { "snowman1picture.png", "snowman2picture.png", "snowman3picture.png", "snowman4picture.png", "snowman5picture.png", "snowman6picture.png" };
            this.PicturesWithFullLocation = new() { path + "snowman1picture.png", path + "snowman2picture.png", path + "snowman3picture.png", path + "snowman4picture.png", path + "snowman5picture.png", path + "snowman6picture.png" };
            GetMysteryWord();
        }

        public void GuessALetter(Letter letter)
        {
            if (this.Word.WordValue.Contains(letter.LetterValue))
            {
                letter.BackColor = LetterCorrectColor;
                for (int i = 0; i < this.Word.WordValue.Length; i++)
                {
                    if (this.Word.WordValue[i].ToString() == letter.LetterValue)
                    {
                        this.WordDisplay.WordValue = this.WordDisplay.WordValue.Remove(i, 1);
                        this.WordDisplay.WordValue = this.WordDisplay.WordValue.Insert(i, letter.LetterValue);
                    }
                }
            }
            else
            {
                var newPictures = new ObservableCollection<string>(Pictures);
                int numblankpics = Pictures.Where(p => p.Contains("blank") == true).Count();
                newPictures[numblankpics] = "snowmanblankpicture.png";
                Pictures = newPictures;
                var newPicturesWithFullLocation = new ObservableCollection<string>(PicturesWithFullLocation);
                int numblankpics2 = PicturesWithFullLocation.Where(p => p.Contains("blank") == true).Count();
                newPicturesWithFullLocation[numblankpics2] = path + "snowmanblankpicture.png";
                PicturesWithFullLocation = newPicturesWithFullLocation;
                letter.BackColor = LetterIncorrectColor;
            }
            letter.IsEnabled = false;
        }

        public void DetectGameWonOrLost()
        {
            if (this.WordDisplay.WordValue == this.Word.WordValue)
            {
                this.GameStatus = GameStatusEnum.GameWon;
            }
            else if (Pictures.Count(pb => pb.Contains("blank")) == 6 || PicturesWithFullLocation.Count(pb => pb.Contains("blank")) == 6)
            {
                this.GameStatus = GameStatusEnum.GameLost;
            }
            DisplayScore();
        }

        public void GiveUp()
        {
            this.GameStatus = GameStatusEnum.GiveUp;
            DisplayScore();
        }

        public void DisplayScore()
        {
            switch (GameStatus)
            {
                case GameStatusEnum.GameWon:
                    _score += 1;
                    this.Message.MessageText = GameWonMessages[rnd.Next(0, GameWonMessages.Count)];
                    this.Message.Color = DisplayWinningColor;
                    this.WordDisplay.Color = DisplayWinningColor;
                    break;
                case GameStatusEnum.GameLost:
                    _score -= 1;
                    this.Message.MessageText = GameLostMessages[rnd.Next(0, GameLostMessages.Count)];
                    this.Message.Color = DisplayLosingColor;
                    this.WordDisplay.WordValue = Word.WordValue;
                    this.WordDisplay.Color = DisplayLosingColor;
                    break;
                case GameStatusEnum.GiveUp:
                    _score -= 1;
                    this.Message.MessageText = GiveUpMessages[rnd.Next(0, GiveUpMessages.Count)];
                    this.Message.Color = DisplayGiveUpColor;
                    this.WordDisplay.WordValue = Word.WordValue;
                    this.WordDisplay.Color = DisplayGiveUpColor;
                    break;
            }
            ScoreChanged?.Invoke(this, new EventArgs());
        }

        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
