namespace Puzzle.Events
{
    public class EventService
    {
        public EventController OnTurnInitiated;
        public EventController OnTurnCompleted;
        public EventController<int> OnLevelSelected;
        public EventController OnGameWon;
        public EventController OnGameOver;

        public EventService()
        {
            OnTurnInitiated = new EventController();
            OnTurnCompleted = new EventController();
            OnLevelSelected = new EventController<int>();
            OnGameWon = new EventController();
            OnGameOver = new EventController();
        }
    }
}