using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public Transform playerCamera;
    public Camera cam;

    private float xRotation = 0f;
    private CharacterController characterController;
    private Vector3 velocity;
    private float gravity = -9.81f;


    public GameObject effectManager;
    public effect activeEffect;

    public GameObject pannelManager;
    public pannel activePannel;
    
    public bool effectActive;
    public GameObject effectIndicator, rotateIndicator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Ascunde cursorul mouse-ului în timpul jocului
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. ROTIRE CAMERA (LOOK AROUND)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitează privirea sus/jos

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 2. MIȘCARE (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // 3. GRAVITAȚIE SIMPLĂ (să nu plutească pe scări/podea)
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if(effectActive)
        {
            for(int i = 0; i < effectManager.transform.childCount; i++)
            {
                if(effectManager.transform.GetChild(i).name == activeEffect.ToString())
                {
                    effectManager.transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            for(int i = 0; i < pannelManager.transform.childCount; i++)
            {
                if(pannelManager.transform.GetChild(i).name == activePannel.ToString())
                {
                    pannelManager.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            
        }
        else
        {
            for(int i = 0; i < effectManager.transform.childCount; i++)
            {
                if(effectManager.transform.GetChild(i).name == activeEffect.ToString())
                {
                    effectManager.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            for(int i = 0; i < pannelManager.transform.childCount; i++)
            {
                if(pannelManager.transform.GetChild(i).name == activePannel.ToString())
                {
                    pannelManager.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(effectActive)
            {
                effectActive = false;
                effectIndicator.SetActive(false);

            }
            else
            {
                effectActive = true;
                effectIndicator.SetActive(true);
            }
        }

        // Raycast pentru a detecta camera din fața jucătorului
        Debug.DrawRay(
            Camera.main.transform.position,
            Camera.main.transform.forward * 100f,
            Color.red
        );
        
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
        {
           
            if (hit.collider.CompareTag("camera"))
            {
                rotateIndicator.SetActive(true);
                if(Input.GetKey(KeyCode.Q))
                {
                    hit.transform.Rotate(Vector3.down, 90f * Time.deltaTime, Space.World);
                }
            }
            else
            {
                rotateIndicator.SetActive(false);
            }
        }
        
    }
}