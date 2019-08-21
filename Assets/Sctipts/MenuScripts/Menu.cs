using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   [SerializeField] InputField nameField;
   [SerializeField] GameObject levels;
   [SerializeField] GameObject menuPanel;
   [SerializeField] GameObject optionPanel;
   [SerializeField] GameObject soundToggle;

    private bool menuSoundOn;


    private void Start() {
        
        if (PlayerPrefs.HasKey("PlayerName")) {
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }

#region MenuSound

        menuSoundOn = PlayerPrefs.GetInt("MenuSound") == 1 ? true : false;
        AudioManager.Instance.Play("MenuTheme");
        if (!menuSoundOn)
            AudioListener.volume = 0;

#endregion

    }

    private void Update() {
        // отключение звука
        menuSoundOn = soundToggle.GetComponent<Toggle>().isOn;

        if(menuSoundOn)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;

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
        if (optionPanel.active) {
            menuPanel.SetActive(true);
            optionPanel.SetActive(false);
        } else if (levels.active) {
            menuPanel.SetActive(true);
            levels.SetActive(false);
        }
    }

    public void OnClickOption() {
        optionPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void SaveLocalization(string localization) {
        PlayerPrefs.SetString("localization", localization);
    }

    #region selectLevel
    public void Level(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
   
    #endregion
}
