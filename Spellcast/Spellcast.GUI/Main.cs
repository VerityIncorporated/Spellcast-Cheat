namespace Spellcast.GUI;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
    }

    private readonly List<TextBox> textBoxes = new();
    private List<TextBox> textBoxesInOrder = new();
    private readonly Dictionary.Main Dictionary = new(@"C:\Users\Nathan Roberts\Downloads\Dictionary3.txt", 5, 5);

    private void Main_Load(object sender, EventArgs e)
    {
        foreach (var control in Controls)
        {
            if (control is TextBox box)
            {
                textBoxes.Add(box);
            }
        }

        textBoxesInOrder = textBoxes.OrderBy(s => GetNumberFromName(s.Name)).ToList();
    }

    private void getbestwordButton_Click(object sender, EventArgs e)
    {
        var letters = textBoxes.Select(tb => tb.Text).ToArray();
        var inputGrid = Dictionary.ConvertToGrid(string.Join("", letters));
        var bestWord = Dictionary.GetBestWord(string.Join("", letters));

        bestwordLabel.Text = $@"Best word: {bestWord}";

        foreach (var textBox in textBoxes)
        {
            textBox.BackColor = SystemColors.Window;
        }

        var currentBox = 0;
        var wordPositions = Dictionary.FindWordPositions(bestWord, inputGrid);
        foreach (var (row, column) in wordPositions)
        {
            currentBox += 1;

            var textBox = textBoxes[row * 5 + column];
            textBox.BackColor = Color.Yellow;
            textBox.Text += $@" [{currentBox}]";
        }
    }

    private static int GetNumberFromName(string name)
    {
        var numberString = new string(name.Where(char.IsDigit).ToArray());
        var number = int.Parse(numberString);
        return number;
    }
}