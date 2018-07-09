namespace RBACS
{
    partial class ClusteringOutput
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClusteringOutput));
            this.clustersDataGridView = new System.Windows.Forms.DataGridView();
            this.exportAsCSVButton = new System.Windows.Forms.Button();
            this.recommendTemplateButton = new System.Windows.Forms.Button();
            this.clusteringFindTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mapButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.clustersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // clustersDataGridView
            // 
            this.clustersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clustersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.clustersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.clustersDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.clustersDataGridView.Location = new System.Drawing.Point(12, 12);
            this.clustersDataGridView.Name = "clustersDataGridView";
            this.clustersDataGridView.Size = new System.Drawing.Size(757, 351);
            this.clustersDataGridView.TabIndex = 0;
            this.clustersDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clustersDataGridView_CellContentDoubleClick);
            this.clustersDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.clustersDataGridView_ColumnHeaderMouseClick);
            // 
            // exportAsCSVButton
            // 
            this.exportAsCSVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportAsCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportAsCSVButton.Location = new System.Drawing.Point(12, 371);
            this.exportAsCSVButton.Name = "exportAsCSVButton";
            this.exportAsCSVButton.Size = new System.Drawing.Size(217, 23);
            this.exportAsCSVButton.TabIndex = 1;
            this.exportAsCSVButton.Text = "Export As CSV";
            this.exportAsCSVButton.UseVisualStyleBackColor = true;
            this.exportAsCSVButton.Click += new System.EventHandler(this.exportAsCSVButton_Click);
            // 
            // recommendTemplateButton
            // 
            this.recommendTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.recommendTemplateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recommendTemplateButton.Location = new System.Drawing.Point(235, 371);
            this.recommendTemplateButton.Name = "recommendTemplateButton";
            this.recommendTemplateButton.Size = new System.Drawing.Size(263, 23);
            this.recommendTemplateButton.TabIndex = 2;
            this.recommendTemplateButton.Text = "Recommend Template For Each";
            this.recommendTemplateButton.UseVisualStyleBackColor = true;
            this.recommendTemplateButton.Click += new System.EventHandler(this.recommendTemplateButton_Click);
            // 
            // clusteringFindTextBox
            // 
            this.clusteringFindTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clusteringFindTextBox.Location = new System.Drawing.Point(639, 373);
            this.clusteringFindTextBox.Name = "clusteringFindTextBox";
            this.clusteringFindTextBox.Size = new System.Drawing.Size(130, 20);
            this.clusteringFindTextBox.TabIndex = 3;
            this.clusteringFindTextBox.TextChanged += new System.EventHandler(this.clusteringFindTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(603, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Find:";
            // 
            // mapButton
            // 
            this.mapButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mapButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapButton.Location = new System.Drawing.Point(504, 371);
            this.mapButton.Name = "mapButton";
            this.mapButton.Size = new System.Drawing.Size(60, 23);
            this.mapButton.TabIndex = 5;
            this.mapButton.Text = "Map";
            this.mapButton.UseVisualStyleBackColor = true;
            this.mapButton.Click += new System.EventHandler(this.mapButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox1.Location = new System.Drawing.Point(570, 373);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(27, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "3";
            // 
            // ClusteringOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 406);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.mapButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clusteringFindTextBox);
            this.Controls.Add(this.recommendTemplateButton);
            this.Controls.Add(this.exportAsCSVButton);
            this.Controls.Add(this.clustersDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClusteringOutput";
            this.Text = "Clustering Output";
            ((System.ComponentModel.ISupportInitialize)(this.clustersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView clustersDataGridView;
        private System.Windows.Forms.Button exportAsCSVButton;
        private System.Windows.Forms.Button recommendTemplateButton;
        private System.Windows.Forms.TextBox clusteringFindTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mapButton;
        private System.Windows.Forms.TextBox textBox1;
    }
}