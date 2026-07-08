
namespace game_prototype.Entity
{
    #region STRUCT HELPERS
    // classe per la definizione e gestione dei punti extra
    public class ExtraModifier
    {
        public int Points { get; set; } // punti da applicare
        public string Source { get; } // fonte dei punti (es. amuleto, anello, pozione)

        // metodo strutturale per il settaggio dei punti  
        public ExtraModifier(int points, string source)
        {
            Points = points;
            Source = source;
        }
    }

    // classe per la definizione e gestione dei punti temporanei, il funzionamento si basa su quelli extra ma aggiunge un limite temporale (durata)
    public class TempModifier : ExtraModifier
    {
        public int Duration { get; set; } // aggiunta durata dei punti 

        // metodo per il settaggio dei punti  
        public TempModifier(int points, string source, int duration) : base(points, source)
        {
            Duration = duration;
        }

        public bool IsExpired => Duration <= 0;
    }
    #endregion

    #region STATS FORM
    // Interfaccia base di una statistica
    public interface IStat
    {
        string Name { get; } // nome della statistica
        int BasePoints { get; set; } // punti fissi della statistica
        int ExtraPoints { get; } // punti extra dati tramite Buff o Debuff
        int TempPoints { get; } // ulteriori punti extra ma con durata temporanea
        int TotalPoints { get; } // somma totale dei punti applicati

        void AddBasePoints(int points); // permette l'aggiunta di punti Base
        void RemoveBasePoints(int points); // permette l'aggiunta di punti Base

        void AddExtraPoint(int points, string source); // permette l'aggiunta di punti Extra
        void RemoveExtraPoint(string source); // permette rimozione di punti Extra

        void AddTempPoints(int points, string source, int duration); // permette l'aggiunta di punti Temporanei 
        void RemoveTempPoints(int points, string source, int duration); // permette rimozione di punti Temporanei utile in caso di rimozione forzata del modificatore

        void UpdateTurnStat(); // serve per gestire lo scalare del tempo delle punti temporanei delle stats
    }

    // classe astratta per la definizione avanzata 
    public abstract class StatAbs : IStat
    {
        public string Name { get; }
        public int BasePoints { get; set; }

        private readonly Dictionary<string, ExtraModifier> _extraModifiers = new(); // dizionario con tutti gli eventuali punti extra
        private readonly Dictionary<string, TempModifier> _tempModifiers = new(); // dizionario con tutti gli eventuali punti temporanei

        public int ExtraPoints => _extraModifiers.Sum(m => m.Value.Points); // totale dei punti extra
        public int TempPoints => _tempModifiers.Sum(m => m.Value.Points); // totale dei punti temporanei 
        public int TotalPoints => BasePoints + ExtraPoints + TempPoints; // totale di tutti i punti

        public void AddBasePoints(int points)
        {
            BasePoints += points;
        }

        public void RemoveBasePoints(int points)
        {
            BasePoints -= points;
        }

        // se il dizionario contiene gia quella statistica collegata a quella sorgente, setta nuovamente il modificatore, altrimenti lo aggiunge exnovo 
        public void AddExtraPoint(int points, string source)
        {
            if (_extraModifiers.ContainsKey(source))
            {
                // cerca l'elemento nel dizionario tramite a key [source] e modifica i punti
                _extraModifiers[source].Points = points;
            }
            else
            {
                // crea un nuovo elemento nel dizionario con key [source] e value [ExtraModifier] (instance)
                _extraModifiers.Add(source, new ExtraModifier(points, source));
            }
        }

        // cerca nel dizionario la sorgente e rimuove il modificatore collegato
        public void RemoveExtraPoint(string source)
        {
            _extraModifiers.Remove(source);
        }

        // se l'effetto esiste giá, esso viene resettato se invece non é ancora presente esso viene aggiunto exnovo
        public void AddTempPoints(int points, string source, int duration)
        {
            if (_tempModifiers.ContainsKey(source))
            {
                _tempModifiers[source].Points = points;
                _tempModifiers[source].Duration = duration;
            }
            else
            {
                _tempModifiers.Add(source, new TempModifier(points, source, duration));
            }
        }

        public void RemoveTempPoints(int points, string source, int duration)
        {
            _tempModifiers.Remove(source);
        }

        public void UpdateTurnStat()
        {
            // Lista di supporto per segnare le chiavi da rimuovere (non si può modificare un dizionario mentre ci si cicla dentro)
            List<string> expiredKeys = new();

            foreach (var kvp in _tempModifiers) // kvp -> key value pair (coppia chiave valore)
            {
                kvp.Value.Duration--;

                if (kvp.Value.IsExpired)
                {
                    expiredKeys.Add(kvp.Key);
                }
            }

            // Rimuoviamo gli effetti scaduti tramite la loro chiave
            foreach (var key in expiredKeys)
            {
                _tempModifiers.Remove(key);
            }
        }
    }

    public class Stat : IStat
    {
        public string Name { get; }
        public int BasePoints { get; set; }

        private readonly Dictionary<string, ExtraModifier> _extraModifiers = new();
        private readonly Dictionary<string, TempModifier> _tempModifiers = new();

        public int ExtraPoints => _extraModifiers.Sum(m => m.Value.Points);
        public int TempPoints => _tempModifiers.Sum(m => m.Value.Points);
        public int TotalPoints => BasePoints + ExtraPoints + TempPoints;

        // structure method
        public Stat(string name, int basePoints = 0)
        {
            Name = name;
            BasePoints = basePoints;
        }

        public void AddBasePoints(int points)
        {
            BasePoints += points;
        }

        public void RemoveBasePoints(int points)
        {
            BasePoints -= points;
        }

        public void AddExtraPoint(int points, string source)
        {
            if (_extraModifiers.ContainsKey(source))
            {
                _extraModifiers[source].Points = points;
            } else
            {
                _extraModifiers.Add(source, new ExtraModifier(points, source));
            }
        }

        public void RemoveExtraPoint(string source)
        {
            _extraModifiers.Remove(source);
        }

        public void AddTempPoints(int points, string source, int duration)
        {
            if (_tempModifiers.ContainsKey(source))
            {
                _tempModifiers[source].Points = points;
                _tempModifiers[source].Duration = duration;
            }
            else
            {
                _tempModifiers.Add(source, new TempModifier(points, source, duration));
            }
        }

        public void RemoveTempPoints(int points, string source, int duration)
        {
            _tempModifiers.Remove(source);
        }

        public void UpdateTurnStat()
        {
            List<string> expiredKeys = new();

            foreach (var kvp in _tempModifiers)
            {
                kvp.Value.Duration--;

                if (kvp.Value.IsExpired)
                {
                    expiredKeys.Add(kvp.Key);
                }
            }

            foreach (var key in expiredKeys)
            {
                _tempModifiers.Remove(key);
            }
        }
    }

    public class Stats
    {
        public Stat Health { get; } // punti vita
        public Stat Magic { get; } // punti magia

        public Stat Strength { get; } // forza fisica
        public Stat Dexterity { get; } // destrezza fisica
        public Stat Constitution { get; } // costituzione corporea e resistenza

        public Stat Inteligence { get; }
        public Stat Faith { get; }
        public Stat Wisdom { get; }
        
        public Stat Charisma { get; }
        public Stat Luck { get; }
    }
    #endregion
}