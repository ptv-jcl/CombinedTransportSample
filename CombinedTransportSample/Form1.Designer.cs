namespace CombinedTransportSample
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.map = new Ptv.XServer.Controls.Map.FormsMap();
            this.label1 = new System.Windows.Forms.Label();
            this.combinedTransportDepartureTextBox = new System.Windows.Forms.TextBox();
            this.combinedTransportArrivalTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchFerryButton = new System.Windows.Forms.Button();
            this.combinedTransportDataGridView = new System.Windows.Forms.DataGridView();
            this.fromCityTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toCityTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.calcualteRouteButton = new System.Windows.Forms.Button();
            this.resultCombinedTransportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destinationNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.truckBlockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hazardousBlockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.combustibleBlockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.combinedTransportDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultCombinedTransportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // map
            // 
            this.map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.map.Center = ((System.Windows.Point)(resources.GetObject("map.Center")));
            this.map.CoordinateDiplayFormat = Ptv.XServer.Controls.Map.CoordinateDiplayFormat.Degree;
            this.map.FitInWindow = false;
            this.map.InvertMouseWheel = false;
            this.map.Location = new System.Drawing.Point(671, 12);
            this.map.MaxZoom = 19;
            this.map.MinZoom = 0;
            this.map.MouseDoubleClickZoom = true;
            this.map.MouseDragMode = Ptv.XServer.Controls.Map.Gadgets.DragMode.SelectOnShift;
            this.map.MouseWheelSpeed = 0.5D;
            this.map.Name = "map";
            this.map.ShowCoordinates = true;
            this.map.ShowLayers = true;
            this.map.ShowMagnifier = true;
            this.map.ShowNavigation = true;
            this.map.ShowOverview = true;
            this.map.ShowScale = true;
            this.map.ShowZoomSlider = true;
            this.map.Size = new System.Drawing.Size(506, 405);
            this.map.TabIndex = 0;
            this.map.UseAnimation = true;
            this.map.UseDefaultTheme = true;
            this.map.UseMiles = false;
            this.map.XMapCopyright = "Please configure a valid copyright text!";
            this.map.XMapCredentials = "";
            this.map.XMapStyle = "";
            this.map.XMapUrl = "";
            this.map.ZoomLevel = 1D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Combined transport departure";
            // 
            // combinedTransportDepartureTextBox
            // 
            this.combinedTransportDepartureTextBox.Location = new System.Drawing.Point(168, 14);
            this.combinedTransportDepartureTextBox.Name = "combinedTransportDepartureTextBox";
            this.combinedTransportDepartureTextBox.Size = new System.Drawing.Size(263, 20);
            this.combinedTransportDepartureTextBox.TabIndex = 1;
            // 
            // combinedTransportArrivalTextBox
            // 
            this.combinedTransportArrivalTextBox.Location = new System.Drawing.Point(168, 43);
            this.combinedTransportArrivalTextBox.Name = "combinedTransportArrivalTextBox";
            this.combinedTransportArrivalTextBox.Size = new System.Drawing.Size(263, 20);
            this.combinedTransportArrivalTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Combined transport arrival";
            // 
            // searchFerryButton
            // 
            this.searchFerryButton.Location = new System.Drawing.Point(12, 69);
            this.searchFerryButton.Name = "searchFerryButton";
            this.searchFerryButton.Size = new System.Drawing.Size(419, 23);
            this.searchFerryButton.TabIndex = 3;
            this.searchFerryButton.Text = "Search combined transports";
            this.searchFerryButton.UseVisualStyleBackColor = true;
            this.searchFerryButton.Click += new System.EventHandler(this.searchFerryButton_Click);
            // 
            // combinedTransportDataGridView
            // 
            this.combinedTransportDataGridView.AutoGenerateColumns = false;
            this.combinedTransportDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.combinedTransportDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.combinedTransportDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.startNameDataGridViewTextBoxColumn,
            this.destinationNameDataGridViewTextBoxColumn,
            this.Time,
            this.truckBlockDataGridViewTextBoxColumn,
            this.hazardousBlockDataGridViewTextBoxColumn,
            this.combustibleBlockDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn5});
            this.combinedTransportDataGridView.DataSource = this.resultCombinedTransportBindingSource;
            this.combinedTransportDataGridView.Location = new System.Drawing.Point(12, 98);
            this.combinedTransportDataGridView.MultiSelect = false;
            this.combinedTransportDataGridView.Name = "combinedTransportDataGridView";
            this.combinedTransportDataGridView.ReadOnly = true;
            this.combinedTransportDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.combinedTransportDataGridView.Size = new System.Drawing.Size(653, 150);
            this.combinedTransportDataGridView.TabIndex = 4;
            // 
            // fromCityTextBox
            // 
            this.fromCityTextBox.Location = new System.Drawing.Point(168, 254);
            this.fromCityTextBox.Name = "fromCityTextBox";
            this.fromCityTextBox.Size = new System.Drawing.Size(263, 20);
            this.fromCityTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "From city";
            // 
            // toCityTextBox
            // 
            this.toCityTextBox.Location = new System.Drawing.Point(168, 280);
            this.toCityTextBox.Name = "toCityTextBox";
            this.toCityTextBox.Size = new System.Drawing.Size(263, 20);
            this.toCityTextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "To city";
            // 
            // calcualteRouteButton
            // 
            this.calcualteRouteButton.Location = new System.Drawing.Point(12, 306);
            this.calcualteRouteButton.Name = "calcualteRouteButton";
            this.calcualteRouteButton.Size = new System.Drawing.Size(419, 23);
            this.calcualteRouteButton.TabIndex = 7;
            this.calcualteRouteButton.Text = "Calculate route";
            this.calcualteRouteButton.UseVisualStyleBackColor = true;
            this.calcualteRouteButton.Click += new System.EventHandler(this.calcualteRouteButton_Click);
            // 
            // resultCombinedTransportBindingSource
            // 
            this.resultCombinedTransportBindingSource.DataSource = typeof(XServers.XLocate.ResultCombinedTransport);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn6.HeaderText = "name";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 58;
            // 
            // startNameDataGridViewTextBoxColumn
            // 
            this.startNameDataGridViewTextBoxColumn.DataPropertyName = "StartName";
            this.startNameDataGridViewTextBoxColumn.HeaderText = "StartName";
            this.startNameDataGridViewTextBoxColumn.Name = "startNameDataGridViewTextBoxColumn";
            this.startNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.startNameDataGridViewTextBoxColumn.Width = 82;
            // 
            // destinationNameDataGridViewTextBoxColumn
            // 
            this.destinationNameDataGridViewTextBoxColumn.DataPropertyName = "DestinationName";
            this.destinationNameDataGridViewTextBoxColumn.HeaderText = "DestinationName";
            this.destinationNameDataGridViewTextBoxColumn.Name = "destinationNameDataGridViewTextBoxColumn";
            this.destinationNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.destinationNameDataGridViewTextBoxColumn.Width = 113;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 55;
            // 
            // truckBlockDataGridViewTextBoxColumn
            // 
            this.truckBlockDataGridViewTextBoxColumn.DataPropertyName = "TruckBlock";
            this.truckBlockDataGridViewTextBoxColumn.HeaderText = "TruckBlock";
            this.truckBlockDataGridViewTextBoxColumn.Name = "truckBlockDataGridViewTextBoxColumn";
            this.truckBlockDataGridViewTextBoxColumn.ReadOnly = true;
            this.truckBlockDataGridViewTextBoxColumn.Width = 87;
            // 
            // hazardousBlockDataGridViewTextBoxColumn
            // 
            this.hazardousBlockDataGridViewTextBoxColumn.DataPropertyName = "HazardousBlock";
            this.hazardousBlockDataGridViewTextBoxColumn.HeaderText = "HazardousBlock";
            this.hazardousBlockDataGridViewTextBoxColumn.Name = "hazardousBlockDataGridViewTextBoxColumn";
            this.hazardousBlockDataGridViewTextBoxColumn.ReadOnly = true;
            this.hazardousBlockDataGridViewTextBoxColumn.Width = 110;
            // 
            // combustibleBlockDataGridViewTextBoxColumn
            // 
            this.combustibleBlockDataGridViewTextBoxColumn.DataPropertyName = "CombustibleBlock";
            this.combustibleBlockDataGridViewTextBoxColumn.HeaderText = "CombustibleBlock";
            this.combustibleBlockDataGridViewTextBoxColumn.Name = "combustibleBlockDataGridViewTextBoxColumn";
            this.combustibleBlockDataGridViewTextBoxColumn.ReadOnly = true;
            this.combustibleBlockDataGridViewTextBoxColumn.Width = 116;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "type";
            this.dataGridViewTextBoxColumn7.HeaderText = "type";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 52;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn5.HeaderText = "id";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 40;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 429);
            this.Controls.Add(this.calcualteRouteButton);
            this.Controls.Add(this.toCityTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fromCityTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.combinedTransportDataGridView);
            this.Controls.Add(this.combinedTransportArrivalTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.searchFerryButton);
            this.Controls.Add(this.combinedTransportDepartureTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.map);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.combinedTransportDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultCombinedTransportBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ptv.XServer.Controls.Map.FormsMap map;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox combinedTransportDepartureTextBox;
        private System.Windows.Forms.TextBox combinedTransportArrivalTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button searchFerryButton;
        private System.Windows.Forms.DataGridView combinedTransportDataGridView;
        private System.Windows.Forms.TextBox fromCityTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox toCityTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button calcualteRouteButton;
        private System.Windows.Forms.BindingSource resultCombinedTransportBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn startNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destinationNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn truckBlockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hazardousBlockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn combustibleBlockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}

