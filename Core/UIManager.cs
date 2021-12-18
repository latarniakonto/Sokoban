using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager m_GameManager;
    [SerializeField] private TextMeshProUGUI m_TimeText;
    [SerializeField] private TextMeshProUGUI m_MovesText;
    [SerializeField] private TextMeshProUGUI m_PushesText;
    [SerializeField] private TextMeshProUGUI m_ScoreTimeText;
    [SerializeField] private TextMeshProUGUI m_ScoreMovesText;
    [SerializeField] private TextMeshProUGUI m_ScorePushesText;
    [SerializeField] private TextMeshProUGUI m_Level1TimeText;
    [SerializeField] private TextMeshProUGUI m_Level1MovesText;
    [SerializeField] private TextMeshProUGUI m_Level1PushesText;
    [SerializeField] private TextMeshProUGUI m_Level2TimeText;
    [SerializeField] private TextMeshProUGUI m_Level2MovesText;
    [SerializeField] private TextMeshProUGUI m_Level2PushesText;
    private float m_TimeTaken = 0f;

    private int m_PlayerMoves = 0;

    private int m_PlayerPushes = 0;
    private SavingSystem m_SavingSystem = new SavingSystem();
    void Start() 
    {
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(m_Level1TimeText != null && m_Level1MovesText != null && m_Level1PushesText != null)
        {
            LevelScore score = m_SavingSystem.Load("Level1");
            Debug.Log(score.ToString());
            if(score.level != "TBD")
            {
                m_Level1TimeText.text = GetMinutesOnTimer(score.time).ToString() + ":" + GetSecondsOnTimer(score.time).ToString();
                m_Level1MovesText.text = score.moves.ToString();
                m_Level1PushesText.text = score.pushes.ToString();
            }
        }
        if(m_Level2TimeText != null && m_Level2MovesText != null && m_Level2PushesText != null)
        {
            LevelScore score = m_SavingSystem.Load("Level2");
            
            if(score.level != "TBD")
            {                
                m_Level2TimeText.text = GetMinutesOnTimer(score.time).ToString() + ":" + GetSecondsOnTimer(score.time).ToString();
                m_Level2MovesText.text = score.moves.ToString();
                m_Level2PushesText.text = score.pushes.ToString();
            }
        }
    }
    void Update()
    {
        if(m_TimeText != null && m_MovesText != null && m_PushesText != null && !m_GameManager.GetLevelEnded())
        {
            m_TimeTaken += Time.deltaTime;
            m_TimeText.text = GetMinutesOnTimer(m_TimeTaken).ToString() + ":" + GetSecondsOnTimer(m_TimeTaken).ToString();                
            m_MovesText.text = m_PlayerMoves.ToString();
            m_PushesText.text = m_PlayerPushes.ToString();
            m_GameManager.SetGameTime(m_TimeTaken);
        }
        if(m_ScoreTimeText != null && m_ScoreMovesText != null && m_ScorePushesText != null)
        {
            float m_Time = m_GameManager.GetGameTime();            
            m_ScoreTimeText.text = GetMinutesOnTimer(m_Time).ToString() + ":" + GetSecondsOnTimer(m_Time).ToString();                
            m_ScoreMovesText.text = m_GameManager.GetGameMoves().ToString();
            m_ScorePushesText.text = m_GameManager.GetGamePushes().ToString();
        }
        
    }
    private int GetMinutesOnTimer(float seconds) => (int)Mathf.Round(seconds) / 60;     

    private int GetSecondsOnTimer(float seconds) => (int) Mathf.Round(seconds) % 60; 

    public void SetPlayerMoves(int moves) 
    {
        m_PlayerMoves = moves;
        m_GameManager.SetGameMoves(moves);
    }

    public void SetPlayerPushes(int pushes)
    {        
        m_PlayerPushes = pushes;
        m_GameManager.SetGamePushes(pushes);
    }
}
