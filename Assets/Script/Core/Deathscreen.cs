using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    private bool canRestart = false;

    // called by Health.cs when the player dies
    public void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        promptText.text = "Press X to Restart";
        canRestart = true;
    }

    // called by Health.cs if the player rewinds back to life after dying
    public void HideDeathScreen()
    {
        deathPanel.SetActive(false);
        canRestart = false;
    }

    private void Update()
    {
        if (canRestart && Input.GetKeyDown(KeyCode.X))
        {
            canRestart = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}