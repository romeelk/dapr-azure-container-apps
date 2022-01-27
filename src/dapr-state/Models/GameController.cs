namespace daprstate.Models
{
    using Dapr.Client;
    public class GameController 
    {
        private readonly Game game;
        private readonly DaprClient daprClient;

        private bool isGameRunning;

        public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }

        public GameController(Game game){
            this.game  = game;
            this.daprClient = new DaprClientBuilder().Build();
        }

        public void Start(){
            Console.WriteLine($"Starting game {game.Name}");
            game.Simulate();
            IsGameRunning = true;
        }
        public void Pause(){
       
        }
        public void End() {
            Console.WriteLine("Are you sure you want to stop the game? Hit enter to confirm");
            var keyInfo = Console.ReadKey();

            if(keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Saving game state..");
                daprClient.SaveStateAsync<GameState>("gameState","currentGame",game.GameState).GetAwaiter().GetResult();
                IsGameRunning = false;
            }
        }
    }

}