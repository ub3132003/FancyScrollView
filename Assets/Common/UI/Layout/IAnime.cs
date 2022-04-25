using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnime 
{
     bool Anime { get; set; }
     float AnimeXDuration { get; set; }
     float AnimeYDuration { get; set; }
     TweenParamSO ParamSO { get; set; }
}
