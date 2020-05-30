using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 direction;
    public float height;
    public const float MinDistance = 1;
    public const float MaxDistance = 10;
    public bool LookAtBoss { get; set; }

    private GameObject boss;

    public float Distance
    {
        get => distance;
        set => distance = Mathf.Clamp(value, MinDistance, MaxDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
        LookAtBoss = false;
        boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentDirection = direction;
        if (LookAtBoss)
        {
            currentDirection = (boss.transform.position - player.transform.position).normalized;
        }
        Vector3 lookPosition = playerTransform.position + playerTransform.up * height;
        transform.rotation = Quaternion.LookRotation(currentDirection);

        float currentDistance = Distance;
        Ray backRay = new Ray(lookPosition, -currentDirection);
        const int playerLayer = 1 << 8;
        if (Physics.Raycast(backRay, out RaycastHit hit, Distance, ~playerLayer))
        {
            currentDistance = Mathf.Min(Distance, hit.distance - 0.1f);
        }
        transform.position = lookPosition - transform.forward * currentDistance;
    }

    private float distance = 2;
    private Transform playerTransform;
}
