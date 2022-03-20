using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text lifeText;
    public int maxLifeCount = 10;
    private int lifeRechargeRate = 1;

    public int lives;

    public const string LifeCountKey = "LifeCount";
    public const string LifeRechargeKey = "LifeRecharge";

    void Start()
    {
        OnApplicationFocus(true);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus) { return; }

        CancelInvoke(); 

        lives = PlayerPrefs.GetInt(LifeCountKey, maxLifeCount);
        lifeText.text = "Lives " + (lives).ToString();

        if (lives <= 0)
        {
            string livesReadyString = PlayerPrefs.GetString(LifeRechargeKey, string.Empty);
            if (livesReadyString == string.Empty) { return; }

            System.DateTime livesReady = System.DateTime.Parse(livesReadyString);

            if (System.DateTime.Now > livesReady)
            {
                Recharge();
            }
            else
            {
                Invoke("Recharge", (livesReady - System.DateTime.Now).Seconds);
            }
        }
    }

    private void Recharge()
    {
        lives = maxLifeCount;
        PlayerPrefs.SetInt(LifeCountKey, lives);
    }

    public void UseLife()
    {
        int currentLifeCount = PlayerPrefs.GetInt(LifeCountKey, 0);
        int newLifeCount = currentLifeCount - 1; 
        PlayerPrefs.SetInt(LifeCountKey, newLifeCount);

        if(currentLifeCount <=0)
        {
            Invoke("QuitToMenu", 1f);

            System.DateTime livesRecharged = System.DateTime.Now.AddMinutes(lifeRechargeRate);
            PlayerPrefs.SetString(LifeRechargeKey, livesRecharged.ToString());
        }
    }
}
