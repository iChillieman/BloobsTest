using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ChillieFirst
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo("✅ Hello World Mod loaded!");

            var harmony = new Harmony("com.chillieman.helloWorld");
            harmony.PatchAll();
        }
    }
}
