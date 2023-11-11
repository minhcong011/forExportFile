using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteController : MonoBehaviour
{
    [SerializeField] private List<Sprite> emotes = new List<Sprite>();

    [SerializeField] private MessBox messBox;

    public void CreateEmote(Vector3 position)
    {
        MessBox clone = Instantiate(messBox, this.transform);
        clone.transform.position = position +new Vector3(0,0.2f,0);
        clone.SetSprite(emotes[Random.Range(0, emotes.Count)]);
    }

}
