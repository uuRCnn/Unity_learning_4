using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject karePrefab;

    [SerializeField] private GameObject karelerPaneli;

    [SerializeField] private Transform SoruPaneli;
    
    private GameObject[] karelerDizisi = new GameObject[25];


    private void Start()
    {
        SoruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        kareleriOlustur();
    }

    public void kareleriOlustur()
    {
        for (int i = 0; i < karelerDizisi.Length; i++)
        {
            karelerDizisi[i] = Instantiate(karePrefab);
            karelerDizisi[i].transform.parent = karelerPaneli.transform;
        }
        StartCoroutine(DoFadeRoutine());
        BolumDegerliniTexteYazdır();
        Invoke("SoruPaneliniAç", 2f);

    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.08f);
        }
    }

    void BolumDegerliniTexteYazdır()
    {
        foreach (var kare in karelerDizisi)
        {
            int RasgeleDeger = Random.Range(1, 13);
            kare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RasgeleDeger.ToString();
        }
    }

    void SoruPaneliniAç()
    {
        SoruPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
}
