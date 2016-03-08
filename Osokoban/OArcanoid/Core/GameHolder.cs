using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using GamesCommon.GameCore;

namespace OArcanoid.Core
{
	[Export]
	public class GameHolder : IGameHolder
	{
		private readonly CompositionContainer container;

		[ImportingConstructor]
		public GameHolder(CompositionContainer container)
		{
			this.container = container;
			ReloadRandomLevel();
		}

		public Game CurrentGame { get; private set; }
		public IBoard CurrentBoard => CurrentGame;

		public void ReloadRandomLevel()
		{
			CurrentGame = container.GetExportedValue<Game>();
		}
	}
}