using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultatPanel : MonoBehaviour
{
    public TextMeshProUGUI player;
    public TextMeshProUGUI opponent;

    private void Update()
    {
        player.text = GameManager._instance.playerScore.ToString();
        opponent.text = GameManager._instance.OppenentScore.ToString();
    }
}
