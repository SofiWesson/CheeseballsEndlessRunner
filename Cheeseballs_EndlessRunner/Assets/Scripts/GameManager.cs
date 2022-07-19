using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public bool gameActive;
    [SerializeField] private float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            //UpdateTimer();
        }
    }

    public void StartGame()
    {
        gameActive = true;
    }

    /*private void UpdateTimer()
    {
        gameTime = gameTime <= 100 ? gameTime + Time.deltaTime : 0;
        int milliseconds = (int)gameTime; 
        //int millisecondstens = Mathf.FloorToInt(gameTime * 100 % 100 / 10);
        int seconds =  //Mathf.FloorToInt(gameTime % 60 % 10);

        int minutes = Mathf.FloorToInt(gameTime / 60);
        int secondstens = Mathf.FloorToInt(gameTime % 60 / 10);

        timerText.text = "Time: " + minutes + ":" + secondstens + seconds + ":" + milliseconds;
    }

    string FormatTime(float time)
    {
        int intTime = (int)time;
        int tempName = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = String.Format("{0:00}:{1:00}:{2:000}", tempName, seconds, fraction);
        return timeText;
    }*/
}
