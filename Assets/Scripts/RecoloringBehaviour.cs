using UnityEngine;
using Random = UnityEngine.Random;

public class RecoloringBehaviour : MonoBehaviour
{
    [SerializeField] private float _hueCubeMin;
    [SerializeField] private float _hueCubeMax;
    [SerializeField] private float _saturationCubeMin;
    [SerializeField] private float _saturationCubeMax;
    [SerializeField] private float _brightnessMin;
    [SerializeField] private float _brightnessMax;

    [SerializeField] private float _colorChangeTime;
    [SerializeField] private float _delayAfterColorChange;

    private Renderer _renderer;
    private Color _startColor;
    private Color _nextColor;

    private float _recoloringTime;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }
    private void GenerateNextColor()
    {
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(_hueCubeMin, _hueCubeMax, _saturationCubeMin, _saturationCubeMax, _brightnessMin,
            _brightnessMax);
    }
    private void Update()
    {
        if (_colorChangeTime < _delayAfterColorChange)
        {
            _colorChangeTime += Time.deltaTime;
            return;
        }
        if (_recoloringTime < _colorChangeTime)
        {
            _recoloringTime += Time.deltaTime;
            _renderer.material.color = Color.Lerp(_startColor, _nextColor, _recoloringTime / _colorChangeTime);
        }
        else
        {
            _colorChangeTime = 0;
            _recoloringTime = 0;
            GenerateNextColor();
        }
    }
}