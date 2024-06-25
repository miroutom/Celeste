using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SaveLoadManager : MonoBehaviour {
    public CollectableSaveLoader collectableSaveLoader;

    [ContextMenu("Load Game")]
    public void LoadGame() {
        Repository.LoadState();
        this.collectableSaveLoader.LoadData();
    }

    [ContextMenu("Save Game")]
    public void SaveGame() {
        this.collectableSaveLoader.SaveData();
        Repository.SaveState();
    }

    void OnPreRender() {
        LoadGame();
    }
}
