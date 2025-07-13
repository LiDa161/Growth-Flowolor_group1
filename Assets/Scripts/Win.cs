using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject winCanvas; // Giao diện Win sẽ bật khi thắng

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("Win!");
            if (winCanvas != null)
            {
                winCanvas.SetActive(true);
            }
        }
    }
}
