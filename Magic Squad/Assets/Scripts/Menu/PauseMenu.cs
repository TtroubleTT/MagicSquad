using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject playerUI;

    private EnemyKills _enemyKills;

    private void Start()
    {
        _enemyKills = GameObject.FindGameObjectWithTag("KillUI").GetComponent<EnemyKills>();
    }
    
    private void Update()
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

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0;
        GameIsPause = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Home()
    {
        GameIsPause = false;
        _enemyKills.SaveData();
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
        _enemyKills.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        GameIsPause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
