namespace game_prototype.Entity
{
    public interface ICharacterClass
    {
        string Name { get; }
        int Level { get; protected set; }
        int ExperiencePoint { get; set; }
        int PointForLevel { get; set; }
        string[] Bonus { get; }
        string[] Competences { get; }
        string[] BaseEquipment { get; }
        string[] Skills { get; }
        string Description { get; }

        void UpdatePointForLevels();
        void LevelUp();
        void ControlExperience();
        void CollectExp(int points);
    }

    public abstract class CharacterClassAbs : ICharacterClass
    {
        public string Name { get; }
        public int Level { get; set; }
        public int ExperiencePoint { get; set; }
        public int PointForLevel { get; set; }
        public string[] Bonus { get; }
        public string[] Competences { get; }
        public string[] BaseEquipment { get; }
        public string[] Skills { get; }
        public string Description { get; }

        protected CharacterClassAbs(string name, int startingLevel, int startingPointsForLevel)
        {
            Name = name;
            Level = startingLevel;
            PointForLevel = startingPointsForLevel;
            ExperiencePoint = 0;
        }

        public void UpdatePointForLevels()
        {
            double calculated = PointForLevel * 1.35;
            PointForLevel = (int)Math.Round(calculated, MidpointRounding.AwayFromZero);
        }

        public void LevelUp()
        {
            Level += 1;
            UpdatePointForLevels();
        }

        public void ControlExperience()
        {
            while (ExperiencePoint >= PointForLevel) {
                LevelUp();
            }
        }

        public void CollectExp(int points)
        {
            ExperiencePoint += points;
            ControlExperience();
        }
    }
    
    public class CharacterClass : CharacterClassAbs
    {
        public CharacterClass(string name, int level, int pointForLevel) : base(name, level, pointForLevel) {}
    }   

    public class Warrior : CharacterClass
    {
        public Warrior(): base("Warrior", 1, 100)
        {
            ExperiencePoint = 0;
            Bonus = ["+2 Strength"];
            Competences = ["Heavy Weapon", "Normal Weapon", "Light Weapon", "Normal Armor", "Light Armor"];
            BaseEquipment = ["Long Sword", "Short Bow", "Knife", "Light Armor", "Backpack", "200 Gold Coin"];
            Skills = ["Extra Attack", "Stamina Supply", "Temperance"];
            Description = "The consummate master of arms, trained for years in the use of a wide array of weapons and in combat techniques to successfully subdue the enemy.";
        }
    }
}