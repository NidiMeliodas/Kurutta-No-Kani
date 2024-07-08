using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private AudioClip keySound;
    [SerializeField] GameObject player;

    public bool IsPickedUp;

    private Vector2 vel;
    public float smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in the inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPickedUp && player != null)
        {
            Vector3 offset = new Vector3(-0.5f, 1f, 0);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !IsPickedUp)
        {
            IsPickedUp = true;
            Sound_Manager.instance.PlaySound(keySound);
        }
    }
}
