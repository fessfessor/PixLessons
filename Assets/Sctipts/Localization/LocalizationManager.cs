using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set;}

    private Dictionary<string, string> localizedText;
    private string missingText = "Localized text not found";

    [SerializeField] private string ruFileName;
    [SerializeField] private string engFileName;

    public bool isReady = false;

    private void Awake() {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        localizedText = new Dictionary<string, string>();

        //Предзагрузка языка
        if (PlayerPrefs.HasKey("localization")) {
            if (PlayerPrefs.GetString("localization").Equals("ru")) {
                LoadLocalizedText(ruFileName);
            }else if (PlayerPrefs.GetString("localization").Equals("eng")) {
                LoadLocalizedText(engFileName);
            }
        }
        else {
            LoadLocalizedText(engFileName);
        }
    }

    

    public void LoadLocalizedText(string fileName) {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

//Читаем все из файла
        if (File.Exists(filePath)) {
            string dataJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataJson);
//Заполняем коллекцию
            for(int i=0; i< loadedData.items.Length; i++) {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains " + localizedText.Count + " entries.");
        }
        else {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;

    }

    public string GetLocalizedValue(string key) {
        string result = missingText;

        if (localizedText.ContainsKey(key)) {
            result = localizedText[key];
        }

        return result;
        
    }

    public void test() {
        Debug.Log("test");
    }

    

    
}
