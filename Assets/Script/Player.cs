using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private
        Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float score;
    [SerializeField] float pointsPerCollectable = 25;
    [SerializeField] private Text scoreText;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpPower = 5f;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Vector3 originalPosition;
    private Rigidbody rigidBodyComponent;
    private GameObject lastCoinTouched;

    private const int ENEMY = 7;
    private const int COIN = 8;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one player instance");
        }
        //This means this game can ever only have 1 player.
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        score = 0;
        UpdateScoreDisplay();
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
            //Vector3 position = other.transform.position;
            //Destroy(other.gameObject);
            // TODO: add points here
            score += pointsPerCollectable;
            UpdateScoreDisplay();

            //Debug.Log(other.gameObject.GetComponentIndex(Coin));
            lastCoinTouched = other.gameObject;
            OnCoinTouched?.Invoke(this, new OnCoinTouchedEventArgs {
                coin = lastCoinTouched
            });
        }
    }

    private void RestartGame()
    {
        transform.position = originalPosition;
        score = 0;
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay() {
        scoreText.text = "Score " + score;

    }

    public event EventHandler<OnCoinTouchedEventArgs> OnCoinTouched;
    public class OnCoinTouchedEventArgs : EventArgs {
        public GameObject coin;
    }

}
