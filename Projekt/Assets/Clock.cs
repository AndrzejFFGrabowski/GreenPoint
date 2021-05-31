using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public int second = 0;
    public int minute = 0;
    public int hundreth = 0;
    Pause pause;
    private string zeroS = "0";
    private string zeroH = "0";
    private float updateInterval = 0.01f;
    void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<Pause>();
        InvokeRepeating("UpdateInterval", updateInterval, updateInterval);
    }
    void UpdateInterval()
    {
        if (!pause.gameIsPaused)
        {
            hundreth++;
            if (hundreth == 10) zeroH = "";
            if (hundreth == 100)
            {
                hundreth = 0;
                zeroH = "0";
                second++;
                if (second == 10) zeroS = "";
                if (second == 60)
                {
                    minute++;
                    second = 0;
                    zeroS = "0";
                }
            }
            GetComponent<UnityEngine.UI.Text>().text = minute.ToString() + ":" + zeroS + second.ToString() + ":" + zeroH + hundreth.ToString();
        }
    }
}
