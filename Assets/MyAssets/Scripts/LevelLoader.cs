using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int LevelType;
    public int MoveLimit;
    public int TimeLimit;
    public int RowCount;
    public int ColCount;
    public int SkipDuration;
    public int SkipAttemptCount;
    public int rewardCoins;
    public List<CellInfo> CellInfo;
    public List<MovableInfo> MovableInfo;
    public List<ExitInfo> ExitInfo;
}

[System.Serializable]
public class CellInfo
{
    public int Row;
    public int Col;
    public int Type;
}

[System.Serializable]
public class MovableInfo
{
    public int Row;
    public int Col;
    public List<int> Direction;
    public int Length;
    public int Colors;
    public int KeyId;
    public int LockId;
}

[System.Serializable]
public class ExitInfo
{
    public int Row;
    public int Col;
    public int Direction;
    public List<int> Colors;
}

public class LevelLoader : SingletonBase<LevelLoader>
{
    public int amountMoveable1Key;
    public int amountMoveable2Key;
    public GameObject cellPrefab;
    public GameObject exitPrefab;
    public GameObject movablePrefab, movablePrefab2FrontFacing, movablePrefab2BackFacing, movablePrefab3FrontFacing, movablePrefab3BackFacing;
    public CameraFollow cameraFollow;
    public GameManager gameManager;


    private List<MovableItem> movableItems = new();
    private int upY = 0;
    public void LoadLevel(int levelNo)
    {
        float randToCreateLock = Random.value;
        bool levelHaveLock = false;

        TextAsset jsonFile = Resources.Load<TextAsset>("Levels/Level" + levelNo); // No need for .json extension
        LevelData levelData = JsonUtility.FromJson<LevelData>(jsonFile.text);

        GameManager.Instance.SetLevelTime(levelData.MovableInfo.Count);
        if (levelData.MoveLimit != 0)
            gameManager.SetMoveLimit(levelData.MoveLimit);

        // Spawn and position CellInfo objects
        foreach (var cell in levelData.CellInfo)
        {
            Vector3 position = new Vector3(cell.Row + upY, 0, cell.Col);
            Transform tra = Instantiate(cellPrefab, position, Quaternion.identity).transform;

            cameraFollow.AddTarget(tra);
        }
        foreach(var moveable in levelData.MovableInfo)
        {
            if (moveable.LockId != 0)
            {
                levelHaveLock = true;
            }
        }
        int amountLock = 1;
        int amountKey = amountLock * 3;
        if (!levelHaveLock && randToCreateLock <= 0.3f && levelData.MovableInfo.Count > 10)
        {
            while (amountKey > 0)
            {
                int rand = 0;

                do rand = Random.Range(0, levelData.MovableInfo.Count);
                while (levelData.MovableInfo[rand].KeyId != 0 || levelData.MovableInfo[rand].LockId != 0);

                levelData.MovableInfo[rand].KeyId = 1;
                amountKey--;
            }
            while (amountLock > 0)
            {
                int rand;

                do rand = Random.Range(0, levelData.MovableInfo.Count);
                while (levelData.MovableInfo[rand].KeyId != 0 || levelData.MovableInfo[rand].LockId != 0 || levelData.MovableInfo[rand].Length == 1);

                levelData.MovableInfo[rand].LockId = 1;
                amountLock--;
            }
        }
        // Spawn and position MovableInfo objects
        foreach (var movable in levelData.MovableInfo)
        {
            Vector3 position = new Vector3(movable.Row + upY, 0, movable.Col);
            Quaternion rotation = Quaternion.Euler(0, (movable.Direction.Contains(1) || movable.Direction.Contains(3)) ? 90 : 0, 0);
            GameObject prefabToSpawn = null;

            if (movable.Length == 2)
            {
                if (rotation.eulerAngles.y == 0)
                    prefabToSpawn = movablePrefab2FrontFacing;
                else if (rotation.eulerAngles.y == 90)
                    prefabToSpawn = movablePrefab2BackFacing;
            }
            else if (movable.Length == 3)
            {
                if (rotation.eulerAngles.y == 90)
                    prefabToSpawn = movablePrefab3FrontFacing;
                else if (rotation.eulerAngles.y == 0)
                    prefabToSpawn = movablePrefab3BackFacing;
            }
            else if (movable.Length == 1)
                prefabToSpawn = movablePrefab;

            GameObject obj = Instantiate(prefabToSpawn, position, rotation);

            if (movable.KeyId != 0)
                obj.GetComponentInChildren<Key>(true).gameObject.SetActive(true);

            if (movable.LockId != 0)
            {
                obj.GetComponentInChildren<LockItem>(true).gameObject.SetActive(true);
                obj.GetComponent<MovableItem>().isLocked = true;
            }
            movableItems.Add(obj.GetComponent<MovableItem>());
            AssignColor(obj, movable.Colors, movable.Direction[0]);
        }

        // Spawn and position ExitInfo objects
        foreach (var exit in levelData.ExitInfo)
        {
            Vector3 position = new Vector3(exit.Row + upY, 0, exit.Col);
            float yRotation = exit.Direction switch
            {
                2 => 90,
                0 => -90,
                1 => 0,
                3 => 180,
                _ => 0
            };
            Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
            GameObject obj = null;

            obj = Instantiate(exitPrefab, position, rotation);

            for (int i = 0; i < exit.Colors.Count; i++)
            {
                AssignColor(obj, exit.Colors[i], exit.Direction);
            }
        }
    }

    void AssignColor(GameObject obj, int colorCode,int directionID)
    {
        Color color = colorCode switch
        {
            4 => Color.magenta, // Pink
            1 => Color.green,
            0 => Color.red,
            2 => Color.blue,
            3 => Color.yellow,
            _ => Color.white
        };

        ColorManager renderer = obj.GetComponent<ColorManager>();
        renderer.SetColor(color, colorCode, directionID);
    }
    public void ShowFindBooster()
    {
        foreach(MovableItem movableItem in movableItems)
        {
            if (movableItem != null && movableItem.CanMoveAnyDirection())
            {
                movableItem.suggestBorder.SetActive(true);
                return;
            }
        }
    }
    private int GetAmountLock(int amountMoveable)
    {
        if (amountMoveable >= amountMoveable1Key) return 1;
        else if (amountMoveable >= amountMoveable2Key) return 2;
        else return 3;
    }
}
