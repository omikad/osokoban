using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace OArcanoid.Core.Items
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class BallItem : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public BallItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public int ZIndex => 20;

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Ball, cellRect);
		}
	}
}