namespace daprstate.Models
{
    public enum GameType {None=0,RolePlay,ShootemUp,FlightSimulator};
    public class Game 
    {
        private string name;

        private GameType gameType;
        private GameState gameState;

        private List<string> gameCommands = new List<string>{"FireGun","Health","FireLaser"};

        private bool isGameRunning;

        public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }

        public Game(string name, GameType gameType)
        {   
            this.name = name ?? throw new ArgumentNullException(nameof(name), "Game name is invalid");
            this.gameType = gameType;
            this.gameState = new GameState();
        }

        public string Name { get => name; set => name = value; }
        public GameState GameState { get => gameState; set => gameState = value; }

        public void Simulate()
        {
            var pointGenerator = new Random();
            var gameCommandGenerator = new Random();
            Console.WriteLine("Simulating player...");
            IsGameRunning = true;

            while(IsGameRunning)
            {
                GameState.StartTime = DateTime.UtcNow;
                GameState.GameScore+= pointGenerator.Next(30);

                var nextCommand = gameCommands[gameCommandGenerator.Next(gameCommands.Count)];

                Console.WriteLine($"Player performing game commands {nextCommand}");
            
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            IsGameRunning = false;
        }
    }
}