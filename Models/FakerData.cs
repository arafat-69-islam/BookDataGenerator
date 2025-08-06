namespace BookDataGenerator.Models
{
    public class FakerData
    {
        public string[] Titles { get; }
        public string[] FirstNames { get; }
        public string[] LastNames { get; }
        public string[] Publishers { get; }
        public string[] ReviewTexts { get; }
        public string[] Companies { get; }

        public FakerData(string language, string country)
        {
            Companies = new[] { "Hyatt Inc", "Abernathy and Sons", "Roob Group", "OBSESSOR LLC", "Lang - Hahn" };
            ReviewTexts = new[] {
                "This book changed my perspective!",
                "Couldn't stop reading until 3 AM.",
                "The author's best work so far.",
                "Perfect for a rainy weekend.",
                "I expected more from the ending."
            };

            Titles = Array.Empty<string>();
            FirstNames = Array.Empty<string>();
            LastNames = Array.Empty<string>();
            Publishers = Array.Empty<string>();

            switch (language)
            {
                case "English":
                    Titles = new[] {
                        "The Silent Echo", "Midnight in New York", "100 Things To Do With Cars",
                        "Beyond the Horizon", "The Last Algorithm", "Stranger in a Strange Land"
                    };
                    FirstNames = new[] { "John", "Jane", "Michael", "Emily", "David", "Sarah" };
                    LastNames = new[] { "Smith", "Doe", "Johnson", "Williams", "Brown", "Lee" };
                    Publishers = new[] { "Penguin Random House", "HarperCollins", "Simon & Schuster" };
                    break;
                case "French":
                    Titles = new[] {
                        "Le Petit Prince", "Marseille et la dame rose", "La Librairie des livres interdits",
                        "Il suffit parfois d'un été", "Le Bleu", "La grand arbre"
                    };
                    FirstNames = new[] { "Jean", "Marie", "Pierre", "Sophie", "Jacques", "Claire" };
                    LastNames = new[] { "Dupont", "Martin", "Bernard", "Petit", "Moreau", "Leroy" };
                    Publishers = new[] { "Éditions Gallimard", "Flammarion", "Albin Michel" };
                    break;
                case "German":
                    Titles = new[] {
                        "Der Alchimist", "Die unendliche Geschichte", "Tintenherz",
                        "Der Vorleser", "Das Parfum", "Herr der Diebe"
                    };
                    FirstNames = new[] { "Hans", "Anna", "Thomas", "Claudia", "Wolfgang", "Greta" };
                    LastNames = new[] { "Müller", "Schmidt", "Schneider", "Fischer", "Weber", "Wagner" };
                    Publishers = new[] { "Suhrkamp", "Fischer Verlag", "Rowohlt" };
                    break;
                case "Spanish":
                    Titles = new[] {
                        "Cien años de soledad", "La sombra del viento", "El tiempo entre costuras",
                        "Don Quijote de la Mancha", "La casa de los espíritus", "Como agua para chocolate"
                    };
                    FirstNames = new[] { "Juan", "María", "Carlos", "Ana", "Luis", "Sofía" };
                    LastNames = new[] { "García", "Rodríguez", "González", "Fernández", "López", "Martínez" };
                    Publishers = new[] { "Alfaguara", "Planeta", "Anagrama" };
                    break;
                case "Japanese":
                    Titles = new[] {
                        "ノルウェイの森", "海辺のカフカ", "1Q84",
                        "容疑者Xの献身", "雪国", "こころ"
                    };
                    FirstNames = new[] { "太郎", "花子", "健太", "さくら", "悠真", "美咲" };
                    LastNames = new[] { "佐藤", "鈴木", "高橋", "田中", "伊藤", "山本" };
                    Publishers = new[] { "講談社", "新潮社", "文藝春秋" };
                    break;
                default:
                    Titles = new[] { "Default Title" };
                    FirstNames = new[] { "Default" };
                    LastNames = new[] { "Author" };
                    Publishers = new[] { "Default Publisher" };
                    break;
            }
        }

        public string GetRandomAuthor(Random random)
        {
            if (FirstNames.Length == 0 || LastNames.Length == 0)
                return "Unknown Author";

            return $"{FirstNames[random.Next(FirstNames.Length)]} {LastNames[random.Next(LastNames.Length)]}";
        }
    }
}
