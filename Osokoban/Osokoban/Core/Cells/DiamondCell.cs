using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class DiamondCell : ICell
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public DiamondCell(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public bool CanMoveHere => false;
		public bool IsPlayer => false;

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Diamond, cellRect);			
		}
	}
}