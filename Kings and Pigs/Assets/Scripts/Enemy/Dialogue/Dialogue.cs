using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public AudioClip alert;

    private Animator animator;
    private AudioSource audioSource;

    private bool firstEncounter = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerEncounter()
    {
        if (firstEncounter)
        {
            audioSource.clip = alert;
            audioSource.Play();

            animator.SetTrigger("EncounterPlayer");
            firstEncounter = false;
        }
    }

    public void ResetFirstEncounter()
    {
        firstEncounter = true;
    }
}
