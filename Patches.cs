using HarmonyLib;


namespace ChillieMod
{
    [HarmonyPatch(typeof(LevelUpMessage), "ShowMessage")]
    public static class LevelUpMessagePatch
    {
        private static bool Prefix(ref string message)
        {
            // Replace "Congratulations!" with "Chillieman!"
            if (!string.IsNullOrEmpty(message))
            {
                
                if (message.Contains("Congratulations!"))
                {
                    message = message.Replace("Congratulations!", "Chillieman!");
                }
                else
                {
                    message = "Chillieman! " + message;
                }
            }

            // Return true = let the original method run with the modified message
            return true;
        }
    }

    [HarmonyPatch(typeof(CharacterMovement), "HandleClickMovement")]
    public static class CharacterMovementPatch
    {
        private static bool Prefix()
        {
            // Block movement clicks only when mod UI is visible
            return !ChillieHelper.IsAnyUIShowing();
        }
    }

    [HarmonyPatch(typeof(SkillManager), "StopAllSkills")]
    public static class SkillManagerPatch
    {
        private static bool Prefix()
        {
            if (ChillieHelper.IsAnyUIShowing())
            {
                Plugin.Log.LogDebug("ChillieLog - Blocked StopAllSkills because Mod UI is open");
                return false; // Prevent stopping skills when mod UI is visible
            }
            return true;
        }
    }

}