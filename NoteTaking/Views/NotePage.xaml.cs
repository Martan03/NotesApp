namespace NoteTaking.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
	public string ItemId
	{
		set { LoadNote(value); }
	}

	public NotePage()
	{
		InitializeComponent();

		string appDataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pencotes");
        	string randomFileName = $"{Path.GetRandomFileName()}.txt";

		LoadNote(Path.Combine(appDataPath, randomFileName));
	}

	private void LoadNote(string filename)
	{
		Models.Note noteModel = new Models.Note();
		noteModel.Filename = filename;

		if (File.Exists(filename))
		{
			noteModel.Date = File.GetCreationTime(filename);
			noteModel.Text = File.ReadAllText(filename);
		}

		BindingContext = noteModel;
	}

	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note && File.Exists(note.Filename))
			File.Delete(note.Filename);

		await Shell.Current.GoToAsync("..");
	}

	private void TextEditor_TextChanged(object sender, TextChangedEventArgs e)
	{
        	if (BindingContext is Models.Note note)
			File.WriteAllText(note.Filename, TextEditor.Text);
	}
}
