using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillieFirst
{
    public class XPMonitor : MonoBehaviour
    {
        public static XPMonitor Instance { get; private set; }

        public SkillType selectedSkillType = SkillType.None;

        public float delay = 60f;
        private bool _isMonitoring = false;
        private Coroutine _monitoringCoroutine;

        public static readonly List<SkillType> availableSkills = [
            SkillType.Fishing,
            SkillType.Thieving,
            SkillType.Dexterity,
            SkillType.Woodcutting,
            SkillType.Mining,
            SkillType.Foraging
        ];

        public static XPMonitor Create(float delay = 60f, SkillType skill = SkillType.None)
        {
            if (Instance != null) return Instance;

            var go = new GameObject("XPMonitor");
            DontDestroyOnLoad(go);
            Instance = go.AddComponent<XPMonitor>();

            Instance.SetDelay(delay);
            Instance.selectedSkillType = skill;

            Plugin.Log.LogInfo("✅ XPMonitor initialized");
            return Instance;
        }

        public float GetDelay()
        {
            return delay;
        }

        public void SetDelay(float value)
        {
            delay = value;
        }

        public bool IsMonitoring()
        {
            return _isMonitoring;
        }

        public void StartMonitoring()
        {
            if (_isMonitoring) return;

            Plugin.Log.LogInfo("🚀 Starting XP Auto-Skill Monitor");
            _isMonitoring = true;
            _monitoringCoroutine = StartCoroutine(WatchLastXP());
        }

        public void StopMonitoring()
        {
            if (!_isMonitoring) return;

            Plugin.Log.LogInfo("⛔ Stopping XP Auto-Skill Monitor");
            if (_monitoringCoroutine != null)
                StopCoroutine(_monitoringCoroutine);

            _isMonitoring = false;
        }

        private IEnumerator WatchLastXP()
        {
            while (_isMonitoring)
            {
                if (OfflineProgression.Instance != null)
                {
                    float idleTime = OfflineProgression.Instance.timeSinceLastXP;

                    if (idleTime >= GetDelay())
                    {
                        Plugin.Log.LogInfo($"🪓 Idle detected! Starting auto {selectedSkillType}");
                        MapRequest(selectedSkillType);
                    }
                }

                yield return new WaitForSecondsRealtime(10f);
            }
        }

        private void MapRequest(SkillType type)
        {
            Plugin.Log.LogInfo("Request to map!");
            switch (type)
            {
                case SkillType.Fishing:
                    StartAutoFishing();
                    break;
                case SkillType.Thieving:
                    StartAutoThieving();
                    break;
                case SkillType.Dexterity:
                    StartAutoDex();
                    break;
                case SkillType.Woodcutting:
                    StartAutoChoppin();
                    break;
                case SkillType.Mining:
                    StartAutoMining();
                    break;
                case SkillType.Foraging:
                    StartAutoForaging();
                    break;
                default:
                    Plugin.Log.LogInfo($"SkillType is not supported");
                    break;

            }
        }

        private void StartAutoDex()
        {
            if (AchievementManager.Instance.dexteritySkill.GetDexterityPrestigeLevel() > 0)
            {
                Plugin.Log.LogInfo("Staring Auto Dex");
                AchievementManager.Instance.dexteritySkill.StartAutoDexterity();
            }
            else
            {
                Plugin.Log.LogInfo("Auto Dex is not allowed!");
            }
        }

        private void StartAutoFishing()
        {
            if (AchievementManager.Instance.fishingSkill.GetFishingPrestigeLevel() > 0)
            {
                Plugin.Log.LogInfo("Staring Auto Fishing");
                AchievementManager.Instance.fishingSkill.StartAutoFishing();
            }
            else
            {
                Plugin.Log.LogInfo("Auto Fishing not allowed!");
            }
        }

        private void StartAutoThieving()
        {
            if (AchievementManager.Instance.thievingSkill.GetThievingPrestigeLevel() > 0)
            {
                Plugin.Log.LogInfo("Staring Auto Thieving");
                AchievementManager.Instance.thievingSkill.StartAutoThieving();
            }
            else
            {
                Plugin.Log.LogInfo("Auto Thieving not allowed!");
            }
        }

        private void StartAutoMining()
        {
            if (AchievementManager.Instance.miningSkill.GetMiningPrestigeLevel() > 0)
            {
                Plugin.Log.LogInfo("Staring Auto Mining");
                AchievementManager.Instance.miningSkill.StartAutoMining();
            }
            else
            {
                Plugin.Log.LogInfo("Auto Mining not allowed!");
            }

        }

        private void StartAutoChoppin()
        {
            if (AchievementManager.Instance.woodcuttingSkill.GetWoodcuttingPrestigeLevel() > 0)
            {
                Plugin.Log.LogInfo("Staring Auto Choppin");
                AchievementManager.Instance.woodcuttingSkill.StartAutoWoodcutting();
            }
            else
            {
                Plugin.Log.LogInfo("Auto Chopping not allowed!");
            }
        }

        private void StartAutoForaging()
        {
            if (AchievementManager.Instance.foragingSkill.GetForagingPrestigeLevel() > 0)
            {
                Plugin.Log.LogInfo("Staring Auto Foraging");
                AchievementManager.Instance.foragingSkill.StartAutoForaging();
            }
            else
            {
                Plugin.Log.LogInfo("Auto Foraging not allowed!");
            }
        }

    }

}
