using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class GetScores : MonoBehaviour
{
    private int lastMinute;
    private int lastSecond;
    private int lastHundreth;
    public GameObject namesText;
    public GameObject scoresText;
    public GameObject levelText;

    void Start()
    {
        namesText = GameObject.Find("Plane/Canvas/Names");
        scoresText = GameObject.Find("Plane/Canvas/Scores");
        levelText = GameObject.Find("Plane/Canvas/Level");
        readScores();
    }

    private void readScores()
    {
        StreamReader reader = new StreamReader(File.OpenRead(Application.persistentDataPath+"/HighScores.csv"));
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');
            writeText(values[0], Int32.Parse(values[1]), Int32.Parse(values[2]), Int32.Parse(values[3]), values[4]);
        }
        reader.Close();
    }
    private bool check(List<int> M, List<int> S, List<int> H)
    {
        if (M.LastOrDefault() > lastMinute) return false;
        if (S.LastOrDefault() > lastSecond) return false;
        if (H.LastOrDefault() > lastHundreth) return false;
        return true;
    }
    private void writeText(string names, int minute, int second, int hundreth, string level)
    {
        string zeroH = "";
        string zeroS = "";
        if (hundreth < 10) zeroH = "0";
        if (second < 10) zeroS = "0";
        scoresText.GetComponent<UnityEngine.UI.Text>().text = scoresText.GetComponent<UnityEngine.UI.Text>().text + "\n" + minute + ":" + zeroS + second + ":" + zeroH + hundreth;
        namesText.GetComponent<UnityEngine.UI.Text>().text = namesText.GetComponent<UnityEngine.UI.Text>().text + "\n" + names;
        levelText.GetComponent<UnityEngine.UI.Text>().text = levelText.GetComponent<UnityEngine.UI.Text>().text + "\n" + level;

    }
    public void OnClick()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Clean()
    {
        File.WriteAllText(Application.persistentDataPath+"/HighScores.csv", "");
        scoresText.GetComponent<UnityEngine.UI.Text>().text = "";
        namesText.GetComponent<UnityEngine.UI.Text>().text = "";
        levelText.GetComponent<UnityEngine.UI.Text>().text = "";
    }
}
