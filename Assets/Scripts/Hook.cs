using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

    [SerializeField] private float lineSpeed;
    private LineRenderer lineRenderer;
    private Vector2 startPos;
    private bool isDragging;
    private Vector2 touchDelta;
    private List<Fish> fishies;

    private Vector3 min;
    private Vector3 max;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        fishies = new List<Fish>();
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    void Update() {
        // if (Input.touchCount > 0) {
        //     Touch touch = Input.GetTouch(0);
        //     if (touch.phase == TouchPhase.Began) {
        //         startPos = touch.position;
        //     } else if (touch.phase == TouchPhase.Moved) {
        //         touchDelta = touch.position - startPos;
        //     } else if (touch.phase == TouchPhase.Ended) {
        //         startPos = new Vector2(0, 0);
        //         isDragging = false;
        //         touchDelta = new Vector2(0, 0);
        //     }
        // }

        if (Input.GetMouseButtonDown(0) && !isDragging) {
            startPos = Input.mousePosition;
            isDragging = true;
        }

        if (isDragging) {
            touchDelta = (Vector2)Input.mousePosition - startPos;
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) {
            startPos = new Vector2(0, 0);
            isDragging = false;
            touchDelta = new Vector2(0, 0);
        }

        Move();
        drawFishingLine();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Fish") {
            fishies.Add(collision.gameObject.GetComponent<Fish>());
        }
    }

    private void Move() {
        var deltaX = touchDelta.x * Time.deltaTime * lineSpeed;
        var deltaY = touchDelta.y * Time.deltaTime * lineSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, min.x, max.x);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, min.y, max.y);
        transform.position = new Vector3(newXPos, newYPos);
    }

    private void drawFishingLine() {
        lineRenderer.SetPosition(0, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1)));
        lineRenderer.SetPosition(1, transform.position);
    }

    public List<Fish> getHooked() {
        return fishies;
    }
}
