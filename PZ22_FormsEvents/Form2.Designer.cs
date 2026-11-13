#nullable enable
namespace PZ22;

partial class Form2
{
    private System.ComponentModel.IContainer? components = null;
    private System.Windows.Forms.Label lblFio = null!;
    private System.Windows.Forms.Label lblBirth = null!;
    private System.Windows.Forms.Label lblGender = null!;
    private System.Windows.Forms.Label lblAddress = null!;
    private System.Windows.Forms.Label lblSubscribed = null!;
    private System.Windows.Forms.Label lblNoteCaption = null!;
    private System.Windows.Forms.TextBox txtNote = null!;
    private System.Windows.Forms.PictureBox picPhoto = null!;
    private System.Windows.Forms.Button btnRefresh = null!;
    private System.Windows.Forms.Button btnWriteBack = null!;
    private System.Windows.Forms.Button btnClose = null!;
    private System.Windows.Forms.GroupBox groupBox1 = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        groupBox1 = new GroupBox();
        lblFio = new Label();
        lblBirth = new Label();
        lblGender = new Label();
        lblAddress = new Label();
        lblSubscribed = new Label();
        lblNoteCaption = new Label();
        txtNote = new TextBox();
        picPhoto = new PictureBox();
        btnRefresh = new Button();
        btnWriteBack = new Button();
        btnClose = new Button();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picPhoto).BeginInit();
        SuspendLayout();
        // groupBox1
        groupBox1.Controls.Add(lblFio);
        groupBox1.Controls.Add(lblBirth);
        groupBox1.Controls.Add(lblGender);
        groupBox1.Controls.Add(lblAddress);
        groupBox1.Controls.Add(lblSubscribed);
        groupBox1.Controls.Add(lblNoteCaption);
        groupBox1.Controls.Add(txtNote);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(360, 330);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Данные из первой формы";
        // lblFio
        lblFio.AutoSize = false;
        lblFio.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblFio.Location = new Point(15, 25);
        lblFio.Name = "lblFio";
        lblFio.Size = new Size(330, 26);
        lblFio.Text = "Фамилия Имя";
        // lblBirth
        lblBirth.AutoSize = false;
        lblBirth.Location = new Point(15, 60);
        lblBirth.Name = "lblBirth";
        lblBirth.Size = new Size(330, 20);
        lblBirth.Text = "Дата рождения:";
        // lblGender
        lblGender.AutoSize = false;
        lblGender.Location = new Point(15, 84);
        lblGender.Name = "lblGender";
        lblGender.Size = new Size(330, 20);
        lblGender.Text = "Пол:";
        // lblAddress
        lblAddress.AutoSize = false;
        lblAddress.Location = new Point(15, 108);
        lblAddress.Name = "lblAddress";
        lblAddress.Size = new Size(330, 40);
        lblAddress.Text = "Адрес:";
        // lblSubscribed
        lblSubscribed.AutoSize = false;
        lblSubscribed.Location = new Point(15, 152);
        lblSubscribed.Name = "lblSubscribed";
        lblSubscribed.Size = new Size(330, 20);
        lblSubscribed.Text = "Согласие на обработку:";
        // lblNoteCaption
        lblNoteCaption.AutoSize = false;
        lblNoteCaption.Location = new Point(15, 180);
        lblNoteCaption.Name = "lblNoteCaption";
        lblNoteCaption.Size = new Size(330, 20);
        lblNoteCaption.Text = "Примечание:";
        // txtNote
        txtNote.Location = new Point(15, 202);
        txtNote.Multiline = true;
        txtNote.Name = "txtNote";
        txtNote.ScrollBars = ScrollBars.Vertical;
        txtNote.Size = new Size(330, 112);
        txtNote.TabIndex = 6;
        // picPhoto
        picPhoto.BorderStyle = BorderStyle.FixedSingle;
        picPhoto.Location = new Point(390, 12);
        picPhoto.Name = "picPhoto";
        picPhoto.Size = new Size(160, 180);
        picPhoto.SizeMode = PictureBoxSizeMode.Zoom;
        picPhoto.TabIndex = 1;
        picPhoto.TabStop = false;
        // btnRefresh
        btnRefresh.Location = new Point(390, 204);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(160, 36);
        btnRefresh.TabIndex = 2;
        btnRefresh.Text = "Обновить из формы 1";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        // btnWriteBack
        btnWriteBack.Location = new Point(390, 246);
        btnWriteBack.Name = "btnWriteBack";
        btnWriteBack.Size = new Size(160, 36);
        btnWriteBack.TabIndex = 3;
        btnWriteBack.Text = "Записать в форму 1";
        btnWriteBack.UseVisualStyleBackColor = true;
        btnWriteBack.Click += btnWriteBack_Click;
        // btnClose
        btnClose.Location = new Point(390, 306);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(160, 36);
        btnClose.TabIndex = 4;
        btnClose.Text = "Закрыть";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        // Form2
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(564, 356);
        Controls.Add(btnClose);
        Controls.Add(btnWriteBack);
        Controls.Add(btnRefresh);
        Controls.Add(picPhoto);
        Controls.Add(groupBox1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Form2";
        StartPosition = FormStartPosition.CenterParent;
        Text = "ПЗ22 — данные (форма 2)";
        Shown += Form2_Shown;
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picPhoto).EndInit();
        ResumeLayout(false);
    }
}
