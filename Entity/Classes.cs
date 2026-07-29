namespace game_prototype.Entity
{
    public interface IClass
    {
        string Name { get; }
        int Level { get; }
        int ExperiencePoint { get; set; }
        int PointForLevel { get; set; }
        string Bonus { get; }
        string Competences { get; }
        string BaseEquipment { get; }
        string Skills { get; }
        string Description { get; }

        void UpdatePointForLevels();
        void LevelUp();
        void ControlExperience();
        void CollectExp(int points);
    }

    public abstract class ClassAbs : IClass
    {
        public string Name { get; }
        public int Level { get; set; }
        public int ExperiencePoint { get; set; }
        public int PointForLevel { get; set; }
        public string Bonus { get; }
        public string Competences { get; }
        public string BaseEquipment { get; }
        public string Skills { get; }
        public string Description { get; }

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
            while (ExperiencePoint >= PointForLevel) LevelUp();
        }

        public void CollectExp(int points)
        {
            ExperiencePoint += points;
            ControlExperience();
        }
    }
}