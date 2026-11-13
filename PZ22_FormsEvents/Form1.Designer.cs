#nullable enable
namespace PZ22;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    public System.Windows.Forms.TextBox txtLastName = null!;
    public System.Windows.Forms.TextBox txtFirstName = null!;
    public System.Windows.Forms.TextBox txtAddress = null!;
    public System.Windows.Forms.TextBox txtNote = null!;
    public System.Windows.Forms.DateTimePicker dtpBirthDate = null!;
    public System.Windows.Forms.ComboBox cmbGender = null!;
    public System.Windows.Forms.CheckBox chkSubscribed = null!;
    public System.Windows.Forms.PictureBox picPhoto = null!;
    public System.Windows.Forms.Label lblPhoto = null!;
    private System.Windows.Forms.GroupBox groupBox1 = null!;
    private System.Windows.Forms.GroupBox groupBox2 = null!;
    private System.Windows.Forms.GroupBox groupBox3 = null!;
    private System.Windows.Forms.Label label1 = null!;
    private System.Windows.Forms.Label label2 = null!;
    private System.Windows.Forms.Label label3 = null!;
    private System.Windows.Forms.Label label4 = null!;
    private System.Windows.Forms.Label label5 = null!;
    private System.Windows.Forms.Label label6 = null!;
    private System.Windows.Forms.Button btnAddPhoto = null!;
    private System.Windows.Forms.Button btnShow = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        groupBox1 = new GroupBox();
        label1 = new Label();
        txtLastName = new TextBox();
        label2 = new Label();
        txtFirstName = new TextBox();
        groupBox2 = new GroupBox();
        label3 = new Label();
        txtAddress = new TextBox();
        groupBox3 = new GroupBox();
        label4 = new Label();
        dtpBirthDate = new DateTimePicker();
        label5 = new Label();
        cmbGender = new ComboBox();
        chkSubscribed = new CheckBox();
        label6 = new Label();
        txtNote = new TextBox();
        picPhoto = new PictureBox();
        btnAddPhoto = new Button();
        btnShow = new Button();
        lblPhoto = new Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picPhoto).BeginInit();
        SuspendLayout();
        // groupBox1
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(txtLastName);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(txtFirstName);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(300, 108);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Личные данные";
        // label1
        label1.AutoSize = true;
        label1.Location = new Point(15, 27);
        label1.Name = "label1";
        label1.Text = "Фамилия";
        // txtLastName
        txtLastName.Location = new Point(95, 24);
        txtLastName.Name = "txtLastName";
        txtLastName.Size = new Size(190, 23);
        txtLastName.TabIndex = 1;
        // label2
        label2.AutoSize = true;
        label2.Location = new Point(15, 59);
        label2.Name = "label2";
        label2.Text = "Имя";
        // txtFirstName
        txtFirstName.Location = new Point(95, 56);
        txtFirstName.Name = "txtFirstName";
        txtFirstName.Size = new Size(190, 23);
        txtFirstName.TabIndex = 3;
        // groupBox2
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(txtAddress);
        groupBox2.Location = new Point(12, 128);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(300, 78);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Адрес";
        // label3
        label3.AutoSize = true;
        label3.Location = new Point(15, 34);
        label3.Name = "label3";
        label3.Text = "Адрес";
        // txtAddress
        txtAddress.Location = new Point(70, 31);
        txtAddress.Name = "txtAddress";
        txtAddress.Size = new Size(215, 23);
        txtAddress.TabIndex = 1;
        // groupBox3
        groupBox3.Controls.Add(label4);
        groupBox3.Controls.Add(dtpBirthDate);
        groupBox3.Controls.Add(label5);
        groupBox3.Controls.Add(cmbGender);
        groupBox3.Controls.Add(chkSubscribed);
        groupBox3.Controls.Add(label6);
        groupBox3.Controls.Add(txtNote);
        groupBox3.Location = new Point(12, 214);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(300, 188);
        groupBox3.TabIndex = 2;
        groupBox3.TabStop = false;
        groupBox3.Text = "Дополнительные сведения";
        // label4
        label4.AutoSize = true;
        label4.Location = new Point(15, 28);
        label4.Name = "label4";
        label4.Text = "Дата рождения";
        // dtpBirthDate
        dtpBirthDate.Format = DateTimePickerFormat.Long;
        dtpBirthDate.Location = new Point(130, 24);
        dtpBirthDate.Name = "dtpBirthDate";
        dtpBirthDate.Size = new Size(155, 23);
        dtpBirthDate.TabIndex = 1;
        dtpBirthDate.Value = new DateTime(2000, 1, 1);
        // label5
        label5.AutoSize = true;
        label5.Location = new Point(15, 62);
        label5.Name = "label5";
        label5.Text = "Пол";
        // cmbGender
        cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbGender.Location = new Point(130, 58);
        cmbGender.Name = "cmbGender";
        cmbGender.Size = new Size(155, 23);
        cmbGender.TabIndex = 3;
        // chkSubscribed
        chkSubscribed.AutoSize = true;
        chkSubscribed.Location = new Point(15, 92);
        chkSubscribed.Name = "chkSubscribed";
        chkSubscribed.Size = new Size(230, 19);
        chkSubscribed.TabIndex = 4;
        chkSubscribed.Text = "Согласен на обработку данных";
        // label6
        label6.AutoSize = true;
        label6.Location = new Point(15, 121);
        label6.Name = "label6";
        label6.Text = "Примечание";
        // txtNote
        txtNote.Location = new Point(15, 143);
        txtNote.Multiline = true;
        txtNote.Name = "txtNote";
        txtNote.ScrollBars = ScrollBars.Vertical;
        txtNote.Size = new Size(270, 34);
        txtNote.TabIndex = 6;
        // picPhoto
        picPhoto.BorderStyle = BorderStyle.FixedSingle;
        picPhoto.Location = new Point(325, 12);
        picPhoto.Name = "picPhoto";
        picPhoto.Size = new Size(200, 200);
        picPhoto.SizeMode = PictureBoxSizeMode.Zoom;
        picPhoto.TabIndex = 3;
        picPhoto.TabStop = false;
        // btnAddPhoto
        btnAddPhoto.Location = new Point(325, 220);
        btnAddPhoto.Name = "btnAddPhoto";
        btnAddPhoto.Size = new Size(200, 38);
        btnAddPhoto.TabIndex = 4;
        btnAddPhoto.Text = "Добавить фото";
        btnAddPhoto.UseVisualStyleBackColor = true;
        btnAddPhoto.Click += btnAddPhoto_Click;
        // btnShow
        btnShow.Location = new Point(325, 266);
        btnShow.Name = "btnShow";
        btnShow.Size = new Size(200, 38);
        btnShow.TabIndex = 5;
        btnShow.Text = "Показать данные";
        btnShow.UseVisualStyleBackColor = true;
        btnShow.Click += btnShow_Click;
        // lblPhoto
        lblPhoto.AutoSize = false;
        lblPhoto.Location = new Point(325, 312);
        lblPhoto.Name = "lblPhoto";
        lblPhoto.Size = new Size(200, 20);
        lblPhoto.Text = "Фото не выбрано";
        lblPhoto.ForeColor = Color.Gray;
        // Form1
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(540, 416);
        Controls.Add(lblPhoto);
        Controls.Add(btnShow);
        Controls.Add(btnAddPhoto);
        Controls.Add(picPhoto);
        Controls.Add(groupBox3);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ПЗ22 — анкета (форма 1)";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picPhoto).EndInit();
        ResumeLayout(false);
    }
}
