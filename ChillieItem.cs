using System.Collections.Generic;
using UnityEngine;

namespace ChillieMod
{
    public class ChillieItem(SkillType skillType, string name, int requiredLevel)
    {

        private readonly string _name = name;
        private readonly int _requiredLevel = requiredLevel;
        private readonly SkillType _skillType = skillType;
        private Sprite _icon = null;

        public void SetIcon(Sprite icon)
        {
            _icon = icon;
        }

        public Sprite GetIcon()
        {
            return _icon;
        }

        public SkillType GetSkillType()
        {
            return _skillType;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetRequiredLevel()
        {
            return _requiredLevel;
        }

        public static List<ChillieItem> CreateAllItems()
        {
            return CreateAllForagingItems(); 
        }

        private static List<ChillieItem> CreateAllForagingItems()
        {
            List<ChillieItem> list = new List<ChillieItem>();
            list.Add(new ChillieItem(SkillType.Foraging, "Bloobberries", 1));
            list.Add(new ChillieItem(SkillType.Foraging, "Worm", 2));
            list.Add(new ChillieItem(SkillType.Foraging, "Papyrus Fibers", 3));
            list.Add(new ChillieItem(SkillType.Foraging, "Scarlet Sporecap", 5));
            list.Add(new ChillieItem(SkillType.Foraging, "Cotton", 6));
            list.Add(new ChillieItem(SkillType.Foraging, "Imbued Glory", 8));
            list.Add(new ChillieItem(SkillType.Foraging, "Red Bloobberries", 10));
            list.Add(new ChillieItem(SkillType.Foraging, "Meadow Cress", 13));
            list.Add(new ChillieItem(SkillType.Foraging, "Azure Starshroom", 15));
            list.Add(new ChillieItem(SkillType.Foraging, "Sunsnap", 18));
            list.Add(new ChillieItem(SkillType.Foraging, "Orange Bloobberries", 20));
            list.Add(new ChillieItem(SkillType.Foraging, "Golden Sunfungus", 25));
            list.Add(new ChillieItem(SkillType.Foraging, "Emerald Glory", 28));
            list.Add(new ChillieItem(SkillType.Foraging, "Yellow Bloobberries", 30));
            list.Add(new ChillieItem(SkillType.Foraging, "Twilight Orchidshroom", 35));
            list.Add(new ChillieItem(SkillType.Foraging, "Thornfoot", 38));
            list.Add(new ChillieItem(SkillType.Foraging, "White Bloobberries", 40));
            list.Add(new ChillieItem(SkillType.Foraging, "Clover", 44));
            list.Add(new ChillieItem(SkillType.Foraging, "Golden Chestnutcap", 45));
            list.Add(new ChillieItem(SkillType.Foraging, "Softeye", 48));
            list.Add(new ChillieItem(SkillType.Foraging, "Purple Bloobberries", 50));
            list.Add(new ChillieItem(SkillType.Foraging, "Amethyst Glowstalk", 55));
            list.Add(new ChillieItem(SkillType.Foraging, "Yellow Comb", 58));
            list.Add(new ChillieItem(SkillType.Foraging, "Grey Bloobberries", 60));
            list.Add(new ChillieItem(SkillType.Foraging, "Remire Fungus", 65));
            list.Add(new ChillieItem(SkillType.Foraging, "Bloodmoss", 68));
            list.Add(new ChillieItem(SkillType.Foraging, "Teal Bloobberries", 70));
            list.Add(new ChillieItem(SkillType.Foraging, "Leafshade Shroom", 75));
            list.Add(new ChillieItem(SkillType.Foraging, "Starshade", 78));
            list.Add(new ChillieItem(SkillType.Foraging, "Bluelume Spore", 85));
            list.Add(new ChillieItem(SkillType.Foraging, "Emberplume", 88));
            list.Add(new ChillieItem(SkillType.Foraging, "Void Spore", 95));
            list.Add(new ChillieItem(SkillType.Foraging, "Witch Moss", 98));
            list.Add(new ChillieItem(SkillType.Foraging, "Frosted Bloobberries", 105));
            list.Add(new ChillieItem(SkillType.Foraging, "Frost Worm", 115));
            list.Add(new ChillieItem(SkillType.Foraging, "Hoarfrost Mint", 120));
            list.Add(new ChillieItem(SkillType.Foraging, "Frost Ear", 140));
            list.Add(new ChillieItem(SkillType.Foraging, "Coldsnap", 160));
            list.Add(new ChillieItem(SkillType.Foraging, "Winter Oyster", 180));
            list.Add(new ChillieItem(SkillType.Foraging, "Amberleaf", 210));
            list.Add(new ChillieItem(SkillType.Foraging, "Sand Worm", 215));
            list.Add(new ChillieItem(SkillType.Foraging, "Prickle Bloobberries", 225));
            list.Add(new ChillieItem(SkillType.Foraging, "Palmshade Spore", 234));
            list.Add(new ChillieItem(SkillType.Foraging, "Sunleaf", 255));
            list.Add(new ChillieItem(SkillType.Foraging, "Dewglow Spore", 282));

            return list;
        }
    }
}
