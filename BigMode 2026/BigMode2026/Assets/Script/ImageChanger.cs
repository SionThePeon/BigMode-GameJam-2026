using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Sprite[] barSprites;
    [SerializeField] Image imageComponent;
    private int currentIndex = 0;

    public void spriteChange()
    {
        if (currentIndex < barSprites.Length - 1)
        {
            currentIndex++;
            imageComponent.sprite = barSprites[currentIndex];
        }
    }

}
