using MeltingSnowmanSystem;
using static MeltingSnowmanSystem.Game;

namespace MeltingSnowmanApp
{
    public partial class frmMeltingSnowmanApp : Form
    {
        Game game = new();
        string path = Application.StartupPath + @"\images\";
        List<Button> lstabcbuttons;
        //List<PictureBox> lstpictureboxes;

        public frmMeltingSnowmanApp()
        {
            InitializeComponent();
            //picbox1.DataBindings.Add("ImageLocation", game, "PictureWithFullLocation");
            picbox1.ImageLocation = path + game.Pictures[0];
            picbox2.ImageLocation = path + game.Pictures[1];
            picbox3.ImageLocation = path + game.Pictures[2];
            picbox4.ImageLocation = path + game.Pictures[3];
            picbox5.ImageLocation = path + game.Pictures[4];
            picbox6.ImageLocation = path + game.Pictures[5];
            lstabcbuttons = new() { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
            //lstpictureboxes = new() { picbox1, picbox2, picbox3, picbox4, picbox5, picbox6 };
            MeltingSnowmanSystem.Message message = game.Message;
            lblMessageBox.DataBindings.Add("Text", message, "MessageText");
            lblMessageBox.DataBindings.Add("ForeColor", message, "Color");
            Word word = game.WordDisplay;
            lblMysteryWord.DataBindings.Add("Text", word, "WordValue");
            lblMysteryWord.DataBindings.Add("ForeColor", word, "Color");
            txtScoreBox.DataBindings.Add("Text", game, "Score".ToString());
            btnStart.Click += BtnStart_Click;
            btnGiveUp.Enabled = false;
            btnGiveUp.Click += BtnGiveUp_Click;
            lstabcbuttons.ForEach(b => { 
                Letter letter = game.Letters[lstabcbuttons.IndexOf(b)];
                b.Click += ABCButton_Click;
                b.DataBindings.Add("Text", letter, "LetterValue");
                b.DataBindings.Add("BackColor", letter, "BackColor");
            });
        }

        private void NewGame()
        {
            game.NewGame();
            lstabcbuttons.ForEach(b => b.Enabled = true);
            //picbox1.ImageLocation = path + "SnowmanPicture1.png";
            //picbox2.ImageLocation = path + "SnowmanPicture2.png";
            //picbox3.ImageLocation = path + "SnowmanPicture3.png";
            //picbox4.ImageLocation = path + "SnowmanPicture4.png";
            //picbox5.ImageLocation = path + "SnowmanPicture5.png";
            //picbox6.ImageLocation = path + "SnowmanPicture6.png";
        }

        private void GuessALetter(Button btn)
        {
            btn.Enabled = false;
            btnStart.Enabled = false;
            btnGiveUp.Enabled = true;
            Letter letter = game.Letters[lstabcbuttons.IndexOf(btn)];
            game.GuessALetter(letter);
            //char letter = char.Parse(btn.Text.ToLower());
            //if (mysteryword.Contains(letter))
            //{
            //    btn.BackColor = Color.LimeGreen;
            //    for (int i = 0; i < mysteryword.Length; i++)
            //    {
            //        if (mysteryword[i] == letter)
            //        {
            //            lblMysteryWord.Text = lblMysteryWord.Text.Remove(i, 1);
            //            lblMysteryWord.Text = lblMysteryWord.Text.Insert(i, letter.ToString());
            //        }
            //    }
            //}
            //else
            //{
            //    PictureBox picturebox = lstpictureboxes.FirstOrDefault(pb => pb.Image != null);
            //    picturebox.Image = null;
            //}
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

    }
}
