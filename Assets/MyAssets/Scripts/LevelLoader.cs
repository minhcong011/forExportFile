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

    int[] randomNumbers = {
    3, 7, 12, 19, 25, 31, 37, 42, 49, 55, 60, 66, 72, 79, 85,
    92, 98, 105, 112, 118, 125, 130, 137, 142, 149, 155, 161,
    169, 175, 182, 188, 193, 200, 207, 213, 220, 227, 233, 240,
    5, 11, 17, 23, 29, 35, 41, 47, 53, 59, 65, 71, 77, 83, 89,
    95, 101, 109, 115, 121, 129, 135, 141, 147, 153, 159, 167,
    173, 181, 187, 195, 203, 211, 219
};
    private List<MovableItem> movableItems = new();
    private int upY = 0;

    private LevelData levelData;

    private MovableItem oldMoveSuggest;
    public void LoadLevel(int levelNo)
    {


        TextAsset jsonFile = Resources.Load<TextAsset>("Levels/Level" + levelNo); // No need for .json extension
        levelData = JsonUtility.FromJson<LevelData>(jsonFile.text);

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
                obj.GetComponent<MovableItem>().SetKey(true);

            if (movable.LockId != 0)
            {
                obj.GetComponent<MovableItem>().SetLock(true);
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
        Invoke(nameof(AddLock), 0.3f);
    }
    private void AddLock()
    {
        bool needCreateLock = true;
        bool levelHaveLock = false;
        int amountKey = 0;
        int countRand1 = 10;

        foreach (var moveable in levelData.MovableInfo)
        {
            if (moveable.LockId != 0)
            {
                levelHaveLock = true;
            }
        }

        //foreach (int number in randomNumbers)
        //{
        //    if (number == gameManager.levelNo)
        //    {
        //        needCreateLock = true;
        //        break;
        //    }
        //}
        List<MovableInfo> tmp = new(levelData.MovableInfo);
        if (!levelHaveLock && needCreateLock && levelData.MovableInfo.Count > 20)
        {
            Debug.Log("addLock");
            while (tmp.Count > 0)
            {
                int randLock = 0;
                randLock = Random.Range(0, tmp.Count);
                if (tmp[randLock].Length != 1)
                {
                    int indexRand = levelData.MovableInfo.IndexOf(tmp[randLock]);

                    List<MovableItem> modifiedItems = new();

                    movableItems[indexRand].SetLock(true);
                    modifiedItems.Add(movableItems[indexRand]);

                    foreach (MovableItem move in movableItems)
                    {
                        if (!move.isLocked)
                        {
                            List<GameObject> tmp2 = new();
                            if (move.CheckNonBlockByLock(tmp2))
                            {
                                move.SetKey(true);
                                modifiedItems.Add(move);
                                amountKey++;
                                if (amountKey == 3) return;
                            }
                        }
                    }

                    if (amountKey < 3)
                    {
                        foreach (MovableItem move in modifiedItems)
                        {
                            move.SetLock(false);
                            move.SetKey(false);
                        }
                        amountKey = 0;
                    }
                }
                tmp.RemoveAt(randLock);
            }
        }
    }
    void AssignColor(GameObject obj, int colorCode, int directionID)
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
    public void RemoveAllKey()
    {
        foreach (MovableItem moveable in movableItems)
        {
            Key key = moveable.GetComponentInChildren<Key>();
            if (key)
            {
                key.gameObject.SetActive(false);
            }
        }
    }
    public void ShowFindBooster()
    {
        List<MovableItem> suggestObjs = new();
        foreach (MovableItem movableItem in movableItems)
        {
            if (movableItem != null && movableItem.GetNearCutter() && !movableItem.isLocked && !movableItem.suggestBorder.activeSelf)
            {
                suggestObjs.Add(movableItem);
            }
        }
        if (suggestObjs.Count == 0)
        {
            foreach (MovableItem movableItem in movableItems)
            {
                if (movableItem != null && movableItem.GetCanMoveAnyDirection() && !movableItem.isLocked && !movableItem.suggestBorder.activeSelf)
                {
                    suggestObjs.Add(movableItem);
                }
            }
        }
        int countRand = 0;
        int rand = 0;
        do
        {
            countRand++;
            rand = Random.Range(0, suggestObjs.Count);
        }
        while ((oldMoveSuggest && suggestObjs[rand] == oldMoveSuggest) && countRand < suggestObjs.Count);

        oldMoveSuggest = suggestObjs[rand];

        suggestObjs[rand].suggestBorder.SetActive(true);
    }
    private int GetAmountLock(int amountMoveable)
    {
        if (amountMoveable >= amountMoveable1Key) return 1;
        else if (amountMoveable >= amountMoveable2Key) return 2;
        else return 3;
    }
}

