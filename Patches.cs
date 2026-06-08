using HarmonyLib;

namespace ChillieFirst
{
    [HarmonyPatch]
    public static class LevelUpPatches
    {
        [HarmonyPatch("LevelUpMessage", nameof(LevelUpMessage.ShowMessage))]
        [HarmonyPrefix]
        private static bool ShowMessagePrefix(ref string message)
        {
            Plugin.Log.LogInfo("ChillieLog - LevelUpMessage.ShowMessage is being called!");

            // Replace "Congratulations!" with "Chillieman!"
            if (!string.IsNullOrEmpty(message))
            {
                if (message.Contains("Congratulations!"))
                {
                    message = message.Replace("Congratulations!", "Chillieman!");
                    Plugin.Log.LogInfo("ChillieLog - Replaced 'Congratulations!' with 'Chillieman!' in level up message");
                }
            }

            // Return true = let the original method run with the modified message
            return true;
        }
    }
}
