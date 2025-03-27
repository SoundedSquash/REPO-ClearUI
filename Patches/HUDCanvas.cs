using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ClearUI.Patches
{
    [HarmonyPatch(typeof(HUDCanvas))]
    public static class HUDCanvasPatches
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void HUDCanvasAwakePostfix()
        {
            Settings.RefreshPostProcessEffects();
        }
    }
}