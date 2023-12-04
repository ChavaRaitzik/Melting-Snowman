using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrypticWizard.RandomWordGenerator;

namespace MeltingSnowmanApp
{
    public partial class frmMeltingSnowmanApp : Form
    {
        string path = Application.StartupPath + @"\images\";

        List<string> lstwords;
        List<Button> lstabcbuttons;
        List<PictureBox> lstpictureboxes;
        List<String> lstmessagegamewon = new() { "You Won!", "Great Job!", "Congratulations!" };
        List<String> lstmessagegamelost = new() { "Better Luck Next Time", "Too Many Incorrect Guesses", "Try Again" };
        List<String> lstmessagegiveup = new() { "Never give up", "Next time, just try your best", "Was that really so hard?"};

        WordGenerator wordgenerator = new();

        Random rnd = new();
        enum GameStatusEnum { NotStarted, Playing, GameWon, GameLost, GiveUp }
        GameStatusEnum gamestatus = GameStatusEnum.NotStarted;

        string mysteryword = "";
        string stars = "";

        public frmMeltingSnowmanApp()
        {
            InitializeComponent();
            picbox1.ImageLocation = path + "SnowmanPicture1.png";
            picbox2.ImageLocation = path + "SnowmanPicture2.png";
            picbox3.ImageLocation = path + "SnowmanPicture3.png";
            picbox4.ImageLocation = path + "SnowmanPicture4.png";
            picbox5.ImageLocation = path + "SnowmanPicture5.png";
            picbox6.ImageLocation = path + "SnowmanPicture6.png";
            lstwords = wordgenerator.GetWords(WordGenerator.PartOfSpeech.noun, 1000);
            lstabcbuttons = new() { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
            lstpictureboxes = new() { picbox1, picbox2, picbox3, picbox4, picbox5, picbox6 };
            btnStart.Click += BtnStart_Click;
            btnGiveUp.Enabled = false;
            btnGiveUp.Click += BtnGiveUp_Click;
            lstabcbuttons.ForEach(b => { b.Click += ABCButton_Click; });
        }

        private void GetMysteryWord()
        {
            mysteryword = lstwords[rnd.Next(0, lstwords.Count)].ToLower();
            stars = new string('-', mysteryword.Length);
            lblMysteryWord.Text = stars;
        }

        private void GuessALetter(Button btn)
        {
            char letter = char.Parse(btn.Text.ToLower());
            btn.Enabled = false;
            btnStart.Enabled = false;
            if (mysteryword.Contains(letter))
            {
                btn.BackColor = Color.LimeGreen;
                for (int i = 0; i < mysteryword.Length; i++)
                {
                    if (mysteryword[i] == letter)
                    {
                        lblMysteryWord.Text = lblMysteryWord.Text.Remove(i, 1);
                        lblMysteryWord.Text = lblMysteryWord.Text.Insert(i, letter.ToString());
                    }
                }
                //SM Use replace()
                //I tried using replace(), but it either replaced all blanks with the guessed letter, or it removed all the blanks and it just displayed the guessed letter.....
                //SM Get it.
            }
            else
            {
                btn.BackColor = Color.Red;
                PictureBox picturebox = lstpictureboxes.FirstOrDefault(pb => pb.Image != null);
                picturebox.Image = null;
            }
        }

        private void DetectGameWonOrLost()
        {
            if (lblMysteryWord.Text == mysteryword)
            {
                gamestatus = GameStatusEnum.GameWon;
                //SM This can be after the if statement with the GetForeColor() procedure.
                GetScore();
            }
            else if (lstpictureboxes.TrueForAll(pb => pb.Image == null))
            {
                gamestatus = GameStatusEnum.GameLost;
                GetScore();
            }
            GetForeColor();
        }

        private void GetScore()
        {
            int score = 0;
            List<String> lstmessage = new();
            int.TryParse(txtScoreBox.Text, out score);
            switch (gamestatus)
            {
                case GameStatusEnum.GameWon:
                    score = score + 1;
                    lstmessage = lstmessagegamewon;
                    break;
                case GameStatusEnum.GameLost:
                    score = score - 1;
                    lstmessage = lstmessagegamelost;
                    lblMysteryWord.Text = mysteryword;
                    break;
                case GameStatusEnum.GiveUp:
                    score = score - 1;
                    lstmessage = lstmessagegiveup;
                    lblMysteryWord.Text = mysteryword;
                    break;
            }
            btnGiveUp.Enabled = false;
            btnStart.Enabled = true;
            txtScoreBox.Text = score.ToString();
            lblMessageBox.Text = lstmessage[rnd.Next(0, lstmessage.Count)];
        }

        private void GetForeColor()
        {
            Color color = Color.Black;
            switch (gamestatus)
            {
                case GameStatusEnum.GameWon:
                    color = Color.LimeGreen;
                    break;
                case GameStatusEnum.GameLost:
                    color = Color.Red;
                    break;
                case GameStatusEnum.GiveUp:
                    color = Color.Orange;
                    break;
            }
            lblMysteryWord.ForeColor = color;
            lblMessageBox.ForeColor = color;

        }

        private void NewGame()
        {
            gamestatus = GameStatusEnum.Playing;
            lstabcbuttons.ForEach(b => b.BackColor = Color.DarkBlue);
            lstabcbuttons.ForEach(b => b.Enabled = true);
            btnGiveUp.Enabled = true;
            lblMessageBox.Text = "";
            lblMysteryWord.Text = "";
            picbox1.ImageLocation = path + "SnowmanPicture1.png";
            picbox2.ImageLocation = path + "SnowmanPicture2.png";
            picbox3.ImageLocation = path + "SnowmanPicture3.png";
            picbox4.ImageLocation = path + "SnowmanPicture4.png";
            picbox5.ImageLocation = path + "SnowmanPicture5.png";
            picbox6.ImageLocation = path + "SnowmanPicture6.png";
            GetForeColor();
            GetMysteryWord();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            NewGame();
        }

        private void ABCButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                if (gamestatus == GameStatusEnum.Playing)
                {
                    Button btn = (Button)sender;
                    GuessALetter(btn);
                    DetectGameWonOrLost();
                }
            }
        }

        private void BtnGiveUp_Click(object? sender, EventArgs e)
        {
            gamestatus = GameStatusEnum.GiveUp;
            GetScore();
            GetForeColor();
        }

    }
}
