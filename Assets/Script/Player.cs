using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpPower = 5f;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Vector3 originalPosition;
    private Rigidbody rigidBodyComponent;

    private const int ENEMY = 7;
    private const int COIN = 8;

    // Start is called before the first frame update
    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < -5f)
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
                jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // This is run every physics update
    private void FixedUpdate() {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * speed, rigidBodyComponent.velocity.y, 0);
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        if (jumpKeyWasPressed) {
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == ENEMY)
        {
            RestartGame();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == COIN)
        {
            Destroy(other.gameObject);
            // TODO: add points here
        }
    }

    private void RestartGame()
    {
        transform.position = originalPosition;
    }
}
