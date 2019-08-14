using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   [SerializeField] InputField nameField;
   [SerializeField] GameObject levels;
   [SerializeField] GameObject menuPanel;


    private void Start() {
        if (PlayerPrefs.HasKey("PlayerName")) {
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }

        AudioManager.Instance.Play("MenuTheme");
    }

    public void OnEndEditName() {
        PlayerPrefs.SetString("PlayerName", nameField.text);
    }


   public void OnClickPlay() {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit() {
        Application.Quit();
    }

    public void OnClickSelectLevel() {
        menuPanel.SetActive(false);
        levels.SetActive(true);
    }

    public void OnClickBack() {
        menuPanel.SetActive(true);
        levels.SetActive(false);
    }

    #region selectLevel
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
    #endregion
}
