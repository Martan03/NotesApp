using System.Collections.ObjectModel;

namespace NoteTaking.Models
{
    internal class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public AllNotes() =>
            LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            string appDataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pencotes");
            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            IEnumerable<Note> notes = Directory
                                        .EnumerateFiles(appDataPath, "*.txt")
                                        .Select(filename => new Note()
                                        {
                                            Filename = filename,
                                            Text = File.ReadAllText(filename),
                                            Date = File.GetCreationTime(filename)
                                        })
                                        .OrderByDescending(note => note.Date);
            foreach (Note note in notes)
                Notes.Add(note);
        }
    }
}
