using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChillieFirst
{
    public class ListManagerUI : MonoBehaviour
    {
        public static ListManagerUI Instance { get; private set; }

        private bool showUI = false;
        private Rect windowRect = new Rect(400, 100, 560, 520);

        private string searchText = "";
        private Vector2 scrollPosition = Vector2.zero;

        private ChillieItem selectedItem = null;

        public List<ChillieItem> AllItems { get; private set; } = new List<ChillieItem>();

        public static ListManagerUI Create()
        {
            if (Instance != null) return Instance;

            var go = new GameObject("ListManagerUI");
            DontDestroyOnLoad(go);
            Instance = go.AddComponent<ListManagerUI>();
            List<ChillieItem> allChillieItems = ChillieItem.CreateAllItems();
            List<Item> allGameItems = Item.GetAllItems();

            foreach (ChillieItem item in allChillieItems)
            {
                foreach (Item gameItem in allGameItems)
                {
                    if(item.GetName() == gameItem.itemName)
                    {
                        item.SetTexture(gameItem.commonImage.texture);
                        //item.SetIcon(gameItem.commonImage);
                    }
                }
            }


            Instance.AllItems.AddRange(allChillieItems);

            Plugin.Log.LogInfo("✅ ListManagerUI created");
            return Instance;
        }

        public bool IsShowing()
        {
            return showUI;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) // Press K to toggle
            {
                showUI = !showUI;
            }
        }

        private void OnGUI()
        {
            if (!showUI) return;

            //Event.current.Use();
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUIContent.none);

            windowRect = GUILayout.Window(69421, windowRect, DrawWindow, "ChillieFirst - Item Manager");
        }

        private void DrawWindow(int windowID)
        {
            GUILayout.BeginVertical(GUILayout.Width(530));

            GUILayout.Label("📋 Item Manager", GUILayout.Height(30));

            // Search
            GUILayout.Label("Search:");
            searchText = GUILayout.TextField(searchText, GUILayout.Height(25));

            GUILayout.Space(8);

            // Scrollable List
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(380));

            var filtered = GetFilteredItems();

            foreach (var item in filtered)
            {
                DrawItemRow(item);
            }

            GUILayout.EndScrollView();

            GUILayout.Space(10);

            // Selected Item Info
            if (selectedItem != null)
            {
                GUILayout.Label($"Selected → {selectedItem.GetName()} (Lvl {selectedItem.GetRequiredLevel()})");

                if (GUILayout.Button("✅ Use This Item / Start Action", GUILayout.Height(45)))
                {
                    OnItemSelected(selectedItem);
                }
            }
            else
            {
                GUILayout.Label("No item selected.");
            }

            GUILayout.Space(8);

            if (GUILayout.Button("❌ Close (Press K)", GUILayout.Height(40)))
            {
                showUI = false;
            }

            GUILayout.EndVertical();

            GUI.DragWindow(new Rect(0, 0, 10000, 30));
        }

        private void DrawItemRow(ChillieItem item)
        {
            bool isSelected = selectedItem == item;

            Color originalColor = GUI.color;
            if (isSelected)
                GUI.color = new Color(0.2f, 1f, 0.2f, 1f); // Bright green

            GUILayout.BeginHorizontal(GUILayout.Height(38));

            Texture2D texture = item.GetTeture();
            if (texture != null)
            {
                GUILayout.Label(texture, GUILayout.Width(32), GUILayout.Height(32));
            }
            else
            {
                GUILayout.Space(34); // Reserve space so alignment stays consistent
            }

            // Required Level (fixed width for alignment)
            GUILayout.Label($"Lv.{item.GetRequiredLevel(),-3}", GUILayout.Width(55));

            // Item Name (remaining space)
            string displayName = item.GetName();
            if (GUILayout.Button(displayName, GUILayout.ExpandWidth(true), GUILayout.Height(35)))
            {
                selectedItem = item;
            }

            GUILayout.EndHorizontal();

            GUI.color = originalColor;
        }

        private List<ChillieItem> GetFilteredItems()
        {
            return AllItems.Where(item =>
            {
                if (string.IsNullOrEmpty(searchText))
                    return true;

                return item.GetName().ToLower().Contains(searchText.ToLower());
            }).ToList();
        }

        private void OnItemSelected(ChillieItem item)
        {
            Plugin.Log.LogInfo($"✅ Item Selected: {item.GetName()} (Lvl {item.GetRequiredLevel()})");
            ChillieHelper.SkillActionRequest(item);
        }
    }
}