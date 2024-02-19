using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public void NextScene()
    {
        if (SaveManager.isFileExists())
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            SceneManager.LoadScene("User");
        }
    }
}
