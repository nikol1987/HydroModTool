using System.Diagnostics;

namespace HydroToolChain.App.WindowsHelpers;

public sealed class WindowsHelpers : IWindowsHelpers
{
    public void OpenFolder(string folder)
    {
        var startInfo = new ProcessStartInfo
        {
            Arguments = folder,
            FileName = "explorer.exe"
        };

        Process.Start(startInfo);
    }

    public void StartGame()
    {
        Process.Start(new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = $"steam://rungameid/{Constants.GameId}"
        });
    }
}