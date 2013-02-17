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

namespace rename
{
    public partial class Rename : Form
    {
        public Rename()
        {
            InitializeComponent();
        }

        private string path = "";
        private bool Replace_FieldsAreReady = false;
        private bool Replace_DirIsCorrect = false;


        void DoCheckPath()
        {
            GbReplace.Enabled = path != "";
            //if (path == "")
            //    GbReplace.Enabled = false;
            //else
            //    GbReplace.Enabled = true;
        }

        void RenameBtnToolTip()
        {
            string renameBtnToolTip = Replace_CB_ReplaceFile.Checked && Replace_CB_ReplaceFolder.Checked
                                          ? "Rename Files and Folders"
                                          : Replace_CB_ReplaceFile.Checked && Replace_CB_ReplaceFolder.Checked == false
                                                ? "Rename Files"
                                                : Replace_CB_ReplaceFile.Checked == false && Replace_CB_ReplaceFolder.Checked
                                                      ? "Rename Folders"
                                                      : "";
            //if (Replace_CB_ReplaceFile.Enabled && Replace_CB_ReplaceFile.Enabled)
            //    renameBtnToolTip = "Rename Files and Folders";
            //else if (Replace_CB_ReplaceFile.Enabled && Replace_CB_ReplaceFolder.Enabled == false)
            //    renameBtnToolTip = "Rename Files";
            //else if (Replace_CB_ReplaceFile.Enabled == false && Replace_CB_ReplaceFolder.Enabled)
            //    renameBtnToolTip = "Rename Folders";
            //else
            //    renameBtnToolTip = "Rename";

            TT_main.SetToolTip(Replace_Btn_Rename, renameBtnToolTip);
        }

        bool Replace_FiledsReady()
        {
            if ((Replace_CB_ReplaceFile.Checked == false && Replace_CB_ReplaceFolder.Checked == false)
                || (Replace_TB_From.Text == "") || !Directory.Exists(Path_TB_Path.Text))
            {
                string error = Replace_TB_From.Text == "" ? "- source keyword text box is empty." : "";

                error = error + (Replace_CB_ReplaceFile.Checked == false && Replace_CB_ReplaceFolder.Checked == false
                    ? "\n- you have to select one of the check boxes 'current directory' or 'all directories'" : "");

                error = error + (Replace_DirIsCorrect == false ? "\n- The directory you entered doesn't exist." : "");

                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return false;
            }
            return true;
        }

        FileSystemInfo[] GetDir()
        {
            var dirinfo = new DirectoryInfo(path);
            var entries = dirinfo.GetFileSystemInfos("**",
                                                     Replace_RB_CurrentDir.Checked
                                                         ? SearchOption.TopDirectoryOnly
                                                         : SearchOption.AllDirectories);
            return entries;
        }

        private void Rename_Load(object sender, EventArgs e)
        {
            DoCheckPath();
            //TT_main.SetToolTip(GB_Replace, "replace name");
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
            Replace_DirIsCorrect = Directory.Exists(path) ? true : false;

            DoCheckPath();
            RenameBtnToolTip();
        }

        private void Replace_CB_ReplaceFile_CheckedChanged(object sender, EventArgs e)
        {
            RenameBtnToolTip();
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
            RenameBtnToolTip();
        }

        private void Replace_Btn_Rename_Click(object sender, EventArgs e)
        {
            if (!Replace_FiledsReady())
            {
                Rename_SS_Status.Text = "there is few problem!";
                return;
            }
            foreach (var entry in GetDir())
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
        }

        private void Replace_Btn_Clear_Click(object sender, EventArgs e)
        {
            Replace_TB_From.Text = "";
            Replace_TB_To.Text = "";
            Replace_CB_ReplaceFile.Checked = true;
            Replace_CB_ReplaceFolder.Checked = false;
            Replace_RB_CurrentDir.Checked = true;
            Rename_SS_Status.Text = "fields in replace part changed to default";
        }





    }
}
