//Preprocessor Directives
#include <iostream>

void PrintIntroduction()
{
	std::cout << "\"You have a suitcase with a 3 digit combo lock. . .\"\n";
	std::cout << "Read the clues and input the prospective code.\n\n";
}

void PlayGame()
{
	PrintIntroduction()

	//Declaration Statements
	const int DigitOne = 4; //Const prefix = unchanging var!
	const int DigitTwo = 2;
	const int DigitThree = 7;
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
		std::count << "\nThe case clicks open. You Win.";
	}
	else
	{
		std::count << "\nThe case remains locked.\n";
	}
}

int main()
{
	PlayGame()

	return 0; //Return Statement
}

//Single-line comment

/*multi-
line
comment*/