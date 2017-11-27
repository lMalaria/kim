using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaKao : MonoBehaviour {

    const int m = 6;
    const int n = 6;

    string[,] gameBoard = new string[m, n] {
            {"T","T","T","A","N","T"},
            {"R","R","F","A","C","C"},
            {"R","R","R","F","C","C"},
            {"T","R","R","R","A","A"},
            {"T","T","M","M","M","F"},
            {"T","M","M","T","T","K"}
        };

    int count = 0;
    bool isCapital;
    bool willDown = false;

    public void ShowgameBoard()
    {
        for (int i = 0; i < 6; i++)
        {
          Debug.Log(gameBoard[i,0] + " " + gameBoard[i, 1] + " " + gameBoard[i,2] + " " + gameBoard[i, 3] + " " + gameBoard[i,4] + " " + gameBoard[i, 5]);

        
        }
    }

    // 대문자 인지 소문자인지 확인 함수
    public int CheckCapital(string boardCell)
    {
        char[] stringToChar = boardCell.ToCharArray();
        int charToint = stringToChar[0];

        // 대문자
        if (charToint >= 65 && charToint <= 90)
        {
            isCapital = true;

            return 1;

        }

        //소문자
        else if (charToint >= 97 && charToint <= 122)
        {
            isCapital = false;

            return 0;

        }

        //그 이외
        else
        {

            return -1;

        }

    }

    public int CompareStrings(string a, string b)
    {

        char[] stringToChar1 = a.ToCharArray();
        char[] stringToChar2 = b.ToCharArray();

        int charToint1 = stringToChar1[0];
        int charToint2 = stringToChar2[0];



        // 대문자 vs 대문자
        if (CheckCapital(a) == 1 && CheckCapital(b) == 1 && charToint1 == charToint2)
        {
            return 1;
        }

        // 대문자 vs 소문자
        else if (CheckCapital(a) == 1 && CheckCapital(b) == 0 && (charToint1 == (charToint2 - 32)))
        {
            return 1;
        }

        // 소문자 vs 대문자
        else if (CheckCapital(a) == 0 && CheckCapital(b) == 1 && ((charToint1 - 32) == charToint2))
        {
            return 1;
        }

        // 소문자 vs 소문자
        else if (CheckCapital(a) == 0 && CheckCapital(b) == 0 && charToint1 == charToint2)
        {
            return 1;
        }

        // 이것 이외 비교할 필요 없음
        else
        {
            return 0;
        }
    }

    public void CheckFriends()
    {

        for (int a = 0; a < m - 1; a++)
        {
            for (int b = 0; b < n - 1; b++)
            {
                if (
                        gameBoard[a, b] != "" &&
                        CompareStrings(gameBoard[a, b], gameBoard[a, b + 1]) == 1 &&
                        CompareStrings(gameBoard[a, b], gameBoard[a + 1, b]) == 1 &&
                        CompareStrings(gameBoard[a, b], gameBoard[a + 1, b + 1]) == 1
                   )

                {
                    if (CheckCapital(gameBoard[a, b]) == 1)
                    {
                        gameBoard[a, b] = gameBoard[a, b].ToLower();
                        count++;
                    }

                    if (CheckCapital(gameBoard[a, b + 1]) == 1)
                    {
                        gameBoard[a, b + 1] = gameBoard[a, b + 1].ToLower();
                        count++;
                    }

                    if (CheckCapital(gameBoard[a + 1, b]) == 1)
                    {
                        gameBoard[a + 1, b] = gameBoard[a + 1, b].ToLower();
                        count++;
                    }

                    if (CheckCapital(gameBoard[a + 1, b + 1]) == 1)
                    {
                        gameBoard[a + 1, b + 1] = gameBoard[a + 1, b + 1].ToLower();
                        count++;
                    }


                }

            }

        }



    }

    public void DeleteFriends()
    {


        for (int a = 0; a < m; a++)
        {
            for (int b = 0; b < n; b++)
            {
                if (CheckCapital(gameBoard[a, b]) == 0)
                {
                    gameBoard[a, b] = " ";
                }
            }
        }

    }

    public void MoveDownFriends()
    {


        willDown = false;

        for (int a = 0; a < m - 1; a++)
        {
            for (int b = 0; b < n; b++)
            {
                if (gameBoard[a + 1, b] == " " && gameBoard[a, b] != " ")
                {
                    gameBoard[a + 1, b] = gameBoard[a, b];
                    gameBoard[a, b] = " ";
                    willDown = true;
                }

            }
        }

    }

    public void KaKaoUpdate()
    {
        do
        {
            CheckFriends();
            DeleteFriends();
            MoveDownFriends();

        } while (willDown);

    }
    // Use this for initialization
    void Start() {

        KaKao kakao = new KaKao();

        Debug.Log("게임 진행 전 보드판 표시");
 

        kakao.ShowgameBoard();






        Debug.Log("게임 진행 후 보드판 표시");

        kakao.KaKaoUpdate();
            kakao.ShowgameBoard();
        



    }

    // Update is called once per frame
    void Update()
    {

    }

}
