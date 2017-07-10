using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerController : MonoBehaviour {

    public const int MAX_HEALTH_DEFAULT = 5;

	public bool moving = false;
	private float speed = 4f;
	public Vector3 endPoint;

    // Sprite data
    public Sprite marksman0;
    public Sprite marksman1;
    public Sprite grenadier0;
    public Sprite grenadier1;
    public Sprite fencer0;
    public Sprite fencer1;

    // Unit data
    private int maxHealth = MAX_HEALTH_DEFAULT;
    private int currentHealth = MAX_HEALTH_DEFAULT;
    private int faction;
    private MapData.UnitData.unitType type;
    private string nickname;

    // Unit references
    private GameObject healthBar;
    private GameObject nameLabel;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (moving)
			Move (endPoint);
	}

	public void Move (Vector3 end) {
		//transform.position = new Vector3 (0f, 0f, 0f);
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		if (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (transform.position, end, speed * Time.deltaTime);
			transform.position = newPosition;
		} else {
			moving = false;
		}
	}

    /**
     * Set the sprite of the unit based both on its type (marksman, grenadier, or fencer) and its faction number.  All
     * potential unit sprites are supplied via public fields in the editor.
     **/
    private void updateSprite()
    {
        switch (type)
        {
            case MapData.UnitData.unitType.MARKSMAN:
                if (faction == 0)
                    GetComponent<SpriteRenderer>().sprite = marksman0;
                else if (faction == 1)
                    GetComponent<SpriteRenderer>().sprite = marksman1;
                break;
            case MapData.UnitData.unitType.GRENADIER:
                if (faction == 0)
                    GetComponent<SpriteRenderer>().sprite = grenadier0;
                else if (faction == 1)
                    GetComponent<SpriteRenderer>().sprite = grenadier1;
                break;
            case MapData.UnitData.unitType.FENCER:
                if (faction == 0)
                    GetComponent<SpriteRenderer>().sprite = fencer0;
                else if (faction == 1)
                    GetComponent<SpriteRenderer>().sprite = fencer1;
                break;
            default:
                //Do nothing.
                break;
        }
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setCurrentHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    /**
     * A more context-senesitive method of setting the current health that normalizes its input to a value from 0 to
     * maxHealth and also updates the displayed health.
     * 
     * @param health: The new value for currentHealth, which will be normalized to be between 0 and maxHealth.
     **/
    public void updateHealth(int health)
    {
        int actualHealth = Math.Max(maxHealth, Math.Max(health, 0));
        currentHealth = actualHealth;
        healthBar.GetComponent<HealthBarsHearts>().currentHp = currentHealth;
    }

    public int getFaction()
    {
        return faction;
    }

    public void setFaction(int faction)
    {
        this.faction = faction;
        updateSprite();
    }

    public MapData.UnitData.unitType getType()
    {
        return type;
    }

    public void setType(MapData.UnitData.unitType type)
    {
        this.type = type;
        updateSprite();
    }

    public string getNickname()
    {
        return nickname;
    }

    public void setNickname(string nickname)
    {
        this.nickname = nickname;
    }

    /**
     * A more context-sensitive method of setting the nickname for the unit.  Updates both the internal field and
     * the display.
     * 
     * @param nickname: The new nickname.
     **/
    public void updateNickname(string nickname)
    {
        this.nickname = nickname;
        nameLabel.GetComponent<Text>().text = nickname;
    }

    public GameObject getHealthBar()
    {
        return healthBar;
    }

    public void setHealthBar(GameObject healthBar)
    {
        this.healthBar = healthBar;
    }

    public GameObject getNameLabel()
    {
        return nameLabel;
    }

    public void setNameLabel(GameObject nameLabel)
    {
        this.nameLabel = nameLabel;
    }
 }
