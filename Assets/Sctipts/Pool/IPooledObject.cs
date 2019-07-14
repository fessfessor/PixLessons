using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject {

    IEnumerator OnSpawnFromPool(float delay);

    IEnumerator OnReturnToPool(GameObject gameObject, float delay);
}
