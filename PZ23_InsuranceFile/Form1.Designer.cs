#nullable enable
namespace PZ23;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;
    public System.Windows.Forms.TextBox txtFullName = null!;
    public System.Windows.Forms.TextBox txtCity = null!;
    public System.Windows.Forms.TextBox txtStreet = null!;
    public System.Windows.Forms.TextBox txtHouse = null!;
    public System.Windows.Forms.TextBox txtApartment = null!;
    public System.Windows.Forms.ComboBox cmbGender = null!;
    public System.Windows.Forms.ComboBox cmbInsuranceType = null!;
    public System.Windows.Forms.NumericUpDown numTerm = null!;
    private System.Windows.Forms.Label lblError = null!;
    private System.Windows.Forms.Label lblFilePath = null!;
    private System.Windows.Forms.Button btnOk = null!;
    private System.Windows.Forms.Button btnCancel = null!;
    private System.Windows.Forms.GroupBox groupBox1 = null!;
    private System.Windows.Forms.GroupBox groupBox2 = null!;
    private System.Windows.Forms.GroupBox groupBox3 = null!;
    private System.Windows.Forms.Label label1 = null!;
    private System.Windows.Forms.Label label2 = null!;
    private System.Windows.Forms.Label label3 = null!;
    private System.Windows.Forms.Label label4 = null!;
    private System.Windows.Forms.Label label5 = null!;
    private System.Windows.Forms.Label label6 = null!;
    private System.Windows.Forms.Label label7 = null!;
    private System.Windows.Forms.Label label8 = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        groupBox1 = new GroupBox();
        label1 = new Label();
        txtFullName = new TextBox();
        label2 = new Label();
        cmbGender = new ComboBox();
        groupBox2 = new GroupBox();
        label3 = new Label();
        txtCity = new TextBox();
        label4 = new Label();
        txtStreet = new TextBox();
        label5 = new Label();
        txtHouse = new TextBox();
        label6 = new Label();
        txtApartment = new TextBox();
        groupBox3 = new GroupBox();
        label7 = new Label();
        numTerm = new NumericUpDown();
        label8 = new Label();
        cmbInsuranceType = new ComboBox();
        lblError = new Label();
        btnOk = new Button();
        btnCancel = new Button();
        lblFilePath = new Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numTerm).BeginInit();
        SuspendLayout();
        // groupBox1
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(txtFullName);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(cmbGender);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(480, 92);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Страхователь";
        // label1
        label1.AutoSize = true;
        label1.Location = new Point(15, 27);
        label1.Name = "label1";
        label1.Text = "Фамилия и имя";
        // txtFullName
        txtFullName.Location = new Point(130, 24);
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(330, 23);
        txtFullName.TabIndex = 1;
        // label2
        label2.AutoSize = true;
        label2.Location = new Point(15, 60);
        label2.Name = "label2";
        label2.Text = "Пол";
        // cmbGender
        cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbGender.Location = new Point(130, 56);
        cmbGender.Name = "cmbGender";
        cmbGender.Size = new Size(160, 23);
        cmbGender.TabIndex = 3;
        // groupBox2
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(txtCity);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(txtStreet);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(txtHouse);
        groupBox2.Controls.Add(label6);
        groupBox2.Controls.Add(txtApartment);
        groupBox2.Location = new Point(12, 112);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(480, 100);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Адрес";
        // label3
        label3.AutoSize = true;
        label3.Location = new Point(15, 27);
        label3.Name = "label3";
        label3.Text = "Город";
        // txtCity
        txtCity.Location = new Point(70, 24);
        txtCity.Name = "txtCity";
        txtCity.Size = new Size(180, 23);
        txtCity.TabIndex = 1;
        // label4
        label4.AutoSize = true;
        label4.Location = new Point(265, 27);
        label4.Name = "label4";
        label4.Text = "Улица";
        // txtStreet
        txtStreet.Location = new Point(315, 24);
        txtStreet.Name = "txtStreet";
        txtStreet.Size = new Size(145, 23);
        txtStreet.TabIndex = 3;
        // label5
        label5.AutoSize = true;
        label5.Location = new Point(15, 62);
        label5.Name = "label5";
        label5.Text = "Дом";
        // txtHouse
        txtHouse.Location = new Point(70, 59);
        txtHouse.Name = "txtHouse";
        txtHouse.Size = new Size(80, 23);
        txtHouse.TabIndex = 5;
        // label6
        label6.AutoSize = true;
        label6.Location = new Point(175, 62);
        label6.Name = "label6";
        label6.Text = "Квартира";
        // txtApartment
        txtApartment.Location = new Point(245, 59);
        txtApartment.Name = "txtApartment";
        txtApartment.Size = new Size(80, 23);
        txtApartment.TabIndex = 7;
        // groupBox3
        groupBox3.Controls.Add(label7);
        groupBox3.Controls.Add(numTerm);
        groupBox3.Controls.Add(label8);
        groupBox3.Controls.Add(cmbInsuranceType);
        groupBox3.Location = new Point(12, 220);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(480, 92);
        groupBox3.TabIndex = 2;
        groupBox3.TabStop = false;
        groupBox3.Text = "Договор страхования";
        // label7
        label7.AutoSize = true;
        label7.Location = new Point(15, 27);
        label7.Name = "label7";
        label7.Text = "Срок, месяцев";
        // numTerm
        numTerm.Location = new Point(130, 24);
        numTerm.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
        numTerm.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numTerm.Name = "numTerm";
        numTerm.Size = new Size(80, 23);
        numTerm.TabIndex = 1;
        numTerm.Value = new decimal(new int[] { 12, 0, 0, 0 });
        // label8
        label8.AutoSize = true;
        label8.Location = new Point(15, 62);
        label8.Name = "label8";
        label8.Text = "Вид страхования";
        // cmbInsuranceType
        cmbInsuranceType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbInsuranceType.Location = new Point(130, 58);
        cmbInsuranceType.Name = "cmbInsuranceType";
        cmbInsuranceType.Size = new Size(330, 23);
        cmbInsuranceType.TabIndex = 3;
        // lblError
        lblError.AutoSize = false;
        lblError.ForeColor = Color.Firebrick;
        lblError.Location = new Point(12, 324);
        lblError.Name = "lblError";
        lblError.Size = new Size(480, 20);
        lblError.Text = "";
        // btnOk
        btnOk.Location = new Point(250, 352);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(110, 36);
        btnOk.TabIndex = 3;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        // btnCancel
        btnCancel.Location = new Point(382, 352);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(110, 36);
        btnCancel.TabIndex = 4;
        btnCancel.Text = "Отмена";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // lblFilePath
        lblFilePath.AutoSize = false;
        lblFilePath.ForeColor = Color.Gray;
        lblFilePath.Location = new Point(12, 398);
        lblFilePath.Name = "lblFilePath";
        lblFilePath.Size = new Size(480, 20);
        lblFilePath.Text = "Файл:";
        // Form1
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(504, 430);
        Controls.Add(lblFilePath);
        Controls.Add(btnCancel);
        Controls.Add(btnOk);
        Controls.Add(lblError);
        Controls.Add(groupBox3);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ПЗ23 — страхователи";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numTerm).EndInit();
        ResumeLayout(false);
    }
}
