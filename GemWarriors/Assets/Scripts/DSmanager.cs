using TMPro;
using UnityEngine;

public class DSmanager : MonoBehaviour
{
    [SerializeField] private TMP_Text cScore;
    [SerializeField] private string playerName;
    private void Awake()
    {
        cScore.text = PlayerPrefs.GetInt("curScore").ToString();
    }

    public void GetName(string s)
    {
       playerName = s;
    }
    public void ReturnToMain(){
        if(playerName.Length < 1)
        {
            playerName = "NAME";
        }
        MainMenuManager.LeadersAdd(playerName, PlayerPrefs.GetInt("curScore"));
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
