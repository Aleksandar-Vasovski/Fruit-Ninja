using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Fruit_Ninja
{
    public class Game
    {
        public Image background;
        public List<Element> elements;
        public Score currentScore;
        public int bombsClicked;
        public int time;
        public static Random r = new Random(); 


        /*<summary>
        Inicijalizacija na nova igra i postavuvanje na 
        slucajno odbrana pozadina
        </summary>*/
        public Game()
        {
            elements = new List<Element>();
            bombsClicked = 0;
            currentScore = new Score(0,new DateTime(),"");
            time = 60;
            switch (r.Next(3))
            {
                case 0:
                    {
                       background = Properties.Resources.background1;
                        break;
                    }
                case 1:
                    {
                        background = Properties.Resources.background2;
                        break;
                    }
                case 2:
                    {
                        background = Properties.Resources.background3;
                        break;
                    }
            }
        }


        /*<summary>
        Crtanje na site elementi
        </summary>*/
        public void Draw(PaintEventArgs e)
        {
            foreach(Element el in elements)
                el.Draw(e);
        }


        /*<summary>
        Dvizenje na elementite (ovosje, bombi) i odzemanje poeni 
        za propustenite ovosja
        </summary>*/
        public void MoveElements()
        {
            foreach(Element el in elements)
                el.Move();
            for(int i = elements.Count - 1; i >= 0; i--)
            {
                if (!(elements[i].type.Equals("-10Bomb") || elements[i].type.Equals("GameOverBomb")) && elements[i].ulCorner.Y >= SettingsForm.settings.height)
                {
                    currentScore.points -= 2;
                    elements.Remove(elements[i]);
                    continue;
                }
            }
        }


        /*<summary>
        Proveruva dali tockata koja e prenesena kako argument 
        e del od nekoj od elementite i ako e, togash soodvetno
        dodeluva poeni
        </summary>*/
        public bool checkClick(Point point)
        {
            foreach (Element el in elements)
            {
                if (el.isClicked(point))
                {
                    if (bombsClicked == 3)
                        return true;
                    if (el.type.Equals("-10Bomb"))
                    {
                        currentScore.settleScore(-10);
                        bombsClicked++;
                    }
                    else if (el.type.Equals("GameOverBomb"))
                        return true;
                    else if (el.type.Equals("Banana"))
                        currentScore.settleScore(2);
                    else if (el.type.Equals("Apple"))
                        currentScore.settleScore(3);
                    else if (el.type.Equals("Pineapple"))
                        currentScore.settleScore(4);
                    else if (el.type.Equals("Watermelon"))
                        currentScore.settleScore(5);
                    elements.Remove(el);
                    break;
                }
            }
            return false;
        }
    }
}
