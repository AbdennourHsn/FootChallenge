using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public TextMeshProUGUI money;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI avatar;
    [Space(10)]
    [Header("Ball")]
    [SerializeField]
    private BallSO currBall;
    public TextMeshProUGUI BallName;
    public Image BallImage;

    [Space(10)]
    [Header("Avatars")]
    [SerializeField]
    private AvaterSO currAvatar;
    public TextMeshProUGUI AvatarName;
    public GameObject charactersHandler;

    private void Start()
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        coins.text = stats.nbrOfCoins.ToString();
        money.text = stats.Money.ToString();
        playerName.text = stats.PlayerName.ToString();
        avatar.text = stats.avatar.ToString();
        SetBall(stats.ball);
        SetAvatar(stats.avatar);
    }

    private void SetBall(string name)
    {
        currBall = AssetHandler._instance.balls.Find(b => b.ballName == name);
        BallName.text = currBall.ballName;
        BallImage.sprite = currBall.ballImage;
    }

    private void UpdateBall()
    {
        BallName.text = currBall.ballName;
        BallImage.sprite = currBall.ballImage;
        SaveBall(currBall.ballName);
    }

    private void SetAvatar(string name)
    {
        currAvatar = AssetHandler._instance.avatars.Find(b => b.AvatarName == name);
        AvatarName.text = currAvatar.AvatarName;
        ShowAvatar(name);
    }

    private void UpdateAvatar()
    {
        AvatarName.text = currAvatar.AvatarName;
        ShowAvatar(currAvatar.name);
        SaveAvatar(currAvatar.AvatarName);
    }

    public void nextBall()
    {
        int index = AssetHandler._instance.balls.FindIndex(b => b.ballName == currBall.ballName);
        if (index != -1)
        {
            if (index == AssetHandler._instance.balls.Count-1) index = 0;
            else index += 1;
        }
        currBall = AssetHandler._instance.balls[index];
        UpdateBall();
    }

    public void PreviesBall()
    {
        int index= AssetHandler._instance.balls.FindIndex(b => b.ballName == currBall.ballName);
        if (index != -1)
        {
            if (index == 0) index = AssetHandler._instance.balls.Count - 1;
            else index -= 1;
        }
        currBall = AssetHandler._instance.balls[index];
        UpdateBall();
    }

    public void nextAvatar()
    {
        int index = AssetHandler._instance.avatars.FindIndex(a => a.AvatarName == currAvatar.AvatarName);
        if (index != -1)
        {
            if (index == AssetHandler._instance.avatars.Count - 1) index = 0;
            else index += 1;
        }
        currAvatar = AssetHandler._instance.avatars[index];
        UpdateAvatar();
    }

    public void PreviesAvatar()
    {
        int index = AssetHandler._instance.avatars.FindIndex(b => b.AvatarName == currAvatar.AvatarName);
        if (index != -1)
        {
            if (index == 0) index = AssetHandler._instance.avatars.Count - 1;
            else index -= 1;
        }
        currAvatar = AssetHandler._instance.avatars[index];
        UpdateAvatar();
    }

    private void SaveBall(string ballName)
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        stats.ball = ballName;
        SaveManager.SaveData(stats);
    }

    private void ShowAvatar(string name)
    {
        HideAllAvatars();
        for (int i = 0; i < charactersHandler.transform.childCount; i++)
        {
            if (charactersHandler.transform.GetChild(i).gameObject.name == name)
            {
                charactersHandler.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    private void HideAllAvatars()
    {
        for (int i = 0; i < charactersHandler.transform.childCount; i++)
        {
            charactersHandler.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SaveAvatar(string avatarName)
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        stats.avatar = avatarName;
        SaveManager.SaveData(stats);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
