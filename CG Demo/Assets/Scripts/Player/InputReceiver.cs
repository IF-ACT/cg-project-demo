using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    public GameObject cameraGameObject;
    public float sensitive;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Charactor>();
        eulaAim = player.transform.rotation.eulerAngles;
        camara = cameraGameObject.GetComponent<MyCamera>();
        camara.direction = Quaternion.Euler(eulaAim) * Vector3.forward;
        locked = false;
    }

    public void OnMove(InputValue value)
    {
        player.Move(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        var delta = value.Get<Vector2>();
        delta *= sensitive;
        eulaAim.x = Mathf.Clamp(eulaAim.x - delta.y, -89.99f, 89.99f);
        eulaAim.y += delta.x;
        eulaAim.y %= 360;
        camara.direction = Quaternion.Euler(eulaAim) * Vector3.forward;
        if (!locked)
        {
            player.AimDirection = eulaAim.y;
        }
    }

    public void OnFire(InputValue value)
    {
        if (value.Get<float>() > 0)
        {
            player.Fire(1);
        }
        else
        {
            player.Fire(0);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.Get<float>() > 0)
        {
            player.Jump();
        }
    }

    public void OnScroll(InputValue value)
    {
        camara.Distance -= value.Get<float>() * 0.01f * sensitive;
    }

    public void OnLock(InputValue value)
    {
        if (value.Get<float>() > 0)
        {
            locked = true;
        }
        else
        {
            locked = false;
        }
    }

    public void OnLookAtBoss(InputValue value)
    {
        if (value.Get<float>() > 0)
        {
            camara.LookAtBoss = true;
        }
        else
        {
            camara.LookAtBoss = false;
        }
    }

    private bool locked;
    private MyCamera camara;
    private Charactor player;
    private Vector3 eulaAim;
}
