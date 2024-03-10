using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Television : MonoBehaviour
{
    MeshRenderer _renderer;

    Material[] _mats;
    [SerializeField] private Material[] _tvStation;
    
    int _stationID;    
    [SerializeField] bool _randomStation;


    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _mats = _renderer.sharedMaterials;
        
        StartCoroutine(StationTimer());
    }

    
    void SwitchStation()
    {
        if(_randomStation)
        {
            _stationID = Random.Range(0, _tvStation.Length - 1);
        }
        else
        {
            _stationID++;

            if (_stationID > _tvStation.Length - 1)
                _stationID = 0;
        }

        _mats[1] = _tvStation[_stationID];
        _renderer.sharedMaterials = _mats;

        StartCoroutine(StationTimer());
    }

    IEnumerator StationTimer()
    {
        Debug.Log("Entered Timer");
        yield return new WaitForSeconds(2);
        SwitchStation();
    }

}
