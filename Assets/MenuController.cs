using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IFinishGameListener
{
    [SerializeField] private GameObject blurGameObject;

    [SerializeField] private Button startGameButton;
    [SerializeField] private Button pauseGameButton;
    [SerializeField] private Button resumeGameButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        pauseGameButton.onClick.AddListener(PauseGame);
        resumeGameButton.onClick.AddListener(ResumeGame);

        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        blurGameObject.gameObject.SetActive(true);
        
        startGameButton.gameObject.SetActive(true);
        resumeGameButton.gameObject.SetActive(false);
        pauseGameButton.gameObject.SetActive(false);
    }
    private void StartGame()
    {
        blurGameObject.gameObject.SetActive(false);
        
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

    public void OnFinishGame()
    {
        ShowMainMenu();
    }
}
