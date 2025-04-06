using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text highScoreText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(LoadMainScene);
        quitButton.onClick.AddListener(QuitGame);

        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.LoadPlayer();
            highScoreText.text = "Top player: " + PlayerData.Instance.HighestScorePlayerName + " : " + PlayerData.Instance.HighestScore;
        }
    }
    
    private void LoadMainScene()
    {
        PlayerData.Instance.PlayerName = nameInputField.text;
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
