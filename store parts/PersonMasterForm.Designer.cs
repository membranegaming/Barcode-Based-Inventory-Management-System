namespace store_parts
{
    partial class PersonMasterForm
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
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.Label person_nameLabel;
            System.Windows.Forms.Label employee_idLabel;
            System.Windows.Forms.Label departmentLabel;
            System.Windows.Forms.Label contact_numberLabel;
            this.mainDB = new store_parts.MainDB();
            this.personMasterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.personMasterTableAdapter = new store_parts.MainDBTableAdapters.PersonMasterTableAdapter();
            this.tableAdapterManager = new store_parts.MainDBTableAdapters.TableAdapterManager();
            this.personMasterDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.person_nameTextBox = new System.Windows.Forms.TextBox();
            this.employee_idTextBox = new System.Windows.Forms.TextBox();
            this.departmentTextBox = new System.Windows.Forms.TextBox();
            this.contact_numberTextBox = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecordCount = new System.Windows.Forms.Label();
            idLabel = new System.Windows.Forms.Label();
            person_nameLabel = new System.Windows.Forms.Label();
            employee_idLabel = new System.Windows.Forms.Label();
            departmentLabel = new System.Windows.Forms.Label();
            contact_numberLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.personMasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.personMasterDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(30, 30);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(21, 16);
            idLabel.TabIndex = 1;
            idLabel.Text = "ID:";
            // 
            // person_nameLabel
            // 
            person_nameLabel.AutoSize = true;
            person_nameLabel.Location = new System.Drawing.Point(30, 60);
            person_nameLabel.Name = "person_nameLabel";
            person_nameLabel.Size = new System.Drawing.Size(92, 16);
            person_nameLabel.TabIndex = 3;
            person_nameLabel.Text = "Person Name:*";
            // 
            // employee_idLabel
            // 
            employee_idLabel.AutoSize = true;
            employee_idLabel.Location = new System.Drawing.Point(30, 90);
            employee_idLabel.Name = "employee_idLabel";
            employee_idLabel.Size = new System.Drawing.Size(88, 16);
            employee_idLabel.TabIndex = 5;
            employee_idLabel.Text = "Employee ID:";
            // 
            // departmentLabel
            // 
            departmentLabel.AutoSize = true;
            departmentLabel.Location = new System.Drawing.Point(30, 120);
            departmentLabel.Name = "departmentLabel";
            departmentLabel.Size = new System.Drawing.Size(77, 16);
            departmentLabel.TabIndex = 7;
            departmentLabel.Text = "Department:";
            // 
            // contact_numberLabel
            // 
            contact_numberLabel.AutoSize = true;
            contact_numberLabel.Location = new System.Drawing.Point(30, 150);
            contact_numberLabel.Name = "contact_numberLabel";
            contact_numberLabel.Size = new System.Drawing.Size(105, 16);
            contact_numberLabel.TabIndex = 9;
            contact_numberLabel.Text = "Contact Number:";
            // 
            // mainDB
            // 
            this.mainDB.DataSetName = "MainDB";
            this.mainDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // personMasterBindingSource
            // 
            this.personMasterBindingSource.DataMember = "PersonMaster";
            this.personMasterBindingSource.DataSource = this.mainDB;
            // 
            // personMasterTableAdapter
            // 
            this.personMasterTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.PersonMasterTableAdapter = this.personMasterTableAdapter;
            this.tableAdapterManager.UpdateOrder = store_parts.MainDBTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // personMasterDataGridView
            // 
            this.personMasterDataGridView.AutoGenerateColumns = false;
            this.personMasterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.personMasterDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn1});
            this.personMasterDataGridView.DataSource = this.personMasterBindingSource;
            this.personMasterDataGridView.Location = new System.Drawing.Point(30, 220);
            this.personMasterDataGridView.Name = "personMasterDataGridView";
            this.personMasterDataGridView.RowHeadersWidth = 51;
            this.personMasterDataGridView.RowTemplate.Height = 24;
            this.personMasterDataGridView.Size = new System.Drawing.Size(740, 250);
            this.personMasterDataGridView.TabIndex = 13;
            this.personMasterDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.personMasterDataGridView_CellDoubleClick);
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
            this.dataGridViewTextBoxColumn2.DataPropertyName = "person_name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Person Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "employee_id";
            this.dataGridViewTextBoxColumn3.HeaderText = "Employee ID";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "department";
            this.dataGridViewTextBoxColumn4.HeaderText = "Department";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 130;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "contact_number";
            this.dataGridViewTextBoxColumn5.HeaderText = "Contact Number";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 130;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "is_active";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn1.MinimumWidth = 6;
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 70;
            // 
            // idTextBox
            // 
            this.idTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.personMasterBindingSource, "id", true));
            this.idTextBox.Location = new System.Drawing.Point(145, 27);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.idTextBox.Size = new System.Drawing.Size(200, 22);
            this.idTextBox.TabIndex = 2;
            // 
            // person_nameTextBox
            // 
            this.person_nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.personMasterBindingSource, "person_name", true));
            this.person_nameTextBox.Location = new System.Drawing.Point(145, 57);
            this.person_nameTextBox.Name = "person_nameTextBox";
            this.person_nameTextBox.Size = new System.Drawing.Size(350, 22);
            this.person_nameTextBox.TabIndex = 4;
            // 
            // employee_idTextBox
            // 
            this.employee_idTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.personMasterBindingSource, "employee_id", true));
            this.employee_idTextBox.Location = new System.Drawing.Point(145, 87);
            this.employee_idTextBox.Name = "employee_idTextBox";
            this.employee_idTextBox.Size = new System.Drawing.Size(200, 22);
            this.employee_idTextBox.TabIndex = 6;
            // 
            // departmentTextBox
            // 
            this.departmentTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.personMasterBindingSource, "department", true));
            this.departmentTextBox.Location = new System.Drawing.Point(145, 117);
            this.departmentTextBox.Name = "departmentTextBox";
            this.departmentTextBox.Size = new System.Drawing.Size(200, 22);
            this.departmentTextBox.TabIndex = 8;
            // 
            // contact_numberTextBox
            // 
            this.contact_numberTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.personMasterBindingSource, "contact_number", true));
            this.contact_numberTextBox.Location = new System.Drawing.Point(145, 147);
            this.contact_numberTextBox.Name = "contact_numberTextBox";
            this.contact_numberTextBox.Size = new System.Drawing.Size(200, 22);
            this.contact_numberTextBox.TabIndex = 10;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(510, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 30);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(650, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(510, 70);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 30);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(650, 70);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 30);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(510, 110);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(650, 110);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.Location = new System.Drawing.Point(30, 480);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(123, 18);
            this.lblRecordCount.TabIndex = 20;
            this.lblRecordCount.Text = "Total Persons: 0";
            // 
            // PersonMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.personMasterDataGridView);
            this.Controls.Add(idLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(person_nameLabel);
            this.Controls.Add(this.person_nameTextBox);
            this.Controls.Add(employee_idLabel);
            this.Controls.Add(this.employee_idTextBox);
            this.Controls.Add(departmentLabel);
            this.Controls.Add(this.departmentTextBox);
            this.Controls.Add(contact_numberLabel);
            this.Controls.Add(this.contact_numberTextBox);
            this.Name = "PersonMasterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Person Master - Manage Personnel";
            this.Load += new System.EventHandler(this.PersonMasterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.personMasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.personMasterDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MainDB mainDB;
        private System.Windows.Forms.BindingSource personMasterBindingSource;
        private MainDBTableAdapters.PersonMasterTableAdapter personMasterTableAdapter;
        private MainDBTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView personMasterDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox person_nameTextBox;
        private System.Windows.Forms.TextBox employee_idTextBox;
        private System.Windows.Forms.TextBox departmentTextBox;
        private System.Windows.Forms.TextBox contact_numberTextBox;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordCount;
    }
}
