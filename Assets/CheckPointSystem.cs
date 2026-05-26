using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition;

    public GameObject endNPCs;

    void Start()
    {
        if (lastCheckpointPosition ==Vector3.zero)
        {
            lastCheckpointPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lastCheckpointPosition = transform.position;
            Debug.Log("Checkpoint qeyd olundu!");

            if (endNPCs != null)
            {
                endNPCs.SetActive(true);
            }

            if (transform.parent != null)
            {
                transform.parent.gameObject.SetActive(false);
            }
            else 
            {
                gameObject.SetActive(false);
            }
        }
    }
}
