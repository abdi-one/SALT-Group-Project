using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [Header("Win Screen")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    private bool canRestart = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShowWinScreen();
        }
    }

    private void ShowWinScreen()
    {
        winPanel.SetActive(true);
        promptText.text = "Press X to Restart";
        canRestart = true;
        // disable player movement on win
        FindObjectOfType<PlayerMovement>().enabled = false;
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