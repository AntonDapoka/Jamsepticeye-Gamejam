using CustomCamera;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;

    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject[] startPoints;
    [SerializeField] private FollowCamera2D FC;
    [SerializeField] private Transform farFarAway;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Sprite background1;
    [SerializeField] private Sprite background2;
    [SerializeField] private Sprite background3;

    private void Start()
    {
        //player.position = startPoints[currentLevel].transform.position;

        LevelBoundHolder holder = levels[currentLevel].GetComponent<LevelBoundHolder>();

        FC.leftBound = holder.leftBorder;
        FC.rightBound = holder.rightBorder;
        FC.upperBound = holder.topBorder;
        FC.lowerBound = holder.downBorder;

        player.position = startPoints[currentLevel].transform.position;

        background.sprite = background1;
    }

    public void SetNextLevel()
    {
        levels[currentLevel].transform.position = farFarAway.position;



        currentLevel++;

        if (currentLevel == 3)
            background.sprite = background2;

        if (currentLevel < levels.Length)
        {
            levels[currentLevel].transform.position = Vector3.zero;
            LevelBoundHolder holder = levels[currentLevel].GetComponent<LevelBoundHolder>();

            FC.leftBound = holder.leftBorder;
            FC.rightBound = holder.rightBorder;
            FC.upperBound = holder.topBorder;
            FC.lowerBound = holder.downBorder;
            player.position = startPoints[currentLevel].transform.position;
        }

    }

    public void SetSpecificLevel(int levelnumber)
    {
        currentLevel = levelnumber;
        if (levelnumber < levels.Length)
            player.position = startPoints[levelnumber].transform.position;
    }
}
