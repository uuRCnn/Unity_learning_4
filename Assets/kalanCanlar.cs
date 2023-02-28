using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kalanCanlar : MonoBehaviour
{
    [SerializeField] private GameObject kalanhak1, kalanhak2, kalanhak3;

    public void Kalan_Hakları_Kontrol_et(int kalan_hak)
    {
        switch (kalan_hak)
        {
            case 3:
                kalanhak1.SetActive(true);
                kalanhak2.SetActive(true);
                kalanhak3.SetActive(true);
                break;
            case 2:
                kalanhak1.SetActive(true);
                kalanhak2.SetActive(true);
                kalanhak3.SetActive(false);
                break;
            case 1:
                kalanhak1.SetActive(true);
                kalanhak2.SetActive(false);
                kalanhak3.SetActive(false);
                break;
            case 0:
                kalanhak1.SetActive(false);
                kalanhak2.SetActive(false);
                kalanhak3.SetActive(false);
                break;
        }   
    }
}
