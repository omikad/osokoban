using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Osokoban.Core.Items
{
	[Export]
	public class ItemsFactory
	{
		private readonly ExportProvider container;

		[ImportingConstructor]
		public ItemsFactory(ExportProvider container)
		{
			this.container = container;
		}

		public T CreateItem<T>() where T : IGameItem
		{
			return container.GetExportedValue<T>();
		}
	}
}