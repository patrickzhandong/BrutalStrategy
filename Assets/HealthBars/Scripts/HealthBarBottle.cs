using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class HealthBarBottle : MonoBehaviour
{

    public float maxHp;
    public float currentHp;
    public float criticalHpPercent;
    private float criticalHp;
    public RectTransform content;
    private float height;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Determination the animator
        height = content.rect.height; // Define the object height "Content"

    }

    void Update()
    {

        criticalHp = maxHp / 100 * criticalHpPercent; // Calculation of criticalHP

        Vector3 position = content.anchoredPosition;  // Move the object "Content" Y-axis
        position.y = height * currentHp / maxHp; // Calculation of displacement
        content.anchoredPosition = position;



        if (currentHp < criticalHp) // Conditions start the animation "critical"
        {
            animator.SetBool("critical", true);
        }

        else
        {
            animator.SetBool("critical", false);
        }



    }



    void HealthControl(int value) // Limitation periods values "currentHp"
    {
        currentHp = Mathf.Clamp(value, 0, maxHp);

    }


}
