using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgcount = 5;
    public int obstacleCount = 0;
    public Vector3 obstacleLastposition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        RandomDeploy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Background") || collision.CompareTag("Bottomground") || collision.CompareTag("Topground"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgcount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastposition = obstacle.SetRandomPlace(obstacleLastposition, obstacleCount);
        }
    }


    public void RandomDeploy()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastposition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastposition = obstacles[i].SetRandomPlace(obstacleLastposition, obstacleCount);
        }
    }
}
