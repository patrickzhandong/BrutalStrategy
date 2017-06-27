using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarHeartsWhole : MonoBehaviour
{

    public int currentHp;
    public GameObject GameObjectHeart;
    public Sprite SpraitHeart_0;
    public Sprite SpraitHeart_1;
    public Sprite SpraitHeart_2;
    public Sprite SpraitHeart_3;
    public Sprite SpraitHeart_4;
    public Sprite SpraitHeart_5;

    Animator animator;



    void Awake()
    {
        animator = GetComponent<Animator>(); // Determination the animator


    }
    void Update()
    {

        switch (currentHp) 
        {
            case 0:

                GameObjectHeart.GetComponent<Image>().sprite = SpraitHeart_0;
                break;
            case 1:
                GameObjectHeart.GetComponent<Image>().sprite = SpraitHeart_1;
                break;
            case 2:
                GameObjectHeart.GetComponent<Image>().sprite = SpraitHeart_2;
                break;
            case 3:
                GameObjectHeart.GetComponent<Image>().sprite = SpraitHeart_3;
                break;
            case 4:
                GameObjectHeart.GetComponent<Image>().sprite = SpraitHeart_4;
                break;
            case 5:
                GameObjectHeart.GetComponent<Image>().sprite = SpraitHeart_5;
                break;
        }

        // Conditions start the animation "critical"

        if (currentHp < 3 & currentHp > 0) animator.SetBool("critical", true);
        else animator.SetBool("critical", false);
    }


    void HealthControl(int value) // Limitation periods values "currentHp"
    {
        currentHp = Mathf.Clamp(value, 0, 5);

    }

}
