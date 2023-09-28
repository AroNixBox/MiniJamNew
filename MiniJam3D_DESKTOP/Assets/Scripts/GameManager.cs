using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _registeredObjectives = 0;
    [SerializeField] private TextMeshProUGUI currentObjectivesCount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void RegisterObjective()
    {
        _registeredObjectives++;
        Debug.Log(_registeredObjectives + " Objectives Counted");
        if(currentObjectivesCount)
            currentObjectivesCount.text = _registeredObjectives.ToString();
    }

    public void ObjectiveCollected()
    {
        _registeredObjectives--;
        if(currentObjectivesCount)
            currentObjectivesCount.text = _registeredObjectives.ToString();
        if (_registeredObjectives <= 0)
        {
            GameWon();
        }
    }

    private void GameWon()
    {
        //TODO Add GameWinningScreen
        SceneManager.LoadScene("WinScene"); 
    }
}
