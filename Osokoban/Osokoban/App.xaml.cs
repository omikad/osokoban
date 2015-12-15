using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Threading;

namespace Osokoban
{
	public partial class App
	{
		public static CompositionContainer StartExternal()
		{
			try
			{
				var container = new CompositionContainer(new AssemblyCatalog(typeof(App).Assembly));

				container.ComposeExportedValue<ExportProvider>(container);

				var mainWindow = container.GetExportedValue<MainWindow>();

				mainWindow.Show();

				return container;
			}
			catch (Exception exception)
			{
				OnError(exception);
				return null;
			}
		}

		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			StartExternal();
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
