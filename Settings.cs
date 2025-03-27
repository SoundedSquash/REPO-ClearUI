using BepInEx.Configuration;
using BepInEx.Logging;

namespace ClearUI
{
    public static class Settings
    {
        public static ConfigEntry<bool> IsEnabled { get; set; } = null!;

        public static ManualLogSource Logger { get; private set; } = null!;

        internal static void Initialize(ConfigFile config, ManualLogSource logger)
        {
            Logger = logger;
            
            IsEnabled = config.Bind(
                "General",
                "Enabled",
                true,
                "Enable this mod.");
        }
    }
}