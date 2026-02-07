using UnityEngine;

public class soundEffectManager : MonoBehaviour
{
    [SerializeField] private AudioClip driftSound;
    [SerializeField] private AudioClip pizzaShootSound;
    [SerializeReference] private AudioClip moneySound;

    [SerializeField] private AudioSource audioSource;

    [SerializeField][Range(0f, 1f)] private float driftVolume = 0.7f;
    [SerializeField][Range(0f, 1f)] private float pizzaShootVolume = .5f;
    [SerializeField][Range(0f, 1f)] private float moneyVolume = .5f;


    public void PlayDriftSound()
    {
        if (driftSound != null)
        {
            audioSource.PlayOneShot(driftSound, driftVolume);
        }
    }
    
    public void PlayPizzaShootSound()
    {
        if (pizzaShootSound != null)
        {
            audioSource.PlayOneShot(pizzaShootSound, pizzaShootVolume);
        }
    }

    public void PlayMoneySound()
    {
        if (moneySound != null)
        {
            audioSource.PlayOneShot(moneySound, moneyVolume);
        }
    }

}
