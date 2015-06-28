# DTW_Test
DTW（DPマッチング）のC#用ライブラリ

実装は次のサイトを参考にしました。コスト、経路等の用語はここを参考にしました。  
[https://staff.aist.go.jp/toru-nakata/dpmatching.html](https://staff.aist.go.jp/toru-nakata/dpmatching.html)

### 構成

* DTW_Library: ライブラリ本体
* DTW_Test: ライブラリの動作確認用プロジェクト

### 導入
「DTW_Library/bin/Debug」内の「DTW_Library.dll」を参照し、次のusingディレクティブを追加してください。
```csharp
using DTW;
```

### 使い方

1. 宣言  
  DTW距離を計算したい2つの配列を用意します。今回はint型の配列にしました。

    ```csharp
    int[] original = new int[]{...};
    int[] subject = new int[]{...};
    ```

  そののちに以下のコンストラクタを呼び出します。

    ```csharp
    BasicDTW<int> dtw = new BasicDTW<int>(original,subject,new NumericDistance(),true);
    ```
    
    コンストラクタ呼び出し後、各プロパティやメソッドにアクセスすることで計算結果を得られます。
    
    ```csharp
    double d = dtw.Distance; //DTW距離
    ```



### 仕様など
1. コンストラクタ
    
    ```csharp
    BasicDTW<Type>(Type[] original, Type[] subject, IDistance<Type> dp, bool isPathCalc);
    ```
2. 公開プロパティ

   ```csharp
   public List<DTW_Path> Path; //DPマッチング時の経路
   public double Distance; //DPマッチング（DTW）距離
   public double[,] Cost; //最終的なコスト行列
   public PathPattern[,] From; //どこから来たかを表す配列
   public TimeSpan CalcTime; //計算にかかった時間
   ```
   
3. 公開メソッド

    ```csharp
    public bool IsPath(int i,int j) //指定した要素の組み合わせが経路になっているかどうか
    public List<int> originalPairNum(int num) //originalの指定した要素と対応している要素番号をリスト形式で返すメソッド
    public List<int> subjectPairNum(int num) //subjectの指定した要素と対応している要素番号をリスト形式で返すメソッド
    ```
    
### オプション

* コンストラクタで`isPathCalc`を`false`にすることで、経路計算を行わないようにすることができ、計算時間を短縮できます。
* コンストラクタで`dp`を設定することで自由に距離関数を設定できます。その際は必ず`IDistance<T>`を継承してください。

##### 距離関数の使い方

距離関数は基本的に`IDistance<T>`を継承したクラスで定義されています。
具体的にはIDsitance.cs内に実装してあります。  
例えば`NumericDistance`クラスは2要素の差を距離として扱う距離関数で、`StringDistance`は2要素が同じときは0、異なるときは5を距離とする距離関数です。  
コンストラクタ上では次のように指定します。

    ```csharp
    int[] original = new int[]{...};
    int[] subject = new int[]{...};
    BasicDTW<int> dtw1 =  new BasicDTW<int>(original,subject,new NumericDistance(),true); 
    //
    string[] o = new string[]{...};
    string[] s = new string[]{...};
    BasicDTW<string> dtw2 = new BasicDTW<string>(o,s,new StringDistance(),true;)
    ```

