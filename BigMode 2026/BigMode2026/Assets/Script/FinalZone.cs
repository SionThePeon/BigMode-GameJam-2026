using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Pizza"))
        {
            SceneManager.LoadScene("FinalScene");        
        }
    }
    
    
}
