using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button normalButton;
    public Button advancedButton;
    public Button quitButton;

    public static string difficulty;

    void Start()
    {
        normalButton.onClick.AddListener(SetNormalDifficulty);
        advancedButton.onClick.AddListener(SetAdvancedDifficulty);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void SetNormalDifficulty()
    {
        difficulty = "normal";
        LoadGame();
    }

    private void SetAdvancedDifficulty()
    {
        difficulty = "avancado";
        LoadGame();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Hall");
    }

    private void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
