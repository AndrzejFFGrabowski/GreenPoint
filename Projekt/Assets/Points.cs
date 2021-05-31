using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    
    public int second = 0;
    public int minute = 0;
    public int hundreth = 0;
    private string zeroS = "";
    private string zeroH = "";
    void Start()
    {
        second = PlayerPrefs.GetInt("secondes");
        minute = PlayerPrefs.GetInt("minutes");
        hundreth = PlayerPrefs.GetInt("hundreths");
        if (hundreth < 10) zeroH = "0";
        if (second < 10) zeroS = "0";
        GetComponent<UnityEngine.UI.Text>().text = minute.ToString() + ":" + zeroS + second.ToString() + ":" + zeroH + hundreth.ToString();
    }
}
