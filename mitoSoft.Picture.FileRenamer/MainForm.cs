using mitoSoft.Common.Media;
using mitoSoft.Picture.FileRenamer.Extensions;
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
            this.toolStripStatusLabel.Text = string.Empty;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FormatString))
            {
                this.FormatTextBox.Text = Properties.Settings.Default.FormatString;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            this.FileListBox.Items.Clear();
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

            int i = 0;
            this.toolStripProgressBar.Minimum = 0;
            this.toolStripProgressBar.Maximum = this.FileListBox.Items.Count;
            this.toolStripProgressBar.Value = 0;
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
                        var date = (new MediaFileHandler()).GetCreationDate(file);

                        var dateString = date.ToString(this.FormatTextBox.Text);

                        var newName = $"{dateString}";

                        if ($"{newName}{file.Extension}" != file.Name)
                        {
                            if (File.Exists(@$"{file.Directory}\{newName}{file.Extension}"))
                            {
                                throw new InvalidOperationException($"'{newName}{file.Extension}' could not renamed - it already exists.");
                            }

                            Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(file.FullName, $"{newName}{file.Extension}");
                        }

                        i++;
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
                        }
                        else
                        {
                            this.FileListBox.Items.Refresh(j);
                        }
                    }
                }
            }

            this.toolStripStatusLabel.Text = $"{i} file(s) renamed";
            Application.DoEvents();
        }

        private void FormatTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormatString = this.FormatTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void OpenContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = (FilePath)this.FileListBox.SelectedItem;

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
                    this.contextMenuStrip.Show(this, new Point(e.X, e.Y));
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
                var copiedFiles = new List<FilePath>();
                foreach (FilePath model in this.FileListBox.Items)
                {
                    var file = new FileInfo(model.FullName);

                    this.toolStripStatusLabel.Text = file.FullName;
                    this.toolStripProgressBar.Value++;
                    Application.DoEvents();

                    copiedFiles.Add(ShiftFile(file, dir));
                }

                this.FileListBox.Items.Clear();
                foreach (FilePath model in copiedFiles)
                {
                    this.FileListBox.Items.Add(model);
                }

                this.toolStripStatusLabel.Text = $"{copiedFiles.Count} files moved to 'User/Documents/MediaFileRenamer/...'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static FilePath ShiftFile(FileInfo file, DirectoryInfo directory)
        {
            try
            {
                if (!file.Exists)
                {
                    throw new InvalidOperationException($"File '{file.FullName}' not existing.");
                }

                var date = (new MediaFileHandler()).GetCreationDate(file, false);

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

                File.Copy(file.FullName, newFile, true);

                file = new FileInfo(newFile);

                if (!file.Exists)
                {
                    throw new InvalidOperationException($"File could not copied to '{monthPath}' path.");
                }

                File.Delete(oldFile); //löschen der usprünglichen Datei

                return new FilePath
                {
                    FullName = file.FullName,
                    Error= $"{oldFile} -> {newFile}",
                };
            }
            catch (Exception ex)
            {
                return new FilePath
                {
                    Error = ex.Message,
                    FullName = file.FullName,
                };
            }
        }
    }
}