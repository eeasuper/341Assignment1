using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float boundary = 1f;
    [SerializeField] private float movementSpeed = 2f;
    private float x = 1f;
    private float originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position.x;
        x *= movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(x * Time.deltaTime, 0f, 0f));
        if (Mathf.Abs(transform.position.x - originalPosition) > boundary)
            x *= -1f;
    }
}
