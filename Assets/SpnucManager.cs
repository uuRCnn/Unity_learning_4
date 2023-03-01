using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpnucManager : MonoBehaviour
{
    public void OyunaYenidenBaşla()
    {
        SceneManager.LoadScene("gameLevel");
    }

    public void AnaMenuyeDön()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
