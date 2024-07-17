using MeltingSnowmanSystem;
using static MeltingSnowmanSystem.Game;

namespace MeltingSnowmanTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetMysteryWord()
        {
            Game game = new();
            game.GetMysteryWord();
            int numletters = game.Word.WordValue.Count();
            string msg = $"Mystery Word = {game.Word.WordValue} Number of Letters = {numletters} Number of Dashes = {game.Dash.Count()} MysteryWordDisplay = {game.WordDisplay.WordValue} Word Display Color = {game.WordDisplay.Color}";
            Assert.IsTrue( numletters > 0 && game.Dash.Count() == numletters && game.WordDisplay.WordValue == game.Dash && game.WordDisplay.Color == game.DisplayStandardColor, msg);
            TestContext.WriteLine(msg);
        }

        [Test]
        public void TestNewGame()
        {
            Game game = new();
            game.NewGame();
            bool b = game.Pictures.TrueForAll(p => p.Contains("blank") == false);
            string msg = $"Game Status = {game.GameStatus} Letters Back Color = {game.Letters[0].BackColor} Snowman Is Complete = {b} MysteryWordDisplay = {game.WordDisplay.WordValue}";
            Assert.IsTrue(game.GameStatus == GameStatusEnum.Playing && game.Letters.TrueForAll(l => l.BackColor == game.LetterStandardColor) && b == true && game.WordDisplay.WordValue.Count() > 0, msg);
            TestContext.WriteLine(msg);
        }

        [Test]
        public void TestGuessALetter()
        {
            Game game = new();
            Random rnd = new();
            game.NewGame();
            List<Letter> correctletters = game.Letters.Where(l => game.Word.WordValue.Contains(l.LetterValue)).ToList();
            Letter correctletter = correctletters[rnd.Next(0, correctletters.Count)];
            int oldnumblankpictures = game.Pictures.Count(p => p.Contains("blank") == true);
            game.GuessALetter(correctletter);
            List<Letter> incorrectletters = game.Letters.Where(l => game.Word.WordValue.Contains(l.LetterValue) == false).ToList();
            Letter incorrectletter = incorrectletters[rnd.Next(0, incorrectletters.Count)];
            game.GuessALetter(incorrectletter);
            int newnumblankpictures = game.Pictures.Count(p => p.Contains("blank") == true);
            string msg = $"Mystery Word = {game.Word.WordValue} Correct Letter = {correctletter.LetterValue} Letter Back Color = {correctletter.BackColor} Incorrect Letter = {incorrectletter.LetterValue} Letter Back Color = {incorrectletter.BackColor} Mystery Word Display = {game.WordDisplay.WordValue} Previous # of Blank Pictures = {oldnumblankpictures} New # of Blank Pictures = {newnumblankpictures}";
            Assert.IsTrue(game.WordDisplay.WordValue.Contains(correctletter.LetterValue) && correctletter.BackColor == game.LetterCorrectColor && game.WordDisplay.WordValue.Contains(incorrectletter.LetterValue) == false && incorrectletter.BackColor == game.LetterIncorrectColor && newnumblankpictures > oldnumblankpictures, msg);
            TestContext.WriteLine(msg);
        }

        [Test]

        public void TestDetectGameWonAndGetScore()
        {
            Game game = new();
            game.NewGame();
            game.WordDisplay.WordValue = game.Word.WordValue;
            int oldscore = game.Score;
            game.DetectGameWonOrLost();
            int newscore = game.Score;
            string msg = $"Game Status = {game.GameStatus} Mystery Word Display = {game.WordDisplay.WordValue} Color = {game.WordDisplay.Color} Message = {game.Message.MessageText} Color = {game.Message.Color} Previous Score = {oldscore} New Score = {newscore} ";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.GameWon && game.WordDisplay.WordValue == game.Word.WordValue && game.WordDisplay.Color == game.DisplayWinningColor && game.GameWonMessages.Contains(game.Message.MessageText) == true && game.Message.Color == game.DisplayWinningColor && newscore > oldscore, msg);
            TestContext.WriteLine(msg);
        }

        [Test]
        public void TestDetectGameLostAndGetScore()
        {
            Random rnd = new();
            Game game = new();
            game.NewGame();
            List<Letter> incorrectletters = game.Letters.Where(l => game.Word.WordValue.Contains(l.LetterValue) == false).ToList();
            Letter incorrectletter = incorrectletters[rnd.Next(0, incorrectletters.Count)];
            game.GuessALetter(incorrectletter);
            game.GuessALetter(incorrectletter);
            game.GuessALetter(incorrectletter);
            game.GuessALetter(incorrectletter);
            game.GuessALetter(incorrectletter);
            game.GuessALetter(incorrectletter);
            bool b = game.Pictures.TrueForAll(pb => pb.Contains("blank"));
            int oldscore = game.Score;
            game.DetectGameWonOrLost();
            int newscore = game.Score;
            string msg = $"Game Status = {game.GameStatus} Mystery Word Display = {game.WordDisplay.WordValue} Color = {game.WordDisplay.Color} Message = {game.Message.MessageText} Color = {game.Message.Color} Snowman is Melted = {b} Previous Score = {oldscore} New Score = {newscore} ";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.GameLost && game.WordDisplay.WordValue == game.Word.WordValue && game.WordDisplay.Color == game.DisplayLosingColor && game.GameLostMessages.Contains(game.Message.MessageText) == true && game.Message.Color == game.DisplayLosingColor && newscore < oldscore, msg);
            TestContext.WriteLine(msg);

        }
}
}