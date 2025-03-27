using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ClearUI
{
    public static class Settings
    {
        public static ConfigEntry<bool> ChromaticAberrationEnabled { get; set; } = null!;
        
        public static ConfigEntry<bool> LensDistortionEnabled { get; set; } = null!;
        
        public static ConfigEntry<bool> BloomEnabled { get; set; } = null!;

        public static ManualLogSource Logger { get; private set; } = null!;

        private static List<PostProcessEffectSettings>? PostProcessEffectSettingsList => GameObject
            .Find("Game Director/Post Processing/Post Processing Overlay")?
            .GetComponent<PostProcessVolume>()?
            .profile?
            .settings;

        internal static void Initialize(ConfigFile config, ManualLogSource logger)
        {
            Logger = logger;
            
            var setting = ChromaticAberrationEnabled = config.Bind(
                "General",
                "Chromatic Aberration",
                false,
                "Color distortion around the edges of the screen");
            setting.SettingChanged += OnConfigChanged;
            
            setting = BloomEnabled = config.Bind(
                "General",
                "Bloom",
                false,
                "Glow");
            setting.SettingChanged += OnConfigChanged;
            
            setting = LensDistortionEnabled = config.Bind(
                "General",
                "Lens Distortion",
                false,
                "Curved screen");
            setting.SettingChanged += OnConfigChanged;
            
        }

        private static void OnConfigChanged(object sender, EventArgs e)
        {
            Logger.LogDebug("Settings updated. Refreshing post process effects.");
            Settings.RefreshPostProcessEffects();
        }

        public static void RefreshPostProcessEffects()
        {
            var settings = PostProcessEffectSettingsList;
            if (settings == null) return;
            
            foreach (var setting in settings)
            {
                if (setting == null) continue;
                switch (setting)
                {
                    case Bloom bloom:
                        bloom.active = BloomEnabled.Value;
                        break;
                    case ChromaticAberration chromaticAberration:
                        chromaticAberration.active = ChromaticAberrationEnabled.Value;
                        break;
                    case LensDistortion lensDistortion:
                        lensDistortion.active = LensDistortionEnabled.Value;
                        break;
                }
            }
        }
    }
}