using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    // Add this public property for external access
    public float RemainingTime
    {
        get => remainingTime;
        set => remainingTime = value;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;        
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            // GameOver();
            timerText.color = Color.red;
            EndGame();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndGame()

    {   
        if (gameHasEnded == true)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




}


