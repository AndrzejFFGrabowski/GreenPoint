using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsButtons : MonoBehaviour
{
    public Dropdown dropdown;
    public Scrollbar scrollbar;
    void Start()
    {
        scrollbar.onValueChanged.AddListener((float val) => SetVolume(val));
        if (PlayerPrefs.HasKey("sound")) scrollbar.value = PlayerPrefs.GetFloat("sound");
        dropdown.onValueChanged.AddListener((int val) => SetLevel(val));
        if (PlayerPrefs.HasKey("level")) dropdown.value = PlayerPrefs.GetInt("level") - 1;
    }


    public void SetLevel(int difficulty)
    {
        PlayerPrefs.SetInt("level", difficulty + 1);
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("sound", value);
    }

    public void MenuOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

}
