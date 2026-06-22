using UnityEngine;

public class Sound : MonoBehaviour
{
  public AudioClip jumpSound;
  public AudioClip doublejumpSound;
  private AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }
  

  void Update()
  {
    if(Input.GetButtonDown("Jump"))
    {
      playJumpSound();
    }
  }

  public void playJumpSound()
  {
    audioSource.PlayOneShot(jumpSound);
  }

  public void playDoubleJumpSound()
  {
    audioSource.PlayOneShot(doublejumpSound);
  }



}
