using System.Collections;
using System.Collections.Generic;

public class StringProperty
{
    public static string[] expressionName = new string[]{
        "normal"/*通常*/,
        "smile"/*笑顔*/,
        "closeEyes"/*目をつむる*/,
        "sumg"/*どや顔*/,
        "impatience"/*焦り*/,
        "surprise"/*驚き*/,
        "troubled"/*困り顔*/,
        "sadness"/*悲しみ*/,
        "anger"/*怒り*/,
        "doubt"/*疑問*/,
        "beforeBattle"/*戦闘前*/,
        "damned"/*呆れ顔*/,
        "grin"/*にやけ顔*/,
        "ashamed"/*恥じらい*/,
        "limited"/*限界*/,
        "sleep"/*眠る*/
    };
    public static string[] stillNames = new string[]{//Stillの名前を事前に格納,差分がある場合はStillの差分枚数の記述はしないこと(Editer上で行います)
        "Beginning",
        "Eli-maid",
        "Charlotte-maid",
        "Mico-Maid",
        "Marry-maid",
        "LastBattle",
        "Final-first",
        "Final-second",
        "Final-third",
        "ClearCheki",
    };

    public static string[] loadSceneName = new string[]{
        "Beginning",
        "Tutorial-before",
        "Tutorial-battle",
        "Tutorial-after",
        "Eli-before",
        "Eli-battle",
        "Eli-after",
        "Charlotte-before",
        "Charlotte-battle",
        "Charlotte-after",
        "Mico-before",
        "Mico-battle",
        "Mico-after",
        "Mary-before",
        "Mary-battle",
        "Mary-after",
        "Nanashi-before",
        "Nanashi-battle",
        "Nanashi-after",
        "Final",
        "Endroll"
    };

    public static string[] loadScenePanelName = new string[]{
        "Beginning",
        "Tutorial",
        "Eli",
        "Charlotte",
        "Mico",
        "Mary",
        "Nanashi",
        "Final",
    };
    /*"Beginning",
        "Tutorial-before",
        "Tutorial-battle",
        "Tutorial-after",
        "Eli-before",
        "Eli-battle",
        "Eli-after",
        "Charlotte-after",
        "Charlotte-before",
        "Charlotte-battle",
        "Mico-after",
        "Mico-before",
        "Mico-battle",
        "Mary-after",
        "Mary-before",
        "Mary-battle",
        "Nanashi-after",
        "Nanashi-before",
        "Nanashi-battle",
        "Final",
        "Endroll" */
}
