using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int template = 2;
    public int tutorial = 3;
    public int agile = 4;
    public int precise = 5;
    public int chaos = 6;
    public int pressure = 7;
    public void LoadTemplate()
    {
        SceneManager.LoadScene(template);
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene(tutorial);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(agile);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(precise);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(chaos);
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene(pressure);
    }
}
