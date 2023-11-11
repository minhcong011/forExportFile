using System;
using CI.QuickSave;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000073 RID: 115
public class ExampleSceneManagerController : MonoBehaviour
{
	// Token: 0x060001F6 RID: 502 RVA: 0x0000EA2C File Offset: 0x0000CC2C
	public void Save()
	{
		QuickSaveWriter.Create("Inputs").Write<string>("Input1", this.Input1.text).Write<string>("Input2", this.Input2.text).Write<string>("Input3", this.Input3.text).Write<string>("Input4", this.Input4.text).Commit();
		this.Content.text = QuickSaveRaw.LoadString("Inputs.json");
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
	public void Load()
	{
		QuickSaveReader.Create("Inputs").Read<string>("Input1", delegate(string r)
		{
			this.Input5.text = r;
		}).Read<string>("Input2", delegate(string r)
		{
			this.Input6.text = r;
		}).Read<string>("Input3", delegate(string r)
		{
			this.Input7.text = r;
		}).Read<string>("Input4", delegate(string r)
		{
			this.Input8.text = r;
		});
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000EB24 File Offset: 0x0000CD24
	public void QuickSaveRootExample()
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.LoadImage(new byte[]
		{
			1,
			2,
			3,
			4,
			1,
			2,
			3,
			4,
			1,
			2,
			3,
			4,
			1,
			2,
			3,
			4
		});
		texture2D.Apply();
		QuickSaveRoot.Save<Texture2D>("RootName", texture2D);
		QuickSaveRoot.Load<Texture2D>("RootName");
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000EB70 File Offset: 0x0000CD70
	public void QuickSaveRawExample()
	{
		QuickSaveRaw.SaveString("TextFile.txt", "Some text to save");
		QuickSaveRaw.SaveBytes("BytesFile.txt", new byte[]
		{
			1,
			2,
			3,
			4
		});
		QuickSaveRaw.LoadString("TextFile.txt");
		QuickSaveRaw.LoadBytes("BytesFile.txt");
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
	public void QuickSaveReaderExample()
	{
		string one;
		double two;
		Vector2 three;
		Color four;
		QuickSaveReader.Create("RootName").Read<string>("Key1", delegate(string r)
		{
			one = r;
		}).Read<double>("Key2", delegate(double r)
		{
			two = r;
		}).Read<Vector2>("Key3", delegate(Vector2 r)
		{
			three = r;
		}).Read<Color>("Key4", delegate(Color r)
		{
			four = r;
		});
		QuickSaveReader quickSaveReader = QuickSaveReader.Create("RootName");
		one = quickSaveReader.Read<string>("Key1");
		two = quickSaveReader.Read<double>("Key2");
		three = quickSaveReader.Read<Vector2>("Key3");
		four = quickSaveReader.Read<Color>("Key4");
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000EC88 File Offset: 0x0000CE88
	public void QuickSaveWriterExample()
	{
		string value = "Hello World!";
		double value2 = 45.6789;
		Vector2 value3 = new Vector2(34f, 78.92f);
		Color value4 = new Color(0.1f, 0.5f, 0.8f, 1f);
		QuickSaveWriter.Create("RootName").Write<string>("Key1", value).Write<double>("Key2", value2).Write<Vector2>("Key3", value3).Write<Color>("Key4", value4).Commit();
		QuickSaveWriter quickSaveWriter = QuickSaveWriter.Create("RootName");
		quickSaveWriter.Write<string>("Key1", value);
		quickSaveWriter.Write<double>("Key2", value2);
		quickSaveWriter.Write<Vector2>("Key3", value3);
		quickSaveWriter.Write<Color>("Key4", value4);
		quickSaveWriter.Commit();
	}

	// Token: 0x04000236 RID: 566
	public InputField Input1;

	// Token: 0x04000237 RID: 567
	public InputField Input2;

	// Token: 0x04000238 RID: 568
	public InputField Input3;

	// Token: 0x04000239 RID: 569
	public InputField Input4;

	// Token: 0x0400023A RID: 570
	public InputField Input5;

	// Token: 0x0400023B RID: 571
	public InputField Input6;

	// Token: 0x0400023C RID: 572
	public InputField Input7;

	// Token: 0x0400023D RID: 573
	public InputField Input8;

	// Token: 0x0400023E RID: 574
	public InputField Content;
}
