using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Transform leftBounds;
    public Transform rightBounds;

    public float smoothDampTime = 1f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    private float camWidth, camHeight, levelMinX, levelMaxX;

	// Use this for initialization
	void Start () {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        float leftBoundWidth = leftBounds.GetComponentInChildren<MeshRenderer>().bounds.size.x / 2;
        float rightBoundWidth = rightBounds.GetComponentInChildren<MeshRenderer>().bounds.size.x / 2;

        levelMinX = leftBounds.position.x + leftBoundWidth + (camWidth /2);
        levelMaxX = rightBounds.position.x - rightBoundWidth - (camWidth / 2);

	}
	
	// Update is called once per frame
	void Update () {

        if (player)
        {
            float playerX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, player.position.x));

            float x = Mathf.SmoothDamp(transform.position.x, playerX, ref smoothDampVelocity.x, smoothDampTime);

            transform.position = new Vector3(x, player.position.y+1, transform.position.z);
        }
	}
}
