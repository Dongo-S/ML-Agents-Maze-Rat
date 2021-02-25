using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScalePrinter : MonoBehaviour
{
    public UnityEngine.UI.Text textScale;

    public int timeScale;
    public bool ignoreInt;

    private void Start()
    {
        if (!ignoreInt)
            Time.timeScale = timeScale;
        textScale.text = "Time Scale: "+Time.timeScale;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
