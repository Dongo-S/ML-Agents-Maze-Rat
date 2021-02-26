using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScalePrinter : MonoBehaviour
{
    public UnityEngine.UI.Text textScale;

    public int timeScale;
    public bool ignoreInt;
    [Space]
    public UnityEngine.UI.Text winRateText;
    int wins;
    int loses;
    public void AddWin()
    {
        wins++;
        SetWinRate();
    }


    public void AddLose()
    {
        loses++;

        SetWinRate();
    }


    void SetWinRate()
    {
        winRateText.text = string.Format("Win rate: {0:0.00}%", ((float)wins / (wins + loses) * 100f));
        
    }
    private void Awake()
    {
        MazeAgent[] agents = FindObjectsOfType(typeof(MazeAgent)) as MazeAgent[];

        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].winRateText = this;
        }
    }

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
            Time.timeScale *= 2f;
            textScale.text = "Time Scale: " + Time.timeScale;
        }

    }
}
