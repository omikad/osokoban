﻿using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Osokoban.DataTypes;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class EmptyCell : IGameItem
	{
		public bool CanMoveHere => true;
		public bool IsPlayer => false;
		public int ZIndex => 1;

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawRectangle(Brushes.White, null, cellRect);
		}

		public bool Move(Game game, PointInt currentPoint, PointInt destPoint)
		{
			return false;
		}
	}
}