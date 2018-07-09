namespace RBACS
{
    partial class QueryDetailsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryDetailsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.exportQueryAsFileButton = new System.Windows.Forms.Button();
            this.nearestNeighboursButton = new System.Windows.Forms.Button();
            this.nNDataGridView = new System.Windows.Forms.DataGridView();
            this.kNNAsCSVButton = new System.Windows.Forms.Button();
            this.kNNAsFileButton = new System.Windows.Forms.Button();
            this.kTextBox = new System.Windows.Forms.TextBox();
            this.recommendTemplateButton = new System.Windows.Forms.Button();
            this.titlingRichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupsDataGridView = new System.Windows.Forms.DataGridView();
            this.iKNNResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.queryDetailsFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nNDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iKNNResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryDetailsFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Summary:";
            // 
            // exportQueryAsFileButton
            // 
            this.exportQueryAsFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportQueryAsFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportQueryAsFileButton.Location = new System.Drawing.Point(15, 380);
            this.exportQueryAsFileButton.Name = "exportQueryAsFileButton";
            this.exportQueryAsFileButton.Size = new System.Drawing.Size(98, 23);
            this.exportQueryAsFileButton.TabIndex = 2;
            this.exportQueryAsFileButton.Text = "Export As File";
            this.exportQueryAsFileButton.UseVisualStyleBackColor = true;
            this.exportQueryAsFileButton.Click += new System.EventHandler(this.exportQueryAsFileButton_Click);
            // 
            // nearestNeighboursButton
            // 
            this.nearestNeighboursButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nearestNeighboursButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nearestNeighboursButton.Location = new System.Drawing.Point(116, 380);
            this.nearestNeighboursButton.Name = "nearestNeighboursButton";
            this.nearestNeighboursButton.Size = new System.Drawing.Size(151, 23);
            this.nearestNeighboursButton.TabIndex = 3;
            this.nearestNeighboursButton.Text = "Nearest Neighbours";
            this.nearestNeighboursButton.UseVisualStyleBackColor = true;
            this.nearestNeighboursButton.Click += new System.EventHandler(this.nearestNeighboursButton_Click);
            // 
            // nNDataGridView
            // 
            this.nNDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nNDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.nNDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.nNDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nNDataGridView.DataSource = this.iKNNResultBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.nNDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.nNDataGridView.Location = new System.Drawing.Point(377, 28);
            this.nNDataGridView.Name = "nNDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.nNDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.nNDataGridView.Size = new System.Drawing.Size(433, 346);
            this.nNDataGridView.TabIndex = 4;
            this.nNDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.nNDataGridView_CellContentDoubleClick);
            this.nNDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.nNDataGridView_ColumnHeaderMouseClick);
            // 
            // kNNAsCSVButton
            // 
            this.kNNAsCSVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kNNAsCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kNNAsCSVButton.Location = new System.Drawing.Point(377, 381);
            this.kNNAsCSVButton.Name = "kNNAsCSVButton";
            this.kNNAsCSVButton.Size = new System.Drawing.Size(139, 24);
            this.kNNAsCSVButton.TabIndex = 5;
            this.kNNAsCSVButton.Text = "Export As CSV";
            this.kNNAsCSVButton.UseVisualStyleBackColor = true;
            this.kNNAsCSVButton.Click += new System.EventHandler(this.kNNAsCSVButton_Click);
            // 
            // kNNAsFileButton
            // 
            this.kNNAsFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kNNAsFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kNNAsFileButton.Location = new System.Drawing.Point(522, 381);
            this.kNNAsFileButton.Name = "kNNAsFileButton";
            this.kNNAsFileButton.Size = new System.Drawing.Size(139, 24);
            this.kNNAsFileButton.TabIndex = 6;
            this.kNNAsFileButton.Text = "Export As File";
            this.kNNAsFileButton.UseVisualStyleBackColor = true;
            this.kNNAsFileButton.Click += new System.EventHandler(this.kNNAsFileButton_Click);
            // 
            // kTextBox
            // 
            this.kTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kTextBox.Location = new System.Drawing.Point(273, 381);
            this.kTextBox.Name = "kTextBox";
            this.kTextBox.Size = new System.Drawing.Size(71, 22);
            this.kTextBox.TabIndex = 7;
            this.kTextBox.Text = "All";
            // 
            // recommendTemplateButton
            // 
            this.recommendTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recommendTemplateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recommendTemplateButton.Location = new System.Drawing.Point(667, 382);
            this.recommendTemplateButton.Name = "recommendTemplateButton";
            this.recommendTemplateButton.Size = new System.Drawing.Size(143, 23);
            this.recommendTemplateButton.TabIndex = 8;
            this.recommendTemplateButton.Text = "Recommend Template";
            this.recommendTemplateButton.UseVisualStyleBackColor = true;
            this.recommendTemplateButton.Click += new System.EventHandler(this.recommendTemplateButton_Click);
            // 
            // titlingRichTextBox
            // 
            this.titlingRichTextBox.Location = new System.Drawing.Point(13, 28);
            this.titlingRichTextBox.Name = "titlingRichTextBox";
            this.titlingRichTextBox.Size = new System.Drawing.Size(341, 53);
            this.titlingRichTextBox.TabIndex = 9;
            this.titlingRichTextBox.Text = "";
            // 
            // groupsDataGridView
            // 
            this.groupsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupsDataGridView.Location = new System.Drawing.Point(15, 87);
            this.groupsDataGridView.Name = "groupsDataGridView";
            this.groupsDataGridView.Size = new System.Drawing.Size(339, 287);
            this.groupsDataGridView.TabIndex = 10;
            this.groupsDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.groupsDataGridView_ColumnHeaderMouseClick);
            // 
            // iKNNResultBindingSource
            // 
            this.iKNNResultBindingSource.DataSource = typeof(RBACS.iKNNResult);
            // 
            // queryDetailsFormBindingSource
            // 
            this.queryDetailsFormBindingSource.DataSource = typeof(RBACS.QueryDetailsForm);
            // 
            // QueryDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(865, 416);
            this.Controls.Add(this.groupsDataGridView);
            this.Controls.Add(this.titlingRichTextBox);
            this.Controls.Add(this.recommendTemplateButton);
            this.Controls.Add(this.kTextBox);
            this.Controls.Add(this.kNNAsFileButton);
            this.Controls.Add(this.kNNAsCSVButton);
            this.Controls.Add(this.nNDataGridView);
            this.Controls.Add(this.nearestNeighboursButton);
            this.Controls.Add(this.exportQueryAsFileButton);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryDetailsForm";
            this.Text = "Query Details";
            this.Load += new System.EventHandler(this.QueryDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nNDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iKNNResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryDetailsFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exportQueryAsFileButton;
        private System.Windows.Forms.Button nearestNeighboursButton;
        private System.Windows.Forms.DataGridView nNDataGridView;
        private System.Windows.Forms.Button kNNAsCSVButton;
        private System.Windows.Forms.Button kNNAsFileButton;
        private System.Windows.Forms.TextBox kTextBox;
        private System.Windows.Forms.BindingSource iKNNResultBindingSource;
        private System.Windows.Forms.BindingSource queryDetailsFormBindingSource;
        private System.Windows.Forms.Button recommendTemplateButton;
        private System.Windows.Forms.RichTextBox titlingRichTextBox;
        private System.Windows.Forms.DataGridView groupsDataGridView;
    }
}