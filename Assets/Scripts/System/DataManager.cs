using UnityEngine;

static public class DataManager 
{
    static public string stageKey = "OpenedStage";
    static public string openNextKey = "OpenNextStage";
    static public string volumeKey = "Volume";
    static public string ismutedKey = "IsMute";
    // Initialize data
    static public bool Init() {

        return true;
    }

    // Load data in to game
    static public void Load(string key, string defaultValue, out string data) {
        data = PlayerPrefs.GetString(key, defaultValue);
    }
    static public void Load(string key, int defaultValue, out int data) {
        data = PlayerPrefs.GetInt(key, defaultValue);
    }
    static public void Load(string key, float defaultValue, out float data) {
        data = PlayerPrefs.GetFloat(key);
    }

    // Save in-game data
    static public void Set(string key, string data) {
        PlayerPrefs.SetString(key, data);
    }
    static public void Set(string key, int data) {
        PlayerPrefs.SetInt(key, data);
    }
    static public void Set(string key, float data) {
        PlayerPrefs.SetFloat(key, data);
    }

    // Don't call unless necessary
    static public void Save() {
        PlayerPrefs.Save();
    }

    public static bool CheckValue(string key){
        // 1 true, 0 false
        Debug.Log(key);
        Debug.Log(PlayerPrefs.GetInt(key, 0));
        return PlayerPrefs.GetInt(key, 0) == 1;
    }
}