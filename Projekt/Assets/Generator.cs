using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class Generator : MonoBehaviour
{
    public Rigidbody bulletsProjectile;
    public Rigidbody lifePointsProjectile;
    private Random random;
    public Camera BackCamera;
    public Camera HeadCamera;
    public AudioSource audioSource;
    public GameObject levelText;
    private float updateInterval = 2.00f;
    void Start()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            switch (PlayerPrefs.GetInt("level"))
            {
                case 1:
                    updateInterval = 2.5f;
                    break;
                case 3:
                    updateInterval = 1.5f;
                    break;
                default:
                    break;
            }
        }
        InvokeRepeating("UpdateInterval", updateInterval, updateInterval);
        levelText = GameObject.Find("Canvas/Header/Level");
        setLevelText();
        random = new Random();
        if (PlayerPrefs.HasKey("sound")) audioSource.volume = PlayerPrefs.GetFloat("sound");
    }

    void UpdateInterval()
    {
        Rigidbody clone;
        if (random.Next(10) == 0)
        {
            clone = Instantiate(lifePointsProjectile, generatingPoint(), transform.rotation);
        }
        else
        {
            clone = Instantiate(bulletsProjectile, generatingPoint(), transform.rotation);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (HeadCamera.enabled)
            {
                HeadCamera.enabled = false;
                BackCamera.enabled = true;
            }
            else
            {
                HeadCamera.enabled = true;
                BackCamera.enabled = false;
            }

        }

    }


    Vector3 generatingPoint()
    {
        int x = 9;
        int z = 9;
        if (random.Next(2) == 1) x = -x;
        if (random.Next(2) == 1) z = -z;
        return new Vector3(x, 5, z);
    }
    void setLevelText()
    {
        if ((!PlayerPrefs.HasKey("level")) || PlayerPrefs.GetInt("level") == 2)
        {
            levelText.GetComponent<UnityEngine.UI.Text>().text = "Medium";
        }
        else
        {
            if (PlayerPrefs.GetInt("level") == 1)
            {
                levelText.GetComponent<UnityEngine.UI.Text>().text = "Easy";
            }
            if (PlayerPrefs.GetInt("level") == 3)
            {
                levelText.GetComponent<UnityEngine.UI.Text>().text = "Hard";
            }
        }


    }
}
