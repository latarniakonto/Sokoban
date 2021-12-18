using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_BoxDestinations;

    private float m_GameTime = 0f;
    private int m_GameMoves = 0;
    private int m_GamePushes = 0;

    private bool m_LevelEnded = false;
    private SavingSystem m_SavingSystem = new SavingSystem();

    void Update()
    {
        if(AllBoxesAreInPlace())    
        {            
            m_LevelEnded = true;
            StartCoroutine(PlayNextLevel(2f));
        }

        if((SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 3) && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public IEnumerator PlayNextLevel(float seconds)
    {        
        PlayerPrefs.SetFloat("Time" + SceneManager.GetActiveScene().buildIndex.ToString(), m_GameTime);
        PlayerPrefs.SetInt("Moves" + SceneManager.GetActiveScene().buildIndex.ToString(), m_GameMoves);
        PlayerPrefs.SetInt("Pushes" + SceneManager.GetActiveScene().buildIndex.ToString(), m_GamePushes);                
        LevelScore level_score = new LevelScore(SceneManager.GetActiveScene().name, m_GameTime, m_GameMoves, m_GamePushes);        
        m_SavingSystem.Save(level_score);
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }
    public void LoadLevels()
    {        
        SceneManager.LoadScene(5);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int level)    
    {
        SceneManager.LoadScene(level);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public bool AllBoxesAreInPlace()
    {
        if(m_BoxDestinations == null || m_BoxDestinations.Length == 0) return false;

        foreach(var destination in m_BoxDestinations)
        {
            BoxDestination[] children = destination.GetComponentsInChildren<BoxDestination>();
            if(children == null || children.Length == 0) continue;

            foreach(var child in children)
            {
                if(!child.IsTouchingBox()) return false;
            }
        }
        return true;
    }
    public void SetGameTime(float time) => m_GameTime = time;
    public void SetGameMoves(int moves) => m_GameMoves = moves;
    public void SetGamePushes(int pushes) => m_GamePushes = pushes;    
    public float GetGameTime() 
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name != "End" && SceneManager.GetActiveScene().name != "NextLevel") throw new UnityException("Your are using this method in a wrong scene.");
        float m_TotalLevelTime = PlayerPrefs.GetFloat("Time" + (SceneManager.GetActiveScene().buildIndex - 1).ToString(), 0f);            
        
        return m_TotalLevelTime;
    }
    public int GetGameMoves()
    {
        if(SceneManager.GetActiveScene().name != "End" && SceneManager.GetActiveScene().name != "NextLevel") throw new UnityException("Your are using this method in a wrong scene.");
        int m_TotalLevelMoves = PlayerPrefs.GetInt("Moves" + (SceneManager.GetActiveScene().buildIndex - 1).ToString(), 0);            
    
        return m_TotalLevelMoves;
    }
    public int GetGamePushes() 
    {
        if(SceneManager.GetActiveScene().name != "End" && SceneManager.GetActiveScene().name != "NextLevel") throw new UnityException("Your are using this method in a wrong scene.");
        int m_TotalSumPushes = PlayerPrefs.GetInt("Pushes" + (SceneManager.GetActiveScene().buildIndex - 1).ToString(), 0);            
        return m_TotalSumPushes;
    }
    public bool GetLevelEnded() => m_LevelEnded;
}
