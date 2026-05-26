using UnityEngine;

public class BusStop : MonoBehaviour
{
    [Header("Astanofka ayarları")]
    public bool isDropOffStation = false;

    [Header("NPC qovluğu")]
    public Transform npcContainer;

    private void Start()
    {
        if (npcContainer != null)
        {
            foreach (Transform npc in npcContainer)
            {
                if (isDropOffStation)
                {
                    npc.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleNPCs();
        }
    }

    private void ToggleNPCs()
    {
        if (npcContainer != null)
        {
            foreach (Transform npc in npcContainer)
            {
                npc.gameObject.SetActive(isDropOffStation);
            }
        }
    }
}