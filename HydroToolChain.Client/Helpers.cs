using HydroToolChain.App.Configuration.Models;

namespace HydroToolChain.Client;

public static class Helpers
{
    public static async Task<IReadOnlyCollection<string>> ChooseFilesHelper(string title, string rootPath)
    {
        var result = new List<string>();
        var thread = new Thread(obj =>
        {

            var mbresult = MessageBox.Show("Do you want to select a file? ('No' to select folder)", "Select Files?",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (mbresult == DialogResult.Cancel)
            {
                return;
            }

            var files = (List<string>)obj!;

            if (mbresult == DialogResult.Yes)
            {
                using var dialog = new OpenFileDialog();
                dialog.Title = title;
                dialog.Multiselect = true;
                dialog.Filter = "UE Assets|*.uasset;*.uexp;*.ubulk;*.ini;*.bin;*.umap;*.uplugin;*.uproject";
                dialog.InitialDirectory = rootPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    files.AddRange(dialog.FileNames);
                }

                return;
            }

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.UseDescriptionForTitle = true;
                dialog.Description = title;
                dialog.InitialDirectory = rootPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uasset", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uexp", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.ubulk", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.ini", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.bin", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.umap", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uplugin", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uproject", SearchOption.AllDirectories)
                        .ToList());
                }
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start(result);

        while (thread.IsAlive)
        {
            Thread.Sleep(100);
        }

        return await Task.FromResult(result);
    }

    public static async Task<string?> SelectConfigFile()
    {
        using var dialog = new OpenFileDialog();
        dialog.Title = @"Config to import";
        dialog.Multiselect = false;
        dialog.Filter = @"HydroModdingToolchain Configs|config.json;guids.json;uids.json;appData.json;*-HMT.json";

        return await MainForm.MainFormDispatcher.InvokeAsync(() =>
            dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null);
    }

    public static async Task<bool?> SaveConfigs(ConfigPartials? partial, string content)
    {
        var fileName = partial switch
        {
            ConfigPartials.Guids => "Guids",
            ConfigPartials.Uids => "Uids",
            _ => "Backup"
        };
        
        using var dialog = new SaveFileDialog();
        dialog.Title = @$"Save HMT Config [{fileName}]";
        dialog.DefaultExt = ".json";
        dialog.FileName = $"{fileName}-HMT";

        var result = await MainForm.MainFormDispatcher.InvokeAsync(() =>
            dialog.ShowDialog());

        if (result != DialogResult.OK)
            return result switch
            {
                DialogResult.Cancel => null,
                _ => false
            };
        
        
        await using var fileStream = new StreamWriter(dialog.OpenFile());
        await fileStream.WriteAsync(content);
            
        return true;


    }
}