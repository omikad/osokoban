using System.ComponentModel.Composition;
using System.Windows.Input;
using GamesCommon.DataTypes;
using OSokoban.Core;

namespace OSokoban
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
				case Key.Up:
					gameHolder.CurrentGame.MovePlayer(new PointInt(0, -1));
					break;
				case Key.Down:
					gameHolder.CurrentGame.MovePlayer(new PointInt(0, 1));
					break;
				case Key.Left:
					gameHolder.CurrentGame.MovePlayer(new PointInt(-1, 0));
					break;
				case Key.Right:
					gameHolder.CurrentGame.MovePlayer(new PointInt(1, 0));
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
