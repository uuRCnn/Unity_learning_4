using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject karePrefab;
    [SerializeField] private GameObject karelerPaneli;
    [SerializeField] private Transform SoruPaneli;
    [SerializeField] private TextMeshProUGUI soruTexti;
    [SerializeField] private Sprite[] kareSpirtes;
    [SerializeField] private GameObject sonuçPaneli;
    [SerializeField] private AudioSource audioSource;
    private int bölünensayı, bölensayı, kacıncıSoru, buttondegeri, dogru_sonuc, kalan_hak;
    private string sorununZorlukDerecesi;
    private bool butona_basıldımı = false;
    private GameObject[] karelerDizisi = new GameObject[25];
    private GameObject gecerliKare;
    private kalanCanlar Kalan_Canlar;
    private PuanManager _puanManager;
    public AudioClip butonSesi;

    List<int> bolumDegerlerilistesi = new List<int>();

    private void Awake()
    {
        kalan_hak = 3;
        audioSource = GetComponent<AudioSource>();
        sonuçPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        Kalan_Canlar = Object.FindObjectOfType<kalanCanlar>();
        _puanManager = Object.FindObjectOfType<PuanManager>();
        Kalan_Canlar.Kalan_Hakları_Kontrol_et(kalan_hak);
    }

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
            karelerDizisi[i].transform.GetChild(1).GetComponent<Image>().sprite = kareSpirtes[Random.Range(0,kareSpirtes.Length)];
            karelerDizisi[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
            karelerDizisi[i].transform.GetComponent<Button>().onClick.AddListener(() => buttonaBasıldı());
            karelerDizisi[i].transform.parent = karelerPaneli.transform;
        }

        StartCoroutine(DoFadeRoutine());
        BolumDegerliniTexteYazdır();
        Invoke("SoruPaneliniAç", 2f);
    }

    void buttonaBasıldı()
    {
        if (butona_basıldımı)
        {
            audioSource.PlayOneShot(butonSesi);
            buttondegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform
                .GetChild(0).GetComponent<TextMeshProUGUI>().text);
            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            Sonucu_Konrtolet();
        }
    }

    void Sonucu_Konrtolet()
    {
        if (buttondegeri == dogru_sonuc)
        {
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliKare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " ";
            gecerliKare.transform.GetComponent<Button>().interactable = false;
            _puanManager.PuaniArtir(sorununZorlukDerecesi);
            bolumDegerlerilistesi.RemoveAt(kacıncıSoru);
            if (bolumDegerlerilistesi.Count > 0)
            {
                SoruPaneliniAç();
            }
            else
            {
                OyunBitti();
            }
        }
        else
        {
            kalan_hak--;
            Kalan_Canlar.Kalan_Hakları_Kontrol_et(kalan_hak);
        }

        if (kalan_hak <= 0)
        {
            OyunBitti();
        }
    }

    void OyunBitti()
    {
        butona_basıldımı = false;
        sonuçPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
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
            int RasgeleDeger = Random.Range(2, 13);
            bolumDegerlerilistesi.Add(RasgeleDeger);
            kare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RasgeleDeger.ToString();
        }
    }

    void SoruPaneliniAç()
    {
        SoruyuSor();
        butona_basıldımı = true;
        Debug.Log(kacıncıSoru);
        SoruPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    void SoruyuSor()
    {
        bölensayı = Random.Range(2, 11);
        kacıncıSoru = Random.Range(0, bolumDegerlerilistesi.Count);
        dogru_sonuc = bolumDegerlerilistesi[kacıncıSoru];
        bölünensayı = bölensayı * dogru_sonuc;
        soruTexti.text = bölünensayı.ToString() + " : " + bölensayı.ToString();
        
        if (bölünensayı >=20)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else if (bölünensayı >=40 && bölensayı <= 60)
        {
            sorununZorlukDerecesi = "orta";
        }
        else
        {
            sorununZorlukDerecesi = "zor";
        }
    }
}