﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticleStorm;
using ParticleStorm.Script;

public class ParticleStormManager : MonoBehaviour
{
    public ParticlePrefeb[] particlePrefebs;

    private void BasicStorm1()
    {
        Storm storm = new Storm("BasicStorm1");
        storm.AddBehavior(0, EmitList.Sphere(300, 5, 3), Particle.Find("PerpleBall"))
            .AddBehavior(3, EmitList.RandomSphere(200, 4, 15), Particle.Find("GoldenBall"), 0.05f)
            .AddBehavior(5, EmitList.Ring(40, 10, Vector3.forward, 0, -20, 25), Particle.Find("SmallBlueStar"))
            .AddBehavior(10, EmitList.Ring(100, 20, Vector3.forward, 0, -35, 20), Particle.Find("SmallBlueStar"), 0.05f)
            .AddBehavior(15, EmitList.Sphere(300, 5, 3), Particle.Find("PerpleBall"));
            
    }

    private void PlayerAtkStorm()
    {
        Storm storm = new Storm("PlayerAtkStorm");
        storm.AddBehavior(0, EmitList.Cone(5, 1, 0, 75), Particle.Find("PlayerAttack"), 0.3f);
    }

    void Awake()
    {
        AddScripts();
        CreateParticles();
        CreateStorms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateParticles()
    {
        foreach (var particle in particlePrefebs)
        {
            new Particle(particle);
        }
    }

    private void CreateStorms()
    {
        BasicStorm1();
        PlayerAtkStorm();
    }

    private void AddScripts()
    {
        ParticleScript.AddCollisionScript(new CollisionEvent("Damage10", Damage10));
        ParticleScript.AddCollisionScript(new CollisionEvent("PlayerAttack", PlayerAttack));
    }

    // Particle scripts
    void Damage10(GameObject go, List<ParticleCollisionEvent> events)
    {
        if (go.CompareTag("Player"))
        {
            int damage = events.Count * 10;
            go.GetComponent<Player>().Damaged(damage);
        }
    }

    void PlayerAttack(GameObject go, List<ParticleCollisionEvent> events)
    {
        if (go.CompareTag("Boss"))
        {
            int damage = events.Count * 25;
            go.GetComponent<Boss>().Damaged(damage);
        }
    }
}
