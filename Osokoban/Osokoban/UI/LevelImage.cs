using System.Windows;
using System.Windows.Media;
using Osokoban.Core;

namespace Osokoban.UI
{
	public class LevelImage : FrameworkElement
	{
		private const int cellWidth = 16;
		private const int cellHeight = 16;
		private const int borderThickness = 0;

		private DrawingVisual board = new DrawingVisual();
		private Game game;

		public LevelImage()
		{
			Loaded += OnLoaded;
			Unloaded += RemoveVisualFromTree;
		}

		public void SetGame(Game gameValue)
		{
			game = gameValue;
		}

		public void Redraw()
		{
			using (var dc = board.RenderOpen())
			{
				RenderBorders(dc);
				RenderCells(dc);
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (game == null) return;

			Width = cellWidth * game.CellsX;
			Height = cellHeight * game.CellsY;

			board = new DrawingVisual();

			Redraw();

			AddVisualToTree();
		}

		private void RenderBorders(DrawingContext dc)
		{
			for (var i = 0; i <= game.CellsX; i++)
			{
				var vertical = new Rect(i * cellWidth, 0, borderThickness, cellHeight * game.CellsY + borderThickness);
				dc.DrawRectangle(Brushes.Black, null, vertical);
			}

			for (var i = 0; i <= game.CellsY; i++)
			{
				var horizontal = new Rect(0, i * cellHeight, cellWidth * game.CellsX + borderThickness, borderThickness);
				dc.DrawRectangle(Brushes.Black, null, horizontal);
			}
		}

		private void RenderCells(DrawingContext dc)
		{
			for (var x = 0; x < game.CellsX; x++)
				for (var y = 0; y < game.CellsY; y++)
				{
					var cellRect = new Rect(
						x * cellWidth + borderThickness,
						y * cellHeight + borderThickness,
						cellWidth - borderThickness,
						cellHeight - borderThickness);

					game[x, y].Draw(dc, cellRect);
				}
		}

		private void AddVisualToTree()
		{
			AddVisualChild(board);
			AddLogicalChild(board);
		}

		private void RemoveVisualFromTree(object sender, RoutedEventArgs e)
		{
			RemoveLogicalChild(board);
			RemoveVisualChild(board);
		}

		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			return new PointHitTestResult(this, hitTestParameters.HitPoint);
		}

		protected override Visual GetVisualChild(int index) => board;

		protected override int VisualChildrenCount => 1;
	}
}