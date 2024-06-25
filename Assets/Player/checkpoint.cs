using UnityEngine;

public class checkpoint : MonoBehaviour {
    public Transform player;
    public int index;
    public SaveLoadManager saveLoadManager;
    public CollectableStorage collectableStorage;
    void Awake() {
        player = GameObject.Find("Player").transform;
        if (StaticData.checkpoint_num == index) {
            player.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            StaticData.checkpoint_num = index;
            StaticData.collectable_counter = collectableStorage.GetCollectableCount();
            StaticData.collectable_array = collectableStorage.GetCollectableArray();
            saveLoadManager.SaveGame();
        }
    }
}
