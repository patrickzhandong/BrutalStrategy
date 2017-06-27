using UnityEngine;
using System.Collections;

public class HealthBarsHearts : MonoBehaviour
{

    public int currentHp;
    Animator animator;
    private Transform[] hearts = new Transform[5];


    void Awake()
    {
        animator = GetComponent<Animator>(); // Determination the animator
        for (int i = 0; i < hearts.Length; i++) //Filling an array
        {
            hearts[i] = transform.GetChild(i);
        }
    }


    void Update()
    {
        for (int i = 0; i < hearts.Length; i++) // Enumerating array
        {
            if (i < currentHp) hearts[i].gameObject.SetActive(true); // Deactivating elements is less than "currentHp"
            else hearts[i].gameObject.SetActive(false); // Activating elements is less than "currentHp"
        }

        // Conditions start the animation "critical"

        if (currentHp < 3 & currentHp > 0) animator.SetBool("critical", true);
        else animator.SetBool("critical", false);

        if (currentHp < 2 & currentHp > 0) animator.SetBool("critical2", true);
        else animator.SetBool("critical2", false);
    }


    void HealthControl(int value) // Limitation periods values "currentHp"
    {
        currentHp = Mathf.Clamp(value, 0, 5);

    }
}

