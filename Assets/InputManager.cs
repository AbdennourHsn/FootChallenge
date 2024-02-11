using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;
    public Camera camera;
    private PlayerController player;
    private Oppenent oppenent;
    private Vector3 mouse_pose_0;
    private Vector3 mouse_pose_1;
    private Vector3 direction;
    private bool disable;
    public delegate void OnSwipeDone(Vector3 targetBall , Vector3 targetPlayer , float swipeLenght);
    public static OnSwipeDone SwipeDone;

    private void OnEnable()
    {
        GameManager.PlayerLoseEvent += DisableInput;
        GameManager.PlayerWonEvent += DisableInput;
        GameManager.ResetEvent += ResetInputs;
    }

    private void OnDisable()
    {
        GameManager.PlayerLoseEvent -= DisableInput;
        GameManager.PlayerWonEvent -= DisableInput;
        GameManager.ResetEvent -= ResetInputs;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        oppenent = FindObjectOfType<Oppenent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disable) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 clickPosition = hit.point;
                mouse_pose_0 = new Vector3(clickPosition.x, 0 , clickPosition.z);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 clickPosition = hit.point;
                mouse_pose_1 = new Vector3(clickPosition.x, 0, clickPosition.z);

                if (mouse_pose_0.z > mouse_pose_1.z) return;
                if ((mouse_pose_1 - mouse_pose_0).sqrMagnitude < 1f) return;

                direction = GetDirection(player.transform.position, mouse_pose_1);
                //player.ShootBall(direction, 10);

                
                SwipeDone?.Invoke(GetExtendedEndPos(mouse_pose_0 , oppenent.transform.position.z+0.7f, direction),
                    GetExtendedEndPos(mouse_pose_0, oppenent.transform.position.z, direction), 0);
            }
        }
    }

    public static Vector3 GetExtendedEndPos(Vector3 startPos  , float ZHorizontal , Vector3 Direction)
    {
        float distanceToHorizontalLine = (ZHorizontal - startPos.z) / Direction.z;
        Vector3 extendedEndPos = startPos + Direction * distanceToHorizontalLine;
        return extendedEndPos;
    }

    public static Vector3 GetDirection(Vector3 positionA , Vector3 positionB)
    {
        Vector3 direction = (positionB - positionA).normalized;
        return new Vector3(direction.x, 0, direction.z);
    }

    private void DisableInput() => disable = true;

    public void ResetInputs()
    {
        disable = false;
    }

}
