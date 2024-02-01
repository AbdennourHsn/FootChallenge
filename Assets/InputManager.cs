using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject prefab;
    public Camera camera;
    public PlayerController player;
    private Vector3 mouse_pose_0;
    private Vector3 mouse_pose_1;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                direction = (mouse_pose_1 - player.transform.position).normalized;
                direction = new Vector3(direction.x, 0, direction.z);
                FindObjectOfType<Oppenent>().MoveTo(mouse_pose_1);
                player.ShootBall(direction, 10);
            }
        }
    }
}
