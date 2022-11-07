using mitoSoft.Common.Media;
using mitoSoft.Picture.FileRenamer.Extensions;
using mitoSoft.Picture.FileRenamer.Models;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace mitoSoft.Picture.FileRenamer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel.Text = string.Empty;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FormatString))
            {
                this.FormatTextBox.Text = Properties.Settings.Default.FormatString;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            this.FileListBox.Items.Clear();
            this.ResultListBox.Items.Clear();
        }

        private void SearchFiles_Click(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in this.OpenFileDialog.FileNames)
                {
                    var filePath = new FilePath()
                    {
                        FullName = file,
                    };
                    this.FileListBox.Items.Add(filePath);
                    this.toolStripStatusLabel.Text = $"{this.FileListBox.Items.Count} file(s) selected";
                }
            }
        }

        private void Renaming_Click(object sender, EventArgs e)
        {
            if (this.FileListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("no files selected");
            }

            this.toolStripProgressBar.Minimum = 0;
            this.toolStripProgressBar.Maximum = this.FileListBox.Items.Count;
            this.toolStripProgressBar.Value = 0;
            this.ResultListBox.Items.Clear();
            for (int j = this.FileListBox.Items.Count - 1; j >= 0; j--)
            {
                var model = (FilePath)this.FileListBox.Items[j];

                var file = new FileInfo(model.FullName);

                if (File.Exists(file.FullName))
                {
                    this.toolStripStatusLabel.Text = file.FullName;
                    this.toolStripProgressBar.Value++;
                    Application.DoEvents();

                    try
                    {
                        var newFile = GetFileName(file);

                        Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(file.FullName, newFile.Name);

                        model.FullName = newFile.FullName;
                    }
                    catch (Exception ex)
                    {
                        model.Error = ex.Message;
                    }
                    finally
                    {
                        if (string.IsNullOrEmpty(model.Error))
                        {
                            this.FileListBox.Items.RemoveAt(j);
                            this.ResultListBox.Items.Add(model);
                        }
                        else
                        {
                            this.FileListBox.Items.Refresh(j);
                        }
                    }
                }
            }

            this.toolStripStatusLabel.Text = $"{this.ResultListBox.Items.Count} file(s) renamed";
            Application.DoEvents();
        }

        private FileInfo GetFileName(FileInfo file)
        {
            var date = (new MediaFileHandler()).GetCreationDate(file);

            for (int offset = 0; offset < 60; offset++)
            {
                var dateString = date.AddSeconds(offset).ToString(this.FormatTextBox.Text);

                var fileName = Path.Combine(file.DirectoryName!, $"{dateString}{file.Extension}");

                if (!File.Exists(fileName))
                {
                    return new FileInfo(fileName);
                }
            }

            throw new InvalidProgramException($"New filename of '{file.Name}' cannot be changed to date formatted name (all used dates already existing).");
        }

        private void FormatTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormatString = this.FormatTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void OpenContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDirectory(this.FileListBox);
        }

        private void OpenContainingFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenDirectory(this.ResultListBox);
        }

        private void OpenDirectory(ListBox lb)
        {
            if (lb.SelectedItem == null) { return; }

            var file = (FilePath)lb.SelectedItem;

            var dir = new FileInfo(file.FullName).DirectoryName;

            this.OpenFileDialog.InitialDirectory = dir;

            this.OpenFileDialog.ShowDialog();
        }

        private void FileListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                FileListBox.SelectedIndex = FileListBox.IndexFromPoint(e.Location);
                if (FileListBox.SelectedIndex != -1)
                {
                    this.FileContextMenuStrip.Show(this, new Point(e.X, e.Y));
                }
            }
        }

        private void ResultListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                ResultListBox.SelectedIndex = ResultListBox.IndexFromPoint(e.Location);
                if (ResultListBox.SelectedIndex != -1)
                {
                    this.ResultContextMenuStrip.Show(this,
                        new Point(e.X + this.splitContainer1.SplitterDistance, e.Y));
                }
            }
        }

        private void FolderSort_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileListBox.Items.Count <= 0)
                {
                    throw new InvalidOperationException("No files selected");
                }

                var dir = new DirectoryInfo(
                                   Path.Combine(
                                       Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                                       "MediaFileRenamer"
                                       )
                                   );
                this.toolStripProgressBar.Minimum = 0;
                this.toolStripProgressBar.Maximum = this.FileListBox.Items.Count;
                this.toolStripProgressBar.Value = 0;
                this.ResultListBox.Items.Clear();
                for (int i = this.FileListBox.Items.Count - 1; i >= 0; i--)
                {
                    var file = (FilePath)this.FileListBox.Items[i];
                    this.toolStripStatusLabel.Text = file.FullName;
                    this.toolStripProgressBar.Value++;
                    Application.DoEvents();

                    var model = ShiftFile(file, dir);
                    if (string.IsNullOrEmpty(model.Error))
                    {
                        this.ResultListBox.Items.Add(model);
                        this.FileListBox.Items.Remove(model);
                    }
                }

                this.toolStripStatusLabel.Text = $"{this.ResultListBox.Items.Count} files moved to 'User/Documents/MediaFileRenamer/...'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static FilePath ShiftFile(FilePath file, DirectoryInfo directory)
        {
            try
            {
                if (!File.Exists(file.FullName))
                {
                    throw new InvalidOperationException($"File '{file.FullName}' not existing.");
                }

                var date = (new MediaFileHandler()).GetCreationDate(new FileInfo(file.FullName), false);

                var monthFolder = date.ToString("yyyy_MM_(MMMyy)", CultureInfo.CreateSpecificCulture("de-DE"));

                var monthPath = Path.Combine(directory.FullName, monthFolder);

                if (!Directory.Exists(monthPath))
                {
                    Directory.CreateDirectory(monthPath);
                }

                if (!Directory.Exists(monthPath))
                {
                    throw new InvalidOperationException($"Somthing went wrong during creation of '{monthPath}' path.");
                }

                var oldFile = file.FullName;
                var newFile = Path.Combine(monthPath, file.Name);

                File.Copy(oldFile, newFile, true);

                if (!File.Exists(newFile))
                {
                    throw new InvalidOperationException($"File could not copied to '{monthPath}' path.");
                }

                file.FullName = newFile;

                File.Delete(oldFile); //removing the origin file
            }
            catch (Exception ex)
            {
                file.Error = ex.Message;
            }
            return file;
        }

        private void ShiftToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ResultListBox.SelectedItem == null) { return; }

            var item = this.ResultListBox.SelectedItem;

            this.ResultListBox.Items.Remove(item);
            this.FileListBox.Items.Add(item);
        }
    }
}