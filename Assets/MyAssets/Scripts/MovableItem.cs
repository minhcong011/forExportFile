using Micosmo.SensorToolkit;
using System.Collections.Generic;
using UnityEngine;

public class MovableItem : MonoBehaviour
{
    public Sprite[] sprite;
    public RaySensor rightRaySensor, leftRaySensor, forwardRaySensor, backRaySensor;
    [HideInInspector] public bool canDrag, isLocked, isKey;
    public Color color;
    public SpriteRenderer spriteRender;

    public GameObject suggestBorder;
    private int currentRayLenght;
    private bool isNearCutter;
    [SerializeField] private bool test;
    private void Update()
    {
        if(test && Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(GetNearCutter());
        }
    }
    public void SetLock(bool set)
    {
        isLocked = set;
        GetComponentInChildren<LockItem>(true).gameObject.SetActive(set);
    }
    public void SetKey(bool set)
    {
        isKey = set;
        GetComponentInChildren<Key>(true).gameObject.SetActive(set);
    }
    public void SetMovableItem(Color _color, int spriteID)
    {
        color = _color;
        spriteRender.sprite = sprite[spriteID];
        GetComponentInChildren<SpriteNonRotation>().canReset = true;
    }

    public bool CanMove(string dir)
    {
        if (dir == "Right")
        {
            return CheckRaySensor(backRaySensor);
        }
        else if (dir == "Left")
        {
            return CheckRaySensor(forwardRaySensor);
        }
        else if (dir == "Forward")
        {
            return CheckRaySensor(rightRaySensor);
        }
        else if (dir == "Back")
        {
            return CheckRaySensor(leftRaySensor);
        }

        canDrag = true;
        return false;
    }
    public bool GetNearCutter()
    {
        MouseDrag mouseDrag = GetComponent<MouseDrag>();
        bool nearCutter = false;
        if (!mouseDrag.isHorizontalMover)
        {
            nearCutter = CheckObjIsCutter(rightRaySensor);
            if (!nearCutter)
                nearCutter = CheckObjIsCutter(leftRaySensor);
        }
        else
        {
            nearCutter = CheckObjIsCutter(forwardRaySensor);
            if (!nearCutter)
                nearCutter = CheckObjIsCutter(backRaySensor);
        }
        return nearCutter;
    }
    public bool GetCanMoveAnyDirection()
    {
        MouseDrag mouseDrag = GetComponent<MouseDrag>();
        bool canMove = false;
        if (!mouseDrag.isHorizontalMover)
        {
            canMove = CheckRaySensor(rightRaySensor);
            if(!canMove)
                canMove = CheckRaySensor(leftRaySensor);
        }
        else
        {
            canMove = CheckRaySensor(forwardRaySensor);
            if (!canMove)
                canMove = CheckRaySensor(backRaySensor);
        }
        canDrag = true;
        return canMove;
    }
    private bool CheckRaySensor(RaySensor raySensor)
    {
        if(raySensor.Length >= 100) raySensor.Length /= 100;
        raySensor.Pulse();
        if (raySensor.GetDetections().Count > 0)
        {
            Cutter cutter = raySensor.GetNearestDetection().GetComponent<Cutter>();

            if (cutter && cutter.cutterColors.Contains(color))
            {
                cutter.SetCutterParticleColor(color);
                Debug.Log("Cutter");
                canDrag = false;
                return true;
            }
            else
            {
                canDrag = true;
                return false;
            }
        }
        else
        {
            Debug.Log("No detection");
            canDrag = false;
            return true;
        }
    }
    private bool CheckObjIsCutter(RaySensor raySensor)
    {
        if (raySensor.Length < 100) raySensor.Length *= 100;
        raySensor.Pulse();
        if (raySensor.GetDetections().Count > 0)
        {
            Cutter cutter = raySensor.GetNearestDetection().GetComponent<Cutter>();

            if (cutter && cutter.cutterColors.Contains(color))
            {
                return true;
            }
        }
        return false;
    }
    private GameObject GetRaySensorObj(RaySensor raySensor)
    {
        if (raySensor.Length < 100) raySensor.Length *= 100;
        raySensor.Pulse();
        if(raySensor.GetDetections().Count > 0)
        {
            return raySensor.GetNearestDetection();
        }
        return null;
    }
    private bool GetNearObjCanOut(RaySensor raySensor, List<GameObject> oldObj)
    {
        GameObject nearObj = GetRaySensorObj(raySensor);
        MovableItem move = nearObj.GetComponent<MovableItem>();
        if (move && move.CheckNonBlockByLock(oldObj)) return true;
        return false;
    }
    public bool CheckNonBlockByLock(List<GameObject> oldObj)
    {
        if (oldObj.Contains(gameObject))
        {
            return false;
        }

        oldObj.Add(this.gameObject);
        MouseDrag mouseDrag = GetComponent<MouseDrag>();
        if (GetNearCutter())
        {
            return true;
        }
        else
        {
            if (!mouseDrag.isHorizontalMover)
            {
                if (CheckBlockLockInSide(rightRaySensor)) return false;
                if (CheckBlockLockInSide(leftRaySensor)) return false;
            }
            else
            {
                if (CheckBlockLockInSide(forwardRaySensor)) return false;
                if (CheckBlockLockInSide(backRaySensor)) return false;
            }
            if (!mouseDrag.isHorizontalMover)
            {
                if (GetNearObjCanOut(rightRaySensor, oldObj)) return true;
                if (GetNearObjCanOut(leftRaySensor, oldObj)) return true;
            }
            else
            {
                if (GetNearObjCanOut(forwardRaySensor, oldObj)) return true;
                if (GetNearObjCanOut(backRaySensor, oldObj)) return true;
            }
        }
        return false;
    }
    private bool CheckBlockLockInSide(RaySensor raySensor)
    {
        if (raySensor.Length < 100) raySensor.Length *= 100;
        raySensor.Pulse();
        foreach(GameObject obj in raySensor.GetDetections())
        {
            MovableItem movableItem = obj.GetComponent<MovableItem>();
            if (movableItem && movableItem.isLocked) return true;
        }
        return false;
    }
    public void Unlock()
    {
        isLocked = false;
        AudioManager.Instance.Play("Unlocked");
    }
}