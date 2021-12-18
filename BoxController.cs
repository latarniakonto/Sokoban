using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    private UIManager m_Manager;
    private GameManager m_GameManager;
    private Rigidbody m_Rigidbody;   

    private Vector3 m_LastPosition;
    private float m_DistanceTravelled = 0f;

    private Vector3 m_CurrentPosition; 

    private static int m_BoxPushes = 0;

    [SerializeField] private float m_PushLength = 1.5f;

    [SerializeField] private Material m_RedBox;
    [SerializeField] private Material m_GreenBox;

    private int m_FrameCountInTrigger = -1;
    void Start()
    {        
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
        m_LastPosition = transform.position;
        m_Manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        m_BoxPushes = 0; //clearing cached static variable
        m_GameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {               
        if(m_GameManager.GetLevelEnded()) 
        {
            m_Rigidbody.isKinematic = true;
            return;
        }

        m_Rigidbody.velocity = Vector3.zero;     

        m_DistanceTravelled += (m_LastPosition - transform.position).magnitude; 
        m_LastPosition = transform.position;           
        if(m_DistanceTravelled >= m_PushLength)
        {
            m_BoxPushes++;
            m_DistanceTravelled = 0f;
            m_Manager.SetPlayerPushes(m_BoxPushes);
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(m_GameManager.GetLevelEnded()) return;
        if(other.tag == "Player")   
        {
            m_Rigidbody.isKinematic = false;
            m_FrameCountInTrigger = Time.frameCount;

            Vector3 m_PlayerLocalDirection = transform.InverseTransformPoint(other.transform.position);
            if(m_PlayerLocalDirection.x < -1f)
            {                
                m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }
            else if(m_PlayerLocalDirection.x > 1f)
            {                
                m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }else
            {
                m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }                        
        }
    }      
    private void OnTriggerExit(Collider other) 
    {
        if(m_GameManager.GetLevelEnded()) return;
        if(other.tag == "Player")   
        {
            m_Rigidbody.isKinematic = true;
            m_FrameCountInTrigger = Time.frameCount;

            Vector3 m_PlayerLocalDirection = transform.InverseTransformPoint(other.transform.position);
            if(m_PlayerLocalDirection.x < -1f)
            {                
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;        
            }
            else if(m_PlayerLocalDirection.x > 1f)
            {                
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;        
            }else
            {
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;        
            }
        }
    }
    public int GetFrameCountInTrigger() => m_FrameCountInTrigger;
    public void ChangeColorRed()
    {
        GetComponent<MeshRenderer>().material = m_RedBox;
    }
    public void ChangeColorGreen()
    {
        GetComponent<MeshRenderer>().material = m_GreenBox;
    }
    
}
