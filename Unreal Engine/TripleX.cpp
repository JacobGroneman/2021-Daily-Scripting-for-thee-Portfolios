//Preprocessor Directives
#include <iostream>

int main()
{
	std::cout << "'You have a suitcase with a 3 digit combo lock. . .'" << std::endl;
	std::cout << "Read the clues and input the prospective code." << std::endl;

	//Declaration Statements
	const int DigitOne = 4; //Const prefix = unchanging var!
	const int DigitTwo = 2;
	const int DigitThree = 7;
		int CodeSum = DigitOne + DigitTwo + DigitThree;
		int CodeProduct = DigitOne * DigitTwo * DigitThree;

	//Expression Statements
	std::cout << std::endl;
	std::cout << "- There are 3 digits in the code." << std::endl;
	std::cout << "- The digits add up to: " << CodeSum << std::endl;
	std::cout << "- The digits multiply to give: " << CodeProduct << std::endl;

	int GuessOne, GuessTwo, GuessThree;
	int GuessSum = GuessOne + GuessTwo + GuessThree;
	int GuessProduct = GuessOne * GuessTwo * GuessThree;
		std::cin >> GuessOne
		std::cin >> GuessTwo;
		std::cin >> GuessThree;
	
			//std::cout << "You entered: " << GuessOne << GuessTwo << GuessThree;

	return 0; //Return Statement
}

//Single-line comment

/*multi-
line
comment*/