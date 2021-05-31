using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                if (hit.transform.name == "Play") {  
                    SceneManager.LoadScene("level0");  
                }
                if (hit.transform.name == "Exit") {
                    Application.Quit();
                } 
                if (hit.transform.name == "Options") {
                    SceneManager.LoadScene("Options"); 
                }
                if (hit.transform.name == "HighScores") {
                    SceneManager.LoadScene("HighScores"); 
                }
                if (hit.transform.name == "HowToPlay") {
                    SceneManager.LoadScene("Instruction"); 
                }    
            }
        }
    }
}
