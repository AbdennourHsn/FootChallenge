using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoards : MonoBehaviour
{
    public InfoCard card;

    private void Start()
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        card.Name.text = stats.PlayerName;
        card.Coin.text = stats.nbrOfCoins.ToString(); ;
        card.Passes.text = stats.MaxBallExechange.ToString();
    }
}
