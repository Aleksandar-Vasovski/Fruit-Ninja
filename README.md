# Fruit-Ninja
Windows Forms Project for VP Course (C# Programming) by : Aleksandar Vasovski and Petar Bozarov
#1. Опис на апликацијата
Апликацијата што ја развивме е базирана на познатата игрица [Fruit Ninja](https://play.google.com/store/apps/details?id=com.halfbrick.fruitninjafree&hl=en), при што го сменивме начинот на кој се претставуваат елементите.

Со цел полесно запознавање со околината, на корисникот му ги нудиме веќе познатите дизајни на овошје и бомби и неколку тежински нивоа. Покрај графичките елементи во оваа игра е имплементирано и автоматско зачувување на корисничките профили и освоени поени, за да нему изгледа на корисникот дека игра без никаква цел.

#2. Упатство за користење
#3. Претставување на проблемот
##3.1. Податочни структури
Главните податоци и функции за исцртување на играта и нејзините елементи се чуваат во класа ```public class Main```.

Некои од поважните променливи и нивни дефиниции во коментари се следните:

``` public partial class Main : Form
{
        //dali treba da se promeni goleminata na prozorecot
        public static bool resize;
        //tekoven igrac
        public static User currentUser;
        //kolekcija od site igraci
        public static Dictionary<string, User> users;
        //najdobri ostvaruvanja na igracite
        public static List<Score> topScores;
        //instanca na igrata
        public Game game;
        //pomosna promenliva za brzina na generiranje na objekti
        public int ticks;
        //dali moze da se igra ili e pauzirana igrata
        public bool canClick;
        //lokacija na zacuvuvanje na sostojbata na igrata
        public string saveFile = Environment.CurrentDirectory + "\\save.txt";
}```
