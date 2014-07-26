using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Xbox_360_Guide_Button_Remapper
{
    public partial class frmMain : Form
    {
        string displayKeys;
        List<Key> savedKeys;
        string keyStringToSend;
        private int secondsToWait = 5;
        private int totalSecondsWaited;

        //start minimized code.
        private bool mAllowVisible;     // ContextMenu's Show command used
        private bool mAllowClose;       // ContextMenu's Exit command used
        private bool mLoadFired;        // Form was shown once


        public frmMain(bool display)
        {
            InitializeComponent();

            //lock down box.
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;

            //set notifyIcon object and register event handlers.
            notifyIcon.ContextMenuStrip = menuStrip;
            this.preferencesToolStripMenuItem.Click += preferencesToolStripMenuItem_Click;
            this.exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;


            //set keys to press
            savedKeys = ReadKeysFromFile();
            keyStringToSend = ConvertToKeyString(savedKeys);
            DisplayKeys();

            //add the key listener hook
            HookManager.KeyDown += HookManager_KeyDown;

            //TODO: add controller listener hook.

            //visibility
            if (display == true)
            {
                mAllowVisible = true;
                mLoadFired = true;
                Show();
            }
        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            /*
             * TODO: set as Xbox 360 Guide button.
             * Currently, this uses the Escape (ESC) key to trigger the shortcut.
             * This is a proof of concept.
             * 
             * I need to figure out how to 
             *      1. Read all input from an Xbox 360 Controller as a stream.
             *      2. Determine when a keypress is the Guide button.
             *      
             * From what I have found, using x400 is the key.
             */

            if (e.KeyCode == Keys.Escape)
            {
                SendKeys.Send(keyStringToSend);
            }

            if (timer1.Enabled == true)
            {
                Key currentKey = new Key();
                currentKey.KeyCode = e.KeyCode.ToString();

                //filter out weird things... LControlKey is not ok.
                currentKey.KeyCode = FilterWeirdKeys(currentKey.KeyCode);

                currentKey.KeyValue = e.KeyValue;
                savedKeys.Add(currentKey);

                displayKeys += currentKey.KeyCode + " ";
                lblPressedKeys.Text = displayKeys;
            }
        }

        private void btnChangeKeys_Click(object sender, System.EventArgs e)
        {
            //disable the button.
            btnChangeKeys.Enabled = false;

            //clear the current keys.
            savedKeys = new List<Key>();
            keyStringToSend = string.Empty;

            //start the timer.
            totalSecondsWaited = 0;
            timer1.Enabled = true;

            //set the text.
            lblTimeRemaining.Text = "Beginning countdown...";

            //set the instructions.
            lblInstruction.Visible = true;

            //clear the display keys.
            displayKeys = string.Empty;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (totalSecondsWaited == secondsToWait)
            {
                timer1.Enabled = false;
                lblTimeRemaining.Text = string.Empty;

                //write the new keys to the file.
                WriteToFile(savedKeys);

                //after total seconds, enable the button.
                btnChangeKeys.Enabled = true;

                lblPressedKeys.Text = string.Empty;

                DisplayKeys();

                keyStringToSend = ConvertToKeyString(savedKeys);
            }
            else
            {
                int timeRemaining = secondsToWait - totalSecondsWaited;
                lblTimeRemaining.Text = timeRemaining.ToString() + " seconds left.";

                totalSecondsWaited += 1;
            }
        }

        private void DisplayKeys()
        {
            if (savedKeys.Count == 0)
            {
                lblPressedKeys.Text = "No Keys Assigned";
            }
            else
            {
                foreach (Key key in savedKeys)
                {
                    lblPressedKeys.Text += key.KeyCode + " ";
                }
            }

            //hide instructions
            lblInstruction.Visible = false;

        }

        #region I/O methods
        private List<Key> ReadKeysFromFile()
        {
            List<Key> result = new List<Key>();
            try
            {
                using (Stream stream = File.Open("Keys_config.bin", FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    result = (List<Key>)bformatter.Deserialize(stream);
                }

            }
            catch (FileNotFoundException ex)
            {
                //fuck it do nothing.
            }
            

            return result;
        }

        private void WriteToFile(List<Key> savedKeys)
        {
            using (Stream stream = File.Open("Keys_config.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, savedKeys);
            }
        }

        #endregion

        #region KeyMethods

        private string ConvertToKeyString(List<Key> savedKeys)
        {
            string result = string.Empty;

            if (savedKeys.Count > 0)
            {
                // match keys in savedKeys to this list. Output string in accordance to this webpage:
                //http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send.aspx
                
                foreach(Key key in savedKeys){
                    switch(key.KeyCode.ToUpper()){
                        case ("BACKSPACE"):
                            result += "{BACKSPACE}";
                            break;
                        case ("BREAK"):
                            result += "{BREAK}";
                            break;
                        case ("CAPS LOCK"):
                            result += "{CAPSLOCK}";
                            break;
                        case ("DEL"):
                            result += "{DEL}";
                            break;
                        case ("DELETE"):
                            result += "{DELETE}";
                            break;
                        case ("DOWN ARROW"):
                            result += "{DOWN}";
                            break;
                        case ("END"):
                            result += "{END}";
                            break;
                        case ("ENTER"):
                            result += "{ENTER}";
                            break;
                        case ("ESC"):
                            result += "{ESC}";
                            break;
                        case ("HOME"):
                            result += "{HOME}";
                            break;
                        case ("HELP"):
                            result += "{HELP}";
                            break;
                        case ("INS"):
                            result += "{INS}";
                            break;
                        case ("INSERT"):
                            result += "{INSERT}";
                            break;
                        case ("LEFT ARROW"):
                            result += "{LEFT}";
                            break;
                        case ("NUM LOCK"):
                            result += "{NUMLOCK}";
                            break;
                        case ("PAGE DOWN"):
                            result += "{PGDN}";
                            break;
                        case ("PAGE UP"):
                            result += "{PGUP}";
                            break;
                        case ("PRINT SCREEN"):
                            result += "{PRTSC}";
                            break;
                        case ("RIGHT ARROW"):
                            result += "{RIGHT}";
                            break;
                        case ("SCROLL LOCK"):
                            result += "{SCROLLLOCK}";
                            break;
                        case ("TAB"):
                            result += "{TAB}";
                            break;
                        case ("UP ARROW"):
                            result += "{UP}";
                            break;
                        case ("F1"):
                            result += "{F1}";
                            break;
                        case ("F2"):
                            result += "{F2}";
                            break;
                        case ("F3"):
                            result += "{F3}";
                            break;
                        case ("F4"):
                            result += "{F4}";
                            break;
                        case ("F5"):
                            result += "{F5}";
                            break;
                        case ("F6"):
                            result += "{F6}";
                            break;
                        case ("F7"):
                            result += "{F7}";
                            break;
                        case ("F8"):
                            result += "{F8}";
                            break;
                        case ("F9"):
                            result += "{F9}";
                            break;
                        case ("F10"):
                            result += "{F10}";
                            break;
                        case ("F11"):
                            result += "{F11}";
                            break;
                        case ("F12"):
                            result += "{F12}";
                            break;
                        case ("F13"):
                            result += "{F13}";
                            break;
                        case ("F14"):
                            result += "{F14}";
                            break;
                        case ("F15"):
                            result += "{F15}";
                            break;
                        case ("F16"):
                            result += "{F16}";
                            break;
                        case ("KEYPAD ADD"):
                            result += "{ADD}";
                            break;
                        case ("KEYPAD SUBTRACT"):
                            result += "{SUBTRACT}";
                            break;
                        case ("KEYPAD MULTIPLY"):
                            result += "{MULTIPLY}";
                            break;
                        case ("KEYPAD DIVIDE"):
                            result += "{DIVIDE}";
                            break;
                        case("SHIFT"):
                            result += "+";
                            break;
                        case("CTRL"):
                            result += "^";
                            break;
                        case("ALT"):
                            result += "%";
                            break;
                        case ("A"):
                            result += "(a)";
                            break;
                        case ("B"):
                            result += "(b)";
                            break;
                        case ("C"):
                            result += "(c)";
                            break;
                        case ("D"):
                            result += "(d)";
                            break;
                        case ("E"):
                            result += "(e)";
                            break;
                        case ("F"):
                            result += "(f)";
                            break;
                        case ("G"):
                            result += "(g)";
                            break;
                        case ("H"):
                            result += "(h)";
                            break;
                        case ("I"):
                            result += "(i)";
                            break;
                        case ("J"):
                            result += "(j)";
                            break;
                        case ("K"):
                            result += "(k)";
                            break;
                        case ("L"):
                            result += "(l)";
                            break;
                        case ("M"):
                            result += "(m)";
                            break;
                        case ("N"):
                            result += "(n)";
                            break;
                        case ("O"):
                            result += "(p)";
                            break;
                        case ("Q"):
                            result += "(q)";
                            break;
                        case ("R"):
                            result += "(r)";
                            break;
                        case ("S"):
                            result += "(s)";
                            break;
                        case ("T"):
                            result += "(t)";
                            break;
                        case ("U"):
                            result += "(u)";
                            break;
                        case ("V"):
                            result += "(v)";
                            break;
                        case ("W"):
                            result += "(w)";
                            break;
                        case ("X"):
                            result += "(x)";
                            break;
                        case ("Y"):
                            result += "(y)";
                            break;
                        case ("Z"):
                            result += "(z)";
                            break;
                        case ("0"):
                            result += "0";
                            break;
                        case ("1"):
                            result += "1";
                            break;
                        case ("2"):
                            result += "2";
                            break;
                        case ("3"):
                            result += "3";
                            break;
                        case ("4"):
                            result += "4";
                            break;
                        case ("5"):
                            result += "5";
                            break;
                        case ("6"):
                            result += "6";
                            break;
                        case ("7"):
                            result += "7";
                            break;
                        case ("8"):
                            result += "8";
                            break;
                        case ("9"):
                            result += "9";
                            break;

                        default:
                            //add nothing to the string.
                            break;
                    }
                }
            }

            return result;
        }

        private string FilterWeirdKeys(string input)
        {
            switch (input)
            {
                case ("D1"):
                    return "1";
                case ("D2"):
                    return "2";
                case ("D3"):
                    return "3";
                case ("D4"):
                    return "4";
                case ("D5"):
                    return "5";
                case ("D6"):
                    return "6";
                case ("D7"):
                    return "7";
                case ("D8"):
                    return "8";
                case ("D9"):
                    return "9";
                case ("D0"):
                    return "0";
                case("LControlKey"):
                    return "CTRL";
                case("RControlKey"):
                    return "CTRL";
                case("LShiftKey"):
                    return "SHIFT";
                case("RShiftKey"):
                    return "SHIFT";
                case("Capital"):
                    return "CAPS LOCK";
                case("LMenu"):
                    return "ALT";
                case("RMenu"):
                    return "ALT";
                case("Back"):
                    return "BACKSPACE";

                //TODO: add support for ~ : ; " ' [ ] { } | \ < > ? _ + = and Windows Key.
                    //The ~ symbol can yeild unexpected results. The ConvertToKeyString method will render it as a RETURN / ENTER keystroke. See MSDN website for more details.
                //Why anyone would ever use these keys is beyond me....

                default:
                    return input;

            }
        }

        #endregion

        #region SystemTrayVisibilitySettings
        protected override void SetVisibleCore(bool value)
        {
            if (!mAllowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!mAllowClose)
            {
                this.Hide();
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            //display the dialog.
            mAllowVisible = true;
            mLoadFired = true;
            Show();
        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mAllowClose = mAllowVisible = true;
            if (mLoadFired)
            {
                Close();
            }
            else
            {
                Application.Exit();
            }
        }
        #endregion

    }
}
