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

		string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

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

	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note)
			File.WriteAllText(note.Filename, TextEditor.Text);

		await Shell.Current.GoToAsync("..");
	}

	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note)
		{
			if (File.Exists(note.Filename))
				File.Delete(note.Filename);
		}

		await Shell.Current.GoToAsync("..");
	}
}