using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void Reset() {
        StaticData.checkpoint_num = 0;
        StaticData.collectable_counter = 0;
        for (int i = 0; i < 7; ++i) {
            StaticData.collectable_array[i] = 0;
        }
    }
    public void Back() {
        SceneManager.LoadScene(0);
    }
}
