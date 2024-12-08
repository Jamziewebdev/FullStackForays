bool running = true;
List<Student> studentList = new List<Student>();

// Första listan med hårdkodade studenter för testsyften.
studentList.Add(new Student { Name = "Lisa", Grade = "F" });
studentList.Add(new Student { Name = "William", Grade = "A" });
studentList.Add(new Student { Name = "Erik", Grade = "C" });

// Huvudloop som körs så länge användaren inte har valt avsluta
while (running)
{
    Console.Clear();
    Console.WriteLine("Välkommen till betygssorteraren!");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Välj en åtgärd:");
    Console.WriteLine("1. Lägg till student");
    Console.WriteLine("2. Sortera studenter efter betyg");
    Console.WriteLine("3. Visa alla studenter");
    Console.WriteLine("4. Avsluta program");
    Console.WriteLine("--------------------------------");

    // Tar emot användarinput för att välja case
    int selection;
    if (int.TryParse(Console.ReadLine(), out selection))
    {
        Console.Clear();
        switch (selection)
        {
            case 1:
                AddStudent(studentList);
                Console.WriteLine("Tryck på valfri tangent för att gå tillbaka till huvudmenyn.");
                Console.ReadKey();
                break;

            case 2:
                sortStudents(studentList);
                Console.WriteLine("Tryck på valfri tangent för att gå tillbaka till huvudmenyn.");
                Console.ReadKey();
                break;

            case 3:
                ShowStudents(studentList);
                Console.WriteLine("Tryck på valfri tangent för att gå tillbaka till huvudmenyn.");
                Console.ReadKey();
                break;

            case 4:
                Console.WriteLine("Programmet avslutas");
                return;

            default:
                Console.WriteLine("Ogiltigt val, försök igen!");
                Console.WriteLine("Tryck på valfri tangent för att gå tillbaka till huvudmenyn.");
                Console.ReadKey();
                break;
        }

    }
    else
    {
        Console.WriteLine("Ogiltig inmatning. Välj ett nummer mellan 1 och 4.");
        Console.WriteLine("Tryck på valfri tangent för att gå tillbaka till huvudmenyn.");
        Console.ReadKey();
    }
    
}



void AddStudent(List<Student> studentList)
{
    string name;
    string grade;

    // En do - while-loop som tar emot användarinmatning och om inmatning av namn
    // inte är tom eller mellanslag så går den vidare och tar emot betyg. 
    do
    {
        Console.WriteLine("Ange studentens förnamn: ");
        name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Namn kan inte vara tomt, försök igen.");
        }
    } while (string.IsNullOrWhiteSpace(name));

    // Så länge betyg är valid läggs det till i listan.
    do
    {
        Console.WriteLine("Välj ett betyg mellan A - F.");
        grade = Console.ReadLine().ToUpper();
        if (!IsValidGrade(grade))
        {
            Console.WriteLine("Ogiltigt betyg, vänligen välj A, B, C, D, E eller F.");
        }
    } while (!IsValidGrade(grade));

    // Skapar och lägger till studenten i listan
    var newStudent = new Student { Name = name, Grade = grade };
    studentList.Add(newStudent);
    Console.WriteLine($"Studenten {newStudent.Name} med betyget {newStudent.Grade} har lagts till i listan.");

}

// Validerar betygsinmatning
bool IsValidGrade(string grade)
{
    string[] validGrades = { "A", "B", "C", "D", "E", "F" };
    return Array.Exists(validGrades, g => g == grade);
}


void sortStudents(List<Student> students)
{
    int n = students.Count;

    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - 1; j++)
        {
            // Jämför betyg(om student[j] har högre betyg än student[j + 1], byt plats)
            if (string.Compare(students[j].Grade, students[j + 1].Grade) > 0)
            {
                Student temp = students[j];
                students[j] = students[j + 1];
                students[j + 1] = temp;

            }
        }
    }
    Console.WriteLine("Studenterna har sorterats efter betyg.");
}

// Visar alla studenter med hjälp av en if/else, som först kollar om det finns några studenter
// och om det finns visar den listan i sin helhet.
void ShowStudents(List<Student> studentList)
{
    Console.WriteLine("Lista över alla studenter:");
    if (studentList.Count == 0)
    {
        Console.WriteLine("Inga studenter har lagts till än.");
    }
    else
    {
        foreach (var student in studentList)
        {
            Console.WriteLine($"Namn: {student.Name}, Betyg: {student.Grade}");
        }
    }
}

// Studentklass med namn och betyg
public class Student
{
    public string Name { get; set; }
    public string Grade { get; set; }

}