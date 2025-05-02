// dnSpy decompiler from Assembly-CSharp.dll class: AnimalsLevel
using System;
using System.Collections.Generic;

[Serializable]
public class AnimalsLevel
{
	public int count;

	public int totalWaves;

	public int initialCount;

	public int randomAnimalsCount;

	public List<AnimalMetaInspector> initialMeta;

	public List<AnimalMetaInspector> wavesMeta;

	public List<AnimalMetaInspector> randomAnimalsMeta;

	public List<int> waveGenerationCount;

	public List<int> animalsInWaveArray;

	public List<bool> positionFillIndex;

	public int objectivesId;
}
