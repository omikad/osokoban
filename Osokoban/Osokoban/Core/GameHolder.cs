using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Osokoban.Core
{
	[Export]
	public class GameHolder
	{
		[ImportingConstructor]
		public GameHolder(ExportProvider container)
		{
			CurrentGame = container.GetExportedValue<Game>();
		}

		public Game CurrentGame { get; private set; }
	}
}