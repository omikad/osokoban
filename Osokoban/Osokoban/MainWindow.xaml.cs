using System.ComponentModel.Composition;
using Osokoban.Core;

namespace Osokoban
{
	[Export]
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();

			LevelImage.SetGame(new Game());
		}
	}
}
