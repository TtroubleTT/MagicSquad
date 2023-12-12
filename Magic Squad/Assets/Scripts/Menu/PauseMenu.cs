using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject playerUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0;
        GameIsPause = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1;
        GameIsPause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
