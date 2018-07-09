
namespace RBACS
{
    partial class KohonenForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KohonenForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSelected = new System.Windows.Forms.TabPage();
            this.membersDataGridView = new System.Windows.Forms.DataGridView();
            this.groupsOverviewDataGridView = new System.Windows.Forms.DataGridView();
            this.membersLabel = new System.Windows.Forms.Label();
            this.groupsOverviewLabel = new System.Windows.Forms.Label();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.kMeansStoppingConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.kMIterationsTextBox = new System.Windows.Forms.TextBox();
            this.kMIterationsRadioButton = new System.Windows.Forms.RadioButton();
            this.meanDistancesToCentroidsRadioButton = new System.Windows.Forms.RadioButton();
            this.hACStoppingConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.iterationsTextBox = new System.Windows.Forms.TextBox();
            this.pLDValueTextBox = new System.Windows.Forms.TextBox();
            this.p1DValueTextBox = new System.Windows.Forms.TextBox();
            this.noneButton = new System.Windows.Forms.RadioButton();
            this.iterationsRadioButton = new System.Windows.Forms.RadioButton();
            this.firstDistanceRadioButton = new System.Windows.Forms.RadioButton();
            this.proportionLastDistanceRadioButton = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.kValueTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.clusteringAlgoTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.kMeansRadioButton = new System.Windows.Forms.RadioButton();
            this.hACRadioButton = new System.Windows.Forms.RadioButton();
            this.hACTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.wardRadioButton = new System.Windows.Forms.RadioButton();
            this.centroidRadioButton = new System.Windows.Forms.RadioButton();
            this.averageDistanceRadioButton = new System.Windows.Forms.RadioButton();
            this.completeLinkRadioButton = new System.Windows.Forms.RadioButton();
            this.singleLinkRadioButton = new System.Windows.Forms.RadioButton();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.explorerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.currentNeuronLabel = new System.Windows.Forms.Label();
            this.clusterButton = new System.Windows.Forms.Button();
            this.unClusterButton = new System.Windows.Forms.Button();
            this.csvButton = new System.Windows.Forms.Button();
            this.templatesButton = new System.Windows.Forms.Button();
            this.displayAsClustersCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.membersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsOverviewDataGridView)).BeginInit();
            this.tabPageConfig.SuspendLayout();
            this.kMeansStoppingConditionGroupBox.SuspendLayout();
            this.hACStoppingConditionGroupBox.SuspendLayout();
            this.clusteringAlgoTypeGroupBox.SuspendLayout();
            this.hACTypeGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bubble;
            series1.Legend = "Legend1";
            series1.Name = "Clusters";
            series1.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(501, 382);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            this.chart1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDoubleClick);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageSelected);
            this.tabControl1.Controls.Add(this.tabPageConfig);
            this.tabControl1.Location = new System.Drawing.Point(532, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(353, 375);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageSelected
            // 
            this.tabPageSelected.Controls.Add(this.membersDataGridView);
            this.tabPageSelected.Controls.Add(this.groupsOverviewDataGridView);
            this.tabPageSelected.Controls.Add(this.membersLabel);
            this.tabPageSelected.Controls.Add(this.groupsOverviewLabel);
            this.tabPageSelected.Location = new System.Drawing.Point(4, 22);
            this.tabPageSelected.Name = "tabPageSelected";
            this.tabPageSelected.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSelected.Size = new System.Drawing.Size(345, 349);
            this.tabPageSelected.TabIndex = 0;
            this.tabPageSelected.Text = "Selected";
            this.tabPageSelected.UseVisualStyleBackColor = true;
            // 
            // membersDataGridView
            // 
            this.membersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.membersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.membersDataGridView.Location = new System.Drawing.Point(9, 202);
            this.membersDataGridView.Name = "membersDataGridView";
            this.membersDataGridView.Size = new System.Drawing.Size(330, 135);
            this.membersDataGridView.TabIndex = 3;
            this.membersDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.membersDataGridView_CellContentDoubleClick);
            this.membersDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.membersDataGridView_ColumnHeaderMouseClick);
            // 
            // groupsOverviewDataGridView
            // 
            this.groupsOverviewDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupsOverviewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupsOverviewDataGridView.Location = new System.Drawing.Point(9, 39);
            this.groupsOverviewDataGridView.Name = "groupsOverviewDataGridView";
            this.groupsOverviewDataGridView.Size = new System.Drawing.Size(330, 124);
            this.groupsOverviewDataGridView.TabIndex = 2;
            this.groupsOverviewDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.groupsOverviewDataGridView_ColumnHeaderMouseClick);
            // 
            // membersLabel
            // 
            this.membersLabel.AutoSize = true;
            this.membersLabel.Location = new System.Drawing.Point(6, 178);
            this.membersLabel.Name = "membersLabel";
            this.membersLabel.Size = new System.Drawing.Size(53, 13);
            this.membersLabel.TabIndex = 1;
            this.membersLabel.Text = "Members:";
            // 
            // groupsOverviewLabel
            // 
            this.groupsOverviewLabel.AutoSize = true;
            this.groupsOverviewLabel.Location = new System.Drawing.Point(6, 14);
            this.groupsOverviewLabel.Name = "groupsOverviewLabel";
            this.groupsOverviewLabel.Size = new System.Drawing.Size(92, 13);
            this.groupsOverviewLabel.TabIndex = 0;
            this.groupsOverviewLabel.Text = "Groups Overview:";
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.kMeansStoppingConditionGroupBox);
            this.tabPageConfig.Controls.Add(this.hACStoppingConditionGroupBox);
            this.tabPageConfig.Controls.Add(this.label13);
            this.tabPageConfig.Controls.Add(this.kValueTextBox);
            this.tabPageConfig.Controls.Add(this.label12);
            this.tabPageConfig.Controls.Add(this.clusteringAlgoTypeGroupBox);
            this.tabPageConfig.Controls.Add(this.hACTypeGroupBox);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(345, 349);
            this.tabPageConfig.TabIndex = 1;
            this.tabPageConfig.Text = "Config";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // kMeansStoppingConditionGroupBox
            // 
            this.kMeansStoppingConditionGroupBox.Controls.Add(this.kMIterationsTextBox);
            this.kMeansStoppingConditionGroupBox.Controls.Add(this.kMIterationsRadioButton);
            this.kMeansStoppingConditionGroupBox.Controls.Add(this.meanDistancesToCentroidsRadioButton);
            this.kMeansStoppingConditionGroupBox.Location = new System.Drawing.Point(189, 203);
            this.kMeansStoppingConditionGroupBox.Name = "kMeansStoppingConditionGroupBox";
            this.kMeansStoppingConditionGroupBox.Size = new System.Drawing.Size(150, 109);
            this.kMeansStoppingConditionGroupBox.TabIndex = 42;
            this.kMeansStoppingConditionGroupBox.TabStop = false;
            // 
            // kMIterationsTextBox
            // 
            this.kMIterationsTextBox.Location = new System.Drawing.Point(21, 66);
            this.kMIterationsTextBox.Name = "kMIterationsTextBox";
            this.kMIterationsTextBox.Size = new System.Drawing.Size(38, 20);
            this.kMIterationsTextBox.TabIndex = 2;
            this.kMIterationsTextBox.Text = "100";
            // 
            // kMIterationsRadioButton
            // 
            this.kMIterationsRadioButton.AutoSize = true;
            this.kMIterationsRadioButton.Location = new System.Drawing.Point(6, 43);
            this.kMIterationsRadioButton.Name = "kMIterationsRadioButton";
            this.kMIterationsRadioButton.Size = new System.Drawing.Size(68, 17);
            this.kMIterationsRadioButton.TabIndex = 1;
            this.kMIterationsRadioButton.Text = "Iterations";
            this.kMIterationsRadioButton.UseVisualStyleBackColor = true;
            // 
            // meanDistancesToCentroidsRadioButton
            // 
            this.meanDistancesToCentroidsRadioButton.AutoSize = true;
            this.meanDistancesToCentroidsRadioButton.Checked = true;
            this.meanDistancesToCentroidsRadioButton.Location = new System.Drawing.Point(6, 19);
            this.meanDistancesToCentroidsRadioButton.Name = "meanDistancesToCentroidsRadioButton";
            this.meanDistancesToCentroidsRadioButton.Size = new System.Drawing.Size(89, 17);
            this.meanDistancesToCentroidsRadioButton.TabIndex = 0;
            this.meanDistancesToCentroidsRadioButton.TabStop = true;
            this.meanDistancesToCentroidsRadioButton.Text = "Convergence";
            this.meanDistancesToCentroidsRadioButton.UseVisualStyleBackColor = true;
            // 
            // hACStoppingConditionGroupBox
            // 
            this.hACStoppingConditionGroupBox.Controls.Add(this.iterationsTextBox);
            this.hACStoppingConditionGroupBox.Controls.Add(this.pLDValueTextBox);
            this.hACStoppingConditionGroupBox.Controls.Add(this.p1DValueTextBox);
            this.hACStoppingConditionGroupBox.Controls.Add(this.noneButton);
            this.hACStoppingConditionGroupBox.Controls.Add(this.iterationsRadioButton);
            this.hACStoppingConditionGroupBox.Controls.Add(this.firstDistanceRadioButton);
            this.hACStoppingConditionGroupBox.Controls.Add(this.proportionLastDistanceRadioButton);
            this.hACStoppingConditionGroupBox.Location = new System.Drawing.Point(16, 203);
            this.hACStoppingConditionGroupBox.Name = "hACStoppingConditionGroupBox";
            this.hACStoppingConditionGroupBox.Size = new System.Drawing.Size(159, 109);
            this.hACStoppingConditionGroupBox.TabIndex = 41;
            this.hACStoppingConditionGroupBox.TabStop = false;
            // 
            // iterationsTextBox
            // 
            this.iterationsTextBox.Location = new System.Drawing.Point(96, 58);
            this.iterationsTextBox.Name = "iterationsTextBox";
            this.iterationsTextBox.Size = new System.Drawing.Size(39, 20);
            this.iterationsTextBox.TabIndex = 36;
            this.iterationsTextBox.Text = "100";
            // 
            // pLDValueTextBox
            // 
            this.pLDValueTextBox.Location = new System.Drawing.Point(96, 36);
            this.pLDValueTextBox.Name = "pLDValueTextBox";
            this.pLDValueTextBox.Size = new System.Drawing.Size(39, 20);
            this.pLDValueTextBox.TabIndex = 35;
            this.pLDValueTextBox.Text = "1.5";
            // 
            // p1DValueTextBox
            // 
            this.p1DValueTextBox.Location = new System.Drawing.Point(96, 16);
            this.p1DValueTextBox.Name = "p1DValueTextBox";
            this.p1DValueTextBox.Size = new System.Drawing.Size(39, 20);
            this.p1DValueTextBox.TabIndex = 34;
            this.p1DValueTextBox.Text = "1.5";
            // 
            // noneButton
            // 
            this.noneButton.AutoSize = true;
            this.noneButton.Location = new System.Drawing.Point(6, 84);
            this.noneButton.Name = "noneButton";
            this.noneButton.Size = new System.Drawing.Size(51, 17);
            this.noneButton.TabIndex = 31;
            this.noneButton.Text = "None";
            this.noneButton.UseVisualStyleBackColor = true;
            this.noneButton.CheckedChanged += new System.EventHandler(this.noneButton_CheckedChanged);
            // 
            // iterationsRadioButton
            // 
            this.iterationsRadioButton.AutoSize = true;
            this.iterationsRadioButton.Location = new System.Drawing.Point(6, 61);
            this.iterationsRadioButton.Name = "iterationsRadioButton";
            this.iterationsRadioButton.Size = new System.Drawing.Size(68, 17);
            this.iterationsRadioButton.TabIndex = 33;
            this.iterationsRadioButton.Text = "Iterations";
            this.iterationsRadioButton.UseVisualStyleBackColor = true;
            this.iterationsRadioButton.CheckedChanged += new System.EventHandler(this.iterationsRadioButton_CheckedChanged);
            // 
            // firstDistanceRadioButton
            // 
            this.firstDistanceRadioButton.AutoSize = true;
            this.firstDistanceRadioButton.Checked = true;
            this.firstDistanceRadioButton.Location = new System.Drawing.Point(6, 19);
            this.firstDistanceRadioButton.Name = "firstDistanceRadioButton";
            this.firstDistanceRadioButton.Size = new System.Drawing.Size(84, 17);
            this.firstDistanceRadioButton.TabIndex = 32;
            this.firstDistanceRadioButton.TabStop = true;
            this.firstDistanceRadioButton.Text = "1st Distance";
            this.firstDistanceRadioButton.UseVisualStyleBackColor = true;
            this.firstDistanceRadioButton.CheckedChanged += new System.EventHandler(this.firstDistanceRadioButton_CheckedChanged);
            // 
            // proportionLastDistanceRadioButton
            // 
            this.proportionLastDistanceRadioButton.AutoSize = true;
            this.proportionLastDistanceRadioButton.Location = new System.Drawing.Point(6, 39);
            this.proportionLastDistanceRadioButton.Name = "proportionLastDistanceRadioButton";
            this.proportionLastDistanceRadioButton.Size = new System.Drawing.Size(90, 17);
            this.proportionLastDistanceRadioButton.TabIndex = 30;
            this.proportionLastDistanceRadioButton.Text = "Last Distance";
            this.proportionLastDistanceRadioButton.UseVisualStyleBackColor = true;
            this.proportionLastDistanceRadioButton.CheckedChanged += new System.EventHandler(this.proportionLastDistanceRadioButton_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(191, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "K Value:";
            // 
            // kValueTextBox
            // 
            this.kValueTextBox.Location = new System.Drawing.Point(189, 89);
            this.kValueTextBox.Name = "kValueTextBox";
            this.kValueTextBox.Size = new System.Drawing.Size(49, 20);
            this.kValueTextBox.TabIndex = 39;
            this.kValueTextBox.Text = "2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(-102, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "Clustering Algo:";
            // 
            // clusteringAlgoTypeGroupBox
            // 
            this.clusteringAlgoTypeGroupBox.Controls.Add(this.kMeansRadioButton);
            this.clusteringAlgoTypeGroupBox.Controls.Add(this.hACRadioButton);
            this.clusteringAlgoTypeGroupBox.Location = new System.Drawing.Point(16, 7);
            this.clusteringAlgoTypeGroupBox.Name = "clusteringAlgoTypeGroupBox";
            this.clusteringAlgoTypeGroupBox.Size = new System.Drawing.Size(323, 41);
            this.clusteringAlgoTypeGroupBox.TabIndex = 37;
            this.clusteringAlgoTypeGroupBox.TabStop = false;
            // 
            // kMeansRadioButton
            // 
            this.kMeansRadioButton.AutoSize = true;
            this.kMeansRadioButton.Location = new System.Drawing.Point(178, 16);
            this.kMeansRadioButton.Name = "kMeansRadioButton";
            this.kMeansRadioButton.Size = new System.Drawing.Size(79, 17);
            this.kMeansRadioButton.TabIndex = 24;
            this.kMeansRadioButton.Text = "K-Means++";
            this.kMeansRadioButton.UseVisualStyleBackColor = true;
            // 
            // hACRadioButton
            // 
            this.hACRadioButton.AutoSize = true;
            this.hACRadioButton.Checked = true;
            this.hACRadioButton.Location = new System.Drawing.Point(6, 16);
            this.hACRadioButton.Name = "hACRadioButton";
            this.hACRadioButton.Size = new System.Drawing.Size(47, 17);
            this.hACRadioButton.TabIndex = 24;
            this.hACRadioButton.TabStop = true;
            this.hACRadioButton.Text = "HAC";
            this.hACRadioButton.UseVisualStyleBackColor = true;
            // 
            // hACTypeGroupBox
            // 
            this.hACTypeGroupBox.Controls.Add(this.wardRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.centroidRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.averageDistanceRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.completeLinkRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.singleLinkRadioButton);
            this.hACTypeGroupBox.Location = new System.Drawing.Point(16, 54);
            this.hACTypeGroupBox.Name = "hACTypeGroupBox";
            this.hACTypeGroupBox.Size = new System.Drawing.Size(159, 134);
            this.hACTypeGroupBox.TabIndex = 35;
            this.hACTypeGroupBox.TabStop = false;
            // 
            // wardRadioButton
            // 
            this.wardRadioButton.AutoSize = true;
            this.wardRadioButton.Location = new System.Drawing.Point(6, 106);
            this.wardRadioButton.Name = "wardRadioButton";
            this.wardRadioButton.Size = new System.Drawing.Size(51, 17);
            this.wardRadioButton.TabIndex = 22;
            this.wardRadioButton.Text = "Ward";
            this.wardRadioButton.UseVisualStyleBackColor = true;
            this.wardRadioButton.CheckedChanged += new System.EventHandler(this.wardRadioButton_CheckedChanged);
            // 
            // centroidRadioButton
            // 
            this.centroidRadioButton.AutoSize = true;
            this.centroidRadioButton.Checked = true;
            this.centroidRadioButton.Location = new System.Drawing.Point(6, 83);
            this.centroidRadioButton.Name = "centroidRadioButton";
            this.centroidRadioButton.Size = new System.Drawing.Size(64, 17);
            this.centroidRadioButton.TabIndex = 22;
            this.centroidRadioButton.TabStop = true;
            this.centroidRadioButton.Text = "Centroid";
            this.centroidRadioButton.UseVisualStyleBackColor = true;
            this.centroidRadioButton.CheckedChanged += new System.EventHandler(this.centroidRadioButton_CheckedChanged);
            // 
            // averageDistanceRadioButton
            // 
            this.averageDistanceRadioButton.AutoSize = true;
            this.averageDistanceRadioButton.Location = new System.Drawing.Point(6, 61);
            this.averageDistanceRadioButton.Name = "averageDistanceRadioButton";
            this.averageDistanceRadioButton.Size = new System.Drawing.Size(110, 17);
            this.averageDistanceRadioButton.TabIndex = 22;
            this.averageDistanceRadioButton.Text = "Average Distance";
            this.averageDistanceRadioButton.UseVisualStyleBackColor = true;
            this.averageDistanceRadioButton.CheckedChanged += new System.EventHandler(this.averageDistanceRadioButton_CheckedChanged);
            // 
            // completeLinkRadioButton
            // 
            this.completeLinkRadioButton.AutoSize = true;
            this.completeLinkRadioButton.Location = new System.Drawing.Point(6, 38);
            this.completeLinkRadioButton.Name = "completeLinkRadioButton";
            this.completeLinkRadioButton.Size = new System.Drawing.Size(92, 17);
            this.completeLinkRadioButton.TabIndex = 22;
            this.completeLinkRadioButton.Text = "Complete Link";
            this.completeLinkRadioButton.UseVisualStyleBackColor = true;
            this.completeLinkRadioButton.CheckedChanged += new System.EventHandler(this.completeLinkRadioButton_CheckedChanged);
            // 
            // singleLinkRadioButton
            // 
            this.singleLinkRadioButton.AutoSize = true;
            this.singleLinkRadioButton.Location = new System.Drawing.Point(6, 15);
            this.singleLinkRadioButton.Name = "singleLinkRadioButton";
            this.singleLinkRadioButton.Size = new System.Drawing.Size(77, 17);
            this.singleLinkRadioButton.TabIndex = 22;
            this.singleLinkRadioButton.Text = "Single Link";
            this.singleLinkRadioButton.UseVisualStyleBackColor = true;
            this.singleLinkRadioButton.CheckedChanged += new System.EventHandler(this.singleLinkRadioButton_CheckedChanged);
            // 
            // findTextBox
            // 
            this.findTextBox.Location = new System.Drawing.Point(42, 17);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(179, 20);
            this.findTextBox.TabIndex = 2;
            this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Find:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.findTextBox);
            this.groupBox1.Location = new System.Drawing.Point(274, 396);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // explorerRichTextBox
            // 
            this.explorerRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.explorerRichTextBox.Location = new System.Drawing.Point(398, 80);
            this.explorerRichTextBox.Name = "explorerRichTextBox";
            this.explorerRichTextBox.Size = new System.Drawing.Size(97, 245);
            this.explorerRichTextBox.TabIndex = 5;
            this.explorerRichTextBox.Text = "";
            this.explorerRichTextBox.WordWrap = false;
            // 
            // currentNeuronLabel
            // 
            this.currentNeuronLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentNeuronLabel.AutoSize = true;
            this.currentNeuronLabel.Location = new System.Drawing.Point(412, 57);
            this.currentNeuronLabel.Name = "currentNeuronLabel";
            this.currentNeuronLabel.Size = new System.Drawing.Size(0, 13);
            this.currentNeuronLabel.TabIndex = 6;
            // 
            // clusterButton
            // 
            this.clusterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clusterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clusterButton.Location = new System.Drawing.Point(532, 408);
            this.clusterButton.Name = "clusterButton";
            this.clusterButton.Size = new System.Drawing.Size(91, 25);
            this.clusterButton.TabIndex = 7;
            this.clusterButton.Text = "Cluster";
            this.clusterButton.UseVisualStyleBackColor = true;
            this.clusterButton.Click += new System.EventHandler(this.clusterButton_Click);
            // 
            // unClusterButton
            // 
            this.unClusterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.unClusterButton.Enabled = false;
            this.unClusterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unClusterButton.Location = new System.Drawing.Point(629, 408);
            this.unClusterButton.Name = "unClusterButton";
            this.unClusterButton.Size = new System.Drawing.Size(88, 25);
            this.unClusterButton.TabIndex = 8;
            this.unClusterButton.Text = "Un-Cluster";
            this.unClusterButton.UseVisualStyleBackColor = true;
            this.unClusterButton.Click += new System.EventHandler(this.unClusterButton_Click);
            // 
            // csvButton
            // 
            this.csvButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.csvButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csvButton.Location = new System.Drawing.Point(723, 408);
            this.csvButton.Name = "csvButton";
            this.csvButton.Size = new System.Drawing.Size(55, 25);
            this.csvButton.TabIndex = 9;
            this.csvButton.Text = "CSV";
            this.csvButton.UseVisualStyleBackColor = true;
            this.csvButton.Click += new System.EventHandler(this.csvButton_Click);
            // 
            // templatesButton
            // 
            this.templatesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.templatesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templatesButton.Location = new System.Drawing.Point(784, 408);
            this.templatesButton.Name = "templatesButton";
            this.templatesButton.Size = new System.Drawing.Size(91, 25);
            this.templatesButton.TabIndex = 10;
            this.templatesButton.Text = "Templates";
            this.templatesButton.UseVisualStyleBackColor = true;
            this.templatesButton.Click += new System.EventHandler(this.templatesButton_Click);
            // 
            // displayAsClustersCheckbox
            // 
            this.displayAsClustersCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.displayAsClustersCheckbox.AutoSize = true;
            this.displayAsClustersCheckbox.Enabled = false;
            this.displayAsClustersCheckbox.Location = new System.Drawing.Point(12, 413);
            this.displayAsClustersCheckbox.Name = "displayAsClustersCheckbox";
            this.displayAsClustersCheckbox.Size = new System.Drawing.Size(115, 17);
            this.displayAsClustersCheckbox.TabIndex = 11;
            this.displayAsClustersCheckbox.Text = "Display As Clusters";
            this.displayAsClustersCheckbox.UseVisualStyleBackColor = true;
            // 
            // KohonenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 445);
            this.Controls.Add(this.displayAsClustersCheckbox);
            this.Controls.Add(this.templatesButton);
            this.Controls.Add(this.csvButton);
            this.Controls.Add(this.unClusterButton);
            this.Controls.Add(this.clusterButton);
            this.Controls.Add(this.currentNeuronLabel);
            this.Controls.Add(this.explorerRichTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KohonenForm";
            this.Text = "Self-Organising Map";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageSelected.ResumeLayout(false);
            this.tabPageSelected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.membersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsOverviewDataGridView)).EndInit();
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.kMeansStoppingConditionGroupBox.ResumeLayout(false);
            this.kMeansStoppingConditionGroupBox.PerformLayout();
            this.hACStoppingConditionGroupBox.ResumeLayout(false);
            this.hACStoppingConditionGroupBox.PerformLayout();
            this.clusteringAlgoTypeGroupBox.ResumeLayout(false);
            this.clusteringAlgoTypeGroupBox.PerformLayout();
            this.hACTypeGroupBox.ResumeLayout(false);
            this.hACTypeGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSelected;
        private System.Windows.Forms.DataGridView groupsOverviewDataGridView;
        private System.Windows.Forms.Label membersLabel;
        private System.Windows.Forms.Label groupsOverviewLabel;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.DataGridView membersDataGridView;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox kMeansStoppingConditionGroupBox;
        private System.Windows.Forms.TextBox kMIterationsTextBox;
        private System.Windows.Forms.RadioButton kMIterationsRadioButton;
        private System.Windows.Forms.RadioButton meanDistancesToCentroidsRadioButton;
        private System.Windows.Forms.GroupBox hACStoppingConditionGroupBox;
        private System.Windows.Forms.TextBox iterationsTextBox;
        private System.Windows.Forms.TextBox pLDValueTextBox;
        private System.Windows.Forms.TextBox p1DValueTextBox;
        private System.Windows.Forms.RadioButton noneButton;
        private System.Windows.Forms.RadioButton iterationsRadioButton;
        private System.Windows.Forms.RadioButton firstDistanceRadioButton;
        private System.Windows.Forms.RadioButton proportionLastDistanceRadioButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox kValueTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox clusteringAlgoTypeGroupBox;
        private System.Windows.Forms.RadioButton kMeansRadioButton;
        private System.Windows.Forms.RadioButton hACRadioButton;
        private System.Windows.Forms.GroupBox hACTypeGroupBox;
        private System.Windows.Forms.RadioButton wardRadioButton;
        private System.Windows.Forms.RadioButton centroidRadioButton;
        private System.Windows.Forms.RadioButton averageDistanceRadioButton;
        private System.Windows.Forms.RadioButton completeLinkRadioButton;
        private System.Windows.Forms.RadioButton singleLinkRadioButton;
        private System.Windows.Forms.RichTextBox explorerRichTextBox;
        private System.Windows.Forms.Label currentNeuronLabel;
        private System.Windows.Forms.Button clusterButton;
        private System.Windows.Forms.Button unClusterButton;
        private System.Windows.Forms.Button csvButton;
        private System.Windows.Forms.Button templatesButton;
        private System.Windows.Forms.CheckBox displayAsClustersCheckbox;
    }
}