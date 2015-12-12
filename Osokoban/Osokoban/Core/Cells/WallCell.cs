using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class WallCell : ICell
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public WallCell(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Wall, cellRect);
		}

		public bool CanMoveHere => false;
		public bool IsPlayer => false;
	}
}