using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetPlayerName : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    public void OK()
    {
        PlayerStats player = new PlayerStats
        {
            PlayerName = inputField.text,
            avatar = "Richardson",
            ball = "Main ball",
            stadium="Arena",
            level=1,
            nbrOfCoins=10,
            MaxBallExechange=0,
            Money=1
        };
        SaveManager.SaveData(player);
        SceneManager.LoadScene("Start");
    }
}
