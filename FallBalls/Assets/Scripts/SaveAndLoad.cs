using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private static string path { get { return Path.Combine(Application.persistentDataPath, "Save.txt"); } }
    private void Awake()
    {
        if (File.Exists(path))
        {
            Load();
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    public static void Save()
    {
        SaveScoreManager save = new()
        {
            highscores = ScoreManager.highscores,
            colorIndex = ScoreManager.colorIndex
        };

        File.WriteAllText(path, JsonUtility.ToJson(save));
    }
    public static void Load()
    {
        SaveScoreManager save = JsonUtility.FromJson<SaveScoreManager>(File.ReadAllText(path));

        ScoreManager.highscores = save.highscores;
        ScoreManager.colorIndex = save.colorIndex;
    }
}
