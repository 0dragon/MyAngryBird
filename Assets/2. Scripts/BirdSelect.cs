using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdSelect : MonoBehaviour
{
    public GameObject YellowPrefabs;
    public GameObject RedPrefabs;
    public GameObject GreenPrefabs;
    public GameObject BlackPrefabs;

    public void onClickYellow()
    {
        // GetComponent<Slingshot>().birdPrefab = YellowPrefabs;
        GetComponent<BirdShot>().DestroyBird();
        GetComponent<BirdShot>().birdPrefab = YellowPrefabs;
    }

    public void onClickRed()
    {
        // GetComponent<Slingshot>().birdPrefab = RedPrefabs;
        GetComponent<BirdShot>().DestroyBird();
        GetComponent<BirdShot>().birdPrefab = RedPrefabs;
    }

    public void onClickGreen()
    {
        // GetComponent<Slingshot>().birdPrefab = GreenPrefabs;
        GetComponent<BirdShot>().DestroyBird();
        GetComponent<BirdShot>().birdPrefab = GreenPrefabs;
    }

    public void onClickBlack()
    {
        // GetComponent<Slingshot>().birdPrefab = BlackPrefabs;
        GetComponent<BirdShot>().DestroyBird();
        GetComponent<BirdShot>().birdPrefab = BlackPrefabs;
        
    }
}
