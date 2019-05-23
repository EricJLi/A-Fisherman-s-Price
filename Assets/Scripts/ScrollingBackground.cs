using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    [SerializeField] private float bgSpeed;
    [SerializeField] Sprite[] bgSprites;
    [SerializeField] int[] reps;
    [SerializeField] GameObject bgPrefab;
    List<Background> backgrounds;
    bool isEmpty = false;
    bool done = false;

    public class Background {
        public Background(Sprite s, int r) {
            sprite = s;
            reps = r;
        }
        public Sprite sprite;
        public int reps;
    }

    // Start is called before the first frame update
    void Start() {
        backgrounds = new List<Background>();
        for (int x = 0; x < bgSprites.Length; x++) {
            backgrounds.Add(new Background(bgSprites[x], reps[x]));
        }
        bgPrefab.GetComponent<SpriteRenderer>().sprite = getNextBackground();
        Instantiate(bgPrefab, transform);
        addBackground(transform);
    }

    // Update is called once per frame
    void Update() {
        if (!done) {
            Move();
        }
    }

    void Move() {
        foreach (Transform child in transform) {
            if (isEmpty && child.position.y <= transform.position.y &&
                child.position.y + bgSpeed > transform.position.y && transform.childCount == 2) {
                done = true;
                break;
            }
            child.position += new Vector3(0, bgSpeed, 0);
            if (child.position.y >= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y &&
                (child.position.y - bgSpeed) < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y) {
                addBackground(child);
            }
            if (child.position.y > Camera.main.ViewportToWorldPoint(new Vector3(0, 2, 0)).y) {
                Destroy(child.gameObject);
            }
        }
    }

    void addBackground(Transform t) {
        Sprite s = getNextBackground();
        if (s == null) {
            isEmpty = true;
            return;
        }
        Vector3 newPos = t.position;
        newPos.y -= Camera.main.ViewportToWorldPoint(new Vector3(0, 1.5f, 0)).y;
        bgPrefab.GetComponent<SpriteRenderer>().sprite = s;
        Instantiate(bgPrefab, newPos, transform.rotation, transform);
    }

    Sprite getNextBackground() {
        for (int x = 0; x < backgrounds.Count; x++) {
            if (backgrounds[x].reps > 0) {
                backgrounds[x].reps --;
                return backgrounds[x].sprite;
            }
        }
        return null;
    }
}