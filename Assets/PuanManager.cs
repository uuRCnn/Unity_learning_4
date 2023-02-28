using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuanManager : MonoBehaviour
{
    private int toplamPuan, puanArtisi;
    [SerializeField] private TextMeshProUGUI puanText;

    private void Start()
    {
        puanText.text = toplamPuan.ToString();
    }

    public void PuaniArtir(string zorlukSeviyesi)
    {
        switch (zorlukSeviyesi)
        {
            case "kolay":
                puanArtisi = 5;
                break;
            case "orta":
                puanArtisi = 10;
                break;
            case "zor":
                puanArtisi = 15;
                break;
        }

        toplamPuan += puanArtisi;

        puanText.text = toplamPuan.ToString();
    }
}
