using System;
using CI.QuickSave;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class SaveLoadG : MonoBehaviour
{
	// Token: 0x06000201 RID: 513 RVA: 0x0000ED88 File Offset: 0x0000CF88
	private void Start()
	{
		try
		{
			if (!QuickSaveRoot.Exists("save.txt"))
			{
				SaveLoadG.SaveData();
			}
			else
			{
				SaveLoadG.LoadData();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
	public static void SaveData()
	{
		try
		{
			QuickSaveWriter.Create("save.txt").Write<float>("Sensitivity", VariblesGlobal.Sensitivity).Write<int>("Level", VariblesGlobal.Level).Write<int>("Achievement1", VariblesGlobal.Achievement1).Write<int>("Achievement2", VariblesGlobal.Achievement2).Write<int>("Achievement3", VariblesGlobal.Achievement3).Write<int>("Achievement4", VariblesGlobal.Achievement4).Commit();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000EE50 File Offset: 0x0000D050
	public static void LoadData()
	{
		try
		{
			QuickSaveReader quickSaveReader = QuickSaveReader.Create("save.txt");
			VariblesGlobal.Sensitivity = float.Parse(quickSaveReader.Read<string>("Sensitivity"));
			VariblesGlobal.Level = int.Parse(quickSaveReader.Read<string>("Level"));
			VariblesGlobal.Achievement1 = int.Parse(quickSaveReader.Read<string>("Achievement1"));
			VariblesGlobal.Achievement2 = int.Parse(quickSaveReader.Read<string>("Achievement2"));
			VariblesGlobal.Achievement3 = int.Parse(quickSaveReader.Read<string>("Achievement3"));
			VariblesGlobal.Achievement4 = int.Parse(quickSaveReader.Read<string>("Achievement4"));
			SaveLoadG.SaveData();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0400023F RID: 575
	private GameObject aButtonStartText;

	// Token: 0x04000240 RID: 576
	private GameObject aButtonShopText;
}
