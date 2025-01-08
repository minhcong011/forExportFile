using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlushEffect : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;

            Vector2 uv = GetUVFromScreenPosition(touchPos);

            meshRenderer.material.SetVector("_TouchPoint", new Vector4(uv.x, uv.y, 0, 0));
        }
    }

    Vector2 GetUVFromScreenPosition(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector2 uv = hit.textureCoord;
            return uv;
        }

        return new Vector2(0, 0);
    }
}
