using UnityEngine;

public class PlayerRewind : MonoBehaviour
{
    [Header("Rewind Keys")]
    [SerializeField] private KeyCode setKey = KeyCode.R;
    [SerializeField] private KeyCode teleportKey = KeyCode.F;

    [Header("After-image")]
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private Color afterImageColor = new Color(1f, 1f, 1f, 0.5f);

    private GameObject currentAfterImage;
    private Vector3 savedPosition;
    private float savedHealth;
    private bool hasSavedPoint;

    private Health health;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();

        // loud, obvious errors if anything required is missing
        if (health == null)
            Debug.LogError("[PlayerRewind] No Health component found on " + gameObject.name);
        if (body == null)
            Debug.LogError("[PlayerRewind] No Rigidbody2D component found on " + gameObject.name);
        if (afterImagePrefab == null)
            Debug.LogError("[PlayerRewind] After Image Prefab is not assigned in the Inspector!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(setKey))
        {
            Debug.Log("[PlayerRewind] R pressed - setting rewind point");
            SetRewindPoint();
        }

        if (Input.GetKeyDown(teleportKey))
        {
            Debug.Log("[PlayerRewind] F pressed - teleporting to rewind point");
            TeleportToRewindPoint();
        }
    }

    private void SetRewindPoint()
    {
        if (afterImagePrefab == null)
        {
            Debug.LogWarning("[PlayerRewind] Cannot set rewind point - After Image Prefab is missing.");
            return;
        }

        savedPosition = transform.position;
        savedHealth = health != null ? health.currentHealth : 0f;
        hasSavedPoint = true;

        if (currentAfterImage == null)
        {
            currentAfterImage = Instantiate(afterImagePrefab, savedPosition, transform.rotation);
            Debug.Log("[PlayerRewind] Ghost spawned at " + savedPosition);
        }
        else
        {
            currentAfterImage.transform.position = savedPosition;
            Debug.Log("[PlayerRewind] Ghost moved to " + savedPosition);
        }

        SpriteRenderer ghostRenderer = currentAfterImage.GetComponent<SpriteRenderer>();
        if (ghostRenderer != null && spriteRenderer != null)
        {
            ghostRenderer.sprite = spriteRenderer.sprite;
            ghostRenderer.flipX = spriteRenderer.flipX;
            ghostRenderer.color = afterImageColor;
        }
        else if (ghostRenderer == null)
        {
            Debug.LogWarning("[PlayerRewind] The After Image Prefab has no SpriteRenderer component!");
        }
    }

    private void TeleportToRewindPoint()
    {
        if (!hasSavedPoint)
        {
            Debug.LogWarning("[PlayerRewind] No rewind point set yet - press R first.");
            return;
        }

        transform.position = savedPosition;
        if (health != null)
            health.SetHealth(savedHealth);
        if (body != null)
            body.linearVelocity = Vector2.zero;

        Debug.Log("[PlayerRewind] Teleported to " + savedPosition);
    }
}
