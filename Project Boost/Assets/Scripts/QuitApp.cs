using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Çıktın he");
            Application.Quit();
            
            
        }
    }
    void Quit()
    {
        
        
        
    }
}
