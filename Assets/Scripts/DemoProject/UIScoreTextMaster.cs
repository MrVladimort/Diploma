using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIScoreTextMaster : MonoBehaviour
{
    [FormerlySerializedAs("InitPoints")] public float initPoints;
    [FormerlySerializedAs("LayerPushTo")] public string layerPushTo = "UI"; 
    // Start is called before the first frame update
    private float _currentPoints;

    private TextMesh _textMesh;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<MeshRenderer> ().sortingLayerName = layerPushTo;
        _textMesh = GetComponent<TextMesh>();
        _currentPoints = initPoints;
    }

    public void SetPoints(float points)
    {
        _currentPoints = points;
        _textMesh.text = _currentPoints.ToString();
    }
}
