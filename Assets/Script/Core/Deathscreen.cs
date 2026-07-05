using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    private bool canRestart = false;

    public void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        promptText.text = "Press X to Restart";
        canRestart = true;
        // don't freeze time — just stop the player via PlayerMovement.enabled = false
        // which is already done in Health.cs
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