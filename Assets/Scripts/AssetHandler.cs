using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssetHandler : MonoBehaviour
{
    public static AssetHandler _instance;

    public List<BallSO> balls;
    public List<AvaterSO> avatars;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(_instance);
    }


}
