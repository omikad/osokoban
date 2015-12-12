﻿using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class EmptyCell : ICell
	{
		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawRectangle(Brushes.White, null, cellRect);
		}
	}
}