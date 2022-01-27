namespace  daprstate.Models
{


    public class GameState {
        private DateTime startTime;
        private DateTime endTime;
        private int gameScore;

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public int GameScore { get => gameScore; set => gameScore = value; }
        
    }
    
}