using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    private Canvas pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
        pauseCanvas.enabled = IsPaused;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0.0f : 1.0f;
        pauseCanvas.enabled = IsPaused;
    }
}
