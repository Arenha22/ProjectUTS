using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace UTS
{
	class Program
	{
		static void Main(string[] args)
		{
			var ourWindow = new NativeWindowSettings()
			{
				Size = new Vector2i(600, 600),
				Title = "Project UTS"
			};

			using (var window = new Window(GameWindowSettings.Default, ourWindow))
			{
				window.Run();
			}
		}
	}
}
