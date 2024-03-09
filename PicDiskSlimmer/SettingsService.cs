using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Storage;

namespace PicDiskSlimmer
{
    public class Settings
    {
        public int Quality { get; set; } = 70; // Default value
        public bool DeleteBaseImagesAfterProcessing { get; set; } = false; // Default value
        public int ParallelProcessingThreadsCount { get; set; } = 8;
    }


    public class SettingsService
    {
        private const string SettingsFileName = "settings.json";

        public Settings LoadSettings()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, SettingsFileName);
                if (File.Exists(filePath))
                {
                    using var fileStream = File.OpenRead(filePath);
                    return JsonSerializer.Deserialize<Settings>(fileStream) ?? new Settings();
                }
                else
                {
                    return new Settings();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
                return new Settings();
            }
        }

        public void SaveSettings(Settings settings)
        {
            try
            {
                var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, SettingsFileName);
                var jsonString = JsonSerializer.Serialize(settings);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }
        }
    }
}
