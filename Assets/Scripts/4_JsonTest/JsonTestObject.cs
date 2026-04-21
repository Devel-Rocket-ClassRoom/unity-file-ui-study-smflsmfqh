using UnityEngine;

public class JsonTestObject : MonoBehaviour
{
    public string prefabName;
    private Renderer mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>();
    }

    public void Set(ObjectSaveData data)
    {
        //prefabName = data.prefabName;
        transform.position = data.pos;
        transform.rotation = data.rot;
        transform.localScale = data.scale;
        mat.material.color = data.color;
    }

    public ObjectSaveData GetSaveData()
    {
        ObjectSaveData obj = new ObjectSaveData();
        obj.prefabName = prefabName;
        obj.pos = transform.position;
        obj.rot = transform.rotation;
        obj.scale = transform.localScale;
        obj.color = GetComponent<Renderer>().material.color;

        return obj;
    }
}
