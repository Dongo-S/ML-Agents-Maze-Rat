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
        textScale.text = "Time Scale: " + Time.timeScale;

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 5f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 10f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 15f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Time.timeScale = 20f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Time.timeScale = 25f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Time.timeScale = 30f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Time.timeScale = 35f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Time.timeScale = 40f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 45f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }

    }
}
