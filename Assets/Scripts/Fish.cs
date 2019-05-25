using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int startingValue;
    [SerializeField] private float startingWeight;
    [SerializeField] private float spawnRate;
    [SerializeField] private float range;
    private bool hooked = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (!hooked) {
            transform.position += new Vector3(0, floatSpeed, 0);
            transform.position -= transform.right * Time.deltaTime * movementSpeed;
            seekHook();
        } else {
            transform.position = anchor.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        hooked = true;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.None;
    }

    void seekHook() {
        float curDistance = Vector2.Distance(transform.position, anchor.transform.position);
        if (curDistance < range) {
            Vector2 direction = transform.position - anchor.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5.0f * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 5.0f * Time.deltaTime);
        }
    }
}
