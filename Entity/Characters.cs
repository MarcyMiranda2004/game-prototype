namespace game_prototype.Entity
{
    public interface ICharacters
    {
        string Name { get; set; }
        string Class { get; set; }
        Stats Stats { get; set; }
        int Level { get; set; }
        
    }
}

