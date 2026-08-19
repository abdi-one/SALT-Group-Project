using UnityEngine;

public class EscMenuFun : MonoBehaviour
{
    public GameObject content;

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.P))
      {
        content.SetActive(true);
        Time.timeScale = 0;
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
        content.SetActive(false);
        Time.timeScale = 1;
      }

    }

    
    public void MainMenuButton()
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void MuteToggle(bool mute)
    {
      if(mute)
      {
        AudioListener.volume = 0;
      }
      else
      {
        AudioListener.volume = 1;
      }
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#endif 
      Application.Quit();
    }

}
