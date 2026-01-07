namespace store_parts
{
    partial class PartyMasterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.idLabel = new System.Windows.Forms.Label();
            this.party_nameLabel = new System.Windows.Forms.Label();
            this.contact_personLabel = new System.Windows.Forms.Label();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.mainDB = new store_parts.MainDB();
            this.partyMasterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.partyMasterTableAdapter = new store_parts.MainDBTableAdapters.PartyMasterTableAdapter();
            this.tableAdapterManager = new store_parts.MainDBTableAdapters.TableAdapterManager();
            this.partyMasterDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.party_nameTextBox = new System.Windows.Forms.TextBox();
            this.contact_personTextBox = new System.Windows.Forms.TextBox();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecordCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partyMasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partyMasterDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(30, 30);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(23, 16);
            this.idLabel.TabIndex = 1;
            this.idLabel.Text = "ID:";
            // 
            // party_nameLabel
            // 
            this.party_nameLabel.AutoSize = true;
            this.party_nameLabel.Location = new System.Drawing.Point(30, 60);
            this.party_nameLabel.Name = "party_nameLabel";
            this.party_nameLabel.Size = new System.Drawing.Size(86, 16);
            this.party_nameLabel.TabIndex = 3;
            this.party_nameLabel.Text = "Party Name:*";
            // 
            // contact_personLabel
            // 
            this.contact_personLabel.AutoSize = true;
            this.contact_personLabel.Location = new System.Drawing.Point(30, 90);
            this.contact_personLabel.Name = "contact_personLabel";
            this.contact_personLabel.Size = new System.Drawing.Size(101, 16);
            this.contact_personLabel.TabIndex = 5;
            this.contact_personLabel.Text = "Contact Person:";
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(30, 120);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(49, 16);
            this.phoneLabel.TabIndex = 7;
            this.phoneLabel.Text = "Phone:";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(30, 150);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(61, 16);
            this.addressLabel.TabIndex = 9;
            this.addressLabel.Text = "Address:";
            // 
            // mainDB
            // 
            this.mainDB.DataSetName = "MainDB";
            this.mainDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // partyMasterBindingSource
            // 
            this.partyMasterBindingSource.DataMember = "PartyMaster";
            this.partyMasterBindingSource.DataSource = this.mainDB;
            // 
            // partyMasterTableAdapter
            // 
            this.partyMasterTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ItemMasterTableAdapter = null;
            this.tableAdapterManager.MachineMasterTableAdapter = null;
            this.tableAdapterManager.machinery_item_inward_unit2TableAdapter = null;
            this.tableAdapterManager.PartyMasterTableAdapter = this.partyMasterTableAdapter;
            this.tableAdapterManager.PersonMasterTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = store_parts.MainDBTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // partyMasterDataGridView
            // 
            this.partyMasterDataGridView.AutoGenerateColumns = false;
            this.partyMasterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.partyMasterDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn1});
            this.partyMasterDataGridView.DataSource = this.partyMasterBindingSource;
            this.partyMasterDataGridView.Location = new System.Drawing.Point(30, 300);
            this.partyMasterDataGridView.Name = "partyMasterDataGridView";
            this.partyMasterDataGridView.RowHeadersWidth = 51;
            this.partyMasterDataGridView.RowTemplate.Height = 24;
            this.partyMasterDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.partyMasterDataGridView.Size = new System.Drawing.Size(740, 250);
            this.partyMasterDataGridView.TabIndex = 13;
            this.partyMasterDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.partyMasterDataGridView_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "party_name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Party Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "contact_person";
            this.dataGridViewTextBoxColumn3.HeaderText = "Contact Person";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "phone";
            this.dataGridViewTextBoxColumn4.HeaderText = "Phone";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "address";
            this.dataGridViewTextBoxColumn5.HeaderText = "Address";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "is_active";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn1.MinimumWidth = 6;
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 60;
            // 
            // idTextBox
            // 
            this.idTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.idTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.partyMasterBindingSource, "id", true));
            this.idTextBox.Location = new System.Drawing.Point(160, 27);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(200, 22);
            this.idTextBox.TabIndex = 2;
            this.idTextBox.TabStop = false;
            // 
            // party_nameTextBox
            // 
            this.party_nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.partyMasterBindingSource, "party_name", true));
            this.party_nameTextBox.Location = new System.Drawing.Point(160, 57);
            this.party_nameTextBox.Name = "party_nameTextBox";
            this.party_nameTextBox.Size = new System.Drawing.Size(300, 22);
            this.party_nameTextBox.TabIndex = 4;
            // 
            // contact_personTextBox
            // 
            this.contact_personTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.partyMasterBindingSource, "contact_person", true));
            this.contact_personTextBox.Location = new System.Drawing.Point(160, 87);
            this.contact_personTextBox.Name = "contact_personTextBox";
            this.contact_personTextBox.Size = new System.Drawing.Size(300, 22);
            this.contact_personTextBox.TabIndex = 6;
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.partyMasterBindingSource, "phone", true));
            this.phoneTextBox.Location = new System.Drawing.Point(160, 117);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(200, 22);
            this.phoneTextBox.TabIndex = 8;
            // 
            // addressTextBox
            // 
            this.addressTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.partyMasterBindingSource, "address", true));
            this.addressTextBox.Location = new System.Drawing.Point(160, 147);
            this.addressTextBox.Multiline = true;
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(400, 60);
            this.addressTextBox.TabIndex = 10;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(30, 260);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(140, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(250, 260);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(360, 260);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(560, 260);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(670, 260);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.Location = new System.Drawing.Point(30, 560);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(123, 18);
            this.lblRecordCount.TabIndex = 20;
            this.lblRecordCount.Text = "Total Parties: 0";
            // 
            // PartyMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 590);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.partyMasterDataGridView);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.party_nameLabel);
            this.Controls.Add(this.party_nameTextBox);
            this.Controls.Add(this.contact_personLabel);
            this.Controls.Add(this.contact_personTextBox);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.addressTextBox);
            this.Name = "PartyMasterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Party Master - Manage Parties";
            this.Load += new System.EventHandler(this.PartyMasterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partyMasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partyMasterDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MainDB mainDB;
        private System.Windows.Forms.BindingSource partyMasterBindingSource;
        private MainDBTableAdapters.PartyMasterTableAdapter partyMasterTableAdapter;
        private MainDBTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView partyMasterDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox party_nameTextBox;
        private System.Windows.Forms.TextBox contact_personTextBox;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label party_nameLabel;
        private System.Windows.Forms.Label contact_personLabel;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label addressLabel;
    }
}
