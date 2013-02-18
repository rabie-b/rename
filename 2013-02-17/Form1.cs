using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rename.Properties;

namespace rename
{
    public partial class Rename : Form
    {
        public Rename()
        {
            InitializeComponent();
        }



        //Part of ALL THE FORM
        private string path = "";

        private bool DirIsCorrect;

        private void Rename_Load(object sender, EventArgs e)
        {
            DoCheckPath();
            ProcessBtn_ToolTip(Replace_Btn_Rename, Replace_CB_ReplaceFile, Replace_CB_ReplaceFolder,
                               Resources.ReplaceFileAndFolder, Resources.ReplaceJustFile,
                               Resources.ReplaceJustFolder);            //TT_main.SetToolTip(GB_Replace, "replace name");
        }

        FileSystemInfo[] GetDir(RadioButton CurrentDirRadioButton)
        {
            var dirinfo = new DirectoryInfo(path);
            var entries = dirinfo.GetFileSystemInfos("**",
                                                     CurrentDirRadioButton.Checked
                                                         ? SearchOption.TopDirectoryOnly
                                                         : SearchOption.AllDirectories);
            return entries;
        }



        //part of PATH
        void DoCheckPath()
        {
            GbReplace.Enabled = path != "";
            GbAddToEnd.Enabled = path != "";
            //if (path == "")
            //    GbReplace.Enabled = false;
            //else
            //    GbReplace.Enabled = true;
        }

        private void Path_Llbl_Path_LinkClicked(object sender, EventArgs e)
        {
            DialogResult selectPath = Fbd_Path.ShowDialog();
            if (selectPath != DialogResult.OK) return;
            path = Fbd_Path.SelectedPath;
            Path_TB_Path.Text = path;
        }

        private void Path_TB_Path_TextChanged(object sender, EventArgs e)
        {
            if (Path_TB_Path.Text != "")
                Path_TB_Path.TabIndex = 2;


            path = Path_TB_Path.Text;
            Rename_SS_Status.Text = Directory.Exists(path)
                                        ? "Directory '" + path + "'  is correct"
                                        : Path_TB_Path.Text != "" ? "Directory '" + path + "' doesn't exist" : "Ready";
            DirIsCorrect = Directory.Exists(path) ? true : false;

            DoCheckPath();
            ProcessBtn_ToolTip(Replace_Btn_Rename, Replace_CB_ReplaceFile, Replace_CB_ReplaceFolder,
                               Resources.ReplaceFileAndFolder, Resources.ReplaceJustFile,
                               Resources.ReplaceJustFolder);        }



        //part of REPLACE
        private void ProcessBtn_ToolTip(Button processBtn, CheckBox replaceFile, CheckBox replaceFolder,
                                        string fileAndFolder, string justFile, string justFolder)
        {
            string buttonToolTip = replaceFile.Checked && replaceFile.Checked
                                       ? fileAndFolder
                                       : replaceFile.Checked && !replaceFolder.Checked
                                             ? justFile
                                             : !replaceFile.Checked && replaceFolder.Checked ? justFolder : "";
            TT_main.SetToolTip(processBtn, buttonToolTip);
        }

        bool FieldsReady(CheckBox fileCb, CheckBox folderCb, TextBox textBox, string errorTextBox)
        {
            if ((!fileCb.Checked && !folderCb.Checked) || textBox.Text == "" || !Directory.Exists(path))
            {
                string error = textBox.Text == "" ? errorTextBox : "";
                error += !fileCb.Checked && !folderCb.Checked
                             ? "\n- you have to select one of the check boxes 'current directory' or 'all directories'" : "";
                error += !Directory.Exists(path) ? "\n- The directory you entered doesn't exist." : "";

                MessageBox.Show(error, @"Rename", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }



        private void Replace_CB_ReplaceFile_CheckedChanged(object sender, EventArgs e)
        {
            ProcessBtn_ToolTip(Replace_Btn_Rename, Replace_CB_ReplaceFile, Replace_CB_ReplaceFolder,
                               Resources.ReplaceFileAndFolder, Resources.ReplaceJustFile,
                               Resources.ReplaceJustFolder);
        }

        private void Replace_CB_ReplaceFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (Replace_CB_ReplaceFolder.Checked)
            {
                Replace_RB_AllDir.Enabled = false;
                Replace_RB_CurrentDir.Checked = true;
            }
            else
            {
                Replace_RB_AllDir.Enabled = true;
            }
            ProcessBtn_ToolTip(Replace_Btn_Rename, Replace_CB_ReplaceFile, Replace_CB_ReplaceFolder,
                               Resources.ReplaceFileAndFolder, Resources.ReplaceJustFile,
                               Resources.ReplaceJustFolder);
        }

        private void Replace_Btn_Rename_Click(object sender, EventArgs e)
        {
            if (!FieldsReady(Replace_CB_ReplaceFile, Replace_CB_ReplaceFolder, Replace_TB_From, "- source keyword text box is empty."))
            {
                Rename_SS_Status.Text = "there is few problem!";
                return;
            }

            int numEntries = 0;
            string fullFileNames = "";
            foreach (var entry in GetDir(Replace_RB_CurrentDir))
            {
                string newName = entry.Name.Replace(Replace_TB_From.Text, Replace_TB_To.Text);
                int newnameLength = entry.FullName.Length - entry.Name.Length;
                string newFullName = entry.FullName.Remove(newnameLength) + newName;

                if (Directory.Exists(entry.FullName) && Replace_CB_ReplaceFolder.Checked &&
                    entry.FullName != newFullName)
                {
                    numEntries++;
                    fullFileNames += '-' + entry.FullName.Replace(path, "") + Environment.NewLine;
                }
                else if (!Directory.Exists(entry.FullName) && Replace_CB_ReplaceFile.Checked &&
                         entry.FullName != newFullName)
                {
                    numEntries++;
                    fullFileNames += '-' + entry.FullName.Replace(path, "") + Environment.NewLine;
                }
            }

            if (numEntries == 0)
            {
                MessageBox.Show(
                    "there is no " +
                    (Replace_CB_ReplaceFile.Checked && Replace_CB_ReplaceFolder.Checked
                         ? "file or folder"
                         : Replace_CB_ReplaceFile.Checked && !Replace_CB_ReplaceFolder.Checked ? "file" : "folder") +
                    " with special keyword '" + Replace_TB_From.Text + "' to rename", "Renamer", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }


            string askText = "'" + numEntries +
                             (Replace_CB_ReplaceFile.Checked && Replace_CB_ReplaceFolder.Checked
                                  ? "' files and folders "
                                  : Replace_CB_ReplaceFile.Checked && !Replace_CB_ReplaceFolder.Checked
                                        ? "' files "
                                        : "' folders ") + "are selected. are you sure you want to rename them?" + Environment.NewLine +
                             "From '" + Replace_TB_From.Text + "' To '" + Replace_TB_To.Text + "'." + Environment.NewLine + fullFileNames;
            AskToReplace askToRename = new AskToReplace {AskTexts = askText};
            askToRename.ShowDialog();
            if (!askToRename.AskResult) return;


            foreach (var entry in GetDir(Replace_RB_CurrentDir))
            {
                string newName = entry.Name.Replace(Replace_TB_From.Text, Replace_TB_To.Text);
                int newnameLength = entry.FullName.Length - entry.Name.Length;
                string newFullName = entry.FullName.Remove(newnameLength) + newName;

                if (Directory.Exists(entry.FullName) && entry.FullName != newFullName &&
                    Replace_CB_ReplaceFolder.Checked)
                {
                    Directory.Move(entry.FullName, newFullName);
                }
                else if (entry.FullName != newFullName && Replace_CB_ReplaceFile.Checked &&
                         !Directory.Exists(entry.FullName))
                {
                    File.Move(entry.FullName, newFullName);
                }
            }
            Rename_SS_Status.Text = "'" + numEntries + "'" +
                                    (Replace_CB_ReplaceFile.Checked && Replace_CB_ReplaceFolder.Checked
                                         ? "' files and folders "
                                         : Replace_CB_ReplaceFile.Checked && !Replace_CB_ReplaceFolder.Checked
                                               ? "' files "
                                               : "' folders ") +
                                    " Successfully renamed.";
        }

        private void Replace_Btn_Clear_Click(object sender, EventArgs e)
        {
            Replace_TB_From.Text = "";
            Replace_TB_To.Text = "";
            Replace_CB_ReplaceFile.Checked = true;
            Replace_CB_ReplaceFolder.Checked = false;
            Replace_RB_CurrentDir.Checked = true;
            Rename_SS_Status.Text = Resources.FieldsCleared;
        }


        
        //Part of ADD TO END


        private void Add_Btn_Add_Click(object sender, EventArgs e)
        {
            foreach (var entry in GetDir(Add_Rb_CurrentDir))
            {
                string newName = entry.Name + Add_Tb_Text.Text;
                int dirLength = entry.FullName.Length - entry.Name.Length;
                string newFullName = entry.FullName.Remove(dirLength) + newName;


                if (Directory.Exists(entry.FullName) && entry.Name != newFullName &&
                    Add_Cb_AddToFolder.Checked)
                {
                    Directory.Move(entry.FullName, newFullName);
                }
                else if (entry.FullName != newFullName && Add_Cb_AddToFile.Checked && !Directory.Exists(entry.FullName))
                {
                    File.Move(entry.FullName, newFullName);
                }
            }

        }


    }
}
