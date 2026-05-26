using UnityEngine;

public class BusRespawn : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = CheckPointSystem.lastCheckpointPosition;

        Rigidbody busRb = GetComponent<Rigidbody>();
        if (busRb != null)
        {
            busRb.linearVelocity =Vector3.zero;
            busRb.angularVelocity = Vector3.zero;
        }
    }
}
