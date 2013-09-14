using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BFGFontTool
{
    public partial class BFGFontTool : Form
    {
        public BFGFontTool()
        {
            InitializeComponent();
        }

        private void BFGFontTool_Load(object sender, EventArgs e)
        {
            Program.LoadOptions();

            switch(Program.programOptions.programMode)
            {
                case Program.ProgramOptions.EProgramMode.CreateBFGFontFromBMFont:
                    programModeTabControl.SelectedTab = createBFGFontTabPage;
                    break;
                case Program.ProgramOptions.EProgramMode.DecomposeD3Font:
                    programModeTabControl.SelectedTab = decomposeD3FontTabPage;
                    break;
            }

            bmFontTextBox.Text = Program.programOptions.createBFGOptions.bmFontInputFileName;
            bfgFontTextBox.Text = Program.programOptions.createBFGOptions.bfgFontOutputFileName;

            doom3FontFileTextBox.Text = Program.programOptions.decomposeD3Options.d3FontInputFileName;
            dirWithFontTexturesTextBox.Text = Program.programOptions.decomposeD3Options.dirWithFontTextures;
            bmcfgOutputFileTextBox.Text = Program.programOptions.decomposeD3Options.bmConfigFile;
            glyphImageDirectoryTextBox.Text = Program.programOptions.decomposeD3Options.imageOutputDir;
            langMapsDirTextBox.Text = Program.programOptions.decomposeD3Options.dirWithLangMaps;

            replaceIconsCheckBox.Checked = !Program.programOptions.decomposeD3Options.bmConfigAppend;

            if (Program.programOptions.decomposeD3Options.langs == null)
            {
                Program.programOptions.decomposeD3Options.langs = D3FontDecompose.allLangsExceptRussian;
            }

            foreach (string lang in Program.programOptions.decomposeD3Options.langs)
            {
                int idx = languageCheckedListBox.Items.IndexOf(lang);
                if (idx >= 0)
                {
                    languageCheckedListBox.SetItemChecked(idx, true);
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            Program.SaveOptions();
        }

        private void createBFGFontButton_Click(object sender, EventArgs e)
        {
            TextWriter textOut = new StringWriter();
            if (!Program.programOptions.ValidateOrHelp(textOut))
            {
                MessageBox.Show(this, textOut.ToString(), "Invalid options");
                return;
            }

            try
            {
                Program.createBFG(Program.programOptions.createBFGOptions);
                MessageBox.Show(this, "Creating BFG font complete.");
            }
            catch(Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error creating BFG font");
            }
        }

        private void decomposeD3FontButton_Click(object sender, EventArgs e)
        {
            TextWriter textOut = new StringWriter();
            if (!Program.programOptions.ValidateOrHelp(textOut))
            {
                MessageBox.Show(this, textOut.ToString(), "Invalid options");
                return;
            }

            try
            {
                Program.decomposeD3(Program.programOptions.decomposeD3Options);
                MessageBox.Show(this, "Decomposing Doom 3 font complete.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error decomposing Doom 3 font");
            }
        }

        private void programModeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (programModeTabControl.SelectedTab == createBFGFontTabPage)
                Program.programOptions.programMode = Program.ProgramOptions.EProgramMode.CreateBFGFontFromBMFont;
            if (programModeTabControl.SelectedTab == decomposeD3FontTabPage)
                Program.programOptions.programMode = Program.ProgramOptions.EProgramMode.DecomposeD3Font;
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            TextWriter textOut = new StringWriter();
            Program.programOptions.PrintHelp(textOut);
            MessageBox.Show(this, textOut.ToString(), "Help");
        }

        private void languageCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string language = languageCheckedListBox.Items[e.Index] as string;
            if (e.NewValue == CheckState.Checked)
            {
                Program.programOptions.decomposeD3Options.langs = (Program.programOptions.decomposeD3Options.langs.Union(new string[] { language })).ToArray();
            }
            else
                if (e.NewValue == CheckState.Unchecked)
                {
                    Program.programOptions.decomposeD3Options.langs = (Program.programOptions.decomposeD3Options.langs.Except(new string[] { language })).ToArray();
                }
        }

        private void chooseFile(TextBox textBox, OpenFileDialog openFileDialog)
        {
            try
            {
                openFileDialog.FileName = Path.GetFileName(textBox.Text);
                openFileDialog.InitialDirectory = Path.GetDirectoryName(textBox.Text);
            }
            catch
            { }

            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            textBox.Text = openFileDialog.FileName;
        }

        private void chooseFile(TextBox textBox, SaveFileDialog saveFileDialog)
        {
            try
            {
                saveFileDialog.FileName = Path.GetFileName(textBox.Text);
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(textBox.Text);
            }
            catch
            { }

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            textBox.Text = saveFileDialog.FileName;
        }

        private void chooseDirectory(TextBox textBox, OpenFileDialog openFileDialog)
        {
            try
            {
                openFileDialog.FileName = "dummy";
                openFileDialog.InitialDirectory = textBox.Text;
            }
            catch
            { }

            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            textBox.Text = Path.GetDirectoryName(openFileDialog.FileName);
        }

        private void bmFontBrowseButton_Click(object sender, EventArgs e)
        {
            chooseFile(bmFontTextBox, bmFontOpenFileDialog);
        }

        private void bmFontTextBox_TextChanged(object sender, EventArgs e)
        {
            Program.programOptions.createBFGOptions.bmFontInputFileName = bmFontTextBox.Text;
        }

        private void bfgFontBrowseButton_Click(object sender, EventArgs e)
        {
            chooseFile(bfgFontTextBox, bfgFontSaveFileDialog);
        }

        private void bfgFontTextBox_TextChanged(object sender, EventArgs e)
        {
            Program.programOptions.createBFGOptions.bfgFontOutputFileName = bfgFontTextBox.Text;
        }

        private void doom3FontFileBrowseButton_Click(object sender, EventArgs e)
        {
            chooseFile(doom3FontFileTextBox, d3FontOpenFileDialog);
        }

        private void doom3FontFileTextBox_TextChanged(object sender, EventArgs e)
        {
            Program.programOptions.decomposeD3Options.d3FontInputFileName = doom3FontFileTextBox.Text;
        }

        private void dirWithFontTexturesBrowseButton_Click(object sender, EventArgs e)
        {
            chooseDirectory(dirWithFontTexturesTextBox, dirWithFontTexturesOpenFileDialog);
        }

        private void dirWithFontTexturesTextBox_TextChanged(object sender, EventArgs e)
        {
            Program.programOptions.decomposeD3Options.dirWithFontTextures = dirWithFontTexturesTextBox.Text;
        }

        private void bmcfgOutputFileTextBox_TextChanged(object sender, EventArgs e)
        {
            Program.programOptions.decomposeD3Options.bmConfigFile = bmcfgOutputFileTextBox.Text;
        }

        private void bmcfgOutputFileBrowseButton_Click(object sender, EventArgs e)
        {
            chooseFile(bmcfgOutputFileTextBox, bmcfgOutputSaveFileDialog);
        }

        private void glyphImageDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            Program.programOptions.decomposeD3Options.imageOutputDir = glyphImageDirectoryTextBox.Text;
        }

        private void glyphImageDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            chooseDirectory(glyphImageDirectoryTextBox, glyphImageDirectoryOpenFileDialog);
        }

        private void langMapsDirTextBox_TextChanged(object sender, EventArgs e)
        {
            Program.programOptions.decomposeD3Options.dirWithLangMaps = langMapsDirTextBox.Text;
        }

        private void langMapsDirBrowseButton_Click(object sender, EventArgs e)
        {
            chooseDirectory(langMapsDirTextBox, langMapsDirOpenFileDialog);
        }

        private void replaceIconsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.programOptions.decomposeD3Options.bmConfigAppend = !replaceIconsCheckBox.Checked;
        }
    }
}
