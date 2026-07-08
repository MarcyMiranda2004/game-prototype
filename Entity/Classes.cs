namespace game_prototype.Entity
{
    public interface IClass
    {
        string Name { get; }
        int Level { get; }
        string Bonus { get; }
        string Competences { get; }
        string BaseEquipment { get; }
        string Skills { get; }
    }
}