# Fruit-Ninja
Windows Forms Project for VP Course (C# Programming) by : Aleksandar Vasovski and Petar Bozarov
#1. Опис на апликацијата
Апликацијата што ја развивме е базирана на познатата игрица [Fruit Ninja](https://play.google.com/store/apps/details?id=com.halfbrick.fruitninjafree&hl=en), при што го сменивме начинот на кој се претставуваат елементите.

Со цел полесно запознавање со околината, на корисникот му ги нудиме веќе познатите дизајни на овошје и бомби и неколку тежински нивоа. Покрај графичките елементи во оваа игра е имплементирано и автоматско зачувување на корисничките профили и освоени поени, за да нему изгледа на корисникот дека игра без никаква цел.

#2. Упатство за користење
###2.1.1 Нова Игра
Главната функционалност на апликацијата е можноста за започнување на нова игра.На почетниот екран (тука слика) таа опција е означена со општо познатото копче кое се наоѓа во корисничкиот интерфејс на скоро секоја андроид игра која се наоѓа на Google Play (тука слика 2).

Со кликнување на копчето за Play на кое што има додадено и функционалност за дополнителен опиц преку контролата tooltip (тука слика 3) започнува нова игра во која улогата на играч моментално најавениот корисник.

При иницијализација на играта за моментално најавен корисник се смета играчот Guest при што опција за додавање / избор на постоечки играч се прави во менито за Корисници.

###2.1.2 Управување со корисници и додавање на нов корисник
User Management делот од нашата апликација е прикажан преку мени за корисници (тука слика) до кое се пристапува со кликнување на копчето (тука слика 2) од почетниот екран.

Во овој дел на корисникот му се дава можност за додавање на нов кориснички профил или избор на постоечки профил за продолжување на серијата резултати од тој профил.Моментално најавениот корисник нема опција да го избере својот кориснички профил повторно од листата на кориснички профили. 

Во ова мени како и во многу други е имплементирана и опцијата за враќање назад до почетниот екран - ```Back``` (тука слика). 

###2.1.3 Hall Of Fame
Делот од апликацијата кој му овозможува на корисникот за ги види своите достигнувања во истата или пак да погледне како е тој рангиран на листата на сите претходни корисници на програмата е овозможена со менито за ```Highscores``` (тука слика) до кое се пристапува со кликнување на копчето (тука слика 2) од почетниот екран.

Ова мени овозможува приказ на резултатите на моменталниот корисник и приказ на резултатите на сите претходни корисници на играта , заедно со *времето кога тие резултати биле остварени*.Листите за резултати на моменталниот корисник и сите корисници генерално ги содржат најдобрите 10 достигнувања соодветно.

###2.1.4 Поставки ```(Settings)```
Менито за поставки на нашата апликација содржи неколку тежински нивоа кои имаат различно влијание на исходот на програмата во однос на добиените поени :

  1.  Лесно (*Easy*)
  2.  Средна тежина (*Medium*)
  3.  Тешко (*Hard*)

како и 2 можности за резолуција на прозорецот во кој се одвива играта :
  1.  800х600
  2.  1024х768

Достапувањето до менито за поставки прикажано на сликата (тука слика) е овозможено со клик на копчето (тука слика 2) од почетниот екран.
#3. Претставување на проблемот
##3.1. Податочни структури
Главните податоци и функции за исцртување на играта и нејзините елементи се чуваат во класа ```public class Main```.

Некои од поважните променливи и нивни дефиниции во коментари се следните:

``` c#
public partial class Main : Form
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
}
```

##3.1.1.Game

```c#
public class Game
```

Во оваа класа се чуваат главните податоци за една инстанца од играта (како треба да изгледа екранот, кои елемененти треба да се појавуваат, кои се условите за крај на играта и слично).
