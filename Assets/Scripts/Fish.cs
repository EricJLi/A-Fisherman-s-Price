using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int startingValue;
    [SerializeField] private float startingWeight;
    [SerializeField] private float spawnRate;
    private bool hooked = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (!hooked) {
            transform.position += new Vector3(0, floatSpeed, 0);
            transform.position -= transform.right * Time.deltaTime * movementSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        hooked = true;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.None;
    }
}
