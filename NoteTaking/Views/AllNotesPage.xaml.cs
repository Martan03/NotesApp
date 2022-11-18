namespace NoteTaking.Views;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
		InitializeComponent();

		BindingContext = new Models.AllNotes();
	}

	protected override void OnAppearing()
	{
		((Models.AllNotes)BindingContext).LoadNotes();
	}

	private async void Add_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NotePage));
	}

	private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count == 0)
			return;

		var note = (Models.Note)e.CurrentSelection[0];

		await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Filename}");

		notesCollection.SelectedItem = null;
	}

	private async void NoteCard_Tapped(object sender, EventArgs e)
	{
		var note = ((VisualElement)sender).BindingContext as Models.Note;

		if (note is null)
			return;

		await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Filename}");
	}

	private void RemoveNote_Invoked(object sender, EventArgs e)
    {
		var note = ((SwipeItem)sender).BindingContext as Models.Note;

		if (File.Exists(note.Filename))
			File.Delete(note.Filename);
		BindingContext = new Models.AllNotes();
    }
}
