namespace RBACS
{
    partial class RBAC
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RBAC));
            this.button1 = new System.Windows.Forms.Button();
            this.ConfigTab = new System.Windows.Forms.TabControl();
            this.OutputTab = new System.Windows.Forms.TabPage();
            this.mapUsersTextBox = new System.Windows.Forms.TextBox();
            this.mapUsersButton = new System.Windows.Forms.Button();
            this.clusterUsersButton = new System.Windows.Forms.Button();
            this.exportDataAsCSVButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distinguishedNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userQueryResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Grouping = new System.Windows.Forms.TabPage();
            this.mapGroupingSizeTextBox = new System.Windows.Forms.TextBox();
            this.mapGroupingsButton = new System.Windows.Forms.Button();
            this.clusterGroupingsButton = new System.Windows.Forms.Button();
            this.exportGroupingsAsFilesButton = new System.Windows.Forms.Button();
            this.exportGroupingAsCSVButton = new System.Windows.Forms.Button();
            this.groupingDataGridView = new System.Windows.Forms.DataGridView();
            this.Config = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.permissionsTypeTextBox = new System.Windows.Forms.TextBox();
            this.csvImportButton = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.importFileTextBox = new System.Windows.Forms.TextBox();
            this.andOrGroupBox = new System.Windows.Forms.GroupBox();
            this.orRadioButton = new System.Windows.Forms.RadioButton();
            this.andRadioButton = new System.Windows.Forms.RadioButton();
            this.dNorContainerGroupBox = new System.Windows.Forms.GroupBox();
            this.oURadioButton = new System.Windows.Forms.RadioButton();
            this.dNRadioButton = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.recommendThresholdTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.multipleFilesGroupingExportRadioButton = new System.Windows.Forms.RadioButton();
            this.singleFileGroupingsExportRadioButton = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inclusionsBox = new System.Windows.Forms.RichTextBox();
            this.searchSizeLimitBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.titleRadioButtion = new System.Windows.Forms.RadioButton();
            this.descriptionRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AlgoConfig = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.epochsTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
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
            this.label11 = new System.Windows.Forms.Label();
            this.hACTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.wardRadioButton = new System.Windows.Forms.RadioButton();
            this.centroidRadioButton = new System.Windows.Forms.RadioButton();
            this.averageDistanceRadioButton = new System.Windows.Forms.RadioButton();
            this.completeLinkRadioButton = new System.Windows.Forms.RadioButton();
            this.singleLinkRadioButton = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.clusterByTFIDFRadioButton = new System.Windows.Forms.RadioButton();
            this.clusterByRelativeCountRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rawCountRadioButton = new System.Windows.Forms.RadioButton();
            this.relativeCountRadioButton = new System.Windows.Forms.RadioButton();
            this.queryRichTextBox = new System.Windows.Forms.RichTextBox();
            this.GroupsListBox = new System.Windows.Forms.ListBox();
            this.GroupNumberLabel = new System.Windows.Forms.Label();
            this.userNumberLabel = new System.Windows.Forms.Label();
            this.titlesNumberLabel = new System.Windows.Forms.Label();
            this.descriptionsNumberLabel = new System.Windows.Forms.Label();
            this.dataGroupingButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.xMLImportButton = new System.Windows.Forms.Button();
            this.xMLExportButton = new System.Windows.Forms.Button();
            this.xMLFileNameTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.rBACBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ConfigTab.SuspendLayout();
            this.OutputTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userQueryResultBindingSource)).BeginInit();
            this.Grouping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupingDataGridView)).BeginInit();
            this.Config.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.andOrGroupBox.SuspendLayout();
            this.dNorContainerGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.AlgoConfig.SuspendLayout();
            this.kMeansStoppingConditionGroupBox.SuspendLayout();
            this.hACStoppingConditionGroupBox.SuspendLayout();
            this.clusteringAlgoTypeGroupBox.SuspendLayout();
            this.hACTypeGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rBACBindingSource1)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Franklin Gothic Demi", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(535, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(272, 83);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConfigTab
            // 
            this.ConfigTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigTab.Controls.Add(this.OutputTab);
            this.ConfigTab.Controls.Add(this.Grouping);
            this.ConfigTab.Controls.Add(this.Config);
            this.ConfigTab.Controls.Add(this.AlgoConfig);
            this.ConfigTab.Location = new System.Drawing.Point(12, 18);
            this.ConfigTab.Name = "ConfigTab";
            this.ConfigTab.SelectedIndex = 0;
            this.ConfigTab.Size = new System.Drawing.Size(515, 487);
            this.ConfigTab.TabIndex = 1;
            // 
            // OutputTab
            // 
            this.OutputTab.Controls.Add(this.mapUsersTextBox);
            this.OutputTab.Controls.Add(this.mapUsersButton);
            this.OutputTab.Controls.Add(this.clusterUsersButton);
            this.OutputTab.Controls.Add(this.exportDataAsCSVButton);
            this.OutputTab.Controls.Add(this.dataGridView1);
            this.OutputTab.Location = new System.Drawing.Point(4, 22);
            this.OutputTab.Name = "OutputTab";
            this.OutputTab.Padding = new System.Windows.Forms.Padding(3);
            this.OutputTab.Size = new System.Drawing.Size(507, 461);
            this.OutputTab.TabIndex = 0;
            this.OutputTab.Text = "Users";
            this.OutputTab.UseVisualStyleBackColor = true;
            // 
            // mapUsersTextBox
            // 
            this.mapUsersTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mapUsersTextBox.Location = new System.Drawing.Point(372, 430);
            this.mapUsersTextBox.Name = "mapUsersTextBox";
            this.mapUsersTextBox.Size = new System.Drawing.Size(63, 20);
            this.mapUsersTextBox.TabIndex = 4;
            this.mapUsersTextBox.Text = "5";
            this.mapUsersTextBox.TextChanged += new System.EventHandler(this.mapUsersTextBox_TextChanged);
            // 
            // mapUsersButton
            // 
            this.mapUsersButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mapUsersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapUsersButton.Location = new System.Drawing.Point(291, 428);
            this.mapUsersButton.Name = "mapUsersButton";
            this.mapUsersButton.Size = new System.Drawing.Size(75, 23);
            this.mapUsersButton.TabIndex = 3;
            this.mapUsersButton.Text = "Map";
            this.mapUsersButton.UseVisualStyleBackColor = true;
            this.mapUsersButton.Click += new System.EventHandler(this.mapUsersButton_Click);
            // 
            // clusterUsersButton
            // 
            this.clusterUsersButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.clusterUsersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clusterUsersButton.Location = new System.Drawing.Point(153, 428);
            this.clusterUsersButton.Name = "clusterUsersButton";
            this.clusterUsersButton.Size = new System.Drawing.Size(132, 23);
            this.clusterUsersButton.TabIndex = 2;
            this.clusterUsersButton.Text = "Cluster";
            this.clusterUsersButton.UseVisualStyleBackColor = true;
            this.clusterUsersButton.Click += new System.EventHandler(this.clusterUsersButton_Click);
            // 
            // exportDataAsCSVButton
            // 
            this.exportDataAsCSVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportDataAsCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportDataAsCSVButton.Location = new System.Drawing.Point(6, 428);
            this.exportDataAsCSVButton.Name = "exportDataAsCSVButton";
            this.exportDataAsCSVButton.Size = new System.Drawing.Size(141, 23);
            this.exportDataAsCSVButton.TabIndex = 1;
            this.exportDataAsCSVButton.Text = "Export As CSV";
            this.exportDataAsCSVButton.UseVisualStyleBackColor = true;
            this.exportDataAsCSVButton.Click += new System.EventHandler(this.exportDataAsCSVButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.accountNameDataGridViewTextBoxColumn,
            this.distinguishedNameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.userQueryResultBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(6, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(479, 402);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // accountNameDataGridViewTextBoxColumn
            // 
            this.accountNameDataGridViewTextBoxColumn.DataPropertyName = "AccountName";
            this.accountNameDataGridViewTextBoxColumn.HeaderText = "AccountName";
            this.accountNameDataGridViewTextBoxColumn.Name = "accountNameDataGridViewTextBoxColumn";
            this.accountNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // distinguishedNameDataGridViewTextBoxColumn
            // 
            this.distinguishedNameDataGridViewTextBoxColumn.DataPropertyName = "DistinguishedName";
            this.distinguishedNameDataGridViewTextBoxColumn.HeaderText = "DistinguishedName";
            this.distinguishedNameDataGridViewTextBoxColumn.Name = "distinguishedNameDataGridViewTextBoxColumn";
            this.distinguishedNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userQueryResultBindingSource
            // 
            this.userQueryResultBindingSource.DataSource = typeof(RBACS.UserQueryResult);
            // 
            // Grouping
            // 
            this.Grouping.BackColor = System.Drawing.Color.Transparent;
            this.Grouping.Controls.Add(this.mapGroupingSizeTextBox);
            this.Grouping.Controls.Add(this.mapGroupingsButton);
            this.Grouping.Controls.Add(this.clusterGroupingsButton);
            this.Grouping.Controls.Add(this.exportGroupingsAsFilesButton);
            this.Grouping.Controls.Add(this.exportGroupingAsCSVButton);
            this.Grouping.Controls.Add(this.groupingDataGridView);
            this.Grouping.Location = new System.Drawing.Point(4, 22);
            this.Grouping.Name = "Grouping";
            this.Grouping.Size = new System.Drawing.Size(507, 461);
            this.Grouping.TabIndex = 2;
            this.Grouping.Text = "Grouping";
            // 
            // mapGroupingSizeTextBox
            // 
            this.mapGroupingSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mapGroupingSizeTextBox.Location = new System.Drawing.Point(462, 430);
            this.mapGroupingSizeTextBox.Name = "mapGroupingSizeTextBox";
            this.mapGroupingSizeTextBox.Size = new System.Drawing.Size(35, 20);
            this.mapGroupingSizeTextBox.TabIndex = 5;
            this.mapGroupingSizeTextBox.Text = "5";
            this.mapGroupingSizeTextBox.TextChanged += new System.EventHandler(this.mapGroupingSizeTextBox_TextChanged);
            // 
            // mapGroupingsButton
            // 
            this.mapGroupingsButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mapGroupingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapGroupingsButton.Location = new System.Drawing.Point(398, 428);
            this.mapGroupingsButton.Name = "mapGroupingsButton";
            this.mapGroupingsButton.Size = new System.Drawing.Size(58, 23);
            this.mapGroupingsButton.TabIndex = 4;
            this.mapGroupingsButton.Text = "Map";
            this.mapGroupingsButton.UseVisualStyleBackColor = true;
            this.mapGroupingsButton.Click += new System.EventHandler(this.mapGroupingsButton_Click);
            // 
            // clusterGroupingsButton
            // 
            this.clusterGroupingsButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.clusterGroupingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clusterGroupingsButton.Location = new System.Drawing.Point(317, 428);
            this.clusterGroupingsButton.Name = "clusterGroupingsButton";
            this.clusterGroupingsButton.Size = new System.Drawing.Size(75, 23);
            this.clusterGroupingsButton.TabIndex = 3;
            this.clusterGroupingsButton.Text = "Cluster";
            this.clusterGroupingsButton.UseVisualStyleBackColor = true;
            this.clusterGroupingsButton.Click += new System.EventHandler(this.clusterGroupingsButton_Click);
            // 
            // exportGroupingsAsFilesButton
            // 
            this.exportGroupingsAsFilesButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.exportGroupingsAsFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportGroupingsAsFilesButton.Location = new System.Drawing.Point(149, 428);
            this.exportGroupingsAsFilesButton.Name = "exportGroupingsAsFilesButton";
            this.exportGroupingsAsFilesButton.Size = new System.Drawing.Size(162, 23);
            this.exportGroupingsAsFilesButton.TabIndex = 2;
            this.exportGroupingsAsFilesButton.Text = "Export As File(s)";
            this.exportGroupingsAsFilesButton.UseVisualStyleBackColor = true;
            this.exportGroupingsAsFilesButton.Click += new System.EventHandler(this.exportGroupingsAsFilesButton_Click);
            // 
            // exportGroupingAsCSVButton
            // 
            this.exportGroupingAsCSVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportGroupingAsCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportGroupingAsCSVButton.Location = new System.Drawing.Point(3, 428);
            this.exportGroupingAsCSVButton.Name = "exportGroupingAsCSVButton";
            this.exportGroupingAsCSVButton.Size = new System.Drawing.Size(140, 23);
            this.exportGroupingAsCSVButton.TabIndex = 1;
            this.exportGroupingAsCSVButton.Text = "Export As CSV";
            this.exportGroupingAsCSVButton.UseVisualStyleBackColor = true;
            this.exportGroupingAsCSVButton.Click += new System.EventHandler(this.exportGroupingAsCSVButton_Click);
            // 
            // groupingDataGridView
            // 
            this.groupingDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupingDataGridView.Location = new System.Drawing.Point(3, 3);
            this.groupingDataGridView.Name = "groupingDataGridView";
            this.groupingDataGridView.Size = new System.Drawing.Size(501, 415);
            this.groupingDataGridView.TabIndex = 0;
            this.groupingDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.groupingDataGridView_CellContentDoubleClick);
            this.groupingDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.groupingDataGridView_ColumnHeaderMouseClick);
            // 
            // Config
            // 
            this.Config.Controls.Add(this.groupBox5);
            this.Config.Controls.Add(this.label18);
            this.Config.Controls.Add(this.importFileTextBox);
            this.Config.Controls.Add(this.andOrGroupBox);
            this.Config.Controls.Add(this.dNorContainerGroupBox);
            this.Config.Controls.Add(this.label10);
            this.Config.Controls.Add(this.recommendThresholdTextBox);
            this.Config.Controls.Add(this.pictureBox1);
            this.Config.Controls.Add(this.groupBox3);
            this.Config.Controls.Add(this.label8);
            this.Config.Controls.Add(this.label7);
            this.Config.Controls.Add(this.textBox2);
            this.Config.Controls.Add(this.label5);
            this.Config.Controls.Add(this.inclusionsBox);
            this.Config.Controls.Add(this.searchSizeLimitBox);
            this.Config.Controls.Add(this.label4);
            this.Config.Controls.Add(this.groupBox1);
            this.Config.Controls.Add(this.label3);
            this.Config.Controls.Add(this.label2);
            this.Config.Controls.Add(this.richTextBox2);
            this.Config.Controls.Add(this.label1);
            this.Config.Controls.Add(this.textBox1);
            this.Config.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Config.Location = new System.Drawing.Point(4, 22);
            this.Config.Name = "Config";
            this.Config.Padding = new System.Windows.Forms.Padding(3);
            this.Config.Size = new System.Drawing.Size(507, 461);
            this.Config.TabIndex = 1;
            this.Config.Text = "Config";
            this.Config.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.permissionsTypeTextBox);
            this.groupBox5.Controls.Add(this.csvImportButton);
            this.groupBox5.Location = new System.Drawing.Point(295, 159);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(185, 102);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 74);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 16);
            this.label19.TabIndex = 2;
            this.label19.Text = "Type:";
            // 
            // permissionsTypeTextBox
            // 
            this.permissionsTypeTextBox.Location = new System.Drawing.Point(61, 70);
            this.permissionsTypeTextBox.Name = "permissionsTypeTextBox";
            this.permissionsTypeTextBox.Size = new System.Drawing.Size(114, 22);
            this.permissionsTypeTextBox.TabIndex = 1;
            this.permissionsTypeTextBox.Text = "Other";
            // 
            // csvImportButton
            // 
            this.csvImportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csvImportButton.Location = new System.Drawing.Point(9, 18);
            this.csvImportButton.Name = "csvImportButton";
            this.csvImportButton.Size = new System.Drawing.Size(166, 47);
            this.csvImportButton.TabIndex = 0;
            this.csvImportButton.Text = "Import Permissions from \r\nCSV";
            this.csvImportButton.UseVisualStyleBackColor = true;
            this.csvImportButton.Click += new System.EventHandler(this.csvImportButton_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(233, 402);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 16);
            this.label18.TabIndex = 26;
            this.label18.Text = "Import File:";
            // 
            // importFileTextBox
            // 
            this.importFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.importFileTextBox.Location = new System.Drawing.Point(312, 396);
            this.importFileTextBox.Name = "importFileTextBox";
            this.importFileTextBox.Size = new System.Drawing.Size(159, 22);
            this.importFileTextBox.TabIndex = 25;
            // 
            // andOrGroupBox
            // 
            this.andOrGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.andOrGroupBox.Controls.Add(this.orRadioButton);
            this.andOrGroupBox.Controls.Add(this.andRadioButton);
            this.andOrGroupBox.Location = new System.Drawing.Point(21, 186);
            this.andOrGroupBox.Name = "andOrGroupBox";
            this.andOrGroupBox.Size = new System.Drawing.Size(54, 76);
            this.andOrGroupBox.TabIndex = 24;
            this.andOrGroupBox.TabStop = false;
            // 
            // orRadioButton
            // 
            this.orRadioButton.AutoSize = true;
            this.orRadioButton.Checked = true;
            this.orRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orRadioButton.Location = new System.Drawing.Point(6, 46);
            this.orRadioButton.Name = "orRadioButton";
            this.orRadioButton.Size = new System.Drawing.Size(41, 17);
            this.orRadioButton.TabIndex = 25;
            this.orRadioButton.TabStop = true;
            this.orRadioButton.Text = "OR";
            this.orRadioButton.UseVisualStyleBackColor = true;
            // 
            // andRadioButton
            // 
            this.andRadioButton.AutoSize = true;
            this.andRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.andRadioButton.Location = new System.Drawing.Point(6, 21);
            this.andRadioButton.Name = "andRadioButton";
            this.andRadioButton.Size = new System.Drawing.Size(48, 17);
            this.andRadioButton.TabIndex = 25;
            this.andRadioButton.Text = "AND";
            this.andRadioButton.UseVisualStyleBackColor = true;
            this.andRadioButton.CheckedChanged += new System.EventHandler(this.andRadioButton_CheckedChanged);
            // 
            // dNorContainerGroupBox
            // 
            this.dNorContainerGroupBox.Controls.Add(this.oURadioButton);
            this.dNorContainerGroupBox.Controls.Add(this.dNRadioButton);
            this.dNorContainerGroupBox.Location = new System.Drawing.Point(19, 75);
            this.dNorContainerGroupBox.Name = "dNorContainerGroupBox";
            this.dNorContainerGroupBox.Size = new System.Drawing.Size(56, 74);
            this.dNorContainerGroupBox.TabIndex = 23;
            this.dNorContainerGroupBox.TabStop = false;
            // 
            // oURadioButton
            // 
            this.oURadioButton.AutoSize = true;
            this.oURadioButton.Checked = true;
            this.oURadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oURadioButton.Location = new System.Drawing.Point(8, 40);
            this.oURadioButton.Name = "oURadioButton";
            this.oURadioButton.Size = new System.Drawing.Size(41, 17);
            this.oURadioButton.TabIndex = 25;
            this.oURadioButton.TabStop = true;
            this.oURadioButton.Text = "OU";
            this.oURadioButton.UseVisualStyleBackColor = true;
            // 
            // dNRadioButton
            // 
            this.dNRadioButton.AutoSize = true;
            this.dNRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dNRadioButton.Location = new System.Drawing.Point(8, 17);
            this.dNRadioButton.Name = "dNRadioButton";
            this.dNRadioButton.Size = new System.Drawing.Size(41, 17);
            this.dNRadioButton.TabIndex = 25;
            this.dNRadioButton.Text = "DN";
            this.dNRadioButton.UseVisualStyleBackColor = true;
            this.dNRadioButton.CheckedChanged += new System.EventHandler(this.dNRadioButton_CheckedChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 399);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 16);
            this.label10.TabIndex = 22;
            this.label10.Text = "Recommend Threshold";
            // 
            // recommendThresholdTextBox
            // 
            this.recommendThresholdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recommendThresholdTextBox.Location = new System.Drawing.Point(162, 396);
            this.recommendThresholdTextBox.Name = "recommendThresholdTextBox";
            this.recommendThresholdTextBox.Size = new System.Drawing.Size(53, 22);
            this.recommendThresholdTextBox.TabIndex = 21;
            this.recommendThresholdTextBox.Text = "0.8";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(293, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(188, 167);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox3.Controls.Add(this.multipleFilesGroupingExportRadioButton);
            this.groupBox3.Controls.Add(this.singleFileGroupingsExportRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(339, 275);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(132, 70);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            // 
            // multipleFilesGroupingExportRadioButton
            // 
            this.multipleFilesGroupingExportRadioButton.AutoSize = true;
            this.multipleFilesGroupingExportRadioButton.Location = new System.Drawing.Point(6, 44);
            this.multipleFilesGroupingExportRadioButton.Name = "multipleFilesGroupingExportRadioButton";
            this.multipleFilesGroupingExportRadioButton.Size = new System.Drawing.Size(104, 20);
            this.multipleFilesGroupingExportRadioButton.TabIndex = 1;
            this.multipleFilesGroupingExportRadioButton.TabStop = true;
            this.multipleFilesGroupingExportRadioButton.Text = "Multiple Files";
            this.multipleFilesGroupingExportRadioButton.UseVisualStyleBackColor = true;
            // 
            // singleFileGroupingsExportRadioButton
            // 
            this.singleFileGroupingsExportRadioButton.AutoSize = true;
            this.singleFileGroupingsExportRadioButton.Checked = true;
            this.singleFileGroupingsExportRadioButton.Location = new System.Drawing.Point(6, 12);
            this.singleFileGroupingsExportRadioButton.Name = "singleFileGroupingsExportRadioButton";
            this.singleFileGroupingsExportRadioButton.Size = new System.Drawing.Size(89, 20);
            this.singleFileGroupingsExportRadioButton.TabIndex = 0;
            this.singleFileGroupingsExportRadioButton.TabStop = true;
            this.singleFileGroupingsExportRadioButton.Text = "Single File";
            this.singleFileGroupingsExportRadioButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Export Groupings:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 435);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Export Path:";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(312, 429);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(159, 22);
            this.textBox2.TabIndex = 14;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Include:";
            // 
            // inclusionsBox
            // 
            this.inclusionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inclusionsBox.Location = new System.Drawing.Point(89, 166);
            this.inclusionsBox.Name = "inclusionsBox";
            this.inclusionsBox.Size = new System.Drawing.Size(184, 96);
            this.inclusionsBox.TabIndex = 10;
            this.inclusionsBox.Text = "";
            this.inclusionsBox.TextChanged += new System.EventHandler(this.inclusionsBox_TextChanged);
            // 
            // searchSizeLimitBox
            // 
            this.searchSizeLimitBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchSizeLimitBox.Location = new System.Drawing.Point(122, 429);
            this.searchSizeLimitBox.Name = "searchSizeLimitBox";
            this.searchSizeLimitBox.Size = new System.Drawing.Size(93, 22);
            this.searchSizeLimitBox.TabIndex = 9;
            this.searchSizeLimitBox.Text = "10000";
            this.searchSizeLimitBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchSizeLimitBox_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 435);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Search Size Limit";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.groupBox1.Controls.Add(this.titleRadioButtion);
            this.groupBox1.Controls.Add(this.descriptionRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(89, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(126, 70);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // titleRadioButtion
            // 
            this.titleRadioButtion.AutoSize = true;
            this.titleRadioButtion.Checked = true;
            this.titleRadioButtion.Location = new System.Drawing.Point(6, 15);
            this.titleRadioButtion.Name = "titleRadioButtion";
            this.titleRadioButtion.Size = new System.Drawing.Size(52, 20);
            this.titleRadioButtion.TabIndex = 4;
            this.titleRadioButtion.TabStop = true;
            this.titleRadioButtion.Text = "Title";
            this.titleRadioButtion.UseVisualStyleBackColor = true;
            this.titleRadioButtion.CheckedChanged += new System.EventHandler(this.titleRadioButtion_CheckedChanged);
            // 
            // descriptionRadioButton
            // 
            this.descriptionRadioButton.AutoSize = true;
            this.descriptionRadioButton.Location = new System.Drawing.Point(6, 44);
            this.descriptionRadioButton.Name = "descriptionRadioButton";
            this.descriptionRadioButton.Size = new System.Drawing.Size(94, 20);
            this.descriptionRadioButton.TabIndex = 5;
            this.descriptionRadioButton.Text = "Description";
            this.descriptionRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Group By:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Exclude:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(89, 53);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(184, 96);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            this.richTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox2_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Domain";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(89, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // AlgoConfig
            // 
            this.AlgoConfig.Controls.Add(this.label16);
            this.AlgoConfig.Controls.Add(this.label14);
            this.AlgoConfig.Controls.Add(this.epochsTextBox);
            this.AlgoConfig.Controls.Add(this.label15);
            this.AlgoConfig.Controls.Add(this.kMeansStoppingConditionGroupBox);
            this.AlgoConfig.Controls.Add(this.hACStoppingConditionGroupBox);
            this.AlgoConfig.Controls.Add(this.label13);
            this.AlgoConfig.Controls.Add(this.kValueTextBox);
            this.AlgoConfig.Controls.Add(this.label12);
            this.AlgoConfig.Controls.Add(this.clusteringAlgoTypeGroupBox);
            this.AlgoConfig.Controls.Add(this.label11);
            this.AlgoConfig.Controls.Add(this.hACTypeGroupBox);
            this.AlgoConfig.Controls.Add(this.label9);
            this.AlgoConfig.Controls.Add(this.groupBox4);
            this.AlgoConfig.Controls.Add(this.label6);
            this.AlgoConfig.Controls.Add(this.groupBox2);
            this.AlgoConfig.Location = new System.Drawing.Point(4, 22);
            this.AlgoConfig.Name = "AlgoConfig";
            this.AlgoConfig.Size = new System.Drawing.Size(507, 461);
            this.AlgoConfig.TabIndex = 3;
            this.AlgoConfig.Text = "Algo Config";
            this.AlgoConfig.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(328, 425);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Epochs";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(327, 395);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "Mapping:";
            // 
            // epochsTextBox
            // 
            this.epochsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.epochsTextBox.Location = new System.Drawing.Point(392, 419);
            this.epochsTextBox.Name = "epochsTextBox";
            this.epochsTextBox.Size = new System.Drawing.Size(100, 20);
            this.epochsTextBox.TabIndex = 32;
            this.epochsTextBox.Text = "5";
            this.epochsTextBox.TextChanged += new System.EventHandler(this.epochsTextBox_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 323);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 26);
            this.label15.TabIndex = 31;
            this.label15.Text = "Stopping \r\nCondition:";
            // 
            // kMeansStoppingConditionGroupBox
            // 
            this.kMeansStoppingConditionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kMeansStoppingConditionGroupBox.Controls.Add(this.kMIterationsTextBox);
            this.kMeansStoppingConditionGroupBox.Controls.Add(this.kMIterationsRadioButton);
            this.kMeansStoppingConditionGroupBox.Controls.Add(this.meanDistancesToCentroidsRadioButton);
            this.kMeansStoppingConditionGroupBox.Location = new System.Drawing.Point(322, 307);
            this.kMeansStoppingConditionGroupBox.Name = "kMeansStoppingConditionGroupBox";
            this.kMeansStoppingConditionGroupBox.Size = new System.Drawing.Size(170, 82);
            this.kMeansStoppingConditionGroupBox.TabIndex = 30;
            this.kMeansStoppingConditionGroupBox.TabStop = false;
            // 
            // kMIterationsTextBox
            // 
            this.kMIterationsTextBox.Location = new System.Drawing.Point(82, 42);
            this.kMIterationsTextBox.Name = "kMIterationsTextBox";
            this.kMIterationsTextBox.Size = new System.Drawing.Size(38, 20);
            this.kMIterationsTextBox.TabIndex = 2;
            this.kMIterationsTextBox.Text = "100";
            // 
            // kMIterationsRadioButton
            // 
            this.kMIterationsRadioButton.AutoSize = true;
            this.kMIterationsRadioButton.Location = new System.Drawing.Point(8, 42);
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
            this.meanDistancesToCentroidsRadioButton.Location = new System.Drawing.Point(8, 19);
            this.meanDistancesToCentroidsRadioButton.Name = "meanDistancesToCentroidsRadioButton";
            this.meanDistancesToCentroidsRadioButton.Size = new System.Drawing.Size(89, 17);
            this.meanDistancesToCentroidsRadioButton.TabIndex = 0;
            this.meanDistancesToCentroidsRadioButton.TabStop = true;
            this.meanDistancesToCentroidsRadioButton.Text = "Convergence";
            this.meanDistancesToCentroidsRadioButton.UseVisualStyleBackColor = true;
            this.meanDistancesToCentroidsRadioButton.CheckedChanged += new System.EventHandler(this.meanDistancesToCentroidsRadioButton_CheckedChanged);
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
            this.hACStoppingConditionGroupBox.Location = new System.Drawing.Point(94, 307);
            this.hACStoppingConditionGroupBox.Name = "hACStoppingConditionGroupBox";
            this.hACStoppingConditionGroupBox.Size = new System.Drawing.Size(210, 132);
            this.hACStoppingConditionGroupBox.TabIndex = 29;
            this.hACStoppingConditionGroupBox.TabStop = false;
            // 
            // iterationsTextBox
            // 
            this.iterationsTextBox.Location = new System.Drawing.Point(147, 62);
            this.iterationsTextBox.Name = "iterationsTextBox";
            this.iterationsTextBox.Size = new System.Drawing.Size(51, 20);
            this.iterationsTextBox.TabIndex = 36;
            this.iterationsTextBox.Text = "100";
            this.iterationsTextBox.TextChanged += new System.EventHandler(this.iterationsTextBox_TextChanged);
            // 
            // pLDValueTextBox
            // 
            this.pLDValueTextBox.Location = new System.Drawing.Point(147, 39);
            this.pLDValueTextBox.Name = "pLDValueTextBox";
            this.pLDValueTextBox.Size = new System.Drawing.Size(51, 20);
            this.pLDValueTextBox.TabIndex = 35;
            this.pLDValueTextBox.Text = "1.5";
            this.pLDValueTextBox.TextChanged += new System.EventHandler(this.pLDValueTextBox_TextChanged);
            // 
            // p1DValueTextBox
            // 
            this.p1DValueTextBox.Location = new System.Drawing.Point(147, 16);
            this.p1DValueTextBox.Name = "p1DValueTextBox";
            this.p1DValueTextBox.Size = new System.Drawing.Size(51, 20);
            this.p1DValueTextBox.TabIndex = 34;
            this.p1DValueTextBox.Text = "1.5";
            this.p1DValueTextBox.TextChanged += new System.EventHandler(this.p1DValueTextBox_TextChanged);
            // 
            // noneButton
            // 
            this.noneButton.AutoSize = true;
            this.noneButton.Location = new System.Drawing.Point(6, 88);
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
            this.iterationsRadioButton.Location = new System.Drawing.Point(6, 65);
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
            this.firstDistanceRadioButton.Size = new System.Drawing.Size(135, 17);
            this.firstDistanceRadioButton.TabIndex = 32;
            this.firstDistanceRadioButton.TabStop = true;
            this.firstDistanceRadioButton.Text = "Proportion 1st Distance";
            this.firstDistanceRadioButton.UseVisualStyleBackColor = true;
            this.firstDistanceRadioButton.CheckedChanged += new System.EventHandler(this.firstDistanceRadioButton_CheckedChanged);
            // 
            // proportionLastDistanceRadioButton
            // 
            this.proportionLastDistanceRadioButton.AutoSize = true;
            this.proportionLastDistanceRadioButton.Location = new System.Drawing.Point(6, 42);
            this.proportionLastDistanceRadioButton.Name = "proportionLastDistanceRadioButton";
            this.proportionLastDistanceRadioButton.Size = new System.Drawing.Size(141, 17);
            this.proportionLastDistanceRadioButton.TabIndex = 30;
            this.proportionLastDistanceRadioButton.Text = "Proportion Last Distance";
            this.proportionLastDistanceRadioButton.UseVisualStyleBackColor = true;
            this.proportionLastDistanceRadioButton.CheckedChanged += new System.EventHandler(this.proportionLastDistanceRadioButton_CheckedChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(318, 169);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "K Value:";
            // 
            // kValueTextBox
            // 
            this.kValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kValueTextBox.Location = new System.Drawing.Point(371, 166);
            this.kValueTextBox.Name = "kValueTextBox";
            this.kValueTextBox.Size = new System.Drawing.Size(121, 20);
            this.kValueTextBox.TabIndex = 25;
            this.kValueTextBox.Text = "2";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Clustering Algo:";
            // 
            // clusteringAlgoTypeGroupBox
            // 
            this.clusteringAlgoTypeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clusteringAlgoTypeGroupBox.Controls.Add(this.kMeansRadioButton);
            this.clusteringAlgoTypeGroupBox.Controls.Add(this.hACRadioButton);
            this.clusteringAlgoTypeGroupBox.Location = new System.Drawing.Point(94, 111);
            this.clusteringAlgoTypeGroupBox.Name = "clusteringAlgoTypeGroupBox";
            this.clusteringAlgoTypeGroupBox.Size = new System.Drawing.Size(398, 41);
            this.clusteringAlgoTypeGroupBox.TabIndex = 23;
            this.clusteringAlgoTypeGroupBox.TabStop = false;
            // 
            // kMeansRadioButton
            // 
            this.kMeansRadioButton.AutoSize = true;
            this.kMeansRadioButton.Location = new System.Drawing.Point(228, 16);
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
            this.hACRadioButton.Location = new System.Drawing.Point(10, 16);
            this.hACRadioButton.Name = "hACRadioButton";
            this.hACRadioButton.Size = new System.Drawing.Size(200, 17);
            this.hACRadioButton.TabIndex = 24;
            this.hACRadioButton.TabStop = true;
            this.hACRadioButton.Text = "Hierarchical Agglomerative Clustering";
            this.hACRadioButton.UseVisualStyleBackColor = true;
            this.hACRadioButton.CheckedChanged += new System.EventHandler(this.hACRadioButton_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "HAC Type:";
            // 
            // hACTypeGroupBox
            // 
            this.hACTypeGroupBox.Controls.Add(this.wardRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.centroidRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.averageDistanceRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.completeLinkRadioButton);
            this.hACTypeGroupBox.Controls.Add(this.singleLinkRadioButton);
            this.hACTypeGroupBox.Location = new System.Drawing.Point(94, 158);
            this.hACTypeGroupBox.Name = "hACTypeGroupBox";
            this.hACTypeGroupBox.Size = new System.Drawing.Size(126, 134);
            this.hACTypeGroupBox.TabIndex = 21;
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
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Cluster/KNN By:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.clusterByTFIDFRadioButton);
            this.groupBox4.Controls.Add(this.clusterByRelativeCountRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(322, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(170, 69);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            // 
            // clusterByTFIDFRadioButton
            // 
            this.clusterByTFIDFRadioButton.AutoSize = true;
            this.clusterByTFIDFRadioButton.Location = new System.Drawing.Point(6, 43);
            this.clusterByTFIDFRadioButton.Name = "clusterByTFIDFRadioButton";
            this.clusterByTFIDFRadioButton.Size = new System.Drawing.Size(58, 17);
            this.clusterByTFIDFRadioButton.TabIndex = 1;
            this.clusterByTFIDFRadioButton.Text = "TF-IDF";
            this.clusterByTFIDFRadioButton.UseVisualStyleBackColor = true;
            // 
            // clusterByRelativeCountRadioButton
            // 
            this.clusterByRelativeCountRadioButton.AutoSize = true;
            this.clusterByRelativeCountRadioButton.Checked = true;
            this.clusterByRelativeCountRadioButton.Location = new System.Drawing.Point(6, 17);
            this.clusterByRelativeCountRadioButton.Name = "clusterByRelativeCountRadioButton";
            this.clusterByRelativeCountRadioButton.Size = new System.Drawing.Size(95, 17);
            this.clusterByRelativeCountRadioButton.TabIndex = 0;
            this.clusterByRelativeCountRadioButton.TabStop = true;
            this.clusterByRelativeCountRadioButton.Text = "Relative Count";
            this.clusterByRelativeCountRadioButton.UseVisualStyleBackColor = true;
            this.clusterByRelativeCountRadioButton.CheckedChanged += new System.EventHandler(this.clusterByRelativeCountRadioButton_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "TF-IDF By:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rawCountRadioButton);
            this.groupBox2.Controls.Add(this.relativeCountRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(94, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 69);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // rawCountRadioButton
            // 
            this.rawCountRadioButton.AutoSize = true;
            this.rawCountRadioButton.Location = new System.Drawing.Point(6, 43);
            this.rawCountRadioButton.Name = "rawCountRadioButton";
            this.rawCountRadioButton.Size = new System.Drawing.Size(78, 17);
            this.rawCountRadioButton.TabIndex = 13;
            this.rawCountRadioButton.Text = "Raw Count";
            this.rawCountRadioButton.UseVisualStyleBackColor = true;
            // 
            // relativeCountRadioButton
            // 
            this.relativeCountRadioButton.AutoSize = true;
            this.relativeCountRadioButton.Checked = true;
            this.relativeCountRadioButton.Location = new System.Drawing.Point(6, 17);
            this.relativeCountRadioButton.Name = "relativeCountRadioButton";
            this.relativeCountRadioButton.Size = new System.Drawing.Size(95, 17);
            this.relativeCountRadioButton.TabIndex = 13;
            this.relativeCountRadioButton.TabStop = true;
            this.relativeCountRadioButton.Text = "Relative Count";
            this.relativeCountRadioButton.UseVisualStyleBackColor = true;
            // 
            // queryRichTextBox
            // 
            this.queryRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryRichTextBox.Location = new System.Drawing.Point(536, 194);
            this.queryRichTextBox.Name = "queryRichTextBox";
            this.queryRichTextBox.Size = new System.Drawing.Size(271, 58);
            this.queryRichTextBox.TabIndex = 2;
            this.queryRichTextBox.Text = "No Query";
            // 
            // GroupsListBox
            // 
            this.GroupsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupsListBox.FormattingEnabled = true;
            this.GroupsListBox.Location = new System.Drawing.Point(536, 272);
            this.GroupsListBox.Name = "GroupsListBox";
            this.GroupsListBox.Size = new System.Drawing.Size(271, 186);
            this.GroupsListBox.TabIndex = 3;
            // 
            // GroupNumberLabel
            // 
            this.GroupNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupNumberLabel.AutoSize = true;
            this.GroupNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupNumberLabel.Location = new System.Drawing.Point(533, 475);
            this.GroupNumberLabel.Name = "GroupNumberLabel";
            this.GroupNumberLabel.Size = new System.Drawing.Size(62, 16);
            this.GroupNumberLabel.TabIndex = 4;
            this.GroupNumberLabel.Text = "Groups:0";
            // 
            // userNumberLabel
            // 
            this.userNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.userNumberLabel.AutoSize = true;
            this.userNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNumberLabel.Location = new System.Drawing.Point(621, 475);
            this.userNumberLabel.Name = "userNumberLabel";
            this.userNumberLabel.Size = new System.Drawing.Size(54, 16);
            this.userNumberLabel.TabIndex = 5;
            this.userNumberLabel.Text = "Users:0";
            // 
            // titlesNumberLabel
            // 
            this.titlesNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.titlesNumberLabel.AutoSize = true;
            this.titlesNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titlesNumberLabel.Location = new System.Drawing.Point(533, 500);
            this.titlesNumberLabel.Name = "titlesNumberLabel";
            this.titlesNumberLabel.Size = new System.Drawing.Size(51, 16);
            this.titlesNumberLabel.TabIndex = 6;
            this.titlesNumberLabel.Text = "Titles:0";
            // 
            // descriptionsNumberLabel
            // 
            this.descriptionsNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionsNumberLabel.AutoSize = true;
            this.descriptionsNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionsNumberLabel.Location = new System.Drawing.Point(621, 500);
            this.descriptionsNumberLabel.Name = "descriptionsNumberLabel";
            this.descriptionsNumberLabel.Size = new System.Drawing.Size(93, 16);
            this.descriptionsNumberLabel.TabIndex = 7;
            this.descriptionsNumberLabel.Text = "Descriptions:0";
            // 
            // dataGroupingButton
            // 
            this.dataGroupingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGroupingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGroupingButton.Location = new System.Drawing.Point(536, 110);
            this.dataGroupingButton.Name = "dataGroupingButton";
            this.dataGroupingButton.Size = new System.Drawing.Size(271, 30);
            this.dataGroupingButton.TabIndex = 8;
            this.dataGroupingButton.Text = "Group Data";
            this.dataGroupingButton.UseVisualStyleBackColor = true;
            this.dataGroupingButton.Click += new System.EventHandler(this.dataGroupingButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(6, 12);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(60, 13);
            this.statusLabel.TabIndex = 9;
            this.statusLabel.Text = "Status: Idle";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(306, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 15);
            this.label17.TabIndex = 10;
            this.label17.Text = "Find:";
            // 
            // findTextBox
            // 
            this.findTextBox.Location = new System.Drawing.Point(345, 10);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(182, 20);
            this.findTextBox.TabIndex = 11;
            this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.statusLabel);
            this.groupBox6.Location = new System.Drawing.Point(12, 519);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(788, 28);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            // 
            // xMLImportButton
            // 
            this.xMLImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xMLImportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMLImportButton.Location = new System.Drawing.Point(602, 155);
            this.xMLImportButton.Name = "xMLImportButton";
            this.xMLImportButton.Size = new System.Drawing.Size(50, 31);
            this.xMLImportButton.TabIndex = 13;
            this.xMLImportButton.Text = "Import";
            this.xMLImportButton.UseVisualStyleBackColor = true;
            this.xMLImportButton.Click += new System.EventHandler(this.xMLImportButton_Click);
            // 
            // xMLExportButton
            // 
            this.xMLExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xMLExportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMLExportButton.Location = new System.Drawing.Point(6, 9);
            this.xMLExportButton.Name = "xMLExportButton";
            this.xMLExportButton.Size = new System.Drawing.Size(54, 31);
            this.xMLExportButton.TabIndex = 14;
            this.xMLExportButton.Text = "Export";
            this.xMLExportButton.UseVisualStyleBackColor = true;
            this.xMLExportButton.Click += new System.EventHandler(this.xMLExportButton_Click);
            // 
            // xMLFileNameTextBox
            // 
            this.xMLFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xMLFileNameTextBox.Location = new System.Drawing.Point(66, 15);
            this.xMLFileNameTextBox.Name = "xMLFileNameTextBox";
            this.xMLFileNameTextBox.Size = new System.Drawing.Size(76, 20);
            this.xMLFileNameTextBox.TabIndex = 15;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(536, 155);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 32);
            this.label20.TabIndex = 16;
            this.label20.Text = "XML \r\nDatasets:";
            // 
            // rBACBindingSource1
            // 
            this.rBACBindingSource1.DataSource = typeof(RBACS.RBAC);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.xMLExportButton);
            this.groupBox7.Controls.Add(this.xMLFileNameTextBox);
            this.groupBox7.Location = new System.Drawing.Point(658, 146);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(148, 42);
            this.groupBox7.TabIndex = 17;
            this.groupBox7.TabStop = false;
            // 
            // RBAC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 559);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.xMLImportButton);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.findTextBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dataGroupingButton);
            this.Controls.Add(this.descriptionsNumberLabel);
            this.Controls.Add(this.titlesNumberLabel);
            this.Controls.Add(this.userNumberLabel);
            this.Controls.Add(this.GroupNumberLabel);
            this.Controls.Add(this.GroupsListBox);
            this.Controls.Add(this.queryRichTextBox);
            this.Controls.Add(this.ConfigTab);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RBAC";
            this.Text = "RBAC Analyser";
            this.Load += new System.EventHandler(this.RBAC_Load);
            this.TextChanged += new System.EventHandler(this.RBAC_TextChanged);
            this.ConfigTab.ResumeLayout(false);
            this.OutputTab.ResumeLayout(false);
            this.OutputTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userQueryResultBindingSource)).EndInit();
            this.Grouping.ResumeLayout(false);
            this.Grouping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupingDataGridView)).EndInit();
            this.Config.ResumeLayout(false);
            this.Config.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.andOrGroupBox.ResumeLayout(false);
            this.andOrGroupBox.PerformLayout();
            this.dNorContainerGroupBox.ResumeLayout(false);
            this.dNorContainerGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.AlgoConfig.ResumeLayout(false);
            this.AlgoConfig.PerformLayout();
            this.kMeansStoppingConditionGroupBox.ResumeLayout(false);
            this.kMeansStoppingConditionGroupBox.PerformLayout();
            this.hACStoppingConditionGroupBox.ResumeLayout(false);
            this.hACStoppingConditionGroupBox.PerformLayout();
            this.clusteringAlgoTypeGroupBox.ResumeLayout(false);
            this.clusteringAlgoTypeGroupBox.PerformLayout();
            this.hACTypeGroupBox.ResumeLayout(false);
            this.hACTypeGroupBox.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rBACBindingSource1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl ConfigTab;
        private System.Windows.Forms.TabPage OutputTab;
        private System.Windows.Forms.TabPage Config;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton descriptionRadioButton;
        private System.Windows.Forms.RadioButton titleRadioButtion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox queryRichTextBox;
        private System.Windows.Forms.TextBox searchSizeLimitBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox GroupsListBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label GroupNumberLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox inclusionsBox;
        private System.Windows.Forms.Label userNumberLabel;
        private System.Windows.Forms.Label titlesNumberLabel;
        private System.Windows.Forms.Label descriptionsNumberLabel;
        private System.Windows.Forms.BindingSource rBACBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn distinguishedNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource userQueryResultBindingSource;
        private System.Windows.Forms.TabPage Grouping;
        private System.Windows.Forms.DataGridView groupingDataGridView;
        private System.Windows.Forms.Button dataGroupingButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button exportDataAsCSVButton;
        private System.Windows.Forms.Button exportGroupingsAsFilesButton;
        private System.Windows.Forms.Button exportGroupingAsCSVButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton multipleFilesGroupingExportRadioButton;
        private System.Windows.Forms.RadioButton singleFileGroupingsExportRadioButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox recommendThresholdTextBox;
        private System.Windows.Forms.TabPage AlgoConfig;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton clusterByTFIDFRadioButton;
        private System.Windows.Forms.RadioButton clusterByRelativeCountRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rawCountRadioButton;
        private System.Windows.Forms.RadioButton relativeCountRadioButton;
        private System.Windows.Forms.Button clusterUsersButton;
        private System.Windows.Forms.Button clusterGroupingsButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox kValueTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox clusteringAlgoTypeGroupBox;
        private System.Windows.Forms.RadioButton kMeansRadioButton;
        private System.Windows.Forms.RadioButton hACRadioButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox hACTypeGroupBox;
        private System.Windows.Forms.RadioButton wardRadioButton;
        private System.Windows.Forms.RadioButton centroidRadioButton;
        private System.Windows.Forms.RadioButton averageDistanceRadioButton;
        private System.Windows.Forms.RadioButton completeLinkRadioButton;
        private System.Windows.Forms.RadioButton singleLinkRadioButton;
        private System.Windows.Forms.Label label15;
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
        private System.Windows.Forms.Button mapGroupingsButton;
        private System.Windows.Forms.TextBox mapUsersTextBox;
        private System.Windows.Forms.Button mapUsersButton;
        private System.Windows.Forms.TextBox mapGroupingSizeTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox epochsTextBox;
        private System.Windows.Forms.GroupBox andOrGroupBox;
        private System.Windows.Forms.RadioButton orRadioButton;
        private System.Windows.Forms.RadioButton andRadioButton;
        private System.Windows.Forms.GroupBox dNorContainerGroupBox;
        private System.Windows.Forms.RadioButton oURadioButton;
        private System.Windows.Forms.RadioButton dNRadioButton;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox permissionsTypeTextBox;
        private System.Windows.Forms.Button csvImportButton;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox importFileTextBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button xMLImportButton;
        private System.Windows.Forms.Button xMLExportButton;
        private System.Windows.Forms.TextBox xMLFileNameTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox7;
    }
}

