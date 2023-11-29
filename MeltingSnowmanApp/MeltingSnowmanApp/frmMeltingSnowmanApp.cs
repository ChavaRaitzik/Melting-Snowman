using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        WordGenerator wordgenerator = new();

        Random rnd = new();
        enum GameStatusEnum { NotStarted, Playing, GameWon, GameLost }
        GameStatusEnum gamestatus = GameStatusEnum.NotStarted;

        string mysteryword;
        int numofletters;
        string blanks;
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
            mysteryword = lstwords[rnd.Next(0, lstwords.Count)];
            numofletters = mysteryword.Length;
            blanks = new StringBuilder().Insert(0, "*", numofletters).ToString();
            lblMysteryWord.Text = blanks + mysteryword;
        }

        private void GuessALetter(Button btn)
        {
            string letter = btn.Text.ToLower();
            if (mysteryword.Contains(letter))
            {
                btn.BackColor = Color.Green;
                while (mysteryword.Contains(letter))
                {
                    int index = 0;
                    index = mysteryword.IndexOf(letter);
                    lblMysteryWord.Text = lblMysteryWord.Text.Remove(index, 1);
                    lblMysteryWord.Text = lblMysteryWord.Text.Insert(index, letter);
                    mysteryword.Remove(index, 1);
                    Application.DoEvents();
                }
            }
            else
            {
                btn.BackColor = Color.Red;
                PictureBox picturebox = lstpictureboxes.FirstOrDefault(pb => pb.Image != null);
                picturebox.Image = null;
            }
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            gamestatus = GameStatusEnum.Playing;
            GetMysteryWord();
        }

        private void ABCButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                if (gamestatus == GameStatusEnum.Playing)
                {
                    Button btn = (Button)sender;
                    GuessALetter(btn);
                }
            }
        }

    }
}
