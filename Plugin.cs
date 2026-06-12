using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ChillieFirst
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        // Config entries
        public static ConfigEntry<SkillType> SelectedSkill { get; private set; }
        public static ConfigEntry<float> IdleThreshold { get; private set; }

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo("✅ Hello Chillie Mod loaded!");

            // Setup Config
            SelectedSkill = Config.Bind("General", "SelectedSkill", SkillType.None, "The skill to auto when idle");
            IdleThreshold = Config.Bind("General", "IdleThreshold", 60f, "Seconds of no XP before auto-skilling");

            var harmony = new Harmony("com.chillieman.chilliefirst");
            harmony.PatchAll();

            ModUI.Create(IdleThreshold.Value);
            XPMonitor.Create(IdleThreshold.Value, SelectedSkill.Value).StartMonitoring();
            ListManagerUI.Create();
        }
    }
}
