using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Avatar", menuName = "ScriptableObjects/Avatar", order = 2)]
public class AvaterSO : ScriptableObject
{
    public string AvatarName;
    public GameObject AvatarPrefab;
    public bool isAvailable = true;
}
