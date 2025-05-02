// dnSpy decompiler from Assembly-CSharp.dll class: ProbabilityController
using System;
using UnityEngine;

public class ProbabilityController
{
	public static int ChoiceRandom(BonusItem[] items)
	{
		Array.Sort<BonusItem>(items, (BonusItem x, BonusItem y) => -x.CompareTo(y));
		float[] array = new float[items.Length];
		for (int i = 0; i < items.Length; i++)
		{
			array[i] = items[i].Percent;
		}
		return ProbabilityController.GetRandom(array);
	}

	public static int GetRandom(float[] probability)
	{
		float num = 0f;
		for (int i = 0; i < probability.Length; i++)
		{
			num += probability[i];
		}
		if (num > 1f)
		{
			throw new Exception("Overall probability is greater than 1");
		}
		float num2 = UnityEngine.Random.value * num;
		for (int j = 0; j < probability.Length; j++)
		{
			if (num2 <= probability[j])
			{
				return j;
			}
			num2 -= probability[j];
		}
		return probability.Length - 1;
	}
}
