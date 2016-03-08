using System;
using System.Windows;
using System.Windows.Media;
using GamesCommon.Helpers;
using GamesCommon.GameCore;

namespace GamesCommon.UI
{
	public class LevelImage : FrameworkElement
	{
		public const int CellWidth = 16;
		public const int CellHeight = 16;
		private const int borderThickness = 0;

		private DrawingVisual boardControl = new DrawingVisual();
		private IGameHolder gameHolder;

		public LevelImage()
		{
			Loaded += OnLoaded;
			Unloaded += RemoveVisualFromTree;
		}

		public void SetGame(IGameHolder _gameHolder)
		{
			gameHolder = _gameHolder;
		}

		public void Redraw()
		{
			var board = gameHolder.CurrentBoard;

			using (var dc = boardControl.RenderOpen())
			{
				for (var x = 0; x < board.CellsX; x++)
				{
					for (var y = 0; y < board.CellsY; y++)
					{
						var cellRect = new Rect(
							x * CellWidth + borderThickness,
							y * CellHeight + borderThickness,
							CellWidth - borderThickness,
							CellHeight - borderThickness);

						var item = board.DrawableContent(x, y).ElementWithMax(i => i.ZIndex);

						item.Draw(dc, cellRect);
					}
				}
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			var game = gameHolder.CurrentBoard;

			if (game == null) return;

			Width = CellWidth * game.CellsX;
			Height = CellHeight * game.CellsY;

			boardControl = new DrawingVisual();

			Redraw();

			AddVisualToTree();
		}

		private void AddVisualToTree()
		{
			AddVisualChild(boardControl);
			AddLogicalChild(boardControl);
		}

		private void RemoveVisualFromTree(object sender, RoutedEventArgs e)
		{
			RemoveLogicalChild(boardControl);
			RemoveVisualChild(boardControl);
		}

		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			return new PointHitTestResult(this, hitTestParameters.HitPoint);
		}

		protected override Visual GetVisualChild(int index) => boardControl;

		protected override int VisualChildrenCount => 1;
	}
}