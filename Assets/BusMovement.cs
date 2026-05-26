using UnityEngine;

public class BusMovement : MonoBehaviour
{
    public float motorgucu = 20000f;
    public float maksdonus = 35f;
    public float tormozgucu = 3000f;
    public float CurrentFuel;
    public float maxFuel = 100f;
    public float BenzinAzalmaMiqdari =0.1f;
    public bool isRefueling = false;

    public Transform onSolTeker;
    public Transform onSagTeker;
    public Transform arkaSolTeker;
    public Transform arkaSagTeker;

    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;
    private float tekerDonusBucagi = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ağırlıq mərkəzi: Y=-0.8f (aşmasın), Z=0.5f (orta-ön balans)
        rb.centerOfMass = new Vector3(0f, -0.8f, 0.5f);
        CurrentFuel = 0.7f * maxFuel;
        if (rb != null )
        {
            rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
        }
    }

    void Update()
    {
        GetInput();

        if (CurrentFuel > 0f && !isRefueling)
        {
            CurrentFuel -= BenzinAzalmaMiqdari * Time.deltaTime;

            CurrentFuel = Mathf.Clamp(CurrentFuel, 0f, maxFuel);
        }
    }

    void FixedUpdate()
    {
        if (CurrentFuel <= 0f)
        {
            verticalInput = 0f; 
        }
        HandleMotor();
        HandleSteering();
        UpdateWheelRotation();
    }

    
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    void HandleMotor()
    {
        
        float ireliSuret = transform.InverseTransformDirection(rb.linearVelocity).z;

        
        if (isBraking)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * 2f);
            rb.angularVelocity = Vector3.zero;
            return;
        }

        if (ireliSuret > 0.5f && verticalInput < 0f)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * 1.5f);
            rb.angularVelocity = Vector3.zero;  
        }
        // W ÜZƏRİNDƏN TORMOZ (Geriyə gedəndə W-ya basılarsa)
        else if (ireliSuret < -0.5f && verticalInput > 0f)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * 1.5f);
            rb.angularVelocity = Vector3.zero;
        }
        // 3. NORMAL QAZ VƏ GERİ ÖTÜRMƏ
        else
        {
            Vector3 force = transform.forward * verticalInput * motorgucu;
            rb.AddForce(force);
        }
    }

    void HandleSteering()
    {
        float suret = transform.InverseTransformDirection(rb.linearVelocity).z;

        // Sürət faktoru filtrimiz (0 ilə 1 arası)
        float suretFaktoru = Mathf.Clamp01(Mathf.Abs(suret) / 10f);

        // Qəti Şərt: Maşın tam dayanıbsa yerində fırlanmasın
        if (Mathf.Abs(suret) > 0.5f)
        {
            float donisIstiqaeti = Mathf.Sign(suret);
            float steerAngle = horizontalInput * maksdonus;

            transform.Rotate(Vector3.up * steerAngle * Time.fixedDeltaTime * 1.5f * suretFaktoru * donisIstiqaeti);
        }
    }

    void UpdateWheelRotation()
    {
        float suret = transform.InverseTransformDirection(rb.linearVelocity).z;

        tekerDonusBucagi += suret * Time.fixedDeltaTime * 360f;

        float steerAngle = horizontalInput * maksdonus;
        float yumsaqTekerDonusu = Mathf.Lerp(0f, steerAngle, 0.7f);

        // ÖN TƏKƏRLƏR (Yumşaq sükan fırlanması ilə)
        if (onSolTeker != null) onSolTeker.localRotation = Quaternion.Euler(tekerDonusBucagi, yumsaqTekerDonusu, 0f);
        if (onSagTeker != null) onSagTeker.localRotation = Quaternion.Euler(tekerDonusBucagi, yumsaqTekerDonusu, 0f);

        // ARXA TƏKƏRLƏR (Lokal ox xətası olmasın deyə Space.Self ilə fırladırıq)
        float donusSüreti = suret * Time.fixedDeltaTime * 360f;
        if (arkaSolTeker != null) arkaSolTeker.Rotate(Vector3.right * donusSüreti, Space.Self);
        if (arkaSagTeker != null) arkaSagTeker.Rotate(Vector3.right * donusSüreti, Space.Self);
    }
}