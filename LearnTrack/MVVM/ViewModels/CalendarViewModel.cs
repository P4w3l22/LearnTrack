using LearnTrack.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;
using System.Xml.Serialization;

namespace LearnTrack.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
class CalendarViewModel
{
    public List<CalendarCardModel> CalendarCards { get; set; } = new();
    public List<string> DayTopics { get; set; } = new()
    {
        "Siłownia",
        "Posprzątać",
        "Nauka .NET",
        "Książka",
        "SQL",
        "Praca"
    };
    public List<NoteModel> Notes { get; set; }
    public string ActualDate { get; set; }
    public NoteModel ActualDateNotes { get; set; }



    public CalendarViewModel()
    {
        SetCalendarCards();
        SetNotes();
        SetActualDate();
    }

    private void SetActualDate()
    {
        ActualDate = DateTime.Now.ToString("dd.MM.yyyy");
    }

    private void SetCalendarCards()
    {
        Random random = new();

        for (int i = 1; i <= 30; i++)
        {
            CalendarCards.Add(new CalendarCardModel()
            {
                DayNumber = i,
                NotesNumber = random.Next(0, 4),
                ColumnId = (i - 1) % 7,
                RowId = (i - 1) / 7
            });
        }
    }

    private void SetActualDateNotes()
    {

    }

    private void SetNotes()
    {
        Notes = new()
        {
            new NoteModel() { Title="Siłownia", Description="FBW", Date=DateTime.Now, IsCompleted=false },
            new NoteModel() { Title="Posprzątać", Description="Posprzątać ubrania i resztę pokoju", Date=DateTime.Now, IsCompleted=false },
            new NoteModel() { Title="Nauka .NET", Description="Projekt + rozdział książki", Date=DateTime.Now, IsCompleted=false },
            new NoteModel() { Title="Książka", Description="Książka Gogginsa - 20 stron", Date=DateTime.Now, IsCompleted=false },
            new NoteModel() { Title="SQL", Description="Rozdział książki SQL", Date=DateTime.Now, IsCompleted=false },
            new NoteModel() { Title="Praca", Description="7-17", Date=DateTime.Now, IsCompleted=false }
        };
    }

    public NoteModel GetNoteByTitle(string title) =>
        Notes.FirstOrDefault(x => x.Title == title);

}
