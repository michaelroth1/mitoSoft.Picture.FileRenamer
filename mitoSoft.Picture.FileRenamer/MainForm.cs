using mitoSoft.Common.Media;
using mitoSoft.Picture.FileRenamer.Extensions;
using mitoSoft.Picture.FileRenamer.Helpers;
using mitoSoft.Picture.FileRenamer.Models;
using System.Diagnostics;
using System.Globalization;

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
            toolStripStatusLabel.Text = string.Empty;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FormatString))
            {
                FormatTextBox.Text = Properties.Settings.Default.FormatString;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            FileListBox.Items.Clear();
            ResultListBox.Items.Clear();
        }

        private void SearchFiles_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in OpenFileDialog.FileNames)
                {
                    var filePath = new FilePath()
                    {
                        FullName = file,
                    };
                    FileListBox.Items.Add(filePath);
                    toolStripStatusLabel.Text = $"{FileListBox.Items.Count} file(s) selected";
                }
            }
        }

        private void Renaming_Click(object sender, EventArgs e)
        {
            if (FileListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("no files selected");
            }

            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = FileListBox.Items.Count;
            toolStripProgressBar.Value = 0;
            ResultListBox.Items.Clear();
            for (int j = FileListBox.Items.Count - 1; j >= 0; j--)
            {
                var model = (FilePath)FileListBox.Items[j];

                var file = new FileInfo(model.FullName);

                if (File.Exists(file.FullName))
                {
                    toolStripStatusLabel.Text = file.FullName;
                    toolStripProgressBar.Value++;
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
                            FileListBox.Items.RemoveAt(j);
                            ResultListBox.Items.Add(model);
                        }
                        else
                        {
                            FileListBox.Items.Refresh(j);
                        }
                    }
                }
            }

            toolStripStatusLabel.Text = $"{ResultListBox.Items.Count} file(s) renamed";
            Application.DoEvents();
        }

        private FileInfo GetFileName(FileInfo file)
        {
            var date = (new MediaFileHandler()).GetCreationDate(file);

            for (int offset = 0; offset < 60; offset++)
            {
                var dateString = date.AddSeconds(offset).ToString(FormatTextBox.Text);

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
            Properties.Settings.Default.FormatString = FormatTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void OpenContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDirectory(FileListBox);
        }

        private void OpenContainingFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenDirectory(ResultListBox);
        }

        private void OpenDirectory(ListBox lb)
        {
            if (lb.SelectedItem == null) { return; }

            var file = (FilePath)lb.SelectedItem;

            var dir = new FileInfo(file.FullName).DirectoryName;

            OpenFileDialog.InitialDirectory = dir;

            OpenFileDialog.ShowDialog();
        }

        private void FileListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                FileListBox.SelectedIndex = FileListBox.IndexFromPoint(e.Location);
                if (FileListBox.SelectedIndex != -1)
                {
                    FileContextMenuStrip.Show(this, new Point(e.X, e.Y));
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
                    ResultContextMenuStrip.Show(this,
                        new Point(e.X + splitContainer1.SplitterDistance, e.Y));
                }
            }
        }

        private void FolderSort_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileListBox.Items.Count <= 0)
                {
                    throw new InvalidOperationException("No files selected");
                }

                var dir = new DirectoryInfo(FolderHelper.GetLocalFolder());
                toolStripProgressBar.Minimum = 0;
                toolStripProgressBar.Maximum = FileListBox.Items.Count;
                toolStripProgressBar.Value = 0;
                ResultListBox.Items.Clear();
                for (int i = FileListBox.Items.Count - 1; i >= 0; i--)
                {
                    var file = (FilePath)FileListBox.Items[i];
                    toolStripStatusLabel.Text = file.FullName;
                    toolStripProgressBar.Value++;
                    Application.DoEvents();

                    var model = ShiftFile(file, dir);
                    if (string.IsNullOrEmpty(model.Error))
                    {
                        ResultListBox.Items.Add(model);
                        FileListBox.Items.Remove(model);
                    }
                }

                toolStripStatusLabel.Text = $"{ResultListBox.Items.Count} files moved to '{FolderHelper.GetLocalFolder()}/...'";
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
            if (ResultListBox.SelectedItem == null) { return; }

            var item = ResultListBox.SelectedItem;

            ResultListBox.Items.Remove(item);
            FileListBox.Items.Add(item);
        }

        private void OpenLocalFolder_Click(object sender, EventArgs e)
        {
            throw new Exception("test");

            //var folder = FolderHelper.GetLocalFolder();

            //if (!Directory.Exists(folder) && MessageBox.Show("No local folder found. Do you want to create a 'new' local folder?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    Directory.CreateDirectory(folder);
            //}

            //if (Directory.Exists(folder))
            //{
            //    Process.Start("explorer.exe", folder);
            //}
        }
    }
}