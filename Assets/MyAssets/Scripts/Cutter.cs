using DG.Tweening.Core.Easing;
using System;
using System.Collections.Generic;
using UnityEngine;
using RDG;
[Serializable]
public class CutterSprite
{
    public Sprite[] red;
    public Sprite[] green;
    public Sprite[] blue;
    public Sprite[] yellow;
    public Sprite[] pink;

    public List<Sprite[]> allSprite 
    {
        get
        {
            List<Sprite[]> list = new()
            {
                red,
                green,
                blue,
                yellow,
                pink
            };
            return list;
        }
    }
}
public class Cutter : MonoBehaviour
{
    public ParticleSystem cuttingParticle;
    public List<Color> cutterColors;

    public SpriteRenderer spriteRender;
    public SpriteRenderer spriteRenderL;
    public SpriteRenderer spriteRenderR;

    public CutterSprite sprites;
    public CutterSprite spritesL;
    public CutterSprite spritesR;

    public GameObject arrowM, arrowR, arrowL;
    private GameManager gameManager;

    List<int> colorIDList = new();

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void SetCutter(Color color, int colorID, int directionID)
    {
        cutterColors.Add(color);

        spriteRender.sprite = sprites.allSprite[colorID][directionID];

        spriteRender.GetComponent<SpriteNonRotation>().canReset = true;

        colorIDList.Add(colorID);
        if (cutterColors.Count == 2)
        {
            spriteRender.gameObject.SetActive(false);
            spriteRenderL.gameObject.SetActive(true);
            spriteRenderR.gameObject.SetActive(true);

            spriteRenderL.sprite = spritesL.allSprite[colorIDList[0]][directionID];
            spriteRenderR.sprite = spritesR.allSprite[colorIDList[1]][directionID];

            spriteRenderL.GetComponent<SpriteNonRotation>().canReset = true;
            spriteRenderR.GetComponent<SpriteNonRotation>().canReset = true;

            arrowM.SetActive(false);
            arrowR.SetActive(true);
            arrowL.SetActive(true);
            //containerL.material.color = cutterColors[1];
            //gateL.material.color = cutterColors[1];
        }
    }

    public void SetCutterParticleColor(Color color)
    {
        cuttingParticle.GetComponent<ParticleSystemRenderer>().material.color = color == Color.red ? new Color(0.5686275f, 0.0509804f, 1) : color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10) // Movable Item
        {
            Key key = other.GetComponentInChildren<Key>();

            if (key)
            {
                key.transform.parent = null;
                key.GotoLock();
            }

            if(!GameCache.GC.vibrateOff) Vibration.Vibrate(50);

            AudioManager.Instance.Play("Cut");
            cuttingParticle.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10) // Movable Item
        {
            gameManager.isDragging = false;

            cuttingParticle.Stop();
            Destroy(other.gameObject);

            Invoke(nameof(CheckWin), .5f);
        }
    }

    private void CheckWin()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (FindFirstObjectByType<MovableItem>())
            Debug.Log("NotYet");
        else
            gameManager.Win();
    }
}
