using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TMP_Text leaderbord;

    private static Dictionary<string, int> leaders = new Dictionary<string, int>();


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "JSON.json")))
        {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "JSON.json"));
            leaders = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        }
    }
    private void OnApplicationQuit()
    {
        string json = JsonConvert.SerializeObject(leaders, Formatting.Indented);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "JSON.json"), json);
        
    }
    public static void LeadersAdd(string name, int score)
    {
        if (!leaders.ContainsKey(name))
        {
            leaders.Add(name, score);
        }
        else
        {
            leaders[name] = score;
        }
    }

    private void Awake()
    {
        var sortedList = leaders.ToList();
        sortedList.Sort((pair1, pair2) => pair2.Value - pair1.Value);
        string text = "";
        int i = 0;
        foreach (var kvp in sortedList)
        {
            if (i > 5)
                break;
            text += string.Format($"{kvp.Key}:{kvp.Value}\n");
            i++;
        }
        leaderbord.text = text;
    }


    public void GoToGameplay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
