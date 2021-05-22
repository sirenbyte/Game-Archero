using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMovement : MonoBehaviour
{
    public static JoyStickMovement Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JoyStickMovement>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("JoyStickMovement");
                    instance = instanceContainer.AddComponent<JoyStickMovement>();
                }
            }
            return instance;
        }
    }
    private static JoyStickMovement instance;

    public GameObject smallStick;
    public GameObject bGStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    float stickRadius;
    Vector3 joyStickFirstPosition;

    public bool isPlayerMoving = false;


    void Start()
    {
        stickRadius = bGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        joyStickFirstPosition = bGStick.transform.position;
    }  
    void Update()
    {
        
    }

    public void PointDown()
    {
        bGStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;

        if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            PlayerMovement.Instance.Anim.SetBool("Attack", false);
            PlayerMovement.Instance.Anim.SetBool("Idle", false);
            PlayerMovement.Instance.Anim.SetBool("Walk", true);
        }

        isPlayerMoving = true;
    }

    public void Drag(BaseEventData baseEventData)
    {
        Vector3 DragPosition = Input.mousePosition;
        joyVec = (DragPosition - stickFirstPosition);

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);

        if (stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPosition + joyVec.normalized * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
        }
    }

    public void Drop()
    {
        joyVec = Vector3.zero;
        bGStick.transform.position = joyStickFirstPosition;
        smallStick.transform.position = joyStickFirstPosition;

        if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {

            PlayerMovement.Instance.Anim.SetBool("Attack", false);
            PlayerMovement.Instance.Anim.SetBool("Walk", false);
            PlayerMovement.Instance.Anim.SetBool("Idle", true);
        }

        isPlayerMoving = false;
   
}
}