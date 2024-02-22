using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyChanger : MonoBehaviour
{
    public Color selectedColor;
    public Color unselectedColor;

    public Image[] img;
    public Image[] imgCadres;

    private void Start()
    {
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        SetDifficulty(stats.diffeculty);
    }

    public void SetDifficulty(int i)
    {
        UnselectAll();
        img[i].color = selectedColor;
        imgCadres[i].gameObject.SetActive(true);
        PlayerStats stats = SaveManager.LoadData<PlayerStats>();
        stats.diffeculty = i;
        SaveManager.SaveData(stats);
    }

    public void UnselectAll()
    {
        foreach(Image i in img)
        {
            i.color = unselectedColor;
        }
        foreach (Image i in imgCadres)
        {
            i.gameObject.SetActive(false);
        }
    }
}
