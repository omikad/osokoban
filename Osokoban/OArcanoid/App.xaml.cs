using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Threading;

namespace OArcanoid
{
	public partial class App
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			try
			{
				var container = new CompositionContainer(new AssemblyCatalog(typeof(App).Assembly));

				container.ComposeExportedValue(container);

				container.GetExportedValue<MainWindow>().Show();
			}
			catch (Exception exception)
			{
				OnError(exception);
			}
		}

		private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			OnError(e.Exception);
		}

		private static void OnError(Exception exception)
		{
			MessageBox.Show(exception.ToString());
			Environment.Exit(1);
		}
	}
}
