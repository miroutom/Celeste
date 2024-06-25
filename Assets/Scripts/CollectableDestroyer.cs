using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDestroyer : MonoBehaviour
{
    public Collider2D collectable0;
    public Collider2D collectable1;
    public Collider2D collectable2;
    public Collider2D collectable3;
    public Collider2D collectable4;
    public Collider2D collectable5;
    public Collider2D collectable6;
    void Awake() {
        if (StaticData.collectable_array[0] == 1) {
            Destroy(collectable0.gameObject);
        }
        if (StaticData.collectable_array[1] == 1) {
            Destroy(collectable1.gameObject);
        }
        if (StaticData.collectable_array[2] == 1) {
            Destroy(collectable2.gameObject);
        }
        if (StaticData.collectable_array[3] == 1) {
            Destroy(collectable3.gameObject);
        }
        if (StaticData.collectable_array[4] == 1) {
            Destroy(collectable4.gameObject);
        }
        if (StaticData.collectable_array[5] == 1) {
            Destroy(collectable5.gameObject);
        }
        if (StaticData.collectable_array[6] == 1) {
            Destroy(collectable6.gameObject);
        }
    }
}
