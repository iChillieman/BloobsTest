using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ChillieMod
{
    [BepInPlugin("ChillieMod", "ChillieMod", "0.0.1")]
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

            var harmony = new Harmony("com.chillieman.ChillieMod");
            harmony.PatchAll();

            ModUI.Create(IdleThreshold.Value);
            XPMonitor.Create(IdleThreshold.Value, SelectedSkill.Value).StartMonitoring();
            ListManagerUI.Create();
        }
    }
}
