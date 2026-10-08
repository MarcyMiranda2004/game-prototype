namespace game_prototype.Entity
{
    public class StatModifiers
    {
        public string StatName { get; }
        public int Value { get; }

        public StatModifiers(string statsName, int value)
        {
            StatName = statsName;
            Value = value;
        }

        public override string ToString() => $"{StatName} {(Value >= 0 ? "+" : "")}{Value}";
    }

    public enum SpecialEffectType
    {
        StatusEffect,
        DamageAbsorb,
        DamageReflection,
        DamageReduction,
        DamageIncrease,
        DamageReduction,
        DefenseIncrease,
        DefenseReduction,
        PassiveTrigger,
    }

    public class SpecialEffect
    {
        public string Name { get; }
        public string Description { get; }
        public SpecialEffectType Type { get; }

        public SpecialEffect(string name, string description, SpecialEffectType type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}