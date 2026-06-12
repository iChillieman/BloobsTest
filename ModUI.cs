using UnityEngine;

namespace ChillieFirst
{
    public class ModUI : MonoBehaviour
    {
        public static ModUI Instance { get; private set; }

        private bool showUI = false;
        private Rect windowRect = new Rect(20, 20, 360, 320);
        private int selectedSkillIndex = 0;
        private string timerInput = "60";

        public static ModUI Create(float timer)
        {
            if (Instance != null) return Instance;

            var go = new GameObject("ModSettingsUI");
            DontDestroyOnLoad(go);
            Instance = go.AddComponent<ModUI>();
            Instance.SetStartTime(timer);

            Plugin.Log.LogInfo("✅ Mod UI created");
            return Instance;
        }
        public void SetStartTime(float time)
        {
            timerInput = time.ToString();
        }

        public bool IsShowing()
        {
            return showUI;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                showUI = !showUI;
            }
        }

        private void LateUpdate()
        {
            if (showUI)
            {
                // Aggressive blocking
                Input.ResetInputAxes();

                // Block mouse buttons
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                {
                    // This helps a lot in many games
                    typeof(Input).GetMethod("ResetInputAxes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.Invoke(null, null);
                }
            }
        }

        private void OnGUI()
        {
            if (!showUI) return;

            //// Very aggressive event consumption
            //var e = Event.current;
            //if (e != null && (e.isMouse || e.isKey))
            //{
            //    e.Use();
            //}

            // Also draw a full-screen blocker behind the window
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUIContent.none); // invisible full screen blocker

            windowRect = GUILayout.Window(69420, windowRect, DrawWindow, "ChillieFirst - AFK Skiller");
        }

        private void DrawWindow(int windowID)
        {
            GUILayout.BeginVertical(GUILayout.Width(340));

            GUILayout.Label("🛠 ChillieFirst AFK Skiller", GUILayout.Height(30));

            GUILayout.Space(10);

            GUILayout.Label("AFK Skill:");

            // Individual buttons with disabled state
            DrawSkillButton("None", SkillType.None, 0);
            DrawSkillButton("Mining", SkillType.Mining, 1);
            DrawSkillButton("Dexterity", SkillType.Dexterity, 2);
            DrawSkillButton("Woodcutting", SkillType.Woodcutting, 3);
            DrawSkillButton("Fishing", SkillType.Fishing, 4);
            DrawSkillButton("Thieving", SkillType.Thieving, 5);
            DrawSkillButton("Foraging", SkillType.Foraging, 6);

            GUILayout.Label("Current AFK Skill: " +
                (XPMonitor.Instance != null ? XPMonitor.Instance.selectedSkillType.ToString() : "None"),
                GUILayout.Height(25));

            GUILayout.Label("Current AFK Timer: " +
                (XPMonitor.Instance != null ? $"{XPMonitor.Instance.GetDelay()} seconds" : "None"),
                GUILayout.Height(25));

            GUILayout.Space(15);

            GUILayout.Label("Idle Timer (seconds before AFK skill):");
            timerInput = GUILayout.TextField(timerInput, GUILayout.Width(150));

            GUILayout.Space(20);

            if (GUILayout.Button("✅ Apply Settings", GUILayout.Height(45)))
            {
                ApplySettings();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("❌ Close (Press O)", GUILayout.Height(45)))
            {
                showUI = false;
            }

            GUILayout.EndVertical();

            GUI.DragWindow(new Rect(0, 0, 10000, 30));
        }

        private void DrawSkillButton(string label, SkillType skill, int index)
        {
            if (XPMonitor.Instance == null) return;

            bool isUnlocked = IsSkillUnlocked(skill);
            bool isSelected = XPMonitor.Instance.selectedSkillType == skill;

            // Save original color
            Color originalColor = GUI.color;

            if (isSelected)
            {
                selectedSkillIndex = index;
                GUI.color = new Color(0.2f, 1f, 0.2f, 1f); // Bright Green
                GUILayout.Button($"✅ {label} (ACTIVE)", GUILayout.Height(45));
            }
            else if (isUnlocked)
            {
                GUI.color = Color.white;
                if (GUILayout.Button(label, GUILayout.Height(38)))
                {
                    XPMonitor.Instance.selectedSkillType = skill;
                }
            }
            else
            {
                // Disabled / Locked
                GUI.color = new Color(0.5f, 0.5f, 0.5f, 0.6f); // Grayed out
                GUILayout.Button($"🔒 {label} (Prestige Required)", GUILayout.Height(38));
            }

            // Restore original color
            GUI.color = originalColor;
        }

        private bool IsSkillUnlocked(SkillType skill)
        {
            if (AchievementManager.Instance == null) return false;

            return skill switch
            {
                SkillType.None => true,
                SkillType.Mining => AchievementManager.Instance.miningSkill.GetMiningPrestigeLevel() > 0,
                SkillType.Dexterity => AchievementManager.Instance.dexteritySkill.GetDexterityPrestigeLevel() > 0,
                SkillType.Woodcutting => AchievementManager.Instance.woodcuttingSkill.GetWoodcuttingPrestigeLevel() > 0,
                SkillType.Fishing => AchievementManager.Instance.fishingSkill.GetFishingPrestigeLevel() > 0,
                SkillType.Thieving => AchievementManager.Instance.thievingSkill.GetThievingPrestigeLevel() > 0,
                SkillType.Foraging => AchievementManager.Instance.foragingSkill.GetForagingPrestigeLevel() > 0,
                _ => false
            };
        }

        private void ApplySettings()
        {
            if (XPMonitor.Instance != null)
            {
                SkillType skill = GetSkillTypeFromIndex(selectedSkillIndex);
                XPMonitor.Instance.selectedSkillType = skill;
                Plugin.SelectedSkill.Value = XPMonitor.Instance.selectedSkillType;
                Plugin.Log.LogInfo($"✅ AFK skill changed to: {XPMonitor.Instance.selectedSkillType}");

                if (float.TryParse(timerInput, out float newTimer) && newTimer > 5f && newTimer < 290f)
                {
                    XPMonitor.Instance.SetDelay(newTimer);
                    Plugin.IdleThreshold.Value = newTimer;
                    Plugin.Log.LogInfo($"✅ AFK Timer changed to: {newTimer}s");
                }
            }
        }

        private SkillType GetSkillTypeFromIndex(int index)
        {
            return index switch
            {
                0 => SkillType.None,
                1 => SkillType.Mining,
                2 => SkillType.Dexterity,
                3 => SkillType.Woodcutting,
                4 => SkillType.Fishing,
                5 => SkillType.Thieving,
                6 => SkillType.Foraging,
                _ => SkillType.Dexterity
            };
        }
    }
}