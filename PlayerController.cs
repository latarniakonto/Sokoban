using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private UIManager m_Manager;
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    private float m_TurnSmoothVelocity;    

    private Vector3 m_LastPosition;

    private int m_StepsTravelled = 0;

    private float m_DistanceTravelled = 0f;


    [SerializeField] private float m_Speed = 5;

    [SerializeField] private float m_TurnSmoothTime = 0.1f;

    [SerializeField] private float m_StepLength = 1.5f;
    void Start()
    {        
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_LastPosition = transform.position;
        m_Manager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void FixedUpdate()
    {
        CalculateMovement();

        if(m_DistanceTravelled >= m_StepLength)
        {
            m_StepsTravelled++;
            m_DistanceTravelled = 0f;
            m_Manager.SetPlayerMoves(m_StepsTravelled);
            //Debug.Log(m_StepsTravelled);
        }
    }

    private void CalculateMovement()
    {
        m_DistanceTravelled += (m_LastPosition - transform.position).magnitude; 
        m_LastPosition = transform.position;

        float m_Horizontal = Input.GetAxis("Horizontal");
        float m_Vertical = Input.GetAxis("Vertical");
        if (m_Horizontal != 0f && m_Vertical != 0f)
        {
            m_Vertical = 0f;
        }

        Vector3 m_Direction = new Vector3(m_Horizontal, 0f, m_Vertical).normalized;

        if (m_Direction.magnitude >= 0.1f)
        {
            float m_TargetAngle = Mathf.Atan2(m_Direction.x, m_Direction.z) * Mathf.Rad2Deg + 180f;
            float m_Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_TargetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

            m_Animator.SetBool("Walking", true);            
            FindObjectOfType<SoundManager>().Play("Walking");
            m_Rigidbody.MoveRotation(Quaternion.Euler(0f, m_Angle, 0f));
            m_Rigidbody.MovePosition(transform.position + m_Direction * Time.fixedDeltaTime * m_Speed);

        }
        else
        {
            m_Animator.SetBool("Walking", false);
        }
    }    
}
