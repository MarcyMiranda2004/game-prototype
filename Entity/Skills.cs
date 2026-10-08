namespace game_prototype.Entity
{
    public interface Skill
    {
        string Name { get; }
        string Description { get; }

        IReadOnlyList<StatModifiers> Modifiers { get; }
        IReadOnlyList<SpecialEffect> SpecialEffect { get; }

        public Skill(
            string name,
            string description,
            List<StatModifiers> modifiers,
            List<SpecialEffect> specialEffect
        )
        {
            Name = name;
            Description = description;
            Modifiers = modifiers?.AsReadOnly() ?? new List<StatModifiers>().AsReadOnly();
            SpecialEffects = specialEffects?.AsReadOnly() ?? new List<SpecialEffect>().AsReadOnly();
        }
        
        public bool HasModifiers => Modifiers.Count > 0;
        public bool HasSpecialEffects => SpecialEffects.Count > 0;
    }
}