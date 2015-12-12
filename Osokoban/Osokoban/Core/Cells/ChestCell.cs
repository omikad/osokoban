using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class ChestCell : ICell
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public ChestCell(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public bool CanMoveHere => true;
		public bool IsPlayer => false;
		public bool IsWithDiamond { get; private set; }

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			var img = IsWithDiamond ? assetsManager.ChestClosed : assetsManager.ChestOpened;
			dc.DrawImage(img, cellRect);
		}
	}
}