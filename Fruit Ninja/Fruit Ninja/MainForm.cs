using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.IO;

namespace Fruit_Ninja
{
    public partial class Main : Form
    {
        private CustomToolTip tip;
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

        public Main()
        {
            InitializeComponent();
            resize = false;
            SettingsForm.settings = new Settings(800, 600, "EASY");
            users = new Dictionary<string, User>();
            LoadFromFile();
            User u = new User("Guest");
            if (!users.ContainsKey("Guest"))
                users.Add("Guest", u);
            else
                foreach (User us in users.Values)
                    if (us.name.Equals("Guest"))
                    {
                        u = us;
                        break;
                    }
            currentUser = u;
            topScores = new List<Score>();
            DoubleBuffered = true;
            ticks = 0;
            canClick = false;

            //otstranuvanje na trepkanjeto pri iscrtuvanje na objektite
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            panelGame, new object[] { true });
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadFromFile();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToFile();
        }

        private void panelGame_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e);
            lblTime.Text = string.Format("00:{0:00}", game.time);
        }

        private void pbHighscores_Click(object sender, EventArgs e)
        {
            HighscoresForm highscores = new HighscoresForm();
            highscores.ShowDialog();
        }

        private void pbUser_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            user.ShowDialog();
            lblUser.Text = currentUser.ToString();
        }

        private void pbPlay_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void pbQuit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to quit? Your progress won't be saved.", "Are you a loser?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dr == DialogResult.Yes)
            {
                tableLayoutPanel1.Visible = true;
                panelGame.Visible = false;
                ActiveForm.BackgroundImage = Properties.Resources._2013_08_28_105146;
            }
        }

        private void pbSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                if (resize)
                {
                    if (ActiveForm.Width == 800)
                    {
                        ActiveForm.Width += 224;
                        ActiveForm.Height += 168;
                        resize = false;
                        ReallyCenterToScreen();
                    }
                    else
                    {
                        ActiveForm.Width -= 224;
                        ActiveForm.Height -= 168;
                        resize = true;
                        ReallyCenterToScreen();
                    }
                }
            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit the game?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
                ActiveForm.Close();
        }

        private void panelGame_Click(object sender, EventArgs e)
        {
            if (game.checkClick(PointToClient(Cursor.Position)))
                stopGame();
        }

        private void pbPlay_MouseHover(object sender, EventArgs e)
        {
            tip = new CustomToolTip(new Size(121, 48));
            tip.SetToolTip(pbPlay, "PLAY");
        }

        private void pbSettings_MouseHover(object sender, EventArgs e)
        {
            tip = new CustomToolTip(new Size(220, 48));
            tip.SetToolTip(pbSettings, "SETTINGS");
        }

        private void pbHighscores_MouseHover(object sender, EventArgs e)
        {
            tip = new CustomToolTip(new Size(312, 48));
            tip.SetToolTip(pbHighscores, "HALL OF FAME");
        }

        private void pbUser_MouseHover(object sender, EventArgs e)
        {
            tip = new CustomToolTip(new Size(121, 48));
            tip.SetToolTip(pbUser, "USER");
        }

        private void pbExit_MouseHover(object sender, EventArgs e)
        {
            tip = new CustomToolTip(new Size(121, 48));
            tip.SetToolTip(pbExit, "EXIT");
        }

        //prilagoduvanje na goleminata na imeto na igracot spored dolzinata
        private void lblUser_TextChanged(object sender, EventArgs e)
        {
            if (lblUser.Text.Length > 9)
                lblUser.Font = new Font(lblUser.Font.FontFamily, 8);
            else if (lblUser.Text.Length > 6)
                lblUser.Font = new Font(lblUser.Font.FontFamily, 10);
            else
                lblUser.Font = new Font(lblUser.Font.FontFamily, 12);
        }

        //prodolzuvanje so igrata
        private void pbUnpause_Click(object sender, EventArgs e)
        {
            canClick = true;
            pbUnpause.Visible = false;
            pbPause.Visible = true;
            pbQuit.Visible = false;
            gameTimer.Start();
            timeTimer.Start();
        }

        //stopiranje na igrata
        private void pbPause_Click(object sender, EventArgs e)
        {
            canClick = false;
            pbUnpause.Visible = true;
            pbPause.Visible = false;
            pbQuit.Visible = true;
            gameTimer.Stop();
            timeTimer.Stop();
        }

        //tajmer za generiranje objekti
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            lblScore.Text = game.currentScore.points.ToString();
            if (lblScore.Right > Width)
                lblScore.Left = Width - lblScore.Width - 20;
            int speed = 0;
            switch (SettingsForm.settings.difficulty)
            {
                case "EASY":
                    {
                        speed = 40;
                        break;
                    }
                case "MEDIUM":
                    {
                        speed = 30;
                        break;
                    }
                case "HARD":
                    {
                        speed = 20;
                        break;
                    }
            }
            if (ticks++ % speed == 0)
                game.elements.Add(new Element());
            game.MoveElements();
            Invalidate(true);
        }

        //tajmer za odreduvanje na kraj na igrata
        private void timeTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = string.Format("00:{0:00}", --game.time);
            if (game.time == 0)
                stopGame();
        }

        /*<summary>
        Startuvanje na nova igra
        </summary>*/
        public void newGame()
        {
            pbUnpause.Visible = true;
            pbPause.Visible = false;
            lblScore.Text = "0";
            game = new Game();
            tableLayoutPanel1.Visible = false;
            panelGame.Visible = true;
            ActiveForm.BackgroundImage = game.background;
            switch (SettingsForm.settings.difficulty)
            {
                case "EASY":
                    {
                        gameTimer.Interval = 50;
                        break;
                    }
                case "MEDIUM":
                    {
                        gameTimer.Interval = 30;
                        break;
                    }
                case "HARD":
                    {
                        gameTimer.Interval = 20;
                        break;
                    }
            }
            Invalidate(true);
        }


        /*<summary>
        Kraj na igrata i soodvetna poraka
        </summary>*/
        public void stopGame()
        {
            Score currentScore = new Score(int.Parse(lblScore.Text), DateTime.Now, lblUser.Text);
            topScores.Add(currentScore);
            currentUser.addScore(currentScore);
            gameTimer.Stop();
            timeTimer.Stop();
            string text = "";
            string title = "";
            if (game.time != 0)
            {
                text = string.Format("Thank you for playing {0}. You have scored {1} points! Play again?", currentUser.name, game.currentScore.points);
                title = "Game Over :(";
            }
            else
            {
                text = string.Format("No more time {0}. You have scored {1} points! Play again?", currentUser.name, game.currentScore.points);
                title = "Time Up :(";
            }
            DialogResult dr = MessageBox.Show(text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                newGame();
            }
            else
            {
                tableLayoutPanel1.Visible = true;
                panelGame.Visible = false;
                ActiveForm.BackgroundImage = Properties.Resources._2013_08_28_105146;
            }
        }

        /*<summary>
        Poramnuvanje na prozorecot za da bide sekogash na centar na ekranot 
        </summary>*/
        protected void ReallyCenterToScreen()
        {
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - Height) / 2)
            };
        }

        /*<summary>
        Zacuvuvanje na igracite i nivnite poeni vo datoteka
        </summary>*/
        public void SaveToFile()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(saveFile, FileMode.Create, FileAccess.Write);
                bf.Serialize(fs, users.Values.ToArray());
                fs.Close();
            }
            catch(Exception)
            {

            }
        }

        /*<summary>
        Citanje na igraci i poeni od datoteka
        </summary>*/
        public void LoadFromFile()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(saveFile,FileMode.Open,FileAccess.Read);
                User[] userList = (User[])bf.Deserialize(fs);
                fs.Close();
                users = userList.ToDictionary((x) => x.name, (x) => x);
                foreach(User user in users.Values)
                    foreach (Score s in user.scores)
                        topScores.Add(s);
                User u = new User("Guest");
                if (!users.ContainsKey("Guest"))
                    users.Add("Guest", u);
                else
                    foreach (User us in users.Values)
                        if (us.name.Equals("Guest"))
                        {
                            u = us;
                            break;
                        }
                currentUser = u;
            }
            catch (Exception)
            {

            }
        }

    }
}
