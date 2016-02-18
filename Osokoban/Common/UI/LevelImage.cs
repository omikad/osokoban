using System.Windows;
using System.Windows.Media;
using Common.GameCore;
using Common.Helpers;

namespace Common.UI
{
	public class LevelImage : FrameworkElement
	{
		public const int CellWidth = 16;
		public const int CellHeight = 16;
		private const int borderThickness = 0;

		private DrawingVisual board = new DrawingVisual();
		private IBoard game;

		public LevelImage()
		{
			Loaded += OnLoaded;
			Unloaded += RemoveVisualFromTree;
		}

		public void SetGame(IBoard gameValue)
		{
			game = gameValue;
		}

		public void Redraw()
		{
			using (var dc = board.RenderOpen())
			{
				for (var x = 0; x < game.CellsX; x++)
					for (var y = 0; y < game.CellsY; y++)
					{
						var cellRect = new Rect(
							x * CellWidth + borderThickness,
							y * CellHeight + borderThickness,
							CellWidth - borderThickness,
							CellHeight - borderThickness);

						game.DrawableContent(x, y).ElementWithMax(i => i.ZIndex).Draw(dc, cellRect);
					}
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (game == null) return;

			Width = CellWidth * game.CellsX;
			Height = CellHeight * game.CellsY;

			board = new DrawingVisual();

			Redraw();

			AddVisualToTree();
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