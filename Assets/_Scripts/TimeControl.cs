using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private float _timeScale = 1f;

    private void Update()
    {
        Time.timeScale = _timeScale;
    }
}
