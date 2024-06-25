using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableStorage : MonoBehaviour
{
    public static CollectableStorage Instance;
    public event Action<int> OnCollectableChanged;
    [SerializeField]
    private int currentCollectable;
    private int[] CollectableLevel1 = { 0, 0, 0, 0, 0, 0, 0 };
    public Text Counter;

    private void Awake() {
        Instance = this;
    }

    public void Start() {
        currentCollectable = StaticData.collectable_counter;
        CollectableLevel1 = StaticData.collectable_array;
        Counter.text = 'x' + currentCollectable.ToString();
    }

    public void SetupCollectable(int CollectableNum, int[] CollectableLevel1Num) {
        this.currentCollectable = CollectableNum;
        this.CollectableLevel1 = CollectableLevel1Num;
    }

    public int GetCollectableCount() {
        return this.currentCollectable;
    }

    public int[] GetCollectableArray() {
        return this.CollectableLevel1;
    }

    public void AddCollectable(int index) {
        this.currentCollectable += 1;
        this.CollectableLevel1[index] = 1;
        this.OnCollectableChanged?.Invoke(this.currentCollectable);
    }

    public void ClearProgress() {
        this.currentCollectable = 0;
        for (int i = 0; i < 7; ++i) {
            this.CollectableLevel1[i] = 0;
        }
        this.OnCollectableChanged?.Invoke(this.currentCollectable);
    }
}
