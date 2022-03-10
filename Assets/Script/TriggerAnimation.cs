using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator bossController;

    [SerializeField] private string intro = "Intro";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossController.Play(intro, 0, 0.0f);
        }
    }
}
