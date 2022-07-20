using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText, bestTimeText;
    public GameObject titleScreen, jumpButton, slideButton, timerDisplay, deathScreen, bestTimeObject;
    public ObjHandler objHandler;
    public bool gameActive;
    [SerializeField] private float gameTime, bestTime;

    // Start is called before the first frame update
    void Start()
    {
        bestTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            UpdateTimer();
            if (gameTime > bestTime)
            {
                bestTimeObject.SetActive(false);
            }
        }

        /*if (Input.GetKeyDown("e"))
        {
            EndGame();
        }*/
    }

    public void StartGame()
    {
        gameActive = true;

        titleScreen.SetActive(false);
        jumpButton.SetActive(true);
        slideButton.SetActive(true);
        timerDisplay.SetActive(true);
        objHandler.PlayGame();
    }

    public void EndGame()
    {
        gameActive = false;

        jumpButton.SetActive(false);
        slideButton.SetActive(false);
        deathScreen.SetActive(true);
    }

    public void RestartGame()
    {
        if (gameTime > bestTime)
        {
            bestTime = gameTime;
            HighestTimeUpdate();
        }
        bestTimeObject.SetActive(true);

        gameActive = true;

        jumpButton.SetActive(true);
        slideButton.SetActive(true);
        deathScreen.SetActive(false);

        gameTime = 0;
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

    private void HighestTimeUpdate()
    {
        // This calculation is split in two, because for some reason, doing them in one line creates miscalculations, but separately it's fine
        int fraction = Mathf.FloorToInt(bestTime * 100);
        fraction %= 100;

        int seconds = (int)bestTime % 60;
        int minutes = (int)bestTime / 60;

        bestTimeText.text = "Best Time: " + minutes + ":" + seconds.ToString("00") + ":" + fraction.ToString("00");
    }
}
