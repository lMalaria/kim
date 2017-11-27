using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaKaoExample
{
    public class KaKaoBoard
    {
        const int m = 6;
        const int n = 6;

        int count = 0;
        bool isCapital;
        bool willDown = false;
        

        //보드 생성
        string[,] gameBoard = new string[m, n] { 
            {"T","T","T","A","N","T"}, 
            {"R","R","F","A","C","C"}, 
            {"R","R","R","F","C","C"}, 
            {"T","R","R","R","A","A"}, 
            {"T","T","M","M","M","F"},
            {"T","M","M","T","T","K"}
        };

        //보드 확인
        public void ShowgameBoard()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(gameBoard[i, j]);
                }

                Console.WriteLine("");
            }
        }

        // 대문자 인지 소문자인지 확인 함수
        public int CheckCapital(string boardCell)
        {
            int stringToInt = Convert.ToChar(boardCell);

            // 대문자
            if (stringToInt >= 65 && stringToInt <= 90) 
            {
                isCapital = true;
                
                return 1;
                
            }

            //소문자
            else if (stringToInt >= 97 && stringToInt <= 122) 
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

        public int CompareStrings(string a , string b)
        {

     
            int aInt = Convert.ToChar(a);
            int bInt = Convert.ToChar(b);
            
            // 대문자 vs 대문자
            if(CheckCapital(a) == 1 && CheckCapital(b) == 1 && aInt == bInt )
            {
                return 1;
            }

            // 대문자 vs 소문자
            else if (CheckCapital(a) == 1 && CheckCapital(b) == 0 && ( aInt == (bInt - 32) ) )
            {
                return 1;
            }

            // 소문자 vs 대문자
            else if (CheckCapital(a) == 0 && CheckCapital(b) == 1 && ( (aInt - 32) == bInt) )
            {
                return 1;
            }

            // 소문자 vs 소문자
            else if (CheckCapital(a) == 0 && CheckCapital(b) == 0 && aInt == bInt)
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
                            gameBoard[a , b] != "" &&
                            CompareStrings(gameBoard[a , b], gameBoard[a , b+1]) == 1 && 
                            CompareStrings(gameBoard[a , b], gameBoard[a+1 , b]) == 1 &&
                            CompareStrings(gameBoard[a , b], gameBoard[a+1 , b+1]) == 1
                       )

                    {
                       if( CheckCapital(gameBoard[a,b]) == 1)
                       {
                            gameBoard[a, b] = gameBoard[a, b].ToLower();
                            count++;
                       }

                       if( CheckCapital(gameBoard[a,b+1]) == 1)
                       {
                            gameBoard[a, b + 1] =  gameBoard[a, b + 1].ToLower();
                            count++;
                       }

                       if( CheckCapital(gameBoard[a+1, b]) == 1)
                       {
                            gameBoard[a + 1, b] = gameBoard[a + 1, b].ToLower();
                            count++;
                       }

                       if( CheckCapital(gameBoard[a+1, b+1]) == 1)
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
           
            
                for(int a=0; a<m; a++)
                {
                    for(int b=0; b<n; b++)
                    {
                        if(CheckCapital(gameBoard[a,b]) == 0)
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
                        if (gameBoard[a + 1, b] == " " && gameBoard[a,b] != " ")
                        {
                            gameBoard[a + 1, b] = gameBoard[a, b];
                            gameBoard[a, b] = " ";
                            willDown = true;
                        }

                    }
                }
            
        }
        
        public void Update()
        {
            do
            {

                CheckFriends();
                DeleteFriends();
                MoveDownFriends();
                
            } while (willDown);

        }      
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            KaKaoBoard KaKao = new KaKaoBoard();

            Console.WriteLine("게임 진행 전 보드판 표시");
            Console.WriteLine("");

            KaKao.ShowgameBoard();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("게임을 진행 하시려면 Y혹은 y키를 누르세요");
            Console.WriteLine("게임을 종료하시려면 아무키나 누르세요");
           
            string Input = Console.ReadLine();
            Console.WriteLine("");

            if (Input == "Y" || Input == "y")
            {
                KaKao.Update();
                KaKao.ShowgameBoard();
            }

            else 
            {
                Console.WriteLine("게임을 종료합니다.");
            }



        }
    }
}
