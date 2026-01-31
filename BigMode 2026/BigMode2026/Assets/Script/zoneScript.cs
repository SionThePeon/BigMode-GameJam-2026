using UnityEngine;

public class zoneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            Debug.Log("PIZZAAA");
            Destroy(other.gameObject);
        }
    }
}
