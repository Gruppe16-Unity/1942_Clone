using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Stop the time to pause the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the time to unpause the game
        isPaused = false;
    }


    public void OpenOptions()
    {
        // Open the options menu (You can replace this with your own implementation)
        Debug.Log("Options menu opened");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}