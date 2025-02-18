using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizemin = 1f;
    public float holeSizemax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    FlappyGameManager gameManager;

    private void Start()
    {
        gameManager = FlappyGameManager.Instance;
    }


    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        float holeSize = Random.Range(holeSizemin, holeSizemax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize + 1.2f);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize - 1.2f);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(highPosY, lowPosY);

        transform.position = placePosition;

        return placePosition;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null) gameManager.AddScore(1);
    }
}
