//Preprocessor Directives
#include <iostream>
#include <ctime>

void PrintIntroduction(int Difficulty)
{

	std::cout << "\n\n\"You have a suitcase with a level " << Difficulty;
	std::cout << " combo lock. . .\"\nRead the clues and input the prospective code.\n\n";
}

bool PlayGame(int Difficulty)
{
	PrintIntroduction(Difficulty)

	//Declaration Statements
	const int DigitOne = rand() % Difficulty + Difficulty; //Const prefix = unchanging var!
	const int DigitTwo = rand() % Difficulty + Difficulty;
	const int DigitThree = rand() % Difficulty + Difficulty;
	int CodeSum = DigitOne + DigitTwo + DigitThree;
	int CodeProduct = DigitOne * DigitTwo * DigitThree;

	//Player Guess
	std::cout << std::endl;
	std::cout << "- There are 3 digits in the code.";
	std::cout << "\n- The digits add up to: " << CodeSum;
	std::cout << "\n- The digits multiply to give: " << CodeProduct << std::endl;

	int GuessOne, GuessTwo, GuessThree;
	int GuessSum = GuessOne + GuessTwo + GuessThree;
	int GuessProduct = GuessOne * GuessTwo * GuessThree;
	std::cin >> GuessOne >> GuessTwo >> GuessThree;


	//Input Check
	if (GuessSum == CodeSum && GuessProduct == CodeProduct)
	{
		std::count << "\nThe case clicks open to reveal another case. . .";
		return true;
	}
	else
	{
		std::count << "\nThe case remains locked.\n";
		return false
	}
}

int main()
{
	srand(time(NULL)); //new random sequence based on time of day.

	int LevelDifficulty = 3;
	const int MaxLevelDifficulty = 5;

	while (LevelDifficulty <= MaxLevelDifficulty)
	{
		bool bLevelComplete = PlayGame(LevelDifficulty);
		std::cin.clear();
		std::cin.ignore();

		if (bLevelComplete == true)
		{
			++LevelDifficulty;
		}
	}
	std::cout << "the case clicks open to reveal the item. ;)\n"
	return 0; //Return Statement
}

//Single-line comment

/*multi-
line
comment*/