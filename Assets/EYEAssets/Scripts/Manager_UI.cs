using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_UI : MonoBehaviour
{
    [SerializeField] private Transform _scoreboard;
    [SerializeField] private TMP_Text _scoreText;
    private float _step;
    private float _speed = 3;
    private float _speedMultiplier = 1;
    [SerializeField] private Transform _player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //_step = _speed * _speedMultiplier * Time.deltaTime;   

        //RotateToFacePlayer();
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score\n" + score;
    }

    void RotateToFacePlayer()
    {
        var targetDirection = transform.position - _player.position;
        var newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
