using UnityEngine;

public class Pausable : MonoBehaviour
{
    public GameObject PauseMenu;
    private static bool isPaused = false;
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            isPaused = !isPaused;
            PauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    public void Resume() 
    {
        isPaused = false;
        PauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
