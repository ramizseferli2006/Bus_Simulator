using UnityEngine;

public class GasStation : MonoBehaviour
{
    [Header("Benzin Ayarları")]
    public float fillRate = 5f;
    public float currentBusFuel = 0f;
    public float maxBusFuel = 100f;
    public BusMovement aftobus;

    [Header("Tapança (Nozzle) Ayarları")]
    public Transform nozzle;
    public Transform pumpRestPosition;
    public Transform busFillPosition;

    private bool isPlayerInZone = false;
    private bool isRefueling = false;

    void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            isRefueling = !isRefueling;

            if (isRefueling)
            {
                nozzle.position = busFillPosition.position;
                nozzle.rotation = busFillPosition.rotation;
                aftobus.isRefueling = true;
            }
            else
            {
                nozzle.position = pumpRestPosition.position;
                nozzle.rotation = pumpRestPosition.rotation;
                aftobus.isRefueling = false;
            }
        }

        if (isRefueling)
        {
            aftobus.CurrentFuel += fillRate * Time.deltaTime;
            //Benzin səviyyəsi limiti aşmasın deyə
            aftobus.CurrentFuel = Mathf.Clamp(aftobus.CurrentFuel, 0f, aftobus.maxFuel);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<BusMovement>();
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            isRefueling = false;
            aftobus.isRefueling = false;

            nozzle.position = pumpRestPosition.position;
            nozzle.rotation = pumpRestPosition.rotation;
        }
    }
}