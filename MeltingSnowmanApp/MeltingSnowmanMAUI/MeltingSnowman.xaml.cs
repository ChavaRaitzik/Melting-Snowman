using MeltingSnowmanSystem;
using static MeltingSnowmanSystem.Game;

namespace MeltingSnowmanMAUI;

public partial class MeltingSnowman : ContentPage
{
    Game activegame;
    List<Game> lstgame = new() { new Game() , new Game(), new Game()};
    List<Button> lstabcbuttons;
    //Color standardcolor;
    public MeltingSnowman()
	{
		InitializeComponent();
        lstgame.ForEach(g => g.ScoreChanged += G_ScoreChanged);
        Game1Rb.BindingContext = lstgame[0];
        Game2Rb.BindingContext = lstgame[1];
        Game3Rb.BindingContext = lstgame[2];
        activegame = lstgame[0];
        this.BindingContext = activegame;
        lstabcbuttons = new() { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
        //standardcolor = lstabcbuttons[0].BackgroundColor;
        btnGiveUp.IsEnabled = false;
    }

    private void G_ScoreChanged(object? sender, EventArgs e)
    {
        txtScoreBox.Text = Game.Score.ToString();
    }

    private void NewGame()
    {
        if (activegame.GameStatus != GameStatusEnum.Playing) 
        {
            activegame.NewGame();
            lstabcbuttons.ForEach(b => b.IsEnabled = true);
        };
    }

    private void GuessALetter(Button btn)
    {
        Letter letter = activegame.Letters[lstabcbuttons.IndexOf(btn)];
        activegame.GuessALetter(letter);
        btn.IsEnabled = false;
        btnStart.IsEnabled = false;
        btnGiveUp.IsEnabled = true;
    }

    private void DetectGameWonOrLost()
    {
        activegame.DetectGameWonOrLost();
        if (activegame.GameStatus != GameStatusEnum.Playing)
        {
            btnGiveUp.IsEnabled = false;
            btnStart.IsEnabled = true;
        }
    }

    private void GiveUp()
    {
        activegame.GiveUp();
        btnGiveUp.IsEnabled = false;
        btnStart.IsEnabled = true;
    }

    private void ABCbtn_Clicked(object sender, EventArgs e)
    {
        if (sender is Button)
        {
            if (activegame.GameStatus == GameStatusEnum.Playing)
            {
                Button btn = (Button)sender;
                GuessALetter(btn);
                DetectGameWonOrLost();
            }
        }
    }

    private void btnStart_Clicked(object sender, EventArgs e)
    {
        NewGame();
    }

    private void btnGiveUp_Clicked(object sender, EventArgs e)
    {
        GiveUp();
    }

    private void Game_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton rb = (RadioButton)sender;
        if (rb.IsChecked && rb.BindingContext != null)
        {
            activegame = (Game)rb.BindingContext;
            this.BindingContext = activegame;
            switch (activegame.GameStatus)
            {
                case GameStatusEnum.GameWon:
                case GameStatusEnum.GiveUp:
                case GameStatusEnum.NotStarted:
                    btnGiveUp.IsEnabled = false;
                    btnStart.IsEnabled = true;
                    lstabcbuttons.ForEach(b => b.IsEnabled = true);
                    break;
            }
            //List<Button> buttonstoenable = lstabcbuttons.Where(b => b.BackgroundColor == standardcolor).ToList();
            //buttonstoenable.ForEach(b => b.IsEnabled = true);
        }
    }
}
 