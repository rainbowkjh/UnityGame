using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작업환경에서
/// 오브젝트를 리스트에 추가하고
/// Scene에 오브젝트를 생성 시킨다
/// (에디트 스크립트에서 사용)
/// </summary>
public class CreateObjManager : MonoBehaviour
{
    public List<Object> objList;
    
    /// <summary>
    /// 오브젝트를 리스트에 추가 시킨다
    /// </summary>
    /// <param name="obj"></param>
    public void AddObj(Object obj)
    {
        objList.Add(obj);        
    }

    /// <summary>
    /// 오브젝트 인덱스 값으로 리스트에서 제거한다
    /// </summary>
    /// <param name="index"></param>
    public void DeleteObj(int index)
    {
        objList.RemoveAt(index);
    }

    /// <summary>
    /// 지정한 위치에 오브젝트를 생성 시킨다
    /// </summary>
    /// <param name="index"></param>
    /// <param name="pos"></param>
    public void CreateObj(int index, Vector3 pos)
    {
        Instantiate(objList[index], pos, Quaternion.identity);
    }


}
