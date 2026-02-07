using UnityEngine;

public class upgradeSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip upgradeSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField][Range(0f, 1f)] private float upgradeVolume = 0.7f;

    public void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound, upgradeVolume);
        }
    }
}