using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStat
{
    GameObject obj;
    Vector3 Initialposition;
    Quaternion Initialrotation;
    bool isKinematic = false;

    public ObjectStat(GameObject obj)
    {
        this.obj = obj;
        Initialposition = obj.transform.position;
        Initialrotation = obj.transform.rotation;
        if (obj.name == "L")
        {
            obj.GetComponent<Collider>().enabled = false;
            isKinematic = true;
        }
    }

    public void ResetObj()
    {
        obj.transform.position = Initialposition;
        obj.transform.rotation = Initialrotation;
        obj.GetComponent<Rigidbody>().isKinematic = isKinematic;
        if (obj.name == "L")
        {
            obj.GetComponent<Collider>().enabled = false;
        }
    }
}


public class Obstacles : MonoBehaviour
{
    public Rigidbody[] obstacleItems;

    private List<ObjectStat> Items = new List<ObjectStat>();

    private void OnEnable()
    {
        GameManager.ResetEvent += ResetObstacle;
    }

    private void OnDisable()
    {
        GameManager.ResetEvent -= ResetObstacle;
    }

    private void Start()
    {
        foreach(Rigidbody rb in obstacleItems)
        {
            Items.Add(new ObjectStat(rb.transform.gameObject));
        }
    }

    public void OnObstacleItemTouched()
    {
        foreach (Rigidbody rb in obstacleItems)
        {
            rb.GetComponent<Collider>().enabled = true;
            rb.isKinematic = false;
        }
    }

    public void ResetObstacle()
    {
        foreach (ObjectStat item in Items) item.ResetObj();
    }
}
