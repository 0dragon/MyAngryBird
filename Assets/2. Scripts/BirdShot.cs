using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BirdShot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Button[] birdButtonlist;
    
    public Vector3 currentPosition;
    public float maxLength;
    public float bottomBoundary;
    bool isMouseDown;
    public GameObject birdPrefab;
    public float birdPositionOffset;

    Rigidbody2D bird;
    Collider2D birdCollider;
    List<GameObject> launchedBirds = new List<GameObject>();
    List<Button> birdButtons = new List<Button>();

    public LineRenderer trajectoryLineRenderer;
    public float force;

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        trajectoryLineRenderer.positionCount = 0;
        
        CreateBird();
    }

    public void CreateBird()
    {
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;

        bird.isKinematic = true;

        ResetStrips();
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition
                - center.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
            UpdateTrajectory();
        }
        else
        {
            ResetStrips();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
        currentPosition = idlePosition.position;
    }

    void Shoot()
    {
        bird.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        bird.velocity = birdForce;

        launchedBirds.Add(bird.gameObject);
        
        Invoke("DestroyLaunchedBird", 5f);
        
        bird = null;
        birdCollider = null;
        // Invoke("CreateBird", 2);
        CreateBird();
        
        trajectoryLineRenderer.positionCount = 0;
    }

    void DestroyLaunchedBird() {
        if (launchedBirds.Count > 0) {
            GameObject birdToDestroy = launchedBirds[0];
            Destroy(birdToDestroy);
            launchedBirds.RemoveAt(0);
        }
    }

    public void DestroyBird()
    {
        if (bird != null)
        {
            Destroy(bird.gameObject);
            CreateBird();  
        }
    }
    
    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
    
    void UpdateTrajectory()
    {
        Vector3 velocity = (currentPosition - center.position) * force * -1;
        int numPoints = 50; // 궤적 포인트 개수 증가
        Vector3[] trajectoryPoints = new Vector3[numPoints];
        Vector3 startingPosition = bird.transform.position;
        Vector3 startingVelocity = velocity;

        float timeStep = 0.1f; // 시간 간격 조정

        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep; // 시간을 조정하여 궤적 포인트 생성
            trajectoryPoints[i] = startingPosition + startingVelocity * t + 0.5f * Physics.gravity * t * t;
            trajectoryPoints[i].z = 0; // Z축 고정
        }

        trajectoryLineRenderer.positionCount = numPoints;
        trajectoryLineRenderer.SetPositions(trajectoryPoints);
    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log("새 충돌 감지" + other.gameObject.name);
    // }
}