using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private string startSceneName;

    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(startSceneName, LoadSceneMode.Additive);
        asyncLoad.completed += OnStartGame;
    }

    private void OnStartGame(AsyncOperation loadOperation)
    {
        Debug.Log("Loaded start scene!");

        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
