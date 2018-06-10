using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float m_PlayerSpeed = 10f;
    public float m_PlayerRunSpeedM = 2f;
    public float m_CamSmooth = 10f;
    public bool m_CamOnPlayer = true;
    public bool m_DebugMode = false;
    public float m_ReachDistance = 30f;
    public float m_MouseSelectionRadius = 1f;


    [SerializeField] private bool m_IsRunning = false;
    private Camera m_PlayerCam;
    [SerializeField] private GameObject m_Target = null;

	// Use this for initialization
	void Start () {
        m_PlayerCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        InputManagement();
    }

    void Move()
    {
        float Horizontal;
        float Vertical;

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (Input.GetButton("Run"))
        {
            m_IsRunning = true;
            Horizontal *= m_PlayerRunSpeedM;
            Vertical *= m_PlayerRunSpeedM;
        }

        else
        {
            m_IsRunning = false;
        }

        if (m_CamOnPlayer)//if CamOnPlayer option is activated and need to follow the player
        {
            transform.Translate(new Vector3(Horizontal, Vertical, 0) * Time.fixedDeltaTime * m_PlayerSpeed, Space.World);
            CameraFollowTarget(transform);
            LookAtMouse();
        }
        else//if camera is free
        {
            m_PlayerCam.transform.Translate(new Vector3(Horizontal, Vertical, 0) * Time.fixedDeltaTime * m_PlayerSpeed);
        }

    }

    void LookAtMouse()
    {
        Vector3 distance = m_PlayerCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        distance.Normalize();

        float rotation_z = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }

    void CameraFollowTarget(Transform targetTransform)
    {
        Vector3 newCamPos = Vector3.Lerp(m_PlayerCam.transform.position, targetTransform.position, Time.fixedDeltaTime * m_CamSmooth);
        newCamPos.z = m_PlayerCam.transform.position.z;
        m_PlayerCam.transform.position = newCamPos;
    }

    GameObject RetrieveMouseTarget()//This is going to cast a ray and look what is target by the mouse
    {
        return null;

    }

    GameObject OnCursor(float maxDistanceCast)//this is casting a circle from the mouse to test if something can be selected
    {
        Vector2 mousePos = m_PlayerCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hitArray = new RaycastHit2D[2];
        RaycastHit2D firstHit;

        Physics2D.CircleCastNonAlloc(mousePos, m_MouseSelectionRadius, Vector2.zero, hitArray);
        firstHit = hitArray[0];
        if(firstHit.transform != null)
        {
            if (m_DebugMode) { Debug.LogError("Object found under mouse"); }
            return firstHit.collider.gameObject;
        }

        if (m_DebugMode) { Debug.LogError("No Object found under mouse"); }
        return null;
    }

    GameObject OnPlayerRange(float maxDistanceCast)//This is casting a 2d ray from the player toward the mouse for a certain distance to catch a collider
    {
        Vector2 mouseDirection = m_PlayerCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDirection.Normalize();
        RaycastHit2D hit;

        RaycastHit2D[] hitArray = new RaycastHit2D[2];
        Physics2D.RaycastNonAlloc(transform.position, mouseDirection, hitArray, maxDistanceCast);

        hit = hitArray[1];//this ignore the first collider
        if (m_DebugMode == true)
        {
            Vector2 debugMousePos = m_PlayerCam.ScreenToWorldPoint(Input.mousePosition);
            if (m_DebugMode){Debug.DrawLine(transform.position, debugMousePos, Color.red);}
        }
        if (hit.transform != null)
        {
            Debug.LogError("Object found");
            return hit.collider.gameObject;
        }

        if (m_DebugMode){Debug.LogError("No object found");}
        return null;
    }

    void InputManagement()
    {
        if (Input.GetButton("Fire1"))
        {
            m_Target = OnPlayerRange(m_ReachDistance);
        }

        if (Input.GetButton("Action"))
        {
            m_Target = OnCursor(m_ReachDistance);
        }

    }


   /* void DrawDebugRay()
    {
        if(m_MouseRayDebug)
        {
            m_PlayerCam.WorldToScreenPoint
            Debug.DrawLine(m_PlayerCam.ScreenToWorldPoint(Input.mousePosition), m_PlayerCam.ScreenToWorldPoint(Input.mousePosition) - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Color.red);
        }
    }*/
}
