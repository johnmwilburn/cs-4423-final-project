using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private ScreenFader screenFader;

    public void Play(){
        SceneManager.LoadScene("SampleScene");
        // screenFader.FadeToColor("SampleScene");
    }

    public void Quit(){
        Application.Quit();
    }
}
