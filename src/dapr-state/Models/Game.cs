using Newtonsoft.Json;

namespace daprstate.Models
{
    public enum GameType { None = 0, RolePlay, ShootemUp, FlightSimulator };
    public class Game
    {
        private string _name;
        private GameType _gameType;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _gameScore;
        private List<string> gameCommands = new List<string> { "FireGun", "Health", "FireLaser" };

        private bool isGameRunning;

        [JsonIgnore]
        public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
        private readonly object gameStateLock = new object();

        public string Name { get => _name; set => _name = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }
        public int GameScore { get => _gameScore; set => _gameScore = value; }
        public GameType GameType { get => _gameType; set => _gameType = value; }

        public Game(string name, GameType gameType, DateTime startTime = default, DateTime endTime = default, int gameScore = default(int))
        {
            if(gameScore <0)
            {
                throw new ArgumentException("Gamescore must be non-zero integer", nameof(gameScore));
            }

            _name = name ?? throw new ArgumentNullException(nameof(name), "Game name is invalid");
            GameType = gameType;
            _startTime = startTime;
            _endTime = endTime;
            _gameScore = gameScore;
        }

        public void Simulate()
        {
            var pointGenerator = new Random();
            var gameCommandGenerator = new Random();
            Console.WriteLine("Simulating player...");
            IsGameRunning = true;
            StartTime = DateTime.UtcNow;

            while (IsGameRunning)
            {
                lock (gameStateLock)
                {
                    GameScore += pointGenerator.Next(30);
                }
                var nextCommand = gameCommands[gameCommandGenerator.Next(gameCommands.Count)];

                Console.WriteLine($"Player performing game commands {nextCommand}");

                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            IsGameRunning = false;
            EndTime = DateTime.UtcNow;
        }
    }
}