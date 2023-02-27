using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject strButton, endButton;

    private void Start()
    {
        FadeOut();
    }

    void FadeOut()
    {
        strButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        endButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("gameLevel");
    }
}
