using System;
using System.Windows.Forms;

namespace Fruit_Ninja
{
    public partial class SettingsForm : Form
    {
        public static Settings settings;

        public SettingsForm()
        {
            InitializeComponent();
            updateFields();
        }

        /*<summary>
        Implementacija na izbranite promeni
        </summary>*/
        private void lblOK_Click(object sender, EventArgs e)
        {
            int width = 0;
            int height = 0;
            string difficulty = "";
            if (lblEasy.Enabled == false)
                difficulty = "EASY";
            else if (lblMedium.Enabled == false)
                difficulty = "MEDIUM";
            else if (lblHard.Enabled == false)
                difficulty = "HARD";
            if (lbl800x600.Enabled == false)
            {
                width = 800;
                height = 600;
            }
            else
            {
                width = 1024;
                height = 768;
            }
            Settings newSettings = new Settings(width, height, difficulty);
            if (checkChanges(newSettings))
            {
                DialogResult dr = MessageBox.Show("Do you want to save the changes?", "Save changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    if (settings.height != newSettings.height)
                        Main.resize = true;
                    else
                        Main.resize = false;
                    settings = newSettings;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /*<summary>
        Vizuelna reprezentacija na moznite i onevozmozenite opcii
        </summary>*/
        private void updateFields()
        {
            if (settings.width == 800 && settings.height == 600)
            {
                lbl800x600.Enabled = false;
                lbl1024x768.Enabled = true;
                lbl800x600.Cursor = Cursors.Default;
                lbl1024x768.Cursor = Cursors.Hand;
            }
            else
            {
                lbl1024x768.Enabled = false;
                lbl800x600.Enabled = true;
                lbl800x600.Cursor = Cursors.Hand;
                lbl1024x768.Cursor = Cursors.Default;
            }
            if (settings.difficulty.Equals("EASY"))
            {
                lblEasy.Enabled = false;
                lblMedium.Enabled = true;
                lblHard.Enabled = true;
                lblEasy.Cursor = Cursors.Default;
                lblMedium.Cursor = Cursors.Hand;
                lblHard.Cursor = Cursors.Hand;
            }
            else if (settings.difficulty.Equals("MEDIUM"))
            {
                lblEasy.Enabled = true;
                lblMedium.Enabled = false;
                lblHard.Enabled = true;
                lblEasy.Cursor = Cursors.Hand;
                lblMedium.Cursor = Cursors.Default;
                lblHard.Cursor = Cursors.Hand;
            }
            else if (settings.difficulty.Equals("HARD"))
            {
                lblEasy.Enabled = true;
                lblMedium.Enabled = true;
                lblHard.Enabled = false;
                lblEasy.Cursor = Cursors.Hand;
                lblMedium.Cursor = Cursors.Hand;
                lblHard.Cursor = Cursors.Default;
            }
        }

        /*<summary>
        Proverka dali se napraveni promeni vo nagoduvanjata
        </summary>*/
        private bool checkChanges(Settings newSettings)
        {
            if (SettingsForm.settings.CompareTo(newSettings) == 1)
                return true;
            else
                return false;
        }

        private void lbl1024x768_Click(object sender, EventArgs e)
        {
            if (lbl1024x768.Enabled == true)
            {
                lbl1024x768.Enabled = false;
                lbl800x600.Enabled = true;
                lbl800x600.Cursor = Cursors.Hand;
                lbl1024x768.Cursor = Cursors.Default;
            }
            checkOK();
        }

        private void lbl800x600_Click(object sender, EventArgs e)
        {
            if (lbl800x600.Enabled == true)
            {
                lbl800x600.Enabled = false;
                lbl1024x768.Enabled = true;
                lbl800x600.Cursor = Cursors.Default;
                lbl1024x768.Cursor = Cursors.Hand;
            }
            checkOK();
        }

        private void lblEasy_Click(object sender, EventArgs e)
        {
            if (lblEasy.Enabled == true)
            {
                lblEasy.Enabled = false;
                lblMedium.Enabled = true;
                lblHard.Enabled = true;
                lblEasy.Cursor = Cursors.Default;
                lblMedium.Cursor = Cursors.Hand;
                lblHard.Cursor = Cursors.Hand;
            }
            checkOK();
        }

        private void lblMedium_Click(object sender, EventArgs e)
        {
            if (lblMedium.Enabled == true)
            {
                lblEasy.Enabled = true;
                lblMedium.Enabled = false;
                lblHard.Enabled = true;
                lblEasy.Cursor = Cursors.Hand;
                lblMedium.Cursor = Cursors.Default;
                lblHard.Cursor = Cursors.Hand;
            }
            checkOK();
        }

        private void lblHard_Click(object sender, EventArgs e)
        {
            if (lblHard.Enabled == true)
            {
                lblEasy.Enabled = true;
                lblMedium.Enabled = true;
                lblHard.Enabled = false;
                lblEasy.Cursor = Cursors.Hand;
                lblMedium.Cursor = Cursors.Hand;
                lblHard.Cursor = Cursors.Default;
            }
            checkOK();
        }

        /*<summary>
        Ovozmozuvanje/onevozmozuvanje na kopceto za potvrda na promenite
        </summary>*/
        private void checkOK()
        {
            string resolution = string.Format("{0}x{1}", settings.width, settings.height);
            string difficulty = settings.difficulty;
            string selectedResolution = "";
            if (!lbl1024x768.Enabled)
                selectedResolution = lbl1024x768.Text;
            else
                selectedResolution = lbl800x600.Text;
            string selectedDifficulty = "";
            if (!lblEasy.Enabled)
                selectedDifficulty = lblEasy.Text.ToUpper();
            else if (!lblMedium.Enabled)
                selectedDifficulty = lblMedium.Text.ToUpper();
            else
                selectedDifficulty = lblHard.Text.ToUpper();
            if (selectedResolution.Equals(resolution) && selectedDifficulty.Equals(difficulty))
                lblOK.Enabled = false;
            else
                lblOK.Enabled = true;
        }
    }
}
