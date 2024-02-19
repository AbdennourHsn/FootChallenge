using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GenerateCoin : MonoBehaviour
{
    public GameObject coin;
    public Transform center;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateLoop());
    }

    private IEnumerator GenerateLoop()
    {
        int x= Random.Range(4, 10);
        yield return new WaitForSeconds(x);
        GenerateCoins();
    }

    public void CoibCollected()
    {
        StartCoroutine(GenerateLoop());
    }

    private void GenerateCoins()
    {
        float x = Random.Range(-0.4f, 0.4f);
        float y = Random.Range(-1f, 2f);
        var obj =Instantiate(coin, center.position+Vector3.right*x+Vector3.forward*y, Quaternion.identity);
        obj.transform.localScale = Vector3.zero;
        obj.transform.DOScale(Vector3.one, 1f).OnComplete(() => { obj.GetComponentInChildren<Collider>().enabled = true; });
    }
}
