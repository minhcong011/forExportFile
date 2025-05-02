// dnSpy decompiler from Assembly-CSharp.dll class: ScrollableUi
using System;
using UnityEngine;

public class ScrollableUi : MonoBehaviour
{
	private void Start()
	{
	}

	public void ScrollView(string action)
	{
		if (action != null)
		{
			if (!(action == "horizontal"))
			{
				if (action == "vertical")
				{
					this.VerticalScrollar(this.itemCount, this.UiPrefab);
				}
			}
			else
			{
				this.HorizontalScrollar(this.itemCount, this.UiPrefab);
			}
		}
	}

	public void VerticalScrollar(int itemsCount, GameObject Prefab)
	{
		this.itemCount = itemsCount;
		this.UiPrefab = Prefab;
		RectTransform component = this.UiPrefab.GetComponent<RectTransform>();
		MonoBehaviour.print("rowRectTransform of prefab is " + component);
		RectTransform component2 = base.gameObject.GetComponent<RectTransform>();
		MonoBehaviour.print("containerRectTransform of container is " + component2);
		RectTransform component3 = base.gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
		MonoBehaviour.print("ParentRectTransform of container is " + component3);
		float height = component3.rect.height;
		MonoBehaviour.print("parent height is " + height);
		float width = component2.rect.width;
		float height2 = component.rect.height;
		float num = height2 / 2f;
		int num2 = this.itemCount;
		if (this.itemCount % num2 > 0)
		{
			num2++;
		}
		float num3 = height2 * (float)num2;
		num3 += (float)(this.Gap * num2);
		component2.offsetMin = new Vector2(component2.offsetMin.x, -num3 / 2f);
		component2.offsetMax = new Vector2(component2.offsetMax.x, num3 / 2f);
		float height3 = component2.rect.height;
		MonoBehaviour.print("list height is " + height3);
		float num4 = height3 / 2f;
		num4 -= num;
		MonoBehaviour.print("listHeightDifference is " + num4);
		float num5 = height - height3;
		MonoBehaviour.print("difference b/w parent and list is " + num5);
		num5 /= 2f;
		MonoBehaviour.print("difference divided by 2 is" + num5);
		base.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, num5);
		float num6 = (float)this.Gap;
		for (int i = 0; i < this.itemCount; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UiPrefab, base.transform.position, Quaternion.identity);
			gameObject.name = string.Concat(new object[]
			{
				base.gameObject.name,
				" item at (",
				i,
				")"
			});
			gameObject.transform.parent = base.gameObject.transform;
			gameObject.transform.localScale = Vector3.one;
			RectTransform component4 = gameObject.GetComponent<RectTransform>();
			float height4 = component4.rect.height;
			MonoBehaviour.print(height4);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, num4);
			MonoBehaviour.print("new ui element on position " + gameObject.GetComponent<RectTransform>().anchoredPosition);
			num4 -= height4;
			num4 -= (float)this.Gap;
		}
	}

	public void HorizontalScrollar(int itemsCount, GameObject Prefab)
	{
		this.itemCount = itemsCount;
		this.UiPrefab = Prefab;
		RectTransform component = this.UiPrefab.GetComponent<RectTransform>();
		MonoBehaviour.print("rowRectTransform of prefab is " + component);
		RectTransform component2 = base.gameObject.GetComponent<RectTransform>();
		MonoBehaviour.print("containerRectTransform of container is " + component2);
		RectTransform component3 = base.gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
		MonoBehaviour.print("ParentRectTransform of container is " + component3);
		float width = component3.rect.width;
		MonoBehaviour.print("parent width is " + width);
		float width2 = component.rect.width;
		float num = width2 / 2f;
		int num2 = this.itemCount;
		if (this.itemCount % num2 > 0)
		{
			num2++;
		}
		float num3 = width2 * (float)num2;
		num3 += (float)(this.Gap * num2);
		component2.offsetMin = new Vector2(-num3 / 2f, component2.offsetMin.y);
		component2.offsetMax = new Vector2(num3 / 2f, component2.offsetMax.y);
		float width3 = component2.rect.width;
		MonoBehaviour.print("list width is " + width3);
		float num4 = width - width3;
		MonoBehaviour.print("difference b/w parent and list is " + num4);
		num4 /= 2f;
		MonoBehaviour.print("difference divided by 2 is" + num4);
		base.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(num4, 0f);
		float num5 = width3 / 2f;
		num5 = num5;
		num5 *= -1f;
		num5 += num;
		MonoBehaviour.print("listWidthDifference is " + num5);
		float num6 = (float)this.Gap;
		for (int i = 0; i < this.itemCount; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UiPrefab, base.transform.position, Quaternion.identity);
			gameObject.name = string.Concat(new object[]
			{
				base.gameObject.name,
				" item at (",
				i,
				")"
			});
			gameObject.transform.parent = base.gameObject.transform;
			gameObject.transform.localScale = Vector3.one;
			RectTransform component4 = gameObject.GetComponent<RectTransform>();
			float width4 = component4.rect.width;
			MonoBehaviour.print(width4);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(num5, 0f);
			MonoBehaviour.print("new ui element on position " + gameObject.GetComponent<RectTransform>().anchoredPosition);
			num5 += width4;
			num5 += (float)this.Gap;
		}
	}

	public GameObject UiPrefab;

	public int itemCount;

	public int Gap;
}
