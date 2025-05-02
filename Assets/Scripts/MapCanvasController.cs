// dnSpy decompiler from Assembly-CSharp.dll class: MapCanvasController
using System;
using UnityEngine;

[AddComponentMenu("MiniMap/Map canvas controller")]
[RequireComponent(typeof(RectTransform))]
public class MapCanvasController : MonoBehaviour
{
	public static MapCanvasController Instance
	{
		get
		{
			if (!MapCanvasController._instance)
			{
				MapCanvasController[] array = UnityEngine.Object.FindObjectsOfType<MapCanvasController>();
				if (array.Length != 0)
				{
					if (array.Length == 1)
					{
						MapCanvasController._instance = array[0];
					}
					else
					{
						UnityEngine.Debug.LogError("You have more than one MapCanvasController in the scene.");
					}
				}
				else
				{
					UnityEngine.Debug.LogError("You should add Map prefab to your canvas");
				}
			}
			return MapCanvasController._instance;
		}
	}

	public InnerMap InnerMapComponent
	{
		get
		{
			return this.innerMap;
		}
	}

	public MarkerGroup MarkerGroup
	{
		get
		{
			return this.markerGroup;
		}
	}

	private void Awake()
	{
		if (!this.playerTransform)
		{
			UnityEngine.Debug.LogError("You must specify the player transform");
		}
		this.mapRect = base.GetComponent<RectTransform>();
		this.innerMap = base.GetComponentInChildren<InnerMap>();
		if (!this.innerMap)
		{
			UnityEngine.Debug.LogError("InnerMap component is missing from children");
		}
		this.mapArrow = base.GetComponentInChildren<MapArrow>();
		if (!this.mapArrow)
		{
			UnityEngine.Debug.LogError("MapArrow component is missing from children");
		}
		this.markerGroup = base.GetComponentInChildren<MarkerGroup>();
		if (!this.markerGroup)
		{
			UnityEngine.Debug.LogError("MerkerGroup component is missing. It must be a child of InnerMap");
		}
		this.innerMapRadius = this.innerMap.getMapRadius();
	}

	private void Update()
	{
		if (!this.playerTransform)
		{
			return;
		}
		if (this.rotateMap)
		{
			this.mapRect.rotation = Quaternion.Euler(new Vector3(0f, 0f, this.playerTransform.eulerAngles.y));
			this.mapArrow.rotate(Quaternion.identity);
		}
		else
		{
			this.mapArrow.rotate(Quaternion.Euler(new Vector3(0f, 0f, -this.playerTransform.eulerAngles.y)));
		}
	}

	public void checkIn(MapMarker marker)
	{
		if (!this.playerTransform)
		{
			return;
		}
		float num = this.radarDistance * this.scale;
		float num2 = this.maxRadarDistance * this.scale;
		if (marker.isActive)
		{
			float num3 = this.distanceToPlayer(marker.getPosition());
			float num4 = 1f;
			if (num3 > num)
			{
				if (this.hideOutOfRadius)
				{
					if (marker.isVisible())
					{
						marker.hide();
					}
					return;
				}
				if (num3 > num2)
				{
					if (marker.isVisible())
					{
						marker.hide();
					}
					return;
				}
				if (this.useOpacity)
				{
					float num5 = num2 - num;
					if (num5 <= 0f)
					{
						UnityEngine.Debug.LogError("Max radar distance should be bigger than radar distance");
						return;
					}
					float num6 = num3 - num;
					num4 = 1f - num6 / num5;
					if (num4 < this.minimalOpacity)
					{
						num4 = this.minimalOpacity;
					}
				}
				num3 = num;
			}
			if (!marker.isVisible())
			{
				marker.show();
			}
			Vector3 vector = marker.getPosition() - this.playerTransform.position;
			Vector3 vector2 = new Vector3(vector.x, vector.z, 0f);
			vector2.Normalize();
			float num7 = marker.markerSize / 2f;
			float d = num3 / num * (this.innerMapRadius - num7);
			vector2 *= d;
			marker.setLocalPos(vector2);
			marker.setOpacity(num4);
		}
		else if (marker.isVisible())
		{
			marker.hide();
		}
	}

	private float distanceToPlayer(Vector3 other)
	{
		return Vector2.Distance(new Vector2(this.playerTransform.position.x, this.playerTransform.position.z), new Vector2(other.x, other.z));
	}

	private static MapCanvasController _instance;

	public Transform playerTransform;

	public float radarDistance = 10f;

	public bool hideOutOfRadius = true;

	public bool useOpacity = true;

	public float maxRadarDistance = 10f;

	public bool rotateMap;

	public float scale = 1f;

	public float minimalOpacity = 0.3f;

	private RectTransform mapRect;

	private InnerMap innerMap;

	private MapArrow mapArrow;

	private MarkerGroup markerGroup;

	private float innerMapRadius;
}
