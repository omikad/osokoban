using System.ComponentModel.Composition;
using System.Windows.Input;

namespace OArcanoid
{
	[Export]
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			
		}
	}
}
