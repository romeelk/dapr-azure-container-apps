namespace daprstate.Models
{
    using Dapr.Client;

    public class GameController 
    {
        private readonly Game game;
        private readonly DaprClient daprClient;
        
        public GameController(Game game)
        {
            this.game  = game ?? throw new ArgumentNullException(nameof(game), "Game is null");
            this.daprClient = new DaprClientBuilder().Build();
        }

        public void Start()
        {
            Console.WriteLine($"Starting game {game.Name}");
            game.Simulate();
            Console.WriteLine($"Game is running: {game.IsGameRunning}");
        }
        public Game GetGameState()
        {
            var gameState = daprClient.GetStateAsync<Game>("statestore","currentGame").GetAwaiter().GetResult();
            return gameState;
        }
        public void End() 
        {
            game.Stop();
            Console.WriteLine("Saving game state..");
            daprClient.SaveStateAsync<Game>("statestore","currentGame",game).GetAwaiter().GetResult();
        }   
    }

}