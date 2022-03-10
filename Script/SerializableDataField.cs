using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//(3/3 저장용 파일)
[Serializable]
class SerializableDataField
{
    public int today = 0 ;
    public int gamenum = 0;
    //(3/7)폼에 따른 컷수 제한
    public int max_cut = 4;

   
}
