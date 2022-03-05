using System.Collections;
using System.Collections.Generic;
using System.Json;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEffectUI : MonoBehaviour
{

    private static Dictionary<string, int> effectUIsIndex = new() {
        {"health_boost", 2},
    };

    private static int effectIconSize = 50;
    private static int gapSize = 10;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEffect(string name, int value) {
        GameObject effectUi = this.gameObject.transform.parent.GetChild(effectUIsIndex[name]).gameObject;
        GameObject newEffectUI = Instantiate(effectUi, this.gameObject.transform, true);
        RearrangeIcons();
    }

    private void RearrangeIcons() {
        Vector3 position = this.transform.position;
        Vector3 size = this.GetComponent<Renderer>().bounds.size;
        int childCount = this.gameObject.transform.childCount;
        int yOffset = (int) (size.x - (effectIconSize * childCount + gapSize * (childCount - 1))) / 2;
        for (int i = 0; i < childCount; i++) {
            this.gameObject.transform.GetChild(i).SetPositionAndRotation(
                new Vector3(position.x, position.y + yOffset + effectIconSize * i + gapSize * (i - 1), 0),
                new Quaternion(0, 0, 0, 0));
        }
    }
}
