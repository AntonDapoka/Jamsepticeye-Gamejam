using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject[] startPoints;

    private void Start()
    {
        //player.position = startPoints[currentLevel].transform.position;
    }

    public void SetNextLevel()
    {
        currentLevel++;
        if (currentLevel < levels.Length) 
            player.position = startPoints[currentLevel].transform.position;
    }

    public void SetSpecificLevel(int levelnumber)
    {
        currentLevel = levelnumber;
        if (levelnumber < levels.Length)
            player.position = startPoints[levelnumber].transform.position;
    }
}
