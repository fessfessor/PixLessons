using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
   [SerializeField] InputField nameField;
   [SerializeField] GameObject levels;
   [SerializeField] GameObject menuPanel;
   [SerializeField] GameObject optionPanel;
   [SerializeField] GameObject soundToggle;
   [SerializeField] GameObject languageDropdown;
   [SerializeField] GameObject jumpButtonToggle;
   [SerializeField] GameObject volumeSlider;

    public AudioMixer audioMixer;

    private bool menuSoundOn;


    private void Start() {
        
        if (PlayerPrefs.HasKey("PlayerName")) {
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }

        SetLanguageDropdown();

        #region MenuSound

        //menuSoundOn = PlayerPrefs.GetInt("MenuSound") == 1 ? true : false;
       // AudioManager.Instance.Play("MenuTheme");
        //if (!menuSoundOn)
        //    AudioListener.volume = 0;

        #endregion

        #region JumpButton
        if (PlayerPrefs.HasKey("jumpButton")) {
            if (PlayerPrefs.GetInt("jumpButton") == 0) {
                jumpButtonToggle.GetComponent<Toggle>().isOn = false;
            }
            else if(PlayerPrefs.GetInt("jumpButton") == 1) {
                jumpButtonToggle.GetComponent<Toggle>().isOn = true;
            }
            
        }

        #endregion

        #region volumeSlider
        if (PlayerPrefs.HasKey("volume")) {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
            Debug.Log(AudioListener.volume);
        } else
            AudioListener.volume = 1f;
        #endregion

    }

    private void Update() {
        // отключение звука
        menuSoundOn = soundToggle.GetComponent<Toggle>().isOn;

        //if(menuSoundOn)
        //    AudioListener.volume = 1;
        //else
        //    AudioListener.volume = 0;

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
        //menuPanel.SetActive(false);
        levels.SetActive(true);
    }

    public void OnClickBack() {        
        if (optionPanel.active) {
            //menuPanel.SetActive(true);
            optionPanel.SetActive(false);
        } else if (levels.active) {
            //menuPanel.SetActive(true);
            levels.SetActive(false);
        }
    }

    public void OnClickOption() {
        optionPanel.SetActive(true);
        //menuPanel.SetActive(false);
    }

    public void SaveLocalization(string localization) {
        PlayerPrefs.SetString("localization", localization);
    }

    public void SetVolume(float volume) {
        PlayerPrefs.SetFloat("volume", volume);
        //audioMixer.SetFloat("Volume", volume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        
        
    }

    public void SetLanguage(int index) {
        // 0 - russian, 1- eng
        if(index == 0) {
            LocalizationManager.Instance.LoadLocalizedText("localizedText_ru");
            SaveLocalization("ru");
        }else if (index == 1) {
            LocalizationManager.Instance.LoadLocalizedText("localizedText_en");
            SaveLocalization("eng");
        }
    }

    public void SetLanguageDropdown() {
        if (PlayerPrefs.HasKey("localization")) {
            if (PlayerPrefs.GetString("localization").Equals("ru")) {
                languageDropdown.GetComponent<Dropdown>().value = 0;
            }
            else if (PlayerPrefs.GetString("localization").Equals("eng")) {
                languageDropdown.GetComponent<Dropdown>().value = 1;
            } else {
                languageDropdown.GetComponent<Dropdown>().value = 1;
            }
        }
    }

    public void SetJumpButton(bool jumpButton) {
        PlayerPrefs.SetInt("jumpButton", jumpButton ? 1 : 0);
    }

    #region selectLevel
    public void Level(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
   
    #endregion
}
