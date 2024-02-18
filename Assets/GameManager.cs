using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

[System.Serializable]
public class CustomEvents : UnityEvent
{ }

public enum OppenentLevel
{
    amateur,
    medium,
    expert
}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public OppenentLevel oppenentLevel = OppenentLevel.medium;
    [HideInInspector]
    public int nbrOfPassed;
    [HideInInspector]
    public int nbrCoin = 0;

    [HideInInspector]
    public int playerScore { get; private set; }
    [HideInInspector]
    public int OppenentScore { get; private set; }

    public delegate void OnPlayerWon();
    public delegate void OnPlayerLose();
    public delegate void OnReset();

    public static OnPlayerWon PlayerWonEvent;
    public static OnPlayerLose PlayerLoseEvent;
    public static OnReset ResetEvent;
    public float speedDuration=1.5f;
    public float speedFactor=0.02f;

    [Header("Game life cycle")]
    public CustomEvents OnGameStart;

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
    }

    public void Start()
    {
        LoadAndSetData();
    }

    public void PlayerWon_()
    {
        SaveData();
        nbrOfPassed = 0;
        playerScore += 1;
        PlayerWonEvent?.Invoke();
        StartCoroutine(ResetCourotine());
    }

    public void PlayerLose_()
    {
        SaveData();
        nbrOfPassed = 0;
        OppenentScore += 1;
        PlayerLoseEvent?.Invoke();
        StartCoroutine(ResetCourotine());
    }

    public void Reset_()
    {
        speedDuration = 1.5f;
        ResetEvent?.Invoke();
        UIManager._instance.UpdateScore();
    }

    public void OnGameStart_()
    {
        OnGameStart?.Invoke();
        Reset_();
    }

    IEnumerator ResetCourotine()
    {
        yield return new WaitForSeconds(2);
        this.Reset_();
    }

    public void GetCoin(Vector3 pos)
    {
        this.nbrCoin += 1;
        UIManager._instance.AddCoin(pos , nbrCoin);
    }

    private void Update()
    {
        
    }

    public void Passe()
    {
        speedDuration -= speedFactor;
        nbrOfPassed += 1;
        UIManager._instance.updatePasses();
    }

    private void LoadAndSetData()
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        nbrCoin = stats.nbrOfCoins;
        UIManager._instance.SetCoin(nbrCoin);
    }

    private void SaveData()
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        stats.nbrOfCoins = nbrCoin;
        if (nbrOfPassed > stats.MaxBallExechange) stats.MaxBallExechange = nbrOfPassed;
        SaveManager.SaveData(stats);
    }

}
