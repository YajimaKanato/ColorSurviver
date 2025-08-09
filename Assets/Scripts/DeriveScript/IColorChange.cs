using UnityEngine;
using ColorAttributes;

/// <summary>
/// 色を抜くことに関連するインターフェース
/// </summary>
interface IColorChange
{
    /// <summary>
    /// 色を抜くときに呼び出す関数
    /// この中で実際に行う関数を呼び出す
    /// </summary>
    /// <param name="color"> 抜く色</param>
    public void ColorChange(ColorAttribute color);
}
