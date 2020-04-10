using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour {

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

	// Use this for initialization
	void Start () {
        cam = Camera.main.GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            cam.smoothing = 0.1f;
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if(needText)
            {
                StartCoroutine(placeNameCo());
            }
            StartCoroutine(defaultCamSpeed(cam));
        }
    }

    private IEnumerator defaultCamSpeed(CameraMovement cam)
    {
        yield return new WaitForSeconds(0.9f);
        cam.smoothing = 1f;
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

}