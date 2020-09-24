using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("InfiniteRunner");
    }
    public void OnControls()
    {
        SceneManager.LoadScene("Controls");
    }
}
