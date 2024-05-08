using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGameManager : MonoBehaviour
{
    [Header("Functional References")]
    [SerializeField] private ScreenFader screenFader;

    public void Continue()
    {
        screenFader.FadeToColor("SampleScene");
    }

    public void MainMenu()
    {
        screenFader.FadeToColor("MainMenu");
    }
}
