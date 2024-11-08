using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    private float health = 100f;
    private float stamina = 100f;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] private bool canRegenStamina = true;

    public static PlayerStats Instance
    {
        get { return _instance; }
    }

    public bool HasStamina()
    {
        return stamina > 0f;
    }

    public void AddStamina()
    {
        if (stamina >= 100 || !canRegenStamina)
        {
            return;
        }
        stamina += 10f * Time.deltaTime;
    }

    public void SubtractStamina()
    {
        if (stamina <= 1)
        {
            if (canRegenStamina)
            {
                Debug.Log("Stamina exhausted, starting cooldown");
                StartCoroutine(staminaRunoutCooldown());
            }
            return;
        }
        stamina -= 30f * Time.deltaTime;
    }

    private IEnumerator staminaRunoutCooldown()
    {
        canRegenStamina = false;
        Debug.Log("Stamina runout cooldown started");
        yield return new WaitForSeconds(3f);
        canRegenStamina = true;
        Debug.Log("Stamina runout cooldown ended");
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        statsText.text = "Health: " + health + "\nStamina: " + (int)stamina;

        // Handle stamina regeneration
        if (canRegenStamina && stamina < 100f)
        {
            AddStamina();
        }
    }
}
