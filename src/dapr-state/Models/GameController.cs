namespace daprstate.Models
{
    using Dapr.Client;
    public class GameController 
    {
        private readonly Game game;
        private readonly DaprClient daprClient;
        
        public GameController(Game game){
            this.game  = game;
            this.daprClient = new DaprClientBuilder().Build();
        }

        public void Start(){
            game.GameState.GameScore = GetLastScore();
            Console.WriteLine($"Starting game {game.Name}");
            game.Simulate();
            Console.WriteLine($"Game is running: {game.IsGameRunning}");
        }
        public int GetLastScore(){
              return daprClient.GetStateAsync<int>("statestore","currentGame").GetAwaiter().GetResult();
        }
        public void End() {
    
            Console.WriteLine("Saving game state..");
            daprClient.SaveStateAsync("statestore","currentGame",game.GameState.GameScore).GetAwaiter().GetResult();
            game.Stop();
        }   
    }

}