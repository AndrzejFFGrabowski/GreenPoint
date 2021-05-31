using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;
public class PlayerLogic : MonoBehaviour
{
    public CharacterController characterController;
    public AudioSource audioSource;
    private bool run;
    public int life;
    public int lifePoint;
    private static float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    private int x;
    private int y;
    private int z;
    public GameObject LifeCounter;
    public GameObject LifePointCounter;
    public GameObject TimeCounter;
    void Start()
    {
        LifeCounter = GameObject.Find("Canvas/Header/LifeDisplay");
        LifePointCounter = GameObject.Find("Canvas/Header/LifePointDisplay");
        TimeCounter = GameObject.Find("Canvas/Header/Clock");
        if (PlayerPrefs.HasKey("sound")) audioSource.volume = PlayerPrefs.GetFloat("sound");
        life = 3;
        lifePoint = 0;
        run = true;
    }
    void Update()
    {
        if (run)
        {
            movement();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LifePointTag")
        {
            audioSource.Play();
            lifePoint++;
            LifePointCounter.GetComponent<UnityEngine.UI.Text>().text = lifePoint.ToString();
            Destroy(collision.gameObject);
            if (lifePoint == 3)
            {
                lifePoint = 0;
                LifePointCounter.GetComponent<UnityEngine.UI.Text>().text = lifePoint.ToString();
                life++;
                LifeCounter.GetComponent<UnityEngine.UI.Text>().text = life.ToString();
            }
        }
        if (collision.gameObject.tag == "BulletTag")
        {
            audioSource.Play();
            if (life != 0)
            {
                life--;
                LifeCounter.GetComponent<UnityEngine.UI.Text>().text = life.ToString();
                Destroy(collision.gameObject);
            }
            else
            {
                endGame();
            }
        }
    }

    void movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            z++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            z--;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            y--;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            y++;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            x--;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            x++;
        }
        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            x = 0;
            y = 0;
            z = 0;
        }
        moveDirection = new Vector3(x, y, z);
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);

    }
    private void endGame()
    {
        run = false;
        PlayerPrefs.SetInt("secondes", TimeCounter.GetComponent<Clock>().second);
        PlayerPrefs.SetInt("hundreths", TimeCounter.GetComponent<Clock>().hundreth);
        PlayerPrefs.SetInt("minutes", TimeCounter.GetComponent<Clock>().minute);
        if (readScores())
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene("EnterYourName");
        }
    }
    private bool readScores()
    {

        if (File.Exists(Application.persistentDataPath + "/HighScores.csv"))
        {
            StreamReader reader = new StreamReader(File.OpenRead(Application.persistentDataPath + "/HighScores.csv"));
            int count = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                if (check(Int32.Parse(values[1]), Int32.Parse(values[2]), Int32.Parse(values[3])))
                {
                    reader.Close();
                    PlayerPrefs.SetInt("count", count);
                    return false;
                }
                count++;
            }
            reader.Close();
            if (count < 8)
            {
                PlayerPrefs.SetInt("count", count);
                return false;
            }
            return true;
        }
        else
        {
            File.Create(Application.persistentDataPath + "/HighScores.csv").Dispose();
            PlayerPrefs.SetInt("count", 0);
            return false;
        }

    }
    private bool check(int M, int S, int H)
    {
        if (M > TimeCounter.GetComponent<Clock>().minute) return false;
        if (M < TimeCounter.GetComponent<Clock>().minute) return true;
        if (S > TimeCounter.GetComponent<Clock>().second) return false;
        if (S < TimeCounter.GetComponent<Clock>().second) return true;
        if (H > TimeCounter.GetComponent<Clock>().hundreth) return false;
        if (H < TimeCounter.GetComponent<Clock>().hundreth) return true;
        return false;
    }

}
