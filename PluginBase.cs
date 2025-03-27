using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ClearUI
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class PluginBase : BaseUnityPlugin
    {
        private const string PluginGuid = "soundedsquash.clearui";
        private const string PluginName = "Clear UI";
        private const string PluginVersion = "0.2.0.0";
        
        private readonly Harmony _harmony = new Harmony(PluginGuid);

        private static readonly ManualLogSource ManualLogSource = BepInEx.Logging.Logger.CreateLogSource(PluginGuid);

        public void Awake()
        {
            // Initialize global objects
            Settings.Initialize(Config, ManualLogSource);

            _harmony.PatchAll();
            ManualLogSource.LogInfo($"{PluginName} loaded");
        }
    }
}