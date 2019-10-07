/**
 * SpawnDamageText.cs
 * Created by Akeru on 07/10/2019
 */

using UnityEngine;

public class SpawnDamageText : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _textPrefab;

    #endregion

    #region MonoBehaviour Functions

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SpawnText(Random.Range(1, 200));
        }
    }
    #endregion

    #region Other Functions

    public void SpawnText(int damage)
    {
        var text = SimplePool.Spawn(_textPrefab, Vector3.zero, Quaternion.identity);
        text.transform.SetParent(gameObject.transform);
        text.transform.localScale = Vector3.zero;
        text.GetComponent<DamageText>().SpawnText(damage);
    }
    #endregion
}
