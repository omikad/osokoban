using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace OArcanoid.Core.Items
{
	public enum PadPart
	{
		Left,
		Middle,
		Right
	};

	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class PadItem : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public PadItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public int ZIndex => 10;

		public PadPart Part { get; set; }

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			var imageSource = Part == PadPart.Left ? assetsManager.PadLeft
				: Part == PadPart.Middle ? assetsManager.PadCenter
				: assetsManager.PadRight;

			dc.DrawImage(imageSource, cellRect);
		}
	}
}