using System.ComponentModel.Composition;
using System.Windows.Input;
using OArcanoid.Core;

namespace OArcanoid
{
	[Export]
	public partial class MainWindow
	{
		private readonly GameHolder gameHolder;

		[ImportingConstructor]
		public MainWindow(GameHolder gameHolder)
		{
			this.gameHolder = gameHolder;
			InitializeComponent();
			NewLevel();
		}

		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Left:
					gameHolder.CurrentGame.MoveLeft();
					break;
				case Key.Right:
					gameHolder.CurrentGame.MoveRight();
					break;
				case Key.F2:
					NewLevel();
					break;
			}

			LevelImage.Redraw();
		}

		private void NewLevel()
		{
			gameHolder.ReloadRandomLevel();
			LevelImage.SetGame(gameHolder.CurrentGame);
		}
	}
}
