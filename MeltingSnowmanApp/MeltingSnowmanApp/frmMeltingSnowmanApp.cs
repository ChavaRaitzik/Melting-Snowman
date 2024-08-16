using MeltingSnowmanSystem;

namespace MeltingSnowmanApp
{
    public partial class frmMeltingSnowmanApp : Form
    {
        Game game = new();
        List<Button> lstabcbuttons;
        string path = Application.StartupPath + @"\images\";

        public frmMeltingSnowmanApp()
        {
            InitializeComponent();
            SetImageLocation();
            lstabcbuttons = new() { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
            MeltingSnowmanSystem.Message message = game.Message;
            lblMessageBox.DataBindings.Add("Text", message, "MessageText");
            lblMessageBox.DataBindings.Add("ForeColor", message, "Color");
            Word word = game.WordDisplay;
            lblMysteryWord.DataBindings.Add("Text", word, "WordValue");
            lblMysteryWord.DataBindings.Add("ForeColor", word, "Color");
            btnStart.Click += BtnStart_Click;
            btnGiveUp.Enabled = false;
            btnGiveUp.Click += BtnGiveUp_Click;
            lstabcbuttons.ForEach(b => { 
                Letter letter = game.Letters[lstabcbuttons.IndexOf(b)];
                b.Click += ABCButton_Click;
                b.DataBindings.Add("Text", letter, "LetterValue");
                b.DataBindings.Add("BackColor", letter, "BackColor");
                b.DataBindings.Add("Enabled", letter, "IsEnabled");
            });
            game.ScoreChanged += Game_ScoreChanged;
        }

        private void NewGame()
        {
            game.NewGame();
            SetImageLocation();
        }

        private void GuessALetter(Button btn)
        {
            btnStart.Enabled = false;
            btnGiveUp.Enabled = true;
            Letter letter = game.Letters[lstabcbuttons.IndexOf(btn)];
            game.GuessALetter(letter);
            SetImageLocation();
        }

        private void DetectGameWonOrLost()
        {
            game.DetectGameWonOrLost();
            if (game.GameStatus != Game.GameStatusEnum.Playing)
            {
                btnGiveUp.Enabled = false;
                btnStart.Enabled = true;
            }
        }

        private void GiveUp()
        {
            game.GiveUp();
            btnGiveUp.Enabled = false;
            btnStart.Enabled = true;
        }

        private void SetImageLocation()
        {
            picbox1.ImageLocation = path + game.Pictures[0];
            picbox2.ImageLocation = path + game.Pictures[1];
            picbox3.ImageLocation = path + game.Pictures[2];
            picbox4.ImageLocation = path + game.Pictures[3];
            picbox5.ImageLocation = path + game.Pictures[4];
            picbox6.ImageLocation = path + game.Pictures[5];
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            NewGame();
        }

        private void ABCButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                if (game.GameStatus == Game.GameStatusEnum.Playing)
                {
                    Button btn = (Button)sender;
                    GuessALetter(btn);
                    DetectGameWonOrLost();
                }
            }
        }

        private void BtnGiveUp_Click(object? sender, EventArgs e)
        {
            GiveUp();
        }

        private void Game_ScoreChanged(object? sender, EventArgs e)
        {
            txtScoreBox.Text = Game.Score.ToString();
        }
    }
}
