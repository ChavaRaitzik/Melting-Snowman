using MeltingSnowmanSystem;
using static MeltingSnowmanSystem.Game;

namespace MeltingSnowmanMAUI;

public partial class MeltingSnowman : ContentPage
{
    Game game = new();
    List<Button> lstabcbuttons;
    public MeltingSnowman()
	{
		InitializeComponent();
        this.BindingContext = game;
        lstabcbuttons = new() { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
        btnGiveUp.IsEnabled = false;
    }

    private void NewGame()
    {
        game.NewGame();
        lstabcbuttons.ForEach(b => b.IsEnabled = true);
    }

    private void GuessALetter(Button btn)
    {
        Letter letter = game.Letters[lstabcbuttons.IndexOf(btn)];
        game.GuessALetter(letter);
        btn.IsEnabled = false;
        btnStart.IsEnabled = false;
        btnGiveUp.IsEnabled = true;
    }

    private void DetectGameWonOrLost()
    {
        game.DetectGameWonOrLost();
        if (game.GameStatus != GameStatusEnum.Playing)
        {
            btnGiveUp.IsEnabled = false;
            btnStart.IsEnabled = true;
        }
    }

    private void GiveUp()
    {
        game.GiveUp();
        btnGiveUp.IsEnabled = false;
        btnStart.IsEnabled = true;
    }

    private void ABCbtn_Clicked(object sender, EventArgs e)
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

    private void btnStart_Clicked(object sender, EventArgs e)
    {
        NewGame();
    }

    private void btnGiveUp_Clicked(object sender, EventArgs e)
    {
        GiveUp();
    }
}
