using UnityEngine;

namespace Core
{
    public class GeneratorSpacing : MonoBehaviour
    {
        [SerializeField] private float _spacing = 5f;

        private void OnValidate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = new Vector2(i * _spacing, 0);
            }
        }
    }
}
