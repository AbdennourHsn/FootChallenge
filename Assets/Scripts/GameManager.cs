using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    public bool isMatch;
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



    [Space(10)]
    
    [SerializeField]
    private Transform ballPosition;

    [SerializeField]
    private PlayerController[] players;

    [Space(20)]
    [Header("Game life cycle")]
    public CustomEvents OnGameStart;

    [Space(10)]
    [SerializeField]
    public CustomEvents OnPlayerWonMatch;
    public CustomEvents OnPlayerLoseMatch;


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
        if (isMatch)
        {
            if (OppenentScore > 5)
            {
                PlayerLoseMatch();
            }
            else if (playerScore>5)
            {
                PlayerWonMatch();
            }
            else
            {
                yield return new WaitForSeconds(2);
                this.Reset_();
            }
        }
        else
        {
            yield return new WaitForSeconds(2);
            this.Reset_();
        }
    }

    private void PlayerWonMatch()
    {
        OnPlayerWonMatch?.Invoke();
    }

    private void PlayerLoseMatch()
    {
        OnPlayerLoseMatch?.Invoke();
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

    public void BackToHomeScene()
    {
        SaveData();
        SceneManager.LoadScene("Start");
    }

    public void ReloadScene()
    {
        SaveData();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void LoadAndSetData()
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        nbrCoin = stats.nbrOfCoins;
        UIManager._instance.SetCoin(nbrCoin);
        UIManager._instance.SetPlayerName(stats.PlayerName);
        BallSO ball = AssetHandler._instance.balls.Find(b => b.ballName == stats.ball);
        Instantiate(ball.ballPrefab, ballPosition.position, Quaternion.identity);
        if (stats.avatar == "Richardson") players[0].gameObject.SetActive(true);
        else players[1].gameObject.SetActive(true);

        if (stats.diffeculty == 2) this.oppenentLevel = OppenentLevel.expert;
        else this.oppenentLevel = OppenentLevel.medium;
    }

    private void SaveData()
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        stats.nbrOfCoins = nbrCoin;
        if (nbrOfPassed > stats.MaxBallExechange) stats.MaxBallExechange = nbrOfPassed;
        SaveManager.SaveData(stats);
    }

}
