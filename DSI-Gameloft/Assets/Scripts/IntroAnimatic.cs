using UnityEngine;
using System.Collections;

public class IntroAnimatic : MonoBehaviour {
    void Start() {
        Handheld.PlayFullScreenMovie("Animatic.mp4",Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.Fill);
    }
}
