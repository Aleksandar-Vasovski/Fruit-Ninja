# Fruit-Ninja
Windows Forms Project for VP Course (C# Programming) by : Aleksandar Vasovski and Petar Bozarov
#1. Опис на апликацијата
Апликацијата што ја развивме е базирана на познатата игрица [Fruit Ninja](https://play.google.com/store/apps/details?id=com.halfbrick.fruitninjafree&hl=en), при што го сменивме начинот на кој се претставуваат елементите.

Со цел полесно запознавање со околината, на корисникот му ги нудиме веќе познатите дизајни на овошје и бомби и неколку тежински нивоа. Покрај графичките елементи во оваа игра е имплементирано и автоматско зачувување на корисничките профили и освоени поени, за да нему изгледа на корисникот дека игра без никаква цел.

#2. Упатство за користење
##2.1. Функционалности
###2.1.1. Нова Игра
Главната функционалност на апликацијата е можноста за започнување на нова игра.На [почетниот екран] (https://cloud.githubusercontent.com/assets/19373326/15275536/cc66699c-1ace-11e6-9ecf-bd7c6fe085d3.JPG) таа опција е означена со општо познатото [копче](https://cloud.githubusercontent.com/assets/19373326/15275547/159792b2-1acf-11e6-921d-f7025b052a9b.JPG) кое се наоѓа во корисничкиот интерфејс на скоро секоја андроид игра која се наоѓа на Google Play .

Со кликнување на копчето за Play на кое што има додадено и функционалност за [tooltip] (https://cloud.githubusercontent.com/assets/19373326/15275559/8328191e-1acf-11e6-91b9-a8f05aa3b9ce.JPG) започнува нова игра во која улогата на играч ја има моментално најавениот корисник.

При иницијализација на играта за моментално најавен корисник се смета играчот Guest при што опција за додавање / избор на постоечки играч се прави во менито за Корисници.

###2.1.2. Управување со корисници и додавање на нов корисник
User Management делот од нашата апликација е прикажан преку [мени за корисници] (https://cloud.githubusercontent.com/assets/19373326/15275574/bcbe8e92-1acf-11e6-88ca-52be3430809c.JPG) до кое се пристапува со кликнување на [копчето] (https://cloud.githubusercontent.com/assets/19373326/15275582/f0abb748-1acf-11e6-914b-72377dbf0447.JPG) од почетниот екран.

Во овој дел на корисникот му се дава можност за додавање на нов кориснички профил или избор на постоечки профил за продолжување на серијата резултати од тој профил.Моментално најавениот корисник нема опција да го избере својот кориснички профил повторно од листата на кориснички профили. 

Во ова мени како и во многу други е имплементирана и опцијата за враќање назад до почетниот екран -[Back] (https://cloud.githubusercontent.com/assets/19373326/15275594/3bfbc71a-1ad0-11e6-88f8-7fadd86f6e85.JPG). 

###2.1.3. Hall Of Fame
Делот од апликацијата кој му овозможува на корисникот за ги види своите достигнувања во истата или пак да погледне како е тој рангиран на листата на сите претходни корисници на програмата е овозможена со менито за [Highscores] (https://cloud.githubusercontent.com/assets/19373326/15275598/575b1c40-1ad0-11e6-9a34-d07afab45fa7.JPG) до кое се пристапува со кликнување на [копчето] (https://cloud.githubusercontent.com/assets/19373326/15275600/6d91d3a0-1ad0-11e6-9f03-ef4fb0ac406b.JPG) од почетниот екран.

Ова мени овозможува приказ на резултатите на моменталниот корисник и приказ на резултатите на сите претходни корисници на играта , заедно со *времето кога тие резултати биле остварени*. Листите за резултати на моменталниот корисник и сите корисници генерално ги содржат најдобрите 10 достигнувања соодветно.

###2.1.4. Поставки **(Settings)**
Менито за поставки на нашата апликација содржи неколку тежински нивоа кои имаат различно влијание на исходот на програмата во однос на добиените поени :

  1.  Лесно (*Easy*)
  2.  Средна тежина (*Medium*)
  3.  Тешко (*Hard*)

како и 2 можности за резолуција на прозорецот во кој се одвива играта :
  1.  800х600
  2.  1024х768

Дополнително, оневозможена е слободна промена на димензиите на прозорецот.

Достапувањето до менито за поставки прикажано на [сликата] (https://cloud.githubusercontent.com/assets/19373326/15275611/92544a1a-1ad0-11e6-8b12-a76bc865b0df.JPG) е овозможено со клик на [копчето] (https://cloud.githubusercontent.com/assets/19373326/15275615/aa57b70a-1ad0-11e6-96d0-06934b32c3bc.JPG) од почетниот екран.

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

###3.1.1. Game

```c#
public class Game
```

Во оваа класа се чуваат главните податоци за една инстанца од играта (како треба да изгледа екранот, кои елемененти треба да се појавуваат, кои се условите за крај на играта и слично).

###3.1.2. Element

```c#
public class Element
```

Во оваа класа се дефинирани елементите(овошја, бомби) кои се користат во играта, нивните графички репрезентации, позиции и слично.

###3.1.3. User

```c#
public class User
```

Ова е класата која се занимава со проблемот на корисниците, во која се чуваат имињата на играчите и нивните остварени поени.

###3.1.4. Score

```c#
public class Score
```

Оваа класа репрезентира еден запис за поените на даден корисник и времето на нивно освојување.

###3.1.5. Settings

```c#
public class Settings
```

Во оваа класа се чуваат реалните вредности на параметрите кои може да се изберат од менито *Settings*, како големина на играчкиот прозорец и тежина на играта.

##3.2. Серијализација

Со цел на корисникот да му прикажеме некаква последователност на играта, некои од податоците ги зачувуваме и автоматски ги вчитуваме од датотека (корисниците и нивните поени).

Податоците се вчитуваат со самото отварање на извршната верзија на апликацијата, а се зачувуваат пред самото затворање на прозорецот. На тој начин се одржува база на податоци на сите претходно најавени корисници. Тоа е изведено со помош на бинарна серијализација, при што за да може еден објект да се серијализира, класата од која тој е инстанциран мора да биде означена со **Serializable**.

```c#
    [Serializable]
    public class User
```

```c#
    [Serializable]
    public class Score : IComparable<Score>
```

##3.3. Имплементација на функција за програмско генерирање на елемент

```c#
   /*<summary>
    Generiranje na slucajni vrednosti za atributite na elementot i nivna inicijalizacija
    </summary>*/
    public void generate()
    {
        string difficulty = SettingsForm.settings.difficulty;
        int availableElements = 0;
        if (difficulty.ToUpper().Equals("EASY"))
            availableElements = 4;
        else if (difficulty.ToUpper().Equals("MEDIUM"))
            availableElements = 5;
        else if (difficulty.ToUpper().Equals("HARD"))
            availableElements = 6;
        int chosen = r.Next(availableElements);
        switch (chosen)
        {
            case 0:
                {
                    image = Properties.Resources.Banana;
                    type = "Banana";
                    break;
                }
            case 1:
                {
                    image = Properties.Resources.Green_Apple;
                    type = "Apple";
                    break;
                }
            case 2:
                {
                    image = Properties.Resources.Pineapple;
                    type = "Pineapple";
                    break;
                }
            case 3:
                {
                    image = Properties.Resources.Watermelon;
                    type = "Watermelon";
                    break;
                }
            case 4:
                {
                    image = Properties.Resources._10_Bomb;
                    type = "-10Bomb";
                    break;
                }
            case 5:
                {
                    image = Properties.Resources.bombGameOver;
                    type = "GameOverBomb";
                    break;
                }
            default:
                {
                    type = "";
                    break;
                }
        }
        int positions = (SettingsForm.settings.width - 20) / image.Width;
        int currentPosition = r.Next(positions);
        ulCorner = new Point(currentPosition * image.Width + 10, SettingsForm.settings.height - image.Height / 2);
        urCorner = new Point((currentPosition + 1) * image.Width, SettingsForm.settings.height - image.Height / 2);
        llCorner = new Point(currentPosition * image.Width + 10, SettingsForm.settings.height + image.Height / 2);
        directionX = 0;
        directionY = -10;
        if (currentPosition == 0)
        {
            directionX = 10;
        }
        else if (currentPosition < positions / 2)
        {
            int d = r.Next(2);
            if (d == 0)
                directionX = 10;
            else
                directionX = -10;
        }
        else if (currentPosition == positions - 1)
        {
            directionX = -10;
        }
        else
        {
            int d = r.Next(2);
            if (d == 0)
                directionX = 10;
            else
                directionX = -10;
        }
    }
```

Телото на оваа функција е поделено на неколку потцелини:
  1.  Дефинирање на генераторно множество на елементи (колку и кои овошја ќе се употребат и дали ќе има бомби, во зависност од избраната тежина).
  2.  На случаен начин се одбира слика за елементот од претходно споменатото множество.
  3.  Во зависност од ширината на прозорецот и ширината на елементите се одредува колку стартни позиции може да има секој елемент, па на случаен начин се одбира и позиција за тековниот елемент
  4.  Во зависност од стартната позиција се дефинира листа на точки кои се доволни за опис на позицијата на елементот во секој момент на времето.
  5.  Се одбира правец на движење на споменатиот елемент, при што кога ќе стигне до највисоката позиција на видливиот екран, се менува правецот во вертикално надолу.

##3.4. Кориснички интерфејс

Со цел при промена на димензиите на прозорецот елементите на екранот соодветно да се преместат искористено е TableLayoutPanel (Responsive Layout).
