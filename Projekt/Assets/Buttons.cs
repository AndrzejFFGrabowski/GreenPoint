using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Menu")
                {
                    SceneManager.LoadScene("Menu");
                }
                if (hit.transform.name == "PlayAgain")
                {
                    SceneManager.LoadScene("level0");
                }
                if (hit.transform.name == "Exit")
                {
                    Application.Quit();
                }
            }
        }
    }
}
