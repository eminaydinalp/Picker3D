using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    List<CollectedObject> _collectedObjects;
    public CollectedObject[] CollectedObjects { get => FindObjectsOfType<CollectedObject>(); }

    public void SetCollectingObjects()
    {
        //foreach (var item in _collectedObjects)
        //{
        //    _collectedObjects.Add(item);
        //    item.gameObject.SetActive(true);
        //    item.transform.rotation = Quaternion.identity;
        //    item.transform.position = new Vector3(Random.Range(2f, -6f), 0.25f, Random.Range(50, -80));
        //}
    }
}
