using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
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

        WordGenerator wordgenerator = new();

        Random rnd = new();
        enum GameStatusEnum { NotStarted, Playing, GameWon, GameLost }
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
            if (mysteryword.Contains(letter))
            {
                btn.BackColor = Color.LimeGreen;
                int index = mysteryword.IndexOf(letter);
                List<int> lstindexes = new();
                for (int i = 0; i < mysteryword.Length; i++)
                {
                    if (mysteryword[i] == letter)
                    {
                        lstindexes.Add(i);
                    }
                }
                for (int i = 0; i < lstindexes.Count; i += 1)
                {
                    lblMysteryWord.Text = lblMysteryWord.Text.Remove(lstindexes[i], 1);
                    lblMysteryWord.Text = lblMysteryWord.Text.Insert(lstindexes[i], letter.ToString());
                }
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
            }
            txtScoreBox.Text = score.ToString();
            lblMessageBox.Text = lstmessage[rnd.Next(0, lstmessage.Count)];
        }

        private void GetForeColor()
        {
            switch (gamestatus)
            {
                case GameStatusEnum.GameWon:
                    lblMysteryWord.ForeColor = Color.LimeGreen;
                    lblMessageBox.ForeColor = Color.LimeGreen;
                    break;
                case GameStatusEnum.GameLost:
                    lblMysteryWord.ForeColor = Color.Red;
                    lblMessageBox.ForeColor= Color.Red;
                    break;
                default:
                    lblMysteryWord.ForeColor = Color.Black;
                    break;
            }

        }

        private void NewGame()
        {
            gamestatus = GameStatusEnum.Playing;
            lstabcbuttons.ForEach(b => b.BackColor = Color.DarkBlue);
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

    }
}
