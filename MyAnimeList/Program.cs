using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnimeList
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice;
            string server = "localhost";
            string database = "myanimelistdb";
            string username = "root";
            string password = "";
            string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
            MySqlConnection conn = new MySqlConnection(constring);

            while (true)
            {
                List<string> anime = new List<string>();
                Console.WriteLine("1. Wyświetl Liste Anime");
                Console.WriteLine("2. Wyszukaj Anime po tytule");
                Console.WriteLine("3. Dodaj Anime do listy");
                Console.WriteLine("4. Aktualizuj Anime");
                Console.WriteLine("5. Usuń z listy Anime");
                Console.WriteLine("Wpisz 'ext' żeby wyłączyć");
                Console.Write("Wybór: ");
                choice = Console.ReadLine();
                Console.WriteLine("");

                if (choice.Equals("1"))
                {
                    conn.Open();
                    string query = "select * from animelist";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Console.WriteLine(reader["id"] + ". " + reader["name"] + " " + reader["tag"] + " " + reader["tag1"] + " " + reader["tag2"]);
                        Console.WriteLine(reader["name"] + ", ");             
                    }
                    Console.ReadLine();
                    conn.Close();
                }

               

                if(choice.Equals("2"))
                {
                    conn.Open();
                    Console.Write("Podaj szukany tytuł: ");
                    string wyszuk = Console.ReadLine();
                    Console.WriteLine("");
                    string wyszukaj = "select * from animelist where name like '" + wyszuk + "%';";
                    MySqlCommand wyszuki = new MySqlCommand(wyszukaj, conn);
                    MySqlDataReader wyszukiw = wyszuki.ExecuteReader();
                    
                    while(wyszukiw.Read())
                    { 
                        Console.WriteLine("ID: "+ wyszukiw["id"] + "\n"+ "Tytuł: " + wyszukiw["name"] + "\n" + "tagi: " + wyszukiw["tag"] + " " + wyszukiw["tag1"] + " " + wyszukiw["tag2"] + "\n" + "Ilość odcinków: " + wyszukiw["odc"] + "\n" + "Status: " + wyszukiw["stat"]);
                    }
                    Console.ReadLine();
                    conn.Close();
                }
                if (choice.Equals("3"))
                {
                    Console.Write("Nazwa: ");
                    string nazwat = Console.ReadLine();
                    Console.Write("Tag: ");
                    string tg = Console.ReadLine();
                    Console.Write("Tag: ");
                    string tg1 = Console.ReadLine();
                    Console.Write("Tag: ");
                    string tg2 = Console.ReadLine();
                    Console.Write("Ilość Odcinków: ");
                    string odct = Console.ReadLine();
                    Console.Write("Status (Wstrzymane/ Trwa/ Zakończone): ");
                    string stat = Console.ReadLine();

                    anime.Add(nazwat); //0
                    anime.Add(tg);     //1
                    anime.Add(tg1);    //2
                    anime.Add(tg2);    //3
                    anime.Add(odct);   //4
                    anime.Add(stat);   //5
                                  
                    string insertmysql = "INSERT INTO animelist (name, tag, tag1, tag2, odc, stat) VALUES ('" + anime[0] + "', '" + anime[1] + "', '" + anime[2] + "', '" + anime[3] + "', '" + anime[4] + "', '" + anime[5] + "')";
                    Console.ReadLine();
                    MySqlCommand insertCommand = new MySqlCommand(insertmysql, conn);

                    conn.Open();
                    insertCommand.ExecuteNonQuery();
                    conn.Close();
                }

                if(choice.Equals("4"))
                {
                    List<string> updat = new List<string>();
                    Console.WriteLine("Które dane chcesz zmienić?");
                    Console.Write("name, tag, tag1, tag2, odc, stat: ");
                    string uw = Console.ReadLine();
                    updat.Add(uw);
                    Console.WriteLine("");
                    Console.Write("Na co ma być zamienione?: ");
                    string uw1 = Console.ReadLine();
                    updat.Add(uw1);
                    Console.WriteLine("");
                    Console.Write("Podaj ID anime: ");
                    string uw2 = Console.ReadLine();
                    updat.Add(uw2);

                    string updatemysql = "update animelist set " + updat[0] + "='" + updat[1] + "' where id='" + updat[2] + "'";
                    MySqlCommand updateCommand = new MySqlCommand(updatemysql, conn);

                    conn.Open();
                    updateCommand.ExecuteNonQuery();
                    conn.Close();
                }

                if (choice.Equals("5"))
                {
                    Console.WriteLine("Które Anime chcesz usunąć z listy?");
                    Console.Write("Podaj ID: ");
                    string idd = Console.ReadLine();
                    string deletemysql = "delete from animelist where id= '" + idd + "'";
                    MySqlCommand deleteCommand = new MySqlCommand(deletemysql, conn);

                    conn.Open();
                    deleteCommand.ExecuteNonQuery();
                    conn.Close();
                }

                if(choice.Equals("ext"))
                {
                    Environment.Exit(0);
                }

                Console.Clear();
            }
        }
    }
}
