namespace BFGFontTool
{
    partial class BFGFontTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.programModeTabControl = new System.Windows.Forms.TabControl();
            this.createBFGFontTabPage = new System.Windows.Forms.TabPage();
            this.createBfgFontHelpButton = new System.Windows.Forms.Button();
            this.bfgFontOutputFileLabel = new System.Windows.Forms.Label();
            this.bfgFontBrowseButton = new System.Windows.Forms.Button();
            this.bfgFontTextBox = new System.Windows.Forms.TextBox();
            this.createBFGFontButton = new System.Windows.Forms.Button();
            this.inputBMFontLabel = new System.Windows.Forms.Label();
            this.bmFontBrowseButton = new System.Windows.Forms.Button();
            this.bmFontTextBox = new System.Windows.Forms.TextBox();
            this.decomposeD3FontTabPage = new System.Windows.Forms.TabPage();
            this.languageCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.dirWithFontTexturesLabel = new System.Windows.Forms.Label();
            this.dirWithFontTexturesBrowseButton = new System.Windows.Forms.Button();
            this.dirWithFontTexturesTextBox = new System.Windows.Forms.TextBox();
            this.doom3FontFileLabel = new System.Windows.Forms.Label();
            this.doom3FontFileBrowseButton = new System.Windows.Forms.Button();
            this.doom3FontFileTextBox = new System.Windows.Forms.TextBox();
            this.decomposeD3FontButton = new System.Windows.Forms.Button();
            this.bmFontOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bfgFontSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.d3FontOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dirWithFontTexturesOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.glyphImageDirectoryLabel = new System.Windows.Forms.Label();
            this.glyphImageDirectoryBrowseButton = new System.Windows.Forms.Button();
            this.glyphImageDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.bmcfgOutputFileLabel = new System.Windows.Forms.Label();
            this.bmcfgOutputFileBrowseButton = new System.Windows.Forms.Button();
            this.bmcfgOutputFileTextBox = new System.Windows.Forms.TextBox();
            this.langMapsDirLabel = new System.Windows.Forms.Label();
            this.langMapsDirBrowseButton = new System.Windows.Forms.Button();
            this.langMapsDirTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tdmOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.langMapsDirOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.glyphImageDirectoryOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bmcfgOutputSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.replaceIconsCheckBox = new System.Windows.Forms.CheckBox();
            this.generateFakeD3DatsCheckBox = new System.Windows.Forms.CheckBox();
            this.programModeTabControl.SuspendLayout();
            this.createBFGFontTabPage.SuspendLayout();
            this.decomposeD3FontTabPage.SuspendLayout();
            this.tdmOptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "You can run me from command line. Try: \"BFGFontTool --help\".";
            // 
            // programModeTabControl
            // 
            this.programModeTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.programModeTabControl.Controls.Add(this.createBFGFontTabPage);
            this.programModeTabControl.Controls.Add(this.decomposeD3FontTabPage);
            this.programModeTabControl.Location = new System.Drawing.Point(12, 36);
            this.programModeTabControl.Name = "programModeTabControl";
            this.programModeTabControl.SelectedIndex = 0;
            this.programModeTabControl.Size = new System.Drawing.Size(777, 508);
            this.programModeTabControl.TabIndex = 1;
            this.programModeTabControl.SelectedIndexChanged += new System.EventHandler(this.programModeTabControl_SelectedIndexChanged);
            // 
            // createBFGFontTabPage
            // 
            this.createBFGFontTabPage.Controls.Add(this.generateFakeD3DatsCheckBox);
            this.createBFGFontTabPage.Controls.Add(this.createBfgFontHelpButton);
            this.createBFGFontTabPage.Controls.Add(this.bfgFontOutputFileLabel);
            this.createBFGFontTabPage.Controls.Add(this.bfgFontBrowseButton);
            this.createBFGFontTabPage.Controls.Add(this.bfgFontTextBox);
            this.createBFGFontTabPage.Controls.Add(this.createBFGFontButton);
            this.createBFGFontTabPage.Controls.Add(this.inputBMFontLabel);
            this.createBFGFontTabPage.Controls.Add(this.bmFontBrowseButton);
            this.createBFGFontTabPage.Controls.Add(this.bmFontTextBox);
            this.createBFGFontTabPage.Location = new System.Drawing.Point(4, 22);
            this.createBFGFontTabPage.Name = "createBFGFontTabPage";
            this.createBFGFontTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.createBFGFontTabPage.Size = new System.Drawing.Size(769, 482);
            this.createBFGFontTabPage.TabIndex = 0;
            this.createBFGFontTabPage.Text = "Create BFG font";
            this.createBFGFontTabPage.UseVisualStyleBackColor = true;
            // 
            // createBfgFontHelpButton
            // 
            this.createBfgFontHelpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createBfgFontHelpButton.Location = new System.Drawing.Point(688, 61);
            this.createBfgFontHelpButton.Name = "createBfgFontHelpButton";
            this.createBfgFontHelpButton.Size = new System.Drawing.Size(75, 23);
            this.createBfgFontHelpButton.TabIndex = 7;
            this.createBfgFontHelpButton.Text = "Help";
            this.createBfgFontHelpButton.UseVisualStyleBackColor = true;
            this.createBfgFontHelpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // bfgFontOutputFileLabel
            // 
            this.bfgFontOutputFileLabel.AutoSize = true;
            this.bfgFontOutputFileLabel.Location = new System.Drawing.Point(6, 37);
            this.bfgFontOutputFileLabel.Name = "bfgFontOutputFileLabel";
            this.bfgFontOutputFileLabel.Size = new System.Drawing.Size(101, 13);
            this.bfgFontOutputFileLabel.TabIndex = 6;
            this.bfgFontOutputFileLabel.Text = "BFG font output file:";
            // 
            // bfgFontBrowseButton
            // 
            this.bfgFontBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bfgFontBrowseButton.Location = new System.Drawing.Point(726, 32);
            this.bfgFontBrowseButton.Name = "bfgFontBrowseButton";
            this.bfgFontBrowseButton.Size = new System.Drawing.Size(37, 23);
            this.bfgFontBrowseButton.TabIndex = 5;
            this.bfgFontBrowseButton.Text = "...";
            this.bfgFontBrowseButton.UseVisualStyleBackColor = true;
            this.bfgFontBrowseButton.Click += new System.EventHandler(this.bfgFontBrowseButton_Click);
            // 
            // bfgFontTextBox
            // 
            this.bfgFontTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bfgFontTextBox.Location = new System.Drawing.Point(113, 34);
            this.bfgFontTextBox.Name = "bfgFontTextBox";
            this.bfgFontTextBox.Size = new System.Drawing.Size(607, 20);
            this.bfgFontTextBox.TabIndex = 4;
            this.bfgFontTextBox.TextChanged += new System.EventHandler(this.bfgFontTextBox_TextChanged);
            // 
            // createBFGFontButton
            // 
            this.createBFGFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createBFGFontButton.Location = new System.Drawing.Point(641, 453);
            this.createBFGFontButton.Name = "createBFGFontButton";
            this.createBFGFontButton.Size = new System.Drawing.Size(122, 23);
            this.createBFGFontButton.TabIndex = 3;
            this.createBFGFontButton.Text = "Create BFG font";
            this.createBFGFontButton.UseVisualStyleBackColor = true;
            this.createBFGFontButton.Click += new System.EventHandler(this.createBFGFontButton_Click);
            // 
            // inputBMFontLabel
            // 
            this.inputBMFontLabel.AutoSize = true;
            this.inputBMFontLabel.Location = new System.Drawing.Point(6, 11);
            this.inputBMFontLabel.Name = "inputBMFontLabel";
            this.inputBMFontLabel.Size = new System.Drawing.Size(63, 13);
            this.inputBMFontLabel.TabIndex = 2;
            this.inputBMFontLabel.Text = "BMFont file:";
            // 
            // bmFontBrowseButton
            // 
            this.bmFontBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bmFontBrowseButton.Location = new System.Drawing.Point(726, 6);
            this.bmFontBrowseButton.Name = "bmFontBrowseButton";
            this.bmFontBrowseButton.Size = new System.Drawing.Size(37, 23);
            this.bmFontBrowseButton.TabIndex = 1;
            this.bmFontBrowseButton.Text = "...";
            this.bmFontBrowseButton.UseVisualStyleBackColor = true;
            this.bmFontBrowseButton.Click += new System.EventHandler(this.bmFontBrowseButton_Click);
            // 
            // bmFontTextBox
            // 
            this.bmFontTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bmFontTextBox.Location = new System.Drawing.Point(113, 8);
            this.bmFontTextBox.Name = "bmFontTextBox";
            this.bmFontTextBox.Size = new System.Drawing.Size(607, 20);
            this.bmFontTextBox.TabIndex = 0;
            this.bmFontTextBox.TextChanged += new System.EventHandler(this.bmFontTextBox_TextChanged);
            // 
            // decomposeD3FontTabPage
            // 
            this.decomposeD3FontTabPage.Controls.Add(this.replaceIconsCheckBox);
            this.decomposeD3FontTabPage.Controls.Add(this.tdmOptionsGroupBox);
            this.decomposeD3FontTabPage.Controls.Add(this.glyphImageDirectoryLabel);
            this.decomposeD3FontTabPage.Controls.Add(this.glyphImageDirectoryBrowseButton);
            this.decomposeD3FontTabPage.Controls.Add(this.glyphImageDirectoryTextBox);
            this.decomposeD3FontTabPage.Controls.Add(this.bmcfgOutputFileLabel);
            this.decomposeD3FontTabPage.Controls.Add(this.bmcfgOutputFileBrowseButton);
            this.decomposeD3FontTabPage.Controls.Add(this.bmcfgOutputFileTextBox);
            this.decomposeD3FontTabPage.Controls.Add(this.button3);
            this.decomposeD3FontTabPage.Controls.Add(this.dirWithFontTexturesLabel);
            this.decomposeD3FontTabPage.Controls.Add(this.dirWithFontTexturesBrowseButton);
            this.decomposeD3FontTabPage.Controls.Add(this.dirWithFontTexturesTextBox);
            this.decomposeD3FontTabPage.Controls.Add(this.doom3FontFileLabel);
            this.decomposeD3FontTabPage.Controls.Add(this.doom3FontFileBrowseButton);
            this.decomposeD3FontTabPage.Controls.Add(this.doom3FontFileTextBox);
            this.decomposeD3FontTabPage.Controls.Add(this.decomposeD3FontButton);
            this.decomposeD3FontTabPage.Location = new System.Drawing.Point(4, 22);
            this.decomposeD3FontTabPage.Name = "decomposeD3FontTabPage";
            this.decomposeD3FontTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.decomposeD3FontTabPage.Size = new System.Drawing.Size(769, 482);
            this.decomposeD3FontTabPage.TabIndex = 1;
            this.decomposeD3FontTabPage.Text = "Decompose Doom 3 font";
            this.decomposeD3FontTabPage.UseVisualStyleBackColor = true;
            // 
            // languageCheckedListBox
            // 
            this.languageCheckedListBox.CheckOnClick = true;
            this.languageCheckedListBox.FormattingEnabled = true;
            this.languageCheckedListBox.Items.AddRange(new object[] {
            "czech",
            "danish",
            "dutch",
            "english",
            "french",
            "german",
            "hungarian",
            "italian",
            "polish",
            "portuguese",
            "slovak",
            "spanish",
            "russian"});
            this.languageCheckedListBox.Location = new System.Drawing.Point(147, 45);
            this.languageCheckedListBox.Name = "languageCheckedListBox";
            this.languageCheckedListBox.Size = new System.Drawing.Size(114, 199);
            this.languageCheckedListBox.TabIndex = 14;
            this.languageCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.languageCheckedListBox_ItemCheck);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(682, 412);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Help";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // dirWithFontTexturesLabel
            // 
            this.dirWithFontTexturesLabel.AutoSize = true;
            this.dirWithFontTexturesLabel.Location = new System.Drawing.Point(6, 37);
            this.dirWithFontTexturesLabel.Name = "dirWithFontTexturesLabel";
            this.dirWithFontTexturesLabel.Size = new System.Drawing.Size(135, 13);
            this.dirWithFontTexturesLabel.TabIndex = 12;
            this.dirWithFontTexturesLabel.Text = "Directory with font textures:";
            // 
            // dirWithFontTexturesBrowseButton
            // 
            this.dirWithFontTexturesBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dirWithFontTexturesBrowseButton.Location = new System.Drawing.Point(720, 32);
            this.dirWithFontTexturesBrowseButton.Name = "dirWithFontTexturesBrowseButton";
            this.dirWithFontTexturesBrowseButton.Size = new System.Drawing.Size(37, 23);
            this.dirWithFontTexturesBrowseButton.TabIndex = 11;
            this.dirWithFontTexturesBrowseButton.Text = "...";
            this.dirWithFontTexturesBrowseButton.UseVisualStyleBackColor = true;
            this.dirWithFontTexturesBrowseButton.Click += new System.EventHandler(this.dirWithFontTexturesBrowseButton_Click);
            // 
            // dirWithFontTexturesTextBox
            // 
            this.dirWithFontTexturesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dirWithFontTexturesTextBox.Location = new System.Drawing.Point(156, 34);
            this.dirWithFontTexturesTextBox.Name = "dirWithFontTexturesTextBox";
            this.dirWithFontTexturesTextBox.Size = new System.Drawing.Size(558, 20);
            this.dirWithFontTexturesTextBox.TabIndex = 10;
            this.dirWithFontTexturesTextBox.TextChanged += new System.EventHandler(this.dirWithFontTexturesTextBox_TextChanged);
            // 
            // doom3FontFileLabel
            // 
            this.doom3FontFileLabel.AutoSize = true;
            this.doom3FontFileLabel.Location = new System.Drawing.Point(6, 11);
            this.doom3FontFileLabel.Name = "doom3FontFileLabel";
            this.doom3FontFileLabel.Size = new System.Drawing.Size(84, 13);
            this.doom3FontFileLabel.TabIndex = 9;
            this.doom3FontFileLabel.Text = "Doom 3 font file:";
            // 
            // doom3FontFileBrowseButton
            // 
            this.doom3FontFileBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.doom3FontFileBrowseButton.Location = new System.Drawing.Point(720, 6);
            this.doom3FontFileBrowseButton.Name = "doom3FontFileBrowseButton";
            this.doom3FontFileBrowseButton.Size = new System.Drawing.Size(37, 23);
            this.doom3FontFileBrowseButton.TabIndex = 8;
            this.doom3FontFileBrowseButton.Text = "...";
            this.doom3FontFileBrowseButton.UseVisualStyleBackColor = true;
            this.doom3FontFileBrowseButton.Click += new System.EventHandler(this.doom3FontFileBrowseButton_Click);
            // 
            // doom3FontFileTextBox
            // 
            this.doom3FontFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doom3FontFileTextBox.Location = new System.Drawing.Point(156, 8);
            this.doom3FontFileTextBox.Name = "doom3FontFileTextBox";
            this.doom3FontFileTextBox.Size = new System.Drawing.Size(558, 20);
            this.doom3FontFileTextBox.TabIndex = 7;
            this.doom3FontFileTextBox.TextChanged += new System.EventHandler(this.doom3FontFileTextBox_TextChanged);
            // 
            // decomposeD3FontButton
            // 
            this.decomposeD3FontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.decomposeD3FontButton.Location = new System.Drawing.Point(613, 453);
            this.decomposeD3FontButton.Name = "decomposeD3FontButton";
            this.decomposeD3FontButton.Size = new System.Drawing.Size(153, 23);
            this.decomposeD3FontButton.TabIndex = 0;
            this.decomposeD3FontButton.Text = "Decompose Doom 3 font";
            this.decomposeD3FontButton.UseVisualStyleBackColor = true;
            this.decomposeD3FontButton.Click += new System.EventHandler(this.decomposeD3FontButton_Click);
            // 
            // bmFontOpenFileDialog
            // 
            this.bmFontOpenFileDialog.DefaultExt = "fnt";
            this.bmFontOpenFileDialog.FileName = "bmFont.fnt";
            this.bmFontOpenFileDialog.Filter = "BMFont files (*.fnt)|*.fnt|All files (*.*)|*.*";
            // 
            // bfgFontSaveFileDialog
            // 
            this.bfgFontSaveFileDialog.DefaultExt = "dat";
            // 
            // d3FontOpenFileDialog
            // 
            this.d3FontOpenFileDialog.DefaultExt = "dat";
            this.d3FontOpenFileDialog.Filter = "Doom 3 font files (*.dat)|*.dat|All files (*.*)|*.*";
            // 
            // dirWithFontTexturesOpenFileDialog
            // 
            this.dirWithFontTexturesOpenFileDialog.AddExtension = false;
            this.dirWithFontTexturesOpenFileDialog.CheckFileExists = false;
            this.dirWithFontTexturesOpenFileDialog.DefaultExt = "dds";
            this.dirWithFontTexturesOpenFileDialog.Filter = "Texture files (*.dds, *.tga)|*.dds;*.tga|All files (*.*)|*.*";
            this.dirWithFontTexturesOpenFileDialog.Title = "Choose font textures directory";
            // 
            // glyphImageDirectoryLabel
            // 
            this.glyphImageDirectoryLabel.AutoSize = true;
            this.glyphImageDirectoryLabel.Location = new System.Drawing.Point(6, 115);
            this.glyphImageDirectoryLabel.Name = "glyphImageDirectoryLabel";
            this.glyphImageDirectoryLabel.Size = new System.Drawing.Size(121, 13);
            this.glyphImageDirectoryLabel.TabIndex = 20;
            this.glyphImageDirectoryLabel.Text = "Output images directory:";
            // 
            // glyphImageDirectoryBrowseButton
            // 
            this.glyphImageDirectoryBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.glyphImageDirectoryBrowseButton.Location = new System.Drawing.Point(720, 110);
            this.glyphImageDirectoryBrowseButton.Name = "glyphImageDirectoryBrowseButton";
            this.glyphImageDirectoryBrowseButton.Size = new System.Drawing.Size(37, 23);
            this.glyphImageDirectoryBrowseButton.TabIndex = 19;
            this.glyphImageDirectoryBrowseButton.Text = "...";
            this.glyphImageDirectoryBrowseButton.UseVisualStyleBackColor = true;
            this.glyphImageDirectoryBrowseButton.Click += new System.EventHandler(this.glyphImageDirectoryBrowseButton_Click);
            // 
            // glyphImageDirectoryTextBox
            // 
            this.glyphImageDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glyphImageDirectoryTextBox.Location = new System.Drawing.Point(156, 112);
            this.glyphImageDirectoryTextBox.Name = "glyphImageDirectoryTextBox";
            this.glyphImageDirectoryTextBox.Size = new System.Drawing.Size(558, 20);
            this.glyphImageDirectoryTextBox.TabIndex = 18;
            this.glyphImageDirectoryTextBox.TextChanged += new System.EventHandler(this.glyphImageDirectoryTextBox_TextChanged);
            // 
            // bmcfgOutputFileLabel
            // 
            this.bmcfgOutputFileLabel.AutoSize = true;
            this.bmcfgOutputFileLabel.Location = new System.Drawing.Point(6, 66);
            this.bmcfgOutputFileLabel.Name = "bmcfgOutputFileLabel";
            this.bmcfgOutputFileLabel.Size = new System.Drawing.Size(64, 13);
            this.bmcfgOutputFileLabel.TabIndex = 17;
            this.bmcfgOutputFileLabel.Text = "Oputput file:";
            // 
            // bmcfgOutputFileBrowseButton
            // 
            this.bmcfgOutputFileBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bmcfgOutputFileBrowseButton.Location = new System.Drawing.Point(720, 61);
            this.bmcfgOutputFileBrowseButton.Name = "bmcfgOutputFileBrowseButton";
            this.bmcfgOutputFileBrowseButton.Size = new System.Drawing.Size(37, 23);
            this.bmcfgOutputFileBrowseButton.TabIndex = 16;
            this.bmcfgOutputFileBrowseButton.Text = "...";
            this.bmcfgOutputFileBrowseButton.UseVisualStyleBackColor = true;
            this.bmcfgOutputFileBrowseButton.Click += new System.EventHandler(this.bmcfgOutputFileBrowseButton_Click);
            // 
            // bmcfgOutputFileTextBox
            // 
            this.bmcfgOutputFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bmcfgOutputFileTextBox.Location = new System.Drawing.Point(156, 63);
            this.bmcfgOutputFileTextBox.Name = "bmcfgOutputFileTextBox";
            this.bmcfgOutputFileTextBox.Size = new System.Drawing.Size(558, 20);
            this.bmcfgOutputFileTextBox.TabIndex = 15;
            this.bmcfgOutputFileTextBox.TextChanged += new System.EventHandler(this.bmcfgOutputFileTextBox_TextChanged);
            // 
            // langMapsDirLabel
            // 
            this.langMapsDirLabel.AutoSize = true;
            this.langMapsDirLabel.Location = new System.Drawing.Point(6, 22);
            this.langMapsDirLabel.Name = "langMapsDirLabel";
            this.langMapsDirLabel.Size = new System.Drawing.Size(129, 13);
            this.langMapsDirLabel.TabIndex = 23;
            this.langMapsDirLabel.Text = "Language maps directory:";
            // 
            // langMapsDirBrowseButton
            // 
            this.langMapsDirBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.langMapsDirBrowseButton.Location = new System.Drawing.Point(711, 19);
            this.langMapsDirBrowseButton.Name = "langMapsDirBrowseButton";
            this.langMapsDirBrowseButton.Size = new System.Drawing.Size(37, 23);
            this.langMapsDirBrowseButton.TabIndex = 22;
            this.langMapsDirBrowseButton.Text = "...";
            this.langMapsDirBrowseButton.UseVisualStyleBackColor = true;
            this.langMapsDirBrowseButton.Click += new System.EventHandler(this.langMapsDirBrowseButton_Click);
            // 
            // langMapsDirTextBox
            // 
            this.langMapsDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.langMapsDirTextBox.Location = new System.Drawing.Point(147, 19);
            this.langMapsDirTextBox.Name = "langMapsDirTextBox";
            this.langMapsDirTextBox.Size = new System.Drawing.Size(558, 20);
            this.langMapsDirTextBox.TabIndex = 21;
            this.langMapsDirTextBox.TextChanged += new System.EventHandler(this.langMapsDirTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Languages to analyze:";
            // 
            // tdmOptionsGroupBox
            // 
            this.tdmOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tdmOptionsGroupBox.Controls.Add(this.languageCheckedListBox);
            this.tdmOptionsGroupBox.Controls.Add(this.langMapsDirBrowseButton);
            this.tdmOptionsGroupBox.Controls.Add(this.langMapsDirLabel);
            this.tdmOptionsGroupBox.Controls.Add(this.langMapsDirTextBox);
            this.tdmOptionsGroupBox.Controls.Add(this.label4);
            this.tdmOptionsGroupBox.Location = new System.Drawing.Point(9, 152);
            this.tdmOptionsGroupBox.Name = "tdmOptionsGroupBox";
            this.tdmOptionsGroupBox.Size = new System.Drawing.Size(754, 254);
            this.tdmOptionsGroupBox.TabIndex = 25;
            this.tdmOptionsGroupBox.TabStop = false;
            this.tdmOptionsGroupBox.Text = "The Dark Mod options:";
            // 
            // langMapsDirOpenFileDialog
            // 
            this.langMapsDirOpenFileDialog.AddExtension = false;
            this.langMapsDirOpenFileDialog.CheckFileExists = false;
            this.langMapsDirOpenFileDialog.DefaultExt = "map";
            this.langMapsDirOpenFileDialog.Filter = "Language maps (*.map)|*.map|All files (*.*)|*.*";
            this.langMapsDirOpenFileDialog.Title = "Choose TDM language maps directory";
            // 
            // glyphImageDirectoryOpenFileDialog
            // 
            this.glyphImageDirectoryOpenFileDialog.AddExtension = false;
            this.glyphImageDirectoryOpenFileDialog.CheckFileExists = false;
            this.glyphImageDirectoryOpenFileDialog.DefaultExt = "tga";
            this.glyphImageDirectoryOpenFileDialog.Filter = "Texture files (*.tga)|*.tga|All files (*.*)|*.*";
            this.glyphImageDirectoryOpenFileDialog.Title = "Choose glyph images output directory";
            // 
            // bmcfgOutputSaveFileDialog
            // 
            this.bmcfgOutputSaveFileDialog.DefaultExt = "txt";
            this.bmcfgOutputSaveFileDialog.Filter = "BM config files (*.bmfc, *.txt)|*.bmfc;*.txt|All files (*.*)|*.*";
            // 
            // replaceIconsCheckBox
            // 
            this.replaceIconsCheckBox.AutoSize = true;
            this.replaceIconsCheckBox.Checked = true;
            this.replaceIconsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.replaceIconsCheckBox.Location = new System.Drawing.Point(156, 89);
            this.replaceIconsCheckBox.Name = "replaceIconsCheckBox";
            this.replaceIconsCheckBox.Size = new System.Drawing.Size(89, 17);
            this.replaceIconsCheckBox.TabIndex = 26;
            this.replaceIconsCheckBox.Text = "replace icons";
            this.replaceIconsCheckBox.UseVisualStyleBackColor = true;
            this.replaceIconsCheckBox.CheckedChanged += new System.EventHandler(this.replaceIconsCheckBox_CheckedChanged);
            // 
            // generateFakeD3DatsCheckBox
            // 
            this.generateFakeD3DatsCheckBox.AutoSize = true;
            this.generateFakeD3DatsCheckBox.Location = new System.Drawing.Point(113, 61);
            this.generateFakeD3DatsCheckBox.Name = "generateFakeD3DatsCheckBox";
            this.generateFakeD3DatsCheckBox.Size = new System.Drawing.Size(177, 17);
            this.generateFakeD3DatsCheckBox.TabIndex = 8;
            this.generateFakeD3DatsCheckBox.Text = "Generate Doom 3 .dat\'s for BFG";
            this.generateFakeD3DatsCheckBox.UseVisualStyleBackColor = true;
            this.generateFakeD3DatsCheckBox.CheckedChanged += new System.EventHandler(this.generateFakeD3DatsCheckBox_CheckedChanged);
            // 
            // BFGFontTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 556);
            this.Controls.Add(this.programModeTabControl);
            this.Controls.Add(this.label1);
            this.Name = "BFGFontTool";
            this.Text = "BFGFontTool";
            this.Load += new System.EventHandler(this.BFGFontTool_Load);
            this.programModeTabControl.ResumeLayout(false);
            this.createBFGFontTabPage.ResumeLayout(false);
            this.createBFGFontTabPage.PerformLayout();
            this.decomposeD3FontTabPage.ResumeLayout(false);
            this.decomposeD3FontTabPage.PerformLayout();
            this.tdmOptionsGroupBox.ResumeLayout(false);
            this.tdmOptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl programModeTabControl;
        private System.Windows.Forms.TabPage createBFGFontTabPage;
        private System.Windows.Forms.Button createBFGFontButton;
        private System.Windows.Forms.Label inputBMFontLabel;
        private System.Windows.Forms.Button bmFontBrowseButton;
        private System.Windows.Forms.TextBox bmFontTextBox;
        private System.Windows.Forms.TabPage decomposeD3FontTabPage;
        private System.Windows.Forms.OpenFileDialog bmFontOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog bfgFontSaveFileDialog;
        private System.Windows.Forms.Button decomposeD3FontButton;
        private System.Windows.Forms.Label bfgFontOutputFileLabel;
        private System.Windows.Forms.Button bfgFontBrowseButton;
        private System.Windows.Forms.TextBox bfgFontTextBox;
        private System.Windows.Forms.Label dirWithFontTexturesLabel;
        private System.Windows.Forms.Button dirWithFontTexturesBrowseButton;
        private System.Windows.Forms.TextBox dirWithFontTexturesTextBox;
        private System.Windows.Forms.Label doom3FontFileLabel;
        private System.Windows.Forms.Button doom3FontFileBrowseButton;
        private System.Windows.Forms.TextBox doom3FontFileTextBox;
        private System.Windows.Forms.Button createBfgFontHelpButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckedListBox languageCheckedListBox;
        private System.Windows.Forms.OpenFileDialog d3FontOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog dirWithFontTexturesOpenFileDialog;
        private System.Windows.Forms.GroupBox tdmOptionsGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label langMapsDirLabel;
        private System.Windows.Forms.Button langMapsDirBrowseButton;
        private System.Windows.Forms.TextBox langMapsDirTextBox;
        private System.Windows.Forms.Label glyphImageDirectoryLabel;
        private System.Windows.Forms.Button glyphImageDirectoryBrowseButton;
        private System.Windows.Forms.TextBox glyphImageDirectoryTextBox;
        private System.Windows.Forms.Label bmcfgOutputFileLabel;
        private System.Windows.Forms.Button bmcfgOutputFileBrowseButton;
        private System.Windows.Forms.TextBox bmcfgOutputFileTextBox;
        private System.Windows.Forms.OpenFileDialog langMapsDirOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog glyphImageDirectoryOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog bmcfgOutputSaveFileDialog;
        private System.Windows.Forms.CheckBox replaceIconsCheckBox;
        private System.Windows.Forms.CheckBox generateFakeD3DatsCheckBox;
    }
}

