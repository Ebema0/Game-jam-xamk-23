using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;
    private Image barImage;
    

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<RectTransform>();
        barImage = GetComponent<Image>();
        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        SetSize(Health.totalHealth);
    }
    public void RestartGame()
    {
        // Reset the player's position and score
        
        Scoring.totalScore = 0;

        // Reset the health bar
        Health.totalHealth = 1f;

        // Reload the current scene
        SceneManager.LoadScene(1);
    }
    public void Damage(float damage)
    {
        float newHealth = Health.totalHealth - damage;
        if (newHealth >= 0f)
        {
            Health.totalHealth = newHealth;
        }
        else
        {
            Health.totalHealth = 0f;
        }

        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }

        SetSize(Health.totalHealth);
    }


    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}
