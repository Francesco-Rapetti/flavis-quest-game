using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Version : MonoBehaviour
{
    public Text versioneApp;

    // Start is called before the first frame update
    void Start()
    {
        versioneApp.text = Application.version.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
