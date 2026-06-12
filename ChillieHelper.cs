using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillieFirst
{
    public static class ChillieHelper
    {
        public static bool IsAnyUIShowing()
        {
            bool modUI = ModUI.Instance != null && ModUI.Instance.IsShowing();
            bool listUI = ListManagerUI.Instance != null && ListManagerUI.Instance.IsShowing();

            return modUI || listUI;
        }

        public static void SkillActionRequest(ChillieItem item)
        {
            switch (item.GetSkillType())
            {
                case SkillType.Foraging:
                    MapForageItemToCollectionAction(item.GetRequiredLevel());
                    break;
                default:
                    Plugin.Log.LogError("Cannot request this yet");
                    break;

            }
        }

        private static void MapForageItemToCollectionAction(int requiredLevel)
        {
            ForagingSkill skill = AchievementManager.Instance.foragingSkill;
            skill.isAutoForaging = false;

            switch (requiredLevel)
            {
                case 1: skill.StartLevel1Foraging(); break;
                case 2: skill.StartLevel2Foraging(); break;
                case 3: skill.StartLevel3Foraging(); break;
                case 5: skill.StartLevel5Foraging(); break;
                case 6: skill.StartLevel6Foraging(); break;
                case 8: skill.StartLevel8Foraging(); break;
                case 10: skill.StartLevel10Foraging(); break;
                case 13: skill.StartLevel13Foraging(); break;
                case 15: skill.StartLevel15Foraging(); break;
                case 18: skill.StartLevel18Foraging(); break;
                case 20: skill.StartLevel20Foraging(); break;
                case 25: skill.StartLevel25Foraging(); break;
                case 28: skill.StartLevel28Foraging(); break;
                case 30: skill.StartLevel30Foraging(); break;
                case 35: skill.StartLevel35Foraging(); break;
                case 38: skill.StartLevel38Foraging(); break;
                case 40: skill.StartLevel40Foraging(); break;
                case 44: skill.StartLevel44Foraging(); break;
                case 45: skill.StartLevel45Foraging(); break;
                case 48: skill.StartLevel48Foraging(); break;
                case 50: skill.StartLevel50Foraging(); break;
                case 55: skill.StartLevel55Foraging(); break;
                case 58: skill.StartLevel58Foraging(); break;
                case 60: skill.StartLevel60Foraging(); break;
                case 65: skill.StartLevel65Foraging(); break;
                case 68: skill.StartLevel68Foraging(); break;
                case 70: skill.StartLevel70Foraging(); break;
                case 75: skill.StartLevel75Foraging(); break;
                case 78: skill.StartLevel78Foraging(); break;
                case 85: skill.StartLevel85Foraging(); break;
                case 88: skill.StartLevel88Foraging(); break;
                case 95: skill.StartLevel95Foraging(); break;
                case 98: skill.StartLevel98Foraging(); break;
                case 105: skill.StartLevel105Foraging(); break;
                case 115: skill.StartLevel115Foraging(); break;
                case 120: skill.StartLevel120Foraging(); break;
                case 140: skill.StartLevel140Foraging(); break;
                case 160: skill.StartLevel160Foraging(); break;
                case 180: skill.StartLevel180Foraging(); break;
                case 210: skill.StartLevel210Foraging(); break;
                case 215: skill.StartLevel215Foraging(); break;
                case 225: skill.StartLevel225Foraging(); break;
                case 234: skill.StartLevel234Foraging(); break;
                case 255: skill.StartLevel255Foraging(); break;
                case 282: skill.StartLevel282Foraging(); break;

                default:
                    Plugin.Log.LogError($"Cannot map Foraging level {requiredLevel} - No handler found");
                    break;
            }
        }
    }
}
