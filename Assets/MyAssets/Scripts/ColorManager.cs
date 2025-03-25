using UnityEngine;

public class ColorManager : MonoBehaviour
{
    private Gradient trailColor;

    public void SetColor(Color color, int spriteID, int directionID)
    {     
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (color == Color.red)
        {
            trailColor = gameManager.red;
        }
        else if (color == Color.yellow)
            trailColor = gameManager.yellow;
        else if (color == Color.blue)
            trailColor = gameManager.blue;
        else if (color == Color.green)
            trailColor = gameManager.green;
        else if (color == Color.magenta)
            trailColor = gameManager.pink;

        if (GetComponentInChildren<TrailRenderer>())
            GetComponentInChildren<TrailRenderer>().colorGradient = trailColor;

        if (GetComponent<MovableItem>())
            GetComponent<MovableItem>().SetMovableItem(color, spriteID);
        else if (GetComponent<Cutter>())
            GetComponent<Cutter>().SetCutter(color, spriteID, directionID);        
    }
}
