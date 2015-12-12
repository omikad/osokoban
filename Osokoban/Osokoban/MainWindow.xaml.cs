using System.ComponentModel.Composition;
using Osokoban.Core;

namespace Osokoban
{
	[Export]
	public partial class MainWindow
	{
		[ImportingConstructor]
		public MainWindow(GameHolder gameHolder)
		{
			InitializeComponent();

			LevelImage.SetGame(gameHolder.CurrentGame);
		}
	}
}
