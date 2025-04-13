using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // 싱글톤 인스턴스
    AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // 싱글톤 인스턴스 설정
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        audioSource.clip = this.clip;
        audioSource.Play(); // 게임 시작 시 배경음악 재생
    }
}
