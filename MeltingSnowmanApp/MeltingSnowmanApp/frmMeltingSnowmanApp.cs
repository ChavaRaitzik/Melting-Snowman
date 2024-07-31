using MeltingSnowmanSystem;
using static MeltingSnowmanSystem.Game;

namespace MeltingSnowmanApp
{
    public partial class frmMeltingSnowmanApp : Form
    {
        Game game = new();
        List<Button> lstabcbuttons;
        //List<PictureBox> lstpictureboxes;

        public frmMeltingSnowmanApp()
        {
            InitializeComponent();
            Picture pic1 = game.PicturesWithFullLocation[0];
            picbox1.DataBindings.Add("ImageLocation", pic1, "PictureValue");
            Picture pic2 = game.PicturesWithFullLocation[1];
            picbox2.DataBindings.Add("ImageLocation", pic2, "PictureValue");
            Picture pic3 = game.PicturesWithFullLocation[2];
            picbox3.DataBindings.Add("ImageLocation", pic3, "PictureValue");
            Picture pic4 = game.PicturesWithFullLocation[3];
            picbox4.DataBindings.Add("ImageLocation", pic4, "PictureValue");
            Picture pic5 = game.PicturesWithFullLocation[4];
            picbox5.DataBindings.Add("ImageLocation", pic5, "PictureValue");
            Picture pic6 = game.PicturesWithFullLocation[5];
            picbox6.DataBindings.Add("ImageLocation", pic6, "PictureValue");
            //picbox1.ImageLocation = game.PicturesWithFullLocation[0].PictureValue;
            //picbox2.ImageLocation = game.PicturesWithFullLocation[1].PictureValue;
            //picbox3.ImageLocation = game.PicturesWithFullLocation[2].PictureValue;
            //picbox4.ImageLocation = game.PicturesWithFullLocation[3].PictureValue;
            //picbox5.ImageLocation = game.PicturesWithFullLocation[4].PictureValue;
            //picbox6.ImageLocation = game.PicturesWithFullLocation[5].PictureValue;
            lstabcbuttons = new() { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
            //lstpictureboxes = new() { picbox1, picbox2, picbox3, picbox4, picbox5, picbox6 };
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
        }

        private void GuessALetter(Button btn)
        {
            btnStart.Enabled = false;
            btnGiveUp.Enabled = true;
            Letter letter = game.Letters[lstabcbuttons.IndexOf(btn)];
            game.GuessALetter(letter);
        }

        private void DetectGameWonOrLost()
        {
            game.DetectGameWonOrLost();
            if (game.GameStatus != GameStatusEnum.Playing)
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

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            NewGame();
        }

        private void ABCButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                if (game.GameStatus == GameStatusEnum.Playing)
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
