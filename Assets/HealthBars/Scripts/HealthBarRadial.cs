using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarRadial : MonoBehaviour
{

    public float maxHp;
    public float currentHp;
    public float criticalHpPercent;
    public Image Content;
    public Text HpText;
    private float currentHpForText;
    private float criticalHp;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Determination the animator

    }


    void Update()

    {

        criticalHp = maxHp / 100 * criticalHpPercent; // Calculation of criticalHP

        currentHpForText = currentHp / maxHp * 100; // Calculation of critical percent
        HpText.text = ((int)currentHpForText).ToString(); // The output value "currentHpForText" in the text box

        Content.fillAmount = currentHp / maxHp; // Converting object "Content"

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
