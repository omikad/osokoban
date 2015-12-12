using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Osokoban.Core
{
	[Export]
	public class GameHolder
	{
		private readonly ExportProvider container;

		[ImportingConstructor]
		public GameHolder(ExportProvider container)
		{
			this.container = container;
			ReloadRandomLevel();
		}

		public Game CurrentGame { get; private set; }

		public void ReloadRandomLevel()
		{
			CurrentGame = container.GetExportedValue<Game>();
		}
	}
}