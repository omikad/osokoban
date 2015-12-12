using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class TargetPlaceCell : ICell
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public TargetPlaceCell(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.TargetPlace, cellRect);			
		}
	}
}