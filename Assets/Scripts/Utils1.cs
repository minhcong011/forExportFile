// dnSpy decompiler from Assembly-CSharp.dll class: Utils1
using System;
using System.Collections;
using UnityEngine;

public class Utils1
{
	public static string CreateRandomString(int _length)
	{
		string text = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
		char[] array = new char[_length];
		for (int i = 0; i < _length; i++)
		{
			array[i] = text[UnityEngine.Random.Range(0, text.Length)];
		}
		return new string(array);
	}

	public static bool IsStringEmpty(string str)
	{
		for (int i = 0; i < str.Length; i++)
		{
			if (str[i] != ' ')
			{
				return false;
			}
		}
		return true;
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public static void StopAnimationsInLayer(Animation anim, int layer)
	{
		IEnumerator enumerator = anim.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AnimationState animationState = (AnimationState)obj;
				if (animationState.layer == 3)
				{
					anim.Stop(animationState.clip.name);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static Texture2D CreateTexture1x1(Color color)
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, color);
		texture2D.Apply();
		return texture2D;
	}

	public static CustomInput LoadInput()
	{
		Settings settings = UnityEngine.Object.FindObjectOfType(typeof(Settings)) as Settings;
		return settings.customInput;
	}

	public static void CLog(string tag, string msg, string color)
	{
		string text = tag + msg;
		string[] array = text.Split(new char[]
		{
			'$'
		});
		foreach (string text2 in array)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"<color=",
				color,
				">",
				text2,
				"</color>"
			}));
		}
	}
}
