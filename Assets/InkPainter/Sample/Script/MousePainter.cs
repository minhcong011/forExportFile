using System.Collections;
using UnityEngine;

namespace Es.InkPainter.Sample
{
	public class MousePainter : MonoBehaviour
	{
		[SerializeField] private float timeDelay;
		/// <summary>
		/// Types of methods used to paint.
		/// </summary>
		[System.Serializable]
		private enum UseMethodType
		{
			RaycastHitInfo,
			WorldPoint,
			NearestSurfacePoint,
			DirectUV,
		}

		[SerializeField]
		private Brush brush;

		[SerializeField]
		private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

		[SerializeField]
		bool erase = false;

		private void Start()
		{
			StartCoroutine(Draw());
		}
		IEnumerator Draw()
		{
			while (true)
			{
				if (Input.touchCount != 0)
				{
					Touch touch = Input.GetTouch(0);
					//var ray = Camera.main.ScreenPointToRay(touch.position);
					bool success = true;
					//RaycastHit hitInfo;
					//if (Physics.Raycast(ray, out hitInfo))
					//{
					//	var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
					//	if (paintObject != null)
					//		switch (useMethodType)
					//		{
					//			case UseMethodType.RaycastHitInfo:
					//				success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
					//				break;

					//			case UseMethodType.WorldPoint:
					//				success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
					//				break;

					//			case UseMethodType.NearestSurfacePoint:
					//				success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
					//				break;

					//			case UseMethodType.DirectUV:
					//				if (!(hitInfo.collider is MeshCollider))
					//					Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
					//				success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
					//				break;
					//		}
					//	if (!success)
					//		Debug.LogError("Failed to paint.");
					//}
					Debug.Log("a");
					Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit[] hits = Physics.RaycastAll(ray);

                    foreach (RaycastHit hitInfo in hits)
                    {
                        var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                        if (paintObject != null)
                            switch (useMethodType)
                            {
                                case UseMethodType.RaycastHitInfo:
                                    success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
                                    break;

                                case UseMethodType.WorldPoint:
                                    success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
                                    break;

                                case UseMethodType.NearestSurfacePoint:
                                    success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
                                    break;

                                case UseMethodType.DirectUV:
                                    if (!(hitInfo.collider is MeshCollider))
                                        Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
                                    success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
                                    break;
                            }
                        if (!success)
                            Debug.LogError("Failed to paint.");
                    }
                }
				yield return new WaitForSeconds(timeDelay);
            }
		}
		public void OnGUI()
		{
			//if(GUILayout.Button("Reset"))
			//{
			//	foreach(var canvas in FindObjectsOfType<InkCanvas>())
			//		canvas.ResetPaint();
			//}
		}
	}
}