using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestination : MonoBehaviour
{
    public bool m_IsTouchingBox = false;


    private bool m_IsKinematic = false;

    private BoxController m_Box;
    
    private bool m_WasBoxInPlace = false;
    void FixedUpdate() 
    {               
        
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1f))
        {
            if(hit.collider.tag == "Box")
            {
                m_IsTouchingBox = true;
                if(IsBoxInPlace())
                {
                    if(!m_WasBoxInPlace)
                    {
                        FindObjectOfType<SoundManager>().Play("Acknowledge");
                        m_Box = hit.collider.GetComponent<BoxController>();
                        m_Box.ChangeColorGreen();
                        m_WasBoxInPlace = true;
                    }
                }
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            m_IsTouchingBox = false;
            if(m_Box != null)
            {
                m_Box.ChangeColorRed();
                m_Box = null;
                m_WasBoxInPlace = false;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1, Color.white);
            Debug.Log("Did not Hit");
        }

    }
    private bool IsBoxInPlace()
    {
        GameObject m_Parent = transform.parent.gameObject;

        foreach(var child in m_Parent.GetComponentsInChildren<BoxDestination>())
        {
            if(!child.IsTouchingBox()) 
            {
                return false;
            }
        }
        return true;
    }
    public bool IsTouchingBox() => m_IsTouchingBox;
}
