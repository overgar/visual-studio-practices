using System.Drawing;
using System.Drawing.Printing;

namespace PZ27;

public sealed class MainForm : Form
{
    private readonly RichTextBox _editor = new();
    private readonly MenuStrip _menu = new();
    private readonly ToolStrip _toolbar = new();
    private readonly StatusStrip _status = new();
    private readonly ToolStripStatusLabel _statusFile = new();
    private readonly ToolStripStatusLabel _statusPosition = new();
    private readonly ToolStripStatusLabel _statusCount = new();
    private readonly OpenFileDialog _openDialog = new();
    private readonly SaveFileDialog _saveDialog = new();
    private readonly FontDialog _fontDialog = new();
    private readonly PrintDocument _printDocument = new();
    private string? _currentFile;
    private bool _modified;
    private bool _suppressTextChanged;
    private int _printCharIndex;

    public MainForm()
    {
        Text = "SimpleNotepad — ПЗ27";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(900, 640);

        BuildEditor();
        BuildToolbar();
        BuildMenu();
        BuildStatusBar();

        _openDialog.Filter = "RTF files|*.rtf|Text files|*.txt|All files|*.*";
        _openDialog.Title = "Открыть документ";
        _saveDialog.Filter = "RTF files|*.rtf|Text files|*.txt";
        _saveDialog.FileName = "doc1.rtf";
        _saveDialog.DefaultExt = "rtf";
        _saveDialog.AddExtension = true;
        _fontDialog.ShowEffects = true;

        _printDocument.DocumentName = "SimpleNotepad Document";
        _printDocument.BeginPrint += (_, _) => _printCharIndex = 0;
        _printDocument.PrintPage += PrintDocument_PrintPage;

        UpdateTitle();
        UpdateStatus();
    }

    internal PrintDocument PrintDocument => _printDocument;
    internal RichTextBox Editor => _editor;

    private void BuildEditor()
    {
        _editor.Dock = DockStyle.Fill;
        _editor.Font = new Font("Segoe UI", 11F);
        _editor.AcceptsTab = true;
        _editor.HideSelection = false;
        _editor.DetectUrls = false;
        _editor.BorderStyle = BorderStyle.None;
        _editor.TextChanged += (_, _) =>
        {
            if (_suppressTextChanged)
                return;
            _modified = true;
            UpdateTitle();
            UpdateStatus();
        };
        _editor.SelectionChanged += (_, _) => UpdateStatus();
        Controls.Add(_editor);
    }

    private void BuildMenu()
    {
        var file = new ToolStripMenuItem("&File");
        file.DropDownItems.Add(CreateItem("&New", Keys.Control | Keys.N, (_, _) => NewDocument()));
        file.DropDownItems.Add(CreateItem("&Open...", Keys.Control | Keys.O, (_, _) => OpenDocument()));
        file.DropDownItems.Add(CreateItem("&Save", Keys.Control | Keys.S, (_, _) => SaveDocument()));
        file.DropDownItems.Add(CreateItem("Save &As...", Keys.Control | Keys.Shift | Keys.S, (_, _) => SaveDocumentAs()));
        file.DropDownItems.Add(new ToolStripSeparator());
        file.DropDownItems.Add(CreateItem("Page Set&up...", Keys.None, (_, _) => PageSetup()));
        file.DropDownItems.Add(CreateItem("Print Pre&view...", Keys.None, (_, _) => PrintPreview()));
        file.DropDownItems.Add(CreateItem("&Print...", Keys.Control | Keys.P, (_, _) => PrintDocumentDialog()));
        file.DropDownItems.Add(new ToolStripSeparator());
        file.DropDownItems.Add(CreateItem("E&xit", Keys.Alt | Keys.F4, (_, _) => Close()));

        var edit = new ToolStripMenuItem("&Edit");
        edit.DropDownItems.Add(CreateItem("&Undo", Keys.Control | Keys.Z, (_, _) => _editor.Undo()));
        edit.DropDownItems.Add(CreateItem("&Redo", Keys.Control | Keys.Y, (_, _) => _editor.Redo()));
        edit.DropDownItems.Add(new ToolStripSeparator());
        edit.DropDownItems.Add(CreateItem("Cu&t", Keys.Control | Keys.X, (_, _) => _editor.Cut()));
        edit.DropDownItems.Add(CreateItem("&Copy", Keys.Control | Keys.C, (_, _) => _editor.Copy()));
        edit.DropDownItems.Add(CreateItem("&Paste", Keys.Control | Keys.V, (_, _) => _editor.Paste()));
        edit.DropDownItems.Add(CreateItem("&Delete", Keys.Delete, (_, _) => _editor.SelectedText = string.Empty));
        edit.DropDownItems.Add(new ToolStripSeparator());
        edit.DropDownItems.Add(CreateItem("Select &All", Keys.Control | Keys.A, (_, _) => _editor.SelectAll()));

        var format = new ToolStripMenuItem("F&ormat");
        format.DropDownItems.Add(CreateItem("&Font...", Keys.None, (_, _) => ChooseFont()));
        var wordWrap = new ToolStripMenuItem("&Word Wrap") { Checked = true, CheckOnClick = true };
        wordWrap.CheckedChanged += (_, _) => _editor.WordWrap = wordWrap.Checked;
        format.DropDownItems.Add(wordWrap);

        var help = new ToolStripMenuItem("&Help");
        help.DropDownItems.Add(CreateItem("&About", Keys.None, (_, _) => ShowAbout()));

        _menu.Items.Add(file);
        _menu.Items.Add(edit);
        _menu.Items.Add(format);
        _menu.Items.Add(help);
        MainMenuStrip = _menu;
        Controls.Add(_menu);
    }

    private static ToolStripMenuItem CreateItem(string text, Keys shortcut, EventHandler handler)
    {
        var item = new ToolStripMenuItem(text);
        if (shortcut != Keys.None)
        {
            item.ShortcutKeys = shortcut;
            item.ShowShortcutKeys = true;
        }
        item.Click += handler;
        return item;
    }

    private void BuildToolbar()
    {
        _toolbar.Dock = DockStyle.Top;
        _toolbar.GripStyle = ToolStripGripStyle.Hidden;
        _toolbar.RenderMode = ToolStripRenderMode.System;
        _toolbar.Items.Add(CreateToolButton("Создать", (_, _) => NewDocument()));
        _toolbar.Items.Add(CreateToolButton("Открыть", (_, _) => OpenDocument()));
        _toolbar.Items.Add(CreateToolButton("Сохранить", (_, _) => SaveDocument()));
        _toolbar.Items.Add(new ToolStripSeparator());
        _toolbar.Items.Add(CreateToolButton("Отменить", (_, _) => _editor.Undo()));
        _toolbar.Items.Add(CreateToolButton("Вернуть", (_, _) => _editor.Redo()));
        _toolbar.Items.Add(new ToolStripSeparator());
        _toolbar.Items.Add(CreateToolButton("Вырезать", (_, _) => _editor.Cut()));
        _toolbar.Items.Add(CreateToolButton("Копировать", (_, _) => _editor.Copy()));
        _toolbar.Items.Add(CreateToolButton("Вставить", (_, _) => _editor.Paste()));
        _toolbar.Items.Add(new ToolStripSeparator());
        _toolbar.Items.Add(CreateToolButton("Ж", (_, _) => ToggleStyle(FontStyle.Bold), bold: true));
        _toolbar.Items.Add(CreateToolButton("К", (_, _) => ToggleStyle(FontStyle.Italic), italic: true));
        _toolbar.Items.Add(CreateToolButton("Ч", (_, _) => ToggleStyle(FontStyle.Underline), underline: true));
        Controls.Add(_toolbar);
    }

    private static ToolStripButton CreateToolButton(string text, EventHandler handler,
        bool bold = false, bool italic = false, bool underline = false)
    {
        var style = FontStyle.Regular;
        if (bold) style |= FontStyle.Bold;
        if (italic) style |= FontStyle.Italic;
        if (underline) style |= FontStyle.Underline;

        var button = new ToolStripButton(text)
        {
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            Font = new Font("Segoe UI", 9F, style)
        };
        button.Click += handler;
        return button;
    }

    private void BuildStatusBar()
    {
        _status.Dock = DockStyle.Bottom;
        _statusFile.Spring = true;
        _statusFile.TextAlign = ContentAlignment.MiddleLeft;
        _statusPosition.AutoSize = false;
        _statusPosition.Width = 150;
        _statusCount.AutoSize = false;
        _statusCount.Width = 150;
        _status.Items.AddRange(new ToolStripItem[] { _statusFile, _statusPosition, _statusCount });
        Controls.Add(_status);
    }

    private bool ConfirmDiscard()
    {
        if (!_modified)
            return true;

        var result = MessageBox.Show(this, "Сохранить изменения в документе?", "SimpleNotepad",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        return result switch
        {
            DialogResult.Yes => SaveDocument(),
            DialogResult.No => true,
            _ => false
        };
    }

    private void NewDocument()
    {
        if (!ConfirmDiscard())
            return;
        _suppressTextChanged = true;
        _editor.Clear();
        _suppressTextChanged = false;
        _currentFile = null;
        _modified = false;
        UpdateTitle();
        UpdateStatus();
    }

    private void OpenDocument()
    {
        if (!ConfirmDiscard())
            return;
        if (_openDialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            if (Path.GetExtension(_openDialog.FileName).Equals(".rtf", StringComparison.OrdinalIgnoreCase))
            {
                _editor.LoadFile(_openDialog.FileName, RichTextBoxStreamType.RichText);
            }
            else
            {
                _editor.LoadFile(_openDialog.FileName, RichTextBoxStreamType.PlainText);
            }
            _currentFile = _openDialog.FileName;
            _modified = false;
            UpdateTitle();
            UpdateStatus();
        }
        catch (Exception ex)
        {
            try
            {
                _editor.LoadFile(_openDialog.FileName, RichTextBoxStreamType.PlainText);
                _currentFile = _openDialog.FileName;
                _modified = false;
                UpdateTitle();
                UpdateStatus();
            }
            catch
            {
                MessageBox.Show(this, "Не удалось открыть файл:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private bool SaveDocument()
    {
        if (_currentFile is null)
            return SaveDocumentAs();
        return SaveTo(_currentFile);
    }

    private bool SaveDocumentAs()
    {
        if (_saveDialog.ShowDialog(this) != DialogResult.OK)
            return false;
        return SaveTo(_saveDialog.FileName);
    }

    private bool SaveTo(string path)
    {
        try
        {
            var isRtf = Path.GetExtension(path).Equals(".rtf", StringComparison.OrdinalIgnoreCase);
            _editor.SaveFile(path, isRtf ? RichTextBoxStreamType.RichText : RichTextBoxStreamType.PlainText);
            _currentFile = path;
            _modified = false;
            UpdateTitle();
            UpdateStatus();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Не удалось сохранить файл:\n" + ex.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private void ChooseFont()
    {
        if (_editor.SelectionFont is { } current)
            _fontDialog.Font = current;
        if (_fontDialog.ShowDialog(this) == DialogResult.OK)
            _editor.SelectionFont = _fontDialog.Font;
        _editor.Focus();
    }

    private void ToggleStyle(FontStyle style)
    {
        if (_editor.SelectionFont is not { } current)
            return;
        var newStyle = current.Style ^ style;
        _editor.SelectionFont = new Font(current.FontFamily, current.Size, newStyle,
            current.Unit, current.GdiCharSet, current.GdiVerticalFont);
        _editor.Focus();
    }

    private void ShowAbout()
    {
        MessageBox.Show(this,
            "SimpleNotepad — ПЗ27\n\nТекстовый редактор с сохранением RTF/TXT, настройкой страницы, " +
            "предварительным просмотром и печатью документа.\n\nСоздан в Visual Studio на C# / Windows Forms.",
            "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void PageSetup()
    {
        using var dialog = new PageSetupDialog { Document = _printDocument };
        dialog.ShowDialog(this);
    }

    private void PrintPreview()
    {
        using var dialog = new PrintPreviewDialog
        {
            Document = _printDocument,
            Width = 900,
            Height = 700,
            Text = "Предварительный просмотр — SimpleNotepad"
        };
        dialog.ShowDialog(this);
    }

    private void PrintDocumentDialog()
    {
        using var dialog = new PrintDialog { Document = _printDocument, UseEXDialog = true };
        if (dialog.ShowDialog(this) == DialogResult.OK)
            _printDocument.Print();
    }

    private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (e.Graphics is null)
        {
            e.HasMorePages = false;
            return;
        }

        e.HasMorePages = TextPrinter.DrawNextPage(
            e.Graphics, _editor.Text, _editor.Font, e.MarginBounds, ref _printCharIndex);
    }

    private void UpdateTitle()
    {
        var name = _currentFile is null ? "Без имени" : Path.GetFileName(_currentFile);
        Text = (_modified ? "*" : string.Empty) + name + " — SimpleNotepad (ПЗ27)";
    }

    private void UpdateStatus()
    {
        _statusFile.Text = _currentFile ?? "Файл не сохранён";
        var index = _editor.SelectionStart;
        var line = _editor.GetLineFromCharIndex(index);
        var first = _editor.GetFirstCharIndexFromLine(line);
        var column = index - first + 1;
        _statusPosition.Text = $"Стр {line + 1}, стлб {column}";
        _statusCount.Text = $"Символов: {_editor.TextLength}";
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!ConfirmDiscard())
        {
            e.Cancel = true;
            return;
        }
        base.OnFormClosing(e);
    }
}
