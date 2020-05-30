using ParticleStorm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Life { set; get; }
    public float startLife;
    public float Mana { get; set; }
    public float startMana;
    public float atkCold;

    private Image healthBar;
    private Image manaBar;
    private StormGenerator generator;
    private float cold;
    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("Canvas/Double Bar/Status Fill 00").GetComponent<Image>();
        manaBar = GameObject.Find("Canvas/Double Bar/Status Fill 01").GetComponent<Image>();
        boss = GameObject.Find("Boss");
        generator = GetComponentInChildren<StormGenerator>();
        Life = startLife;
        Mana = startMana;
        cold = atkCold;
    }

    public void Damaged(float value)
    {
        Life -= value;
    }

    public void Attack()
    {
        if (cold == atkCold)
        {
            cold = 0;
            Vector3 eular = generator.transform.rotation.eulerAngles;
            Vector3 dir = boss.transform.position - transform.position;
            float x = Vector3.Angle(Vector3.ProjectOnPlane(dir, Vector3.up), dir);
            generator.transform.rotation = Quaternion.Euler(-x, eular.y, eular.z);
            
            generator.Generate(Storm.Find("PlayerAtkStorm"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Life / startLife;
        manaBar.fillAmount = Mana / startMana;
        cold = Mathf.Min(cold + Time.deltaTime, atkCold);
        Debug.DrawRay(generator.transform.position, generator.transform.forward);
    }
}
