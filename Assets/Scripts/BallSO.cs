using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new ball", menuName = "ScriptableObjects/Ball", order = 1)]

public class BallSO : ScriptableObject
{
    public string ballName;
    public Sprite ballImage;
    public GameObject ballPrefab;
    public bool isAvailable=true;
}
