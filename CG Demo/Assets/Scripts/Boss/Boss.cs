using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Life { get; set; }

    [SerializeField]
    private bool beginOnStart;
    [SerializeField]
    private float startLife;
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private float stage1MaxDis;
    [SerializeField]
    private float stage1MinDis;
    [SerializeField]
    private float aimHeightH;
    [SerializeField]
    private float aimHeightL;

    private bool begin = false;
    private Charactor charactor;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        charactor = GetComponent<Charactor>();
        player = GameObject.Find("Player");
        Life = startLife;
        if (beginOnStart)
        {
            Begin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!begin)
        {
            LookAtPlayer();
            return;
        }
        // AI
        if (Life > startLife * 3 / 4)
        {
            LookAtPlayer();
            if (Mathf.Abs(transform.position.y - aimHeightH) > 1)
            {// adjust height
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y +
                    Mathf.Sign(aimHeightH - transform.position.y) * normalSpeed * Time.deltaTime,
                    transform.position.z); 
            }
            if (Vector3.Distance(transform.position, player.transform.position) > stage1MaxDis)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, player.transform.position, normalSpeed * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) > stage1MinDis)
            {
                transform.position = transform.position * 2 - Vector3.MoveTowards(
                    transform.position, player.transform.position, normalSpeed * Time.deltaTime);
            }
        }
        else if (Life > startLife * 1 / 2)
        {

        }
        else if (Life > startLife * 1 / 4)
        {

        }
        else
        {

        }
    }

    public void Begin()
    {
        begin = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void LookAtPlayer() =>
        charactor.AimDirection = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles.y;

}
