using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [Header("Win Screen")]
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    [Header("Next Level")]
    [SerializeField] private string nextSceneName;

    private bool canContinue = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            winCanvas.SetActive(true);
            winPanel.SetActive(true);

            promptText.text = string.IsNullOrEmpty(nextSceneName)
                ? "Press X to Restart"
                : "Press X to Continue";

            canContinue = true;

            collision.GetComponent<PlayerMovement>().enabled = false;

            Rigidbody2D body = collision.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                body.linearVelocity = Vector2.zero;
                body.gravityScale = 0f; // stop them from continuing to fall too
            }

            Animator anim = collision.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("walk", false);
                anim.SetBool("grounded", true); // idle plays when walk=false and grounded=true
            }
        }
    }

    private void Update()
    {
        if (canContinue && Input.GetKeyDown(KeyCode.X))
        {
            canContinue = false;
            LoadNext();
        }
    }

    private void LoadNext()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}