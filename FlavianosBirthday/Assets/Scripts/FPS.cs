using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    // Start is called before the first frame update

    public int avgFrameRate;
    public Text display_Text;
    

    /*private void Start()
    {
        Debug.Log("FPS = 60 (start)");
    }*/

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        display_Text.text = "FPS: "+avgFrameRate.ToString();
    }
}
