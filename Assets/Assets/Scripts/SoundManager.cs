using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField] AudioClip arrow = null;
    [SerializeField] AudioClip death = null;
    [SerializeField] AudioClip fireball = null;
    [SerializeField] AudioClip gameOver = null;
    [SerializeField] AudioClip hit = null;
    [SerializeField] AudioClip level = null;
    [SerializeField] AudioClip newGame = null;
    [SerializeField] AudioClip rock = null;
    [SerializeField] AudioClip towerBuilt = null;

    public AudioClip Arrow {
        get {
            return arrow;
        }
    }
    public AudioClip Death
    {
        get
        {
            return death;
        }
    }
    public AudioClip Fireball
    {
        get
        {
            return fireball;
        }
    }
    public AudioClip GameOver
    {
        get
        {
            return gameOver;
        }
    }
    public AudioClip Hit
    {
        get
        {
            return hit;
        }
    }
    public AudioClip Level
    {
        get
        {
            return level;
        }
    }
    public AudioClip NewGame
    {
        get
        {
            return newGame;
        }
    }
    public AudioClip Rock
    {
        get
        {
            return rock;
        }
    }
    public AudioClip TowerBuilt
    {
        get
        {
            return towerBuilt;
        }
    }

}
