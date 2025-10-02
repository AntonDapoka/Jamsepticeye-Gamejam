using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image[] hearts; // Массив UI Image для каждого сердца
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHearts;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHearts;
    }

    private void UpdateHearts(int current, int max)
    {
        int heartCount = hearts.Length;
        float healthPerHeart = max / (float)heartCount;

        for (int i = 0; i < heartCount; i++)
        {
            if (current >= (i + 1) * healthPerHeart)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}