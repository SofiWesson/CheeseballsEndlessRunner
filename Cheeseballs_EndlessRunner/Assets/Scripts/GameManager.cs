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
            UpdateTimer();
        }
    }

    public void StartGame()
    {
        gameActive = true;
    }

    public void EndGame()
    {
        gameActive = false;
        // Functionality here :P
    }

    private void UpdateTimer()
    {
        gameTime += Time.deltaTime;

        // This calculation is split in two, because for some reason, doing them in one line creates miscalculations, but separately it's fine
        int fraction = Mathf.FloorToInt(gameTime * 100);
        fraction %= 100;

        int seconds = (int)gameTime % 60;
        int minutes = (int)gameTime / 60;

        timerText.text = "Time: " + minutes + ":" + seconds.ToString("00") + ":" + fraction.ToString("00");
    }
}
