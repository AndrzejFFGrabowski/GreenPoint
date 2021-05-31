using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
public class GetName : MonoBehaviour
{
    private int lastMinute;
    private int lastSecond;
    private int lastHundreth;
    private string name;
    public GameObject getText;
    private int lineNr;
    private List<string> listA = new List<string>();
    private List<string> listB = new List<string>();
    private List<string> listC = new List<string>();
    private List<string> listD = new List<string>();
    private List<string> listE = new List<string>();
    private bool log = true;
    void Start()
    {
        getText = GameObject.Find("Plane/Canvas/InputField");
        lastSecond = PlayerPrefs.GetInt("secondes");
        lastMinute = PlayerPrefs.GetInt("minutes");
        lastHundreth = PlayerPrefs.GetInt("hundreths");
        lineNr = PlayerPrefs.GetInt("count");
    }

    public void OnClick()
    {
        name = getText.GetComponent<UnityEngine.UI.InputField>().text;
        readScores();
        writeScores();
        SceneManager.LoadScene("GameOver");
    }

    private void readScores()
    {
        int count = 0;
        StreamReader reader = new StreamReader(File.OpenRead(Application.persistentDataPath+"/HighScores.csv"));
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');
            if (log && count == lineNr)
            {
                log = false;
                addPlayerScore();
            }
            listA.Add(values[0]);
            listB.Add(values[1]);
            listC.Add(values[2]);
            listD.Add(values[3]);
            listE.Add(values[4]);
            count++;
        }
        if (log)
        {
            addPlayerScore();
            count++;
        }
        reader.Close();
    }
    private void writeScores()
    {
        var csv = new StringBuilder();
        string newLine;
        int max = listA.Count;
        if (max > 8) max = 8;
        for (int i = 0; i < max; i++)
        {
            newLine = listA[i] + "; " + listB[i] + "; " + listC[i] + "; " + listD[i] + "; " + listE[i];
            csv.AppendLine(newLine);
        }
        File.WriteAllText(Application.persistentDataPath+"/HighScores.csv", csv.ToString());
    }

    private void addPlayerScore()
    {
        listA.Add(name);
        listB.Add(lastMinute.ToString());
        listC.Add(lastSecond.ToString());
        listD.Add(lastHundreth.ToString());
        listE.Add(level());
    }
    private string level()
    {
        if ((!PlayerPrefs.HasKey("level")) || PlayerPrefs.GetInt("level") == 2)
        {
            return "Medium";
        }
        if (PlayerPrefs.GetInt("level") == 1)
        {
            return "Easy";
        }
        return "Hard";
    }


}
