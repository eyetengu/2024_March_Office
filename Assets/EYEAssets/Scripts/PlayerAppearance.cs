using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField] private Material[] _characterOutfit;
    private SkinnedMeshRenderer _renderer;
    private int _materialID;

    void Start()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ChooseRandomPaintJob();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChooseRandomPaintJob();
    }

    void ChooseRandomPaintJob()
    {
        var randomFinish = Random.Range(0, _characterOutfit.Length);
        _materialID = randomFinish;

        _renderer.material = _characterOutfit[_materialID];
    }
}
