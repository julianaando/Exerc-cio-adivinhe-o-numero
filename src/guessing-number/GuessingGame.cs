namespace guessing_number;

public class GuessNumber
{
    private readonly IRandomGenerator random;
    public GuessNumber() : this(new DefaultRandom()){}
    public GuessNumber(IRandomGenerator obj)
    {
        random = obj;
        RestartGame();
    }

    public int userValue;
    public int randomValue;
    public readonly int maxAttempts = 5;
    public int currentAttempts = 0;

    public int difficultyLevel = 1;
    public bool gameOver;
    private static int playAgain;

    public static string Greet()
    {
        return "---Bem-vindo ao Guessing Game---";
    }

    public string ChooseNumber()
    {
        if (currentAttempts == maxAttempts) {
            gameOver = true;
            return "Você excedeu o número máximo de tentativas! Tente novamente.";
        }

        Console.Write("Digite um número: ");

        string? userEntry = Console.ReadLine();
        if (string.IsNullOrEmpty(userEntry)) {
            Console.WriteLine("Entrada inválida! Reinicie o jogo.");
            Environment.Exit(0);
        }

        bool canConvert = int.TryParse(userEntry, out userValue);
        if(!canConvert) {
            return "Entrada inválida! Não é um número.";
        }
        if(userValue > 100 || userValue < -100) {
            userValue = 0;
            return "Entrada inválida! Valor não está no range.";
        }
        currentAttempts++;
        Console.WriteLine("currentAttempts " + currentAttempts);
        return "Número escolhido!";
    }

    public string RandomNumber()
    {
        int MIN_VALUE = -100;
        int MAX_VALUE = 100;

        randomValue = random.GetInt(MIN_VALUE, MAX_VALUE);
        return $"A máquina escolheu um número de {MIN_VALUE} a {MAX_VALUE}!";
    }

    public string AnalyzePlay()
    {
        if(userValue > randomValue)
        {
            return "Tente um número MENOR";
        }
        else if(userValue < randomValue)
        {
            return "Tente um número MAIOR";
        }
        else if(userValue == randomValue)
        {
            gameOver = true;
            return "ACERTOU!";
        }
        else {
            return "Suas chances acabaram!";
        }
    }

    public void RestartGame()
    {
        userValue = 0;
        randomValue = 0;
        currentAttempts = 0;
        difficultyLevel = 1;
        gameOver = false;
    }

    public static void StartGame() 
    {
        GuessNumber game = new();
        Console.WriteLine(Greet());
        Console.WriteLine(game.RandomNumber());

        while (!game.gameOver)
        {
            Console.WriteLine(game.ChooseNumber());

            if (game.gameOver)
            {
                break;
            }

            Console.WriteLine(game.AnalyzePlay());
        }
    }

    public static void Main(string[] args)
    {
        do
        {
            StartGame();
            Console.WriteLine("Jogo encerrado. Deseja recomeçar?");
            Console.WriteLine("1. Sim - 2. Não");
        } while (int.TryParse(Console.ReadLine(), out playAgain) && playAgain == 1);

        Console.WriteLine("Até a próxima!!");
    }
}