using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Sensibilidad de la cámara
    public float mouseSensitivity = 100f;

    // Velocidad de movimiento del jugador
    public float moveSpeed = 5f;

    // Referencia a la cámara del jugador
    public Transform playerCamera;

    // Control de la rotación de la cámara en el eje vertical
    private float xRotation = 0f;

    // Audio de los pasos
    public AudioSource footstepsAudioSource;

    // Control del movimiento para el sonido
    private bool isMoving;

    void Start()
    {
        // Bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movimiento de la cámara (primera persona)
        RotateCamera();

        // Movimiento del jugador (caminar)
        MovePlayer();

        // Control de sonido de pasos
        HandleFootstepsSound();
    }

    void RotateCamera()
    {
        // Capturar movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ajustar la rotación en el eje X (para mirar hacia arriba y abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limita la rotación vertical

        // Rotación de la cámara
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación del jugador en el eje Y (para mirar a los lados)
        transform.Rotate(Vector3.up * mouseX);
    }

    void MovePlayer()
    {
        // Capturar entrada de movimiento (WASD o flechas)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcular dirección del movimiento
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Aplicar movimiento al jugador
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Comprobar si el jugador está moviéndose
        isMoving = moveDirection != Vector3.zero;
    }

    void HandleFootstepsSound()
    {
        if (isMoving && !footstepsAudioSource.isPlaying)
        {
            footstepsAudioSource.Play(); // Inicia el sonido de pasos
        }
        else if (!isMoving && footstepsAudioSource.isPlaying)
        {
            footstepsAudioSource.Pause(); // Pausa el sonido de pasos
        }
    }
}
