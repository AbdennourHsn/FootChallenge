using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public TextMeshProUGUI playerName;
    public TextMeshPro playerScore;
    public TextMeshPro OppenentScore;
    public TextMeshProUGUI nbrCoin;
    public TextMeshProUGUI nbrPass;

    [Space(10)]
    public GameObject coinImg;
    public Camera mainCamera;
    public RectTransform canvasRect;

    private void OnEnable()
    {
        GameManager.PlayerLoseEvent += UpdateScore;
        GameManager.PlayerWonEvent += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.PlayerLoseEvent -= UpdateScore;
        GameManager.PlayerWonEvent -= UpdateScore;
    }

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

    public void UpdateScore()
    {
        playerScore.text = GameManager._instance.playerScore.ToString();
        OppenentScore.text = GameManager._instance.OppenentScore.ToString();
        
    }

    internal void updatePasses()
    {
        this.nbrPass.text = GameManager._instance.nbrOfPassed.ToString() ;
    }

    internal void AddCoin(Vector3 pos, int Coins)
    {

        nbrCoin.text = Coins.ToString();
        GameObject img =Instantiate(coinImg, canvasRect);
        img.transform.DOMove(nbrCoin.transform.position, 1f);
        img.transform.DOScale(Vector3.one * 0.5f, 1f).OnComplete(()=> { Destroy(img); });
        //img.GetComponent<RectTransform>().position = WorldToCanvasPosition(pos);
        
    }
    public Vector2 WorldToCanvasPosition(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, mainCamera, out canvasPosition);
        return canvasPosition;
    }

    public void SetCoin(int nbrCoin)
    {
        this.nbrCoin.text = nbrCoin.ToString();
    }

    public void SetPlayerName(string name)
    {
        this.playerName.text = name;
    }
}
