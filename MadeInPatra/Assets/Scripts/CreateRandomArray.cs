using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateRandomArray : MonoBehaviour
{
    // Start is called before the first frame update
    private Queue<Sprite> letterQueue = new Queue<Sprite>();
    private int viewLetterNum = 8;//手紙の枚数（人数）
    List<int> randList;
    private void Start()
    {
        SelectViewLetter();
    }
    private void SelectViewLetter()
    {
        int[] letterCount = new int[viewLetterNum + 1];//手紙の枚数がそれぞれ何枚あるかを数える配列、手紙の枚数が表示可能枚数より大きいと困るしそれ以上は無いようにお願いしたいって感じなので配列生成は最大viewLetterNum + 1
        randList = new List<int>();//randの組み合わせに含めてよいList
        List<int>[] userList = new List<int>[viewLetterNum + 1];
        for (int i = 0; i < userList.Length; i++)
        {
            userList[i] = new List<int>();
        }
        for (int i = 0; i < StringProperty.letterCount.Length; i++)
        {
            letterCount[StringProperty.letterCount[i]]++;//それぞれの人の手紙の枚数の人が何名いるかのCount
            userList[StringProperty.letterCount[i]].Add(i);//userListの枚数番目にuserの数字を追加
        }
        for (int i = 1; i < letterCount.Length; i++)
        {
            if (letterCount[i] > 0)
            {
                randList.Add(i);//手紙の枚数カウントの中で1以上のものをRand生成用Listに追加
            }
        }
        //Queueに選択する手紙の枚数のListを追加
        //Sum(0, randList);
        ansList = new bool[500, StringProperty.letterCount.Length];
        GetRandList(0, -1, 0, ansList);
        //以下、File出力用(注意)
        /*StreamWriter sw = new StreamWriter("..\\logtextdateunti.txt", false);//プロジェクトフォルダの最上位に生成される
        sw.WriteLine(logText);
        sw.Flush();
        sw.Close(); */
        //Queueにある数字と同じ枚数の手紙の中からどの手紙を選択するかを決める
    }
    string logText;
    int logInt = 0;
    int ansCount = 0;
    bool[,] ansList;//答え格納用List配列
    private void GetRandList(int num, int index, int ansIndex, bool[,] ansList)
    {
        if (num == viewLetterNum)//合計数が一致
        {
            ansCount++;
            logText += "new[] { ";
            for (int i = 0; i < index + 1; i++)
            {
                logInt += ansList[i, ansIndex] ? StringProperty.letterCount[i] : 0;
                if (ansList[i, ansIndex] == true)
                {
                    logText += i;
                    if (i + 1 < index + 1)
                    {
                        logText += ",";
                    }
                }
            }
            logText += "}, \r\n";
            Debug.Log(logInt);
            logInt = 0;
            return;
        }
        if (num > viewLetterNum || index + 1 > StringProperty.letterCount.Length - 1)//合計数オーバー、配列外参照
        {
            return;
        }
        if (num < viewLetterNum)//合計数がまだ足りてない場合
        {
            ansList[index + 1, ansIndex] = false;
            GetRandList(num, index + 1, ansIndex, ansList);
            for (int i = 0; i <= index; i++)
            {
                ansList[i, ansIndex + 1] = ansList[i, ansIndex];
            }
            ansList[index + 1, ansIndex + 1] = true;
            GetRandList(num + StringProperty.letterCount[index + 1], index + 1, ansIndex + 1, ansList);
        }
    }
}