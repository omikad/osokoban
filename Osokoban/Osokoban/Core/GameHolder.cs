using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace OSokoban.Core
{
	[Export]
	public class GameHolder
	{
		private readonly CompositionContainer container;

		[ImportingConstructor]
		public GameHolder(CompositionContainer container)
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