using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CollectableData {
    public int collectable;
    public int[] collectableArray;
}

public class CollectableSaveLoader : MonoBehaviour
{
    public CollectableStorage collectableStorage;
    public void LoadData() {
        CollectableData data = Repository.GetData<CollectableData>();
        collectableStorage.SetupCollectable(data.collectable, data.collectableArray);
    }

    public void SaveData() {
        int collectable = collectableStorage.GetCollectableCount();
        int[] array = collectableStorage.GetCollectableArray();
        var data = new CollectableData();
        data.collectable = collectable;
        data.collectableArray = array;
        Repository.SetData(data);
    }
}
