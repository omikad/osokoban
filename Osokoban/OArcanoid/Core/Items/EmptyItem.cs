using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace OArcanoid.Core.Items
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class EmptyItem : IGameItem
	{
		public int ZIndex => 1;

		public PadPart Part { get; set; }

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawRectangle(Brushes.White, null, cellRect);
		}
	}
}