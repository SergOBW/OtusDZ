using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IFinishGameListener, IMainMenuListener
{
    [SerializeField] private GameObject blurGameObject;

    [SerializeField] private Button startGameButton;
    [SerializeField] private Button pauseGameButton;
    [SerializeField] private Button resumeGameButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text gameFinishedText;

    [SerializeField]private int gameStartTimer;
    private float _timer;
    private bool _isTimerStarted;

    private void Awake()
    {
        startGameButton.onClick.AddListener(StartTimer);
        pauseGameButton.onClick.AddListener(PauseGame);
        resumeGameButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(OnMainMenuButton);
    }

    public void Update()
    {
        if (_timer >= 0 && _isTimerStarted)
        {
            _timer -= Time.deltaTime;
        } else if (_timer <= 0 && _isTimerStarted)
        {
            StartGame();
        }
        
        int seconds = Mathf.FloorToInt(_timer);
        countdownText.text = $"Game starts in : {seconds}";
    }
    private void ShowMainMenu()
    {
        blurGameObject.gameObject.SetActive(true);
        gameFinishedText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        
        startGameButton.gameObject.SetActive(true);
        resumeGameButton.gameObject.SetActive(false);
        pauseGameButton.gameObject.SetActive(false);
    }
    private void StartTimer()
    {
        _timer = gameStartTimer;
        _isTimerStarted = true;
        
        countdownText.gameObject.SetActive(true);
    }
    private void StartGame()
    {
        _isTimerStarted = false;
        
        blurGameObject.gameObject.SetActive(false);
        gameFinishedText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        
        startGameButton.gameObject.SetActive(false);
        resumeGameButton.gameObject.SetActive(false);
        pauseGameButton.gameObject.SetActive(true);
        
        GameStateController.Instance.StartGame();
    }
    private void PauseGame()
    {
        blurGameObject.gameObject.SetActive(true);
        
        startGameButton.gameObject.SetActive(false);
        pauseGameButton.gameObject.SetActive(false);
        resumeGameButton.gameObject.SetActive(true);
        
        GameStateController.Instance.PauseGame();
    }
    private void ResumeGame()
    {
        blurGameObject.gameObject.SetActive(false);
        
        startGameButton.gameObject.SetActive(false);
        resumeGameButton.gameObject.SetActive(false);
        pauseGameButton.gameObject.SetActive(true);
        
        GameStateController.Instance.ResumeGame();
    }

    private void OnMainMenuButton()
    {
        GameStateController.Instance.MainMenu();
    }

    public void OnFinishGame()
    {
        gameFinishedText.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    public void OnMainMenu()
    {
        ShowMainMenu();
    }
}
