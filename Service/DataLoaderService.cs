namespace BookDataGenerator.Services
{
    public class DataLoaderService
    {
        private readonly Dictionary<string, List<string>> _titles = new()
        {
            { "en-US", new List<string> { "The Silent Forest", "Lost Horizons", "Infinite Dreams", "Shadow of the Wind", "The Last Kingdom", "Echoes of the Past", "Midnight Sun", "Broken Wings", "Hidden Truths", "Whispering Shadows" } },
            { "de-DE", new List<string> { "Der stille Wald", "Verlorene Horizonte", "Unendliche Träume", "Schatten des Windes", "Das letzte Königreich", "Echos der Vergangenheit", "Mitternachtssonne", "Gebrochene Flügel", "Verborgene Wahrheiten", "Flüsternde Schatten" } },
            { "ja-JP", new List<string> { "静かな森", "失われた地平線", "無限の夢", "風の影", "最後の王国", "過去の残響", "真夜中の太陽", "折れた翼", "隠された真実", "ささやく影" } }
        };

        private readonly Dictionary<string, List<string>> _authors = new()
        {
            { "en-US", new List<string> { "John Smith", "Emily Clark", "Michael Brown", "Sarah Johnson", "David Lee", "Jessica Miller", "Robert Wilson", "Laura Davis", "James Moore", "Linda Taylor" } },
            { "de-DE", new List<string> { "Hans Müller", "Anna Schmidt", "Peter Weber", "Julia Fischer", "Thomas Becker", "Sabine Wagner", "Klaus Hoffmann", "Monika Schäfer", "Wolfgang Richter", "Lisa Neumann" } },
            { "ja-JP", new List<string> { "佐藤健", "鈴木一郎", "高橋美咲", "田中太郎", "伊藤花子", "渡辺直美", "山本健太", "中村優子", "小林誠", "加藤真由美" } }
        };

        private readonly Dictionary<string, List<string>> _publishers = new()
        {
            { "en-US", new List<string> { "Penguin Books", "HarperCollins", "Simon & Schuster", "Random House", "Macmillan", "Hachette", "Scholastic", "Oxford Press", "Crown Publishing", "Vintage Books" } },
            { "de-DE", new List<string> { "Verlag Fischer", "Bertelsmann", "Rowohlt", "Suhrkamp", "Hanser", "dtv", "Goldmann", "Heyne", "Ullstein", "Klett-Cotta" } },
            { "ja-JP", new List<string> { "講談社", "集英社", "小学館", "角川書店", "新潮社", "文藝春秋", "光文社", "徳間書店", "河出書房新社", "中央公論新社" } }
        };

        public List<string> GetTitles(string language)
        {
            if (_titles.ContainsKey(language)) return _titles[language];
            return _titles["en-US"];
        }

        public List<string> GetAuthors(string language)
        {
            if (_authors.ContainsKey(language)) return _authors[language];
            return _authors["en-US"];
        }

        public List<string> GetPublishers(string language)
        {
            if (_publishers.ContainsKey(language)) return _publishers[language];
            return _publishers["en-US"];
        }
    }
}
