namespace ShootEmUp
{
    public sealed class GameManager : IStartGameListener, IFinishGameListener
    {
        private GameStateController _gameStateController;
        private EventBus _eventBus;

        public GameManager(GameStateController gameStateController, EventBus eventBus)
        {
            _gameStateController = gameStateController;
            _eventBus = eventBus;
        }
        
        public void OnStartGame()
        {
            _eventBus.Subscribe<PlayerDiedEvent>(FinishGame);
        }
        
        public void OnFinishGame()
        {
            _eventBus.Unsubscribe<PlayerDiedEvent>(FinishGame);
        }

        private void FinishGame(PlayerDiedEvent eventdata)
        {
            _gameStateController.FinishGame();
        }

    }
}