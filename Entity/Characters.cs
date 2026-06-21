namespace game_prototype.Entity
{
    public interface ICharacters
    {
        string Name { get; set; }
        IClasses Class { get; set; }
        int Level { get; set; }
    }
}

